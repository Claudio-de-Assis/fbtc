using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/Recebimento")]
    public class RecebimentoController : ApiController
    {
        private readonly IRecebimentoApplication _recebimentoApplication;

        public RecebimentoController(IRecebimentoApplication recebimentoApplication)
        {
            _recebimentoApplication = recebimentoApplication;
        }
        
        // [Authorize]
        [Route("GetAll/{objetivoPagamento}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetAll(string objetivoPagamento)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (objetivoPagamento == "") throw new Exception("Objetivo do Pagamento não informado!");

                var resultado = _recebimentoApplication.GetAll(objetivoPagamento);

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

                var resultado = _recebimentoApplication.GetRecebimentoById(id);

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
        [Route("PagamentoAssociado/{id:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetPagamentoAssociadoByRecebimentoId(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("Id não informado!");

                var resultado = _recebimentoApplication.GetRecebimentoAssociadoDaoByRecebimentoId(id);

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
        [Route("SetRecebimento/{objetivoPagamento}")]
        [HttpGet]
        public Task<HttpResponseMessage> SetAssociado(string objetivoPagamento)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (objetivoPagamento == "") throw new Exception("Objetivo do Pagamento não informado!");

                var resultado = _recebimentoApplication.SetRecebimento(objetivoPagamento);

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
        [Route("FindAnuidadeByFilters/{nome},{cpf},{crp},{crm},{statusPS},{ano},{mes},{ativo},{tipoPublicoId}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindAnuidadeByFilters(string nome, string cpf,
            string crp, string crm, int statusPS, string ano, string mes, bool? ativo, string tipoPublicoId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _recebimentoApplication.FindAnuidadeByFilters(nome, cpf,
                    crp, crm, statusPS, Convert.ToInt16(ano), Convert.ToInt16(mes),
                    ativo, Convert.ToInt32(tipoPublicoId));

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
        [Route("FindPagamentosByPessoaIdIdFilters/{pessoaId:int},{objetivoPagamento},{ano:int},{statusPS:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindPagamentosByPessoaIdIdFilters(int pessoaId,
            string objetivoPagamento, int ano, int statusPS)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _recebimentoApplication.FindPagamentosByPessoaIdIdFilters(pessoaId,
                    objetivoPagamento, Convert.ToInt16(ano), Convert.ToInt16(statusPS));

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
        [Route("FindByAnuidadeIdFilters/{anuidadeId},{nome},{cpf},{crp},{crm},{statusPS},{ano},{mes},{ativo},{tipoPublicoId}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindByAnuidadeIdFilters(int anuidadeId, string nome, string cpf,
            string crp, string crm, int statusPS, string ano, string mes, bool? ativo, string tipoPublicoId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _recebimentoApplication.FindByAnuidadeIdFilters(anuidadeId,nome, cpf,
                    crp, crm, statusPS, Convert.ToInt16(ano), Convert.ToInt16(mes),
                    ativo, Convert.ToInt32(tipoPublicoId));

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
        [Route("FindEventoByFilters/{nome},{cpf},{crp},{crm},{statusPS},{ano},{mes},{ativo},{tipoEvento},{tipoPublicoId}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindEventoByFilters(string nome, string cpf,
            string crp, string crm, int statusPS, string ano, string mes, bool? ativo, 
            string tipoEvento, string tipoPublicoId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _recebimentoApplication.FindEventoByFilters(nome, cpf,
                    crp, crm, statusPS ,Convert.ToInt16(ano), Convert.ToInt16(mes), 
                    ativo, tipoEvento, Convert.ToInt32(tipoPublicoId));

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
        [Route("FindByEventoIdFilters/{eventoId},{nome},{cpf},{crp},{crm},{statusPS},{ano},{mes},{ativo},{tipoEvento},{tipoPublicoId}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindByEventoIdFilters(int eventoId, string nome, string cpf,
            string crp, string crm, int statusPS, string ano, string mes, bool? ativo, 
            string tipoEvento, string tipoPublicoId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _recebimentoApplication.FindByEventoIdFilters(eventoId, nome, cpf,
                    crp, crm, statusPS, Convert.ToInt16(ano), Convert.ToInt16(mes),
                    ativo, tipoEvento, Convert.ToInt32(tipoPublicoId));

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
        [Route("Recebimento")]
        [HttpPost]
        public Task<HttpResponseMessage> Post(Recebimento recebimento)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            string resultado = "false";

            try
            {
                if (recebimento == null) throw new ArgumentNullException("O objeto 'recebimento' está nulo");

                resultado = _recebimentoApplication.Save(recebimento);

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
        [Route("GetByPessoaId/{objetivoPagamento},{id:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetByPessoaId(string objetivoPagamento, int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("PessoaId não informado!");
                if (objetivoPagamento == "") throw new Exception("ObjetivoPagamento não informado!");

                var resultado = _recebimentoApplication.GetRecebimentoByPessoaId(objetivoPagamento, id);

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
        [Route("GetByEventoId/{id:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetByEventoId(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("EventoId não informado!");

                var resultado = _recebimentoApplication.GetRecebimentoByEventoId(id);

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
        [Route("GetByAnuidadeId/{id:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetByAnuidadeId(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("AnuidadeId não informada!");

                var resultado = _recebimentoApplication.GetRecebimentoByAnuidadeId(id);

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