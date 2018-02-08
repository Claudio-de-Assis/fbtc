using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/PagSeguro")]
    public class PagSeguroController : ApiController
    {
        private readonly string EMail = "anuidades@fbtc.org.br";

        // PRD:
        private readonly string BaseUrl = "https://ws.pagseguro.uol.com.br/v2/";
        private readonly string Token = "E954153EC8584C4A93FFD2808F021477";

        //SandBox
        //private readonly string BaseUrl = "https://ws.sandbox.pagseguro.uol.com.br/v2/";
        //private readonly string Token = "B1AC46BB903E4E8B8667C84B77C2E640";

        private HttpClient httpClient = new HttpClient();

        private readonly IPagSeguroApplication _pagSeguroApplication;

        public PagSeguroController(IPagSeguroApplication pagSeguroApplication)
        {
            _pagSeguroApplication = pagSeguroApplication;
        }

        /// <summary>
        /// Recebendo uma notificação de transação do PagSeguro
        /// Uma vez configurado o endereço para onde o PagSeguro irá enviar notificações, 
        /// o próximo passo é preparar seu sistema para receber, nesse endereço, um código de notificação.
        /// O PagSeguro envia as notificações para a URL que você configurou usando o protocolo HTTP,
        /// pelo método POST.
        /// </summary>
        /// <param name="notificationCode"></param>
        /// <param name="notificationType"></param>
        /// <returns></returns>
        //Recebendo uma notificação de transação:
        // [Authorize]
        [Route("notificacao/")]
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

                resultado = _pagSeguroApplication.NotificationTransacao(notificationCode, notificationType);

                TransacaoPagSeguro _transacaoPagSeguro = await GetTransacaoAPIPagSeguroByNotification(notificationCode.Trim(), EMail, Token);

                if (_transacaoPagSeguro.Code != null && _transacaoPagSeguro.Reference != null)
                    resultado = _pagSeguroApplication.UpdateRecebimentoPagSeguro(_transacaoPagSeguro);

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

                TransacaoPagSeguro _tr = new TransacaoPagSeguro();

                XDocument doc = XDocument.Parse(response);

                foreach (var tr in doc.Descendants("transaction"))
                {
                    TransacaoPagSeguro _tran = new TransacaoPagSeguro()
                    {
                        Date = (string)tr.Element("date") ?? "",
                        Reference = (string)tr.Element("reference") ?? "",
                        Code = (string)tr.Element("code") ?? "",
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

                DateTime _dtFinal = DateTime.Now;
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

                    response = Request.CreateResponse(HttpStatusCode.OK, $"   --   Foram atualizados {_nrRegistrosAtualizados} registros!");
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

                TransactionSearchResult _ts = new TransactionSearchResult();
                _ts.Transacoes = new List<TransacaoPagSeguro>();

                XDocument doc = XDocument.Parse(response);

                foreach (var t in doc.Descendants("transactionSearchResult"))
                {
                    _ts.Date = (DateTime?) t.Element("date") ?? null;

                    foreach (var tr in doc.Descendants("transaction"))
                    {
                        TransacaoPagSeguro _tran = new TransacaoPagSeguro()
                        {
                            Date = (string) tr.Element("date") ?? String.Empty,
                            Reference = (string) tr.Element("reference") ?? String.Empty,
                            Code = (string) tr.Element("code") ?? String.Empty,
                            Type = (int?) tr.Element("type") ?? null,
                            Status = (int?) tr.Element("status") ?? null,
                            PaymentMethod = new PaymentMethodPagSeguro() { Type = (int?) tr.Element("type") ?? null},
                            GrossAmount = (decimal?) tr.Element("grossAmount") ?? null,
                            DiscountAmount = (decimal?) tr.Element("discountAmount") ?? null,
                            NetAmount = (decimal?) tr.Element("netAmount") ?? null,
                            ExtraAmount = (decimal?) tr.Element("extraAmount") ?? null,
                            Lasteventdate = (string) tr.Element("lastEventDate") ?? String.Empty
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
    }
}