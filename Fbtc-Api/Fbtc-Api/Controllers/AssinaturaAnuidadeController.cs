using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using Fbtc.Application.Services;
using Fbtc.Domain.Services;
using Fbtc.Infra.Persistencia.PagSeguro;
using System.Globalization;
using Fbtc.Application.Helper;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/AssinaturaAnuidade")]
    public class AssinaturaAnuidadeController : ApiController
    {
        private readonly IAssinaturaAnuidadeApplication _assinaturaAnuidadeApplication;

        public AssinaturaAnuidadeController(IAssinaturaAnuidadeApplication assinaturaAnuidadeApplication)
        {
            _assinaturaAnuidadeApplication = assinaturaAnuidadeApplication;
        }

        // [Authorize]
        [Route("GetAll")]
        [HttpGet]
        public Task<HttpResponseMessage> GetAll()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _assinaturaAnuidadeApplication.GetAll();

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        // [Authorize]
        [Route("FindByFilters/{anuidadeId:int},{nome},{cpf},{ativo:bool}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindByFilters(int anuidadeId, string nome, string cpf, bool? ativo)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _assinaturaAnuidadeApplication.FindByFilters(anuidadeId, nome, cpf, ativo);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        // [Authorize]
        [Route("FindByPessoaId/{pessoaId:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindByPessoaId(int pessoaId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (pessoaId == 0) throw new Exception("pessoaId não informado!");

                var resultado = _assinaturaAnuidadeApplication.FindByPessoaId(pessoaId);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        // [Authorize]
        [Route("{id:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetAssinaturaAnuidadeById(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("Id não informado!");

                var resultado = _assinaturaAnuidadeApplication.GetAssinaturaAnuidadeById(id);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        // [Authorize]
        [Route("AssinaturaAnuidade")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post(AssinaturaAnuidadeDao a)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            string resultado = "false";
            decimal _valorDecimal;

            try
            {
                if (a == null) throw new ArgumentNullException("O objeto 'assinaturaAnuidadeDao' está nulo!");

                if (a.AssinaturaAnuidadeId == 0 || a.ValorAnuidadeId != a.ValorAnuidadeIdOriginal || a.CodePS == "")
                {
                    if (a.PagamentoIsento == false)
                    {
                        PagSeguroRepository r = new PagSeguroRepository();
                        PagSeguroService s = new PagSeguroService(r);
                        PagSeguroApplication p = new PagSeguroApplication(s);
                        PagSeguroController pagSeguroController = new PagSeguroController(p);

                        _valorDecimal = Functions.CalcularDescontoAnuidade(a);

                        if (a.MembroConfi == false)
                        {
                            string _valor = _valorDecimal.ToString("F", CultureInfo.InvariantCulture);

                            TokenCheckOutPagSeguro _token = await pagSeguroController.GetTokenCheckOutPessoaId(a.AssociadoId, _valor, "1", a.Exercicio, a.AnoTermino, true, true);

                            if (_token != null)
                            {
                                a.CodePS = _token.Code;
                                a.DtCodePS = _token.Date;
                                a.Reference = _token.Reference;
                            }
                        }
                    }
                }

                resultado = _assinaturaAnuidadeApplication.Save(a);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);
                response.ReasonPhrase = resultado;

                tsc.SetResult(response);

                return await tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                    response.ReasonPhrase = ex.Message;
                }
                tsc.SetResult(response);

                return await tsc.Task;
            }
        }

        // [Authorize]
        [Route("FindAssinaturaPendenteByFilters/{anuidadeId:int},{nome},{cpf},{ativo:bool}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindAssinaturaPendenteByFilters(int anuidadeId, string nome, string cpf, bool? ativo)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _assinaturaAnuidadeApplication.FindAssinaturaPendenteByFilters(anuidadeId, nome, cpf, ativo);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }
    }
}