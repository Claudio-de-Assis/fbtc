using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/DescontoAnuidadeAtc")]
    public class DescontoAnuidadeAtcController : ApiController
    {

        private readonly IDescontoAnuidadeAtcApplication _descontoAnuidadeAtcApplication;

        public DescontoAnuidadeAtcController(IDescontoAnuidadeAtcApplication descontoAnuidadeAtcApplication)
        {
            _descontoAnuidadeAtcApplication = descontoAnuidadeAtcApplication;
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
                var resultado = _descontoAnuidadeAtcApplication.GetAll();

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

        [Route("FindDaoByAnuidadeId/{anuidadeId:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetDescontoAnuidadeAtcDaoByAnuidadeId(int anuidadeId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (anuidadeId == 0) throw new Exception("anuidadeId não informado!");

                var resultado = _descontoAnuidadeAtcApplication.GetDescontoAnuidadeAtcDaoByAnuidadeId(anuidadeId);

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
        [Route("FindByFilters/{anuidadeId:int},{nomePessoa},{ativo:bool},{comDesconto:bool}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindByFilters(int anuidadeId, string nomePessoa, bool? ativo, bool? comDesconto)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _descontoAnuidadeAtcApplication.FindByFilters(anuidadeId, nomePessoa, ativo, comDesconto);

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
        public Task<HttpResponseMessage> GetDescontoAnuidadeAtcById(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("Id não informado!");

                var resultado = _descontoAnuidadeAtcApplication.GetDescontoAnuidadeAtcById(id);

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
        [Route("DescontoAnuidadeAtcDao/{id:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetDescontoAnuidadeAtcDaoById(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("Id não informado!");

                var resultado = _descontoAnuidadeAtcApplication.GetDescontoAnuidadeAtcDaoById(id);

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
        [Route("FindDaoByPessoaId/{pessoaId:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetDescontoAnuidadeAtcDaoByPessoaId(int pessoaId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (pessoaId == 0) throw new Exception("pessoaId não informado!");

                var resultado = _descontoAnuidadeAtcApplication.GetDescontoAnuidadeAtcDaoByPessoaId(pessoaId);

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
        [Route("GetDadosNovoDescontoAnuidadeAtcDao/{associadoId:int},{anuidadeId:int},{colaboradorPessoaId:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetDadosNovoDescontoAnuidadeAtcDao(int associadoId, int anuidadeId, int colaboradorPessoaId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (associadoId == 0) throw new Exception("associadoId não informado!");
                if (anuidadeId == 0) throw new Exception("anuidadeId não informado!");
                if (colaboradorPessoaId == 0) throw new Exception("colaboradorPessoaId não informado!");

                var resultado = _descontoAnuidadeAtcApplication.GetDadosNovoDescontoAnuidadeAtcDao(associadoId, anuidadeId, colaboradorPessoaId);

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
        [Route("DescontoAnuidadeAtcDao")]
        [HttpPost]
        public Task<HttpResponseMessage> Post(DescontoAnuidadeAtcDao descontoAnuidadeAtcDao)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            string resultado = "false";

            try
            {
                if (descontoAnuidadeAtcDao == null) throw new ArgumentNullException("O objeto 'descontoAnuidadeAtcDao' está nulo!");

                resultado = _descontoAnuidadeAtcApplication.Save(descontoAnuidadeAtcDao);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);
                response.ReasonPhrase = resultado;

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