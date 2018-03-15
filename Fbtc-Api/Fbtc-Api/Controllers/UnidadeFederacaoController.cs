using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/UnidadeFederacao")]
    public class UnidadeFederacaoController : ApiController
    {
        private readonly IUnidadeFederacaoApplication _unidadeFederacaoApplication;

        public UnidadeFederacaoController(IUnidadeFederacaoApplication unidadeFederacaoApplication)
        {
            _unidadeFederacaoApplication = unidadeFederacaoApplication;
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
                var resultado = _unidadeFederacaoApplication.GetAll();

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
        [Route("GetDisponiveis/{atcId:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetDisponiveis(int atcId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _unidadeFederacaoApplication.GetDisponiveis(atcId);

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
        [Route("GetUtilizadas")]
        [HttpGet]
        public Task<HttpResponseMessage> GetUtilizadas()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _unidadeFederacaoApplication.GetUtilizadas();

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