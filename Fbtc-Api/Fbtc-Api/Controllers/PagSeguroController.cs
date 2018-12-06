using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using System.Collections.Generic;
using System.Xml.Linq;
using Fbtc.Infra.Helpers;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/PagSeguro")]
    public class PagSeguroController : ApiController
    {
        private readonly Boolean PS_IsDebug;
        private readonly string EMail;
        private readonly string BaseUrl;
        private readonly string Token;

        private HttpClient httpClient = new HttpClient();

        private readonly IPagSeguroApplication _pagSeguroApplication;

        public PagSeguroController(IPagSeguroApplication pagSeguroApplication)
        {
            _pagSeguroApplication = pagSeguroApplication;

            PS_IsDebug = Boolean.Parse(ConfigHelper.GetKeyAppSetting("PS_IsDebug"));

            if (PS_IsDebug)
            {
                // Dados do Sandbox:
                EMail = ConfigHelper.GetKeyAppSetting("PSSbox_EMail");
                BaseUrl = ConfigHelper.GetKeyAppSetting("PSSbox_BaseUrl");
                Token = ConfigHelper.GetKeyAppSetting("PSSbox_Token");
            } else
            {
                // Dados de produção:
                EMail = ConfigHelper.GetKeyAppSetting("PSPrd_EMail");
                BaseUrl = ConfigHelper.GetKeyAppSetting("PSPrd_BaseUrl");
                Token = ConfigHelper.GetKeyAppSetting("PSPrd_Token");
            }
        }

        /// <summary>
        /// Recebendo uma notificação de transação do PagSeguro
        /// Uma vez configurado o endereço para onde o PagSeguro irá enviar notificações, 
        /// o próximo passo é preparar seu sistema para receber, nesse endereço, um código de notificação.
        /// O PagSeguro envia as notificações para a URL que você configurou usando o protocolo HTTP,
        /// pelo método POST.
        /// </summary>
        /// <param name="notificationCode"></param>
        /// <param name="notificationType">transaction</param>
        /// <returns></returns>
        //Recebendo uma notificação de transação:
        // [Authorize]
        [Route("notificacao")]
        [HttpPost]
        public async Task<HttpResponseMessage> ReceiveNotificationTransacao(string notificationCode, string notificationType)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            string resultado = "false";

            try
            {
                if (notificationCode is null) throw new ArgumentNullException("notificationCode está nulo!");
                if (notificationType is null) throw new ArgumentNullException("notificationType está nulo!");

                // Grava os dados da noficação recebida:
                resultado = _pagSeguroApplication.NotificationTransacao(notificationCode, notificationType);

                TransacaoPagSeguro _transacaoPagSeguro = await GetTransacaoAPIPagSeguroByNotification(notificationCode.Trim(), EMail, Token);

                if (_transacaoPagSeguro.NotificationCode != null && _transacaoPagSeguro.Reference != null)
                    resultado = _pagSeguroApplication.SaveDadosTransacaoPagSeguro(_transacaoPagSeguro);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return await tsc.Task;
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

                tsc.SetResult(response);

                return await tsc.Task;
            }
        }

        /// <summary>
        /// Para obter o código de checkout para uma venda junto ao PagSeguro. Função para uso externo
        /// método GET 
        /// </summary>
        /// <param name="notificationCode"></param>
        /// <param name="eMail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("getTokenCheckOut/")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetTokenCheckOut(int associadoId, string valor, string tipoEndereco, int anoInicio, int anoTermino, bool enderecoRequerido, bool isAnuidade)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                CheckOutPagSeguro _ck = _pagSeguroApplication.getDadosParaCheckOutPagSeguro(associadoId, valor, tipoEndereco, anoInicio, anoTermino, enderecoRequerido, isAnuidade);

                TokenCheckOutPagSeguro token = await GetTokenDinamicoAPIPagSeguro(EMail, Token, _ck);

                response = Request.CreateResponse(HttpStatusCode.OK, token);

                tsc.SetResult(response);

                return await tsc.Task;
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

                tsc.SetResult(response);

                return await tsc.Task;
            }
        }

        /// <summary>
        /// Para obter o código de checkout para uma venda junto ao PagSeguro. Essa função é para uso interno 
        /// método GET 
        /// </summary>
        /// <param name="notificationCode"></param>
        /// <param name="eMail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TokenCheckOutPagSeguro> GetTokenCheckOutPessoaId(int pessoaId, string valor, string tipoEndereco, int anoInicio, int anoTermino, bool enderecoRequerido, bool isAnuidade)
        {
            try
            {
                CheckOutPagSeguro _ck = _pagSeguroApplication.getDadosParaCheckOutPagSeguro(pessoaId, valor, tipoEndereco, anoInicio, anoTermino, enderecoRequerido, isAnuidade);

                TokenCheckOutPagSeguro token = await GetTokenDinamicoAPIPagSeguro(EMail, Token, _ck);

                return token;
            }
            catch (Exception ex)
            {
                throw new Exception($"ATENÇÃO: Ocorreu um erro durante o acesso ao PagSeguro. Exception Type:{ex.GetType()}. Erro:{ex.Message}");
            }
        }

        /// <summary>
        /// Para consultar uma notificação de transação no PagSeguro, você deve fazer uma requisição à API de 
        /// Consulta de Notificações, informando o código da notificação. Utilizar o protocolo HTTP e o 
        /// método GET 
        /// </summary>
        /// <param name="notificationCode"></param>
        /// <param name="eMail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TransacaoPagSeguro> GetTransacaoAPIPagSeguroByNotification(string notificationCode, string eMail, string token)
        {
            try
            {
                Uri _uri = new Uri($"{BaseUrl}transactions/notifications/{notificationCode}?email={eMail}&token={token}");

                HttpResponseMessage responseGet = await httpClient.GetAsync(_uri);

                string response = await responseGet.Content.ReadAsStringAsync();

                if (response.Equals("Unauthorized"))
                    throw new Exception("ATENÇÃO: Ocorreu uma falha durante a autenticação do acesso ao serviço PagSeguro/transactions/notifications");

                TransacaoPagSeguro _tr = new TransacaoPagSeguro();
                ErrorsPagSeguro errorsPagSeguro = new ErrorsPagSeguro();

                XDocument doc = XDocument.Parse(response);

                // Verificando se houve erro na requisição:
                foreach (var er in doc.Descendants("error"))
                {
                    errorsPagSeguro.NotificationConde = notificationCode;
                    errorsPagSeguro.DtNotificacaoErro = DateTime.Now;
                    errorsPagSeguro.Code = (string)er.Element("code") ?? "";
                    errorsPagSeguro.Message = (string)er.Element("message") ?? "";
                }

                if (errorsPagSeguro.Message != null)
                    throw new Exception($"ATENÇÃO: Ocorreu um erro ao processar ao consultar notificação junto ao PagSeguro. NotificationCode: {notificationCode}, Código do Erro: {errorsPagSeguro.Code} - Mensagem: {errorsPagSeguro.Message}");
                // Fim da verificação:

                foreach (var tr in doc.Descendants("transaction"))
                {
                    TransacaoPagSeguro _tran = new TransacaoPagSeguro()
                    {
                        Date = (string)tr.Element("date") ?? "",
                        Reference = (string)tr.Element("reference") ?? "",
                        NotificationCode = (string)tr.Element("code") ?? "",
                        Type = (int?)tr.Element("type") ?? null,
                        Status = (int?)tr.Element("status") ?? null,
                        PaymentMethod = new PaymentMethodPagSeguro() { Type = (int?)tr.Element("type") ?? null, Code = (int?)tr.Element("code") ?? null },
                        GrossAmount = (decimal?)tr.Element("grossAmount") ?? null,
                        DiscountAmount = (decimal?)tr.Element("discountAmount") ?? null,
                        NetAmount = (decimal?)tr.Element("netAmount") ?? null,
                        ExtraAmount = (decimal?)tr.Element("extraAmount") ?? null,
                        Lasteventdate = (string)tr.Element("lastEventDate") ?? ""
                    };
                }
                return _tr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta os pagamentos realizados, a partir do dia presente menos a quantidade de dias informado, 
        /// através do PagSeguro.
        /// </summary>
        /// <param name="nrDias"></param>
        /// <param name="nrPage"></param>
        /// <param name="nrMaxPageResults"></param>
        /// <returns></returns>
        // [Authorize]
        [Route("sincronizar/")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetSincronizarRecebimentosPagSeguro(int nrDias, int nrPage, int nrMaxPageResults)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (nrDias == 0) throw new Exception("Número de dias não informado!");

                var dtRef = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

                dtRef = dtRef.AddHours(-2);

                DateTime _dtFinal = dtRef; 
                DateTime _dtInicial = _dtFinal.AddDays(-nrDias);

                string _dtIni, _dtFim;
                _dtIni = _dtInicial.Year + "-" + _dtInicial.Month + "-" + _dtInicial.Day + "T" + _dtInicial.Hour +":"+ _dtInicial.Minute;
                _dtFim = _dtFinal.Year + "-" + _dtFinal.Month + "-" + _dtFinal.Day + "T" + _dtFinal.Hour + ":" + _dtFinal.Minute;

                int _page, _maxPageResults, _nrRegistrosAtualizados;
                _page = nrPage == 0 ? 1 : nrPage;
                _maxPageResults = nrMaxPageResults == 0 ? 100 : nrMaxPageResults;

                _nrRegistrosAtualizados = 0;
                int? _currPage;

                TransactionSearchResult _transacoesPS = await GetSincronizarRecebimentosAPIPagSeguro(_dtIni, _dtFim, _page, _maxPageResults, EMail, Token);

                _currPage = _transacoesPS.CurrentPage ?? 0;

                if (_currPage > 0)
                {
                    while (_currPage <= _transacoesPS.TotalPages)
                    {
                        _nrRegistrosAtualizados += _pagSeguroApplication.UpdateRecebimentosPeriodoPagSeguro(_transacoesPS);

                        _currPage++;

                        if(_currPage <= _transacoesPS.TotalPages)
                            _transacoesPS = await GetSincronizarRecebimentosAPIPagSeguro(_dtIni, _dtFim, (int)_currPage, _maxPageResults, EMail, Token);
                    }

                    response = Request.CreateResponse(HttpStatusCode.OK, $"Foram atualizados {_nrRegistrosAtualizados} registros!");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Não foram encontrados registros para o período informado!"); 
                }

                tsc.SetResult(response);

                return await tsc.Task;
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

                tsc.SetResult(response);

                return await tsc.Task;
            }
        }

        /// <summary>
        /// Consulta ao PagSeguro por período.
        /// </summary>
        /// <param name="dtInicial"></param>
        /// <param name="dtFinal"></param>
        /// <param name="page"></param>
        /// <param name="maxPageResults"></param>
        /// <param name="eMail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TransactionSearchResult> GetSincronizarRecebimentosAPIPagSeguro(string dtInicial, string dtFinal, int page, int maxPageResults, string eMail, string token)
        {
            try
            {
                Uri _uri = new Uri($"{BaseUrl}transactions?initialDate={dtInicial}&finalDate={dtFinal}&page={page}&maxPageResults={maxPageResults}&email={eMail}&token={token}");

                HttpResponseMessage responseGet = await httpClient.GetAsync(_uri);

                string response = await responseGet.Content.ReadAsStringAsync();

                if (response.Equals("Unauthorized"))
                    throw new Exception("ATENÇÃO: Ocorreu uma falha durante a autenticação do acesso ao serviço PagSeguro/transactions");

                TransactionSearchResult _ts = new TransactionSearchResult();
                _ts.Transacoes = new List<TransacaoPagSeguro>();

                ErrorsPagSeguro errorsPagSeguro = new ErrorsPagSeguro();

                XDocument doc = XDocument.Parse(response);

                // Verificando se houve erro na requisição:
                foreach (var er in doc.Descendants("error"))
                {
                    errorsPagSeguro.NotificationConde = "";
                    errorsPagSeguro.DtNotificacaoErro = DateTime.Now;
                    errorsPagSeguro.Code = (string)er.Element("code") ?? "";
                    errorsPagSeguro.Message = (string)er.Element("message") ?? "";
                }

                if (errorsPagSeguro.Message != null)
                    throw new Exception($"ATENÇÃO: Ocorreu um erro ao tentar obter listagem de recebimentos junto ao PagSeguro. Código do Erro: {errorsPagSeguro.Code} - Mensagem: {errorsPagSeguro.Message} - dtInicial: {dtInicial} & dtFinal: {dtFinal}");
                // Fim da verificação:

                foreach (var t in doc.Descendants("transactionSearchResult"))
                {
                    _ts.Date = (DateTime?) t.Element("date") ?? null;

                    foreach (var tr in doc.Descendants("transaction"))
                    {
                        TransacaoPagSeguro _tran = new TransacaoPagSeguro()
                        {
                            Date = (string) tr.Element("date") ?? String.Empty,
                            Reference = (string) tr.Element("reference") ?? String.Empty,
                            NotificationCode = (string) tr.Element("code") ?? String.Empty,
                            Type = (int?) tr.Element("type") ?? null,
                            Status = (int?) tr.Element("status") ?? null,
                            PaymentMethod = new PaymentMethodPagSeguro() { Type = (int?) tr.Element("type") ?? null},
                            GrossAmount = (decimal?) tr.Element("grossAmount") ?? null,
                            DiscountAmount = (decimal?) tr.Element("discountAmount") ?? null,
                            FeeAmount = (decimal?)tr.Element("feeAmount") ?? null,
                            NetAmount = (decimal?) tr.Element("netAmount") ?? null,
                            ExtraAmount = (decimal?) tr.Element("extraAmount") ?? null,
                            Lasteventdate = (string) tr.Element("date") ?? String.Empty
                        };
                        _ts.Transacoes.Add(_tran);
                    }
                    _ts.ResultsInThisPage = (int?) t.Element("resultsInThisPage") ?? null;
                    _ts.CurrentPage = (int?) t.Element("currentPage") ?? null;
                    _ts.TotalPages = (int?) t.Element("totalPages") ?? null;
                }
                return _ts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtém o Token dinâmico no PagSeguro para cada pagamento da FBTC.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="CheckOutPagSeguro"></param>
        /// <returns></returns>
        public async Task<TokenCheckOutPagSeguro> GetTokenDinamicoAPIPagSeguro(string email, string token, CheckOutPagSeguro c)
        {
            try
            {
                Uri _uri = new Uri($"{BaseUrl}checkout");

                var _parametros = new List<KeyValuePair<string, string>>();
                _parametros.Add(new KeyValuePair<string, string>("email", email));
                _parametros.Add(new KeyValuePair<string, string>("token", token));
                _parametros.Add(new KeyValuePair<string, string>("currency", c.Currency));
                _parametros.Add(new KeyValuePair<string, string>("itemId1", c.ItemId1));
                _parametros.Add(new KeyValuePair<string, string>("itemDescription1", c.ItemDescription1));
                _parametros.Add(new KeyValuePair<string, string>("itemAmount1", c.ItemAmount1.ToString()));
                _parametros.Add(new KeyValuePair<string, string>("itemQuantity1", c.ItemQuantity1));
                _parametros.Add(new KeyValuePair<string, string>("itemWeight1", c.ItemWeight1));
                _parametros.Add(new KeyValuePair<string, string>("reference", c.Reference));
                _parametros.Add(new KeyValuePair<string, string>("senderName", c.SenderName));
                _parametros.Add(new KeyValuePair<string, string>("senderAreaCode", c.SenderAreaCode));
                _parametros.Add(new KeyValuePair<string, string>("senderPhone", c.SenderPhone));
                _parametros.Add(new KeyValuePair<string, string>("senderEmail", c.SenderEmail));
                _parametros.Add(new KeyValuePair<string, string>("shippingType", c.ShippingType));
                _parametros.Add(new KeyValuePair<string, string>("shippingAddressRequired", c.ShippingAddressRequired));
                _parametros.Add(new KeyValuePair<string, string>("shippingAddressStreet", c.ShippingAddressStreet));
                _parametros.Add(new KeyValuePair<string, string>("shippingAddressNumber", c.ShippingAddressNumber));
                _parametros.Add(new KeyValuePair<string, string>("shippingAddressComplement", c.ShippingAddressComplement));
                _parametros.Add(new KeyValuePair<string, string>("shippingAddressDistrict", c.ShippingAddressDistrict));
                _parametros.Add(new KeyValuePair<string, string>("shippingAddressPostalCode", c.ShippingAddressPostalCode));
                _parametros.Add(new KeyValuePair<string, string>("shippingAddressCity", c.ShippingAddressCity));
                _parametros.Add(new KeyValuePair<string, string>("shippingAddressState", c.ShippingAddressState));
                _parametros.Add(new KeyValuePair<string, string>("shippingAddressCountry", c.ShippingAddressCountry));
                _parametros.Add(new KeyValuePair<string, string>("timeout", "25"));
                _parametros.Add(new KeyValuePair<string, string>("enableRecovery", "false"));

                var content = new System.Net.Http.FormUrlEncodedContent(_parametros);

                HttpResponseMessage responsePost = await httpClient.PostAsync(_uri, content);

                string response = await responsePost.Content.ReadAsStringAsync();

                if (response.Equals("Unauthorized"))
                    throw new Exception("ATENÇÃO: Ocorreu uma falha durante a autenticação do acesso ao serviço PagSeguro/Checkout");

                TokenCheckOutPagSeguro _ck = new TokenCheckOutPagSeguro();
                ErrorsPagSeguro errorsPagSeguro = new ErrorsPagSeguro();

                XDocument doc = XDocument.Parse(response);

                // Verificando se houve erro na requisição:
                foreach (var er in doc.Descendants("error"))
                {
                    errorsPagSeguro.NotificationConde = "";
                    errorsPagSeguro.DtNotificacaoErro = DateTime.Now;
                    errorsPagSeguro.Code = (string)er.Element("code") ?? "";
                    errorsPagSeguro.Message = (string)er.Element("message") ?? "";
                }

                if (errorsPagSeguro.Message != null)
                    throw new Exception($"ATENÇÃO: Ocorreu um erro ao tentar obter token dinâmico junto ao PagSeguro. Reference: {c.Reference}, Código do Erro: {errorsPagSeguro.Code} - Mensagem: {errorsPagSeguro.Message}");
                // Fim da verificação:

                if (doc != null)
                {
                    foreach (var c1 in doc.Descendants("checkout"))
                    {
                        _ck.Date = (DateTime?)c1.Element("date") ?? null;
                        _ck.Code = (String)c1.Element("code");
                    }
                    _ck.Reference = c.Reference;
                }

                return _ck;
            }
            catch (Exception ex)
            {
                throw new Exception($"ATENÇÃO: Ocorreu um erro durante o acesso ao PagSeguro. Exception Type:{ex.GetType()}. Erro:{ex.Message}");
            }
        }
    }
}