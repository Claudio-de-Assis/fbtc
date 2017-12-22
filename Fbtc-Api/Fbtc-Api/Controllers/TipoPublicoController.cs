using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/TipoPublico")]
    public class TipoPublicoController : ApiController
    {
        private readonly ITipoPublicoApplication _tipoPublicoApplication;

        public TipoPublicoController(ITipoPublicoApplication tipoPublicoApplication)
        {
            _tipoPublicoApplication = tipoPublicoApplication;
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
                var resultado = _tipoPublicoApplication.GetAll();

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
        public Task<HttpResponseMessage> GetById(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("Id não informado!");

                var resultado = _tipoPublicoApplication.GetTipoPublicoById(id);

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
        [Route("Evento/{id:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetValorByEventoId(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("Id não informado!");

                var resultado = _tipoPublicoApplication.GetTipoPublicoValorByEventoId(id);

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