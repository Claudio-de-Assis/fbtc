using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/Relatorios")]
    public class RelatoriosController : ApiController
    {
        private readonly IRelatoriosApplication _relatoriosApplication;

        public RelatoriosController(IRelatoriosApplication relatoriosApplication)
        {
            _relatoriosApplication = relatoriosApplication;
        }

        // [Authorize]
        [Route("GetRptTotalAssociadosTipo")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptTotalAssociadosTipo()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _relatoriosApplication.GetRptTotalAssociadosTipo();

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
        [Route("GetRptRecebimentoStatus/{objetivoPagamento:int},{anoEventoPS:int},{statusPS:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptRecebimentoStatus(int objetivoPagamento, int anoEventoPS, int statusPS)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (objetivoPagamento == 0) throw new Exception("objetivoPagamento não informado!");
                if (anoEventoPS == 0) throw new Exception("anoEventoPS não informado!");

                var resultado = _relatoriosApplication.GetRptRecebimentoStatus(objetivoPagamento, anoEventoPS, statusPS);

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
        [Route("GetRptAssociadosFaixa")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosFaixa()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _relatoriosApplication.GetRptAssociadosFaixa();

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
        [Route("GetRptAssociadosEstados")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosEstados()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _relatoriosApplication.GetRptAssociadosEstados();

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
        [Route("GetRptAssociadosGenero/{genero}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosGenero(string genero)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (string.IsNullOrWhiteSpace(genero)) throw new Exception("genero não informado!");

                var resultado = _relatoriosApplication.GetRptAssociadosGenero(genero);

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
        [Route("GetRptAssociadosAno/{ano:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosAno(int ano)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (ano == 0) throw new Exception("ano não informado!");

                var resultado = _relatoriosApplication.GetRptAssociadosAno(ano);

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
        [Route("GetRptReceitaAnual")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptReceitaAnual()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _relatoriosApplication.GetRptReceitaAnual();

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
