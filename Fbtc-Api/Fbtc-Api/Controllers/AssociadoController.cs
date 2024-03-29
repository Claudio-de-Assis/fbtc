﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/Associado")]
    public class AssociadoController : ApiController
    {
        private readonly IAssociadoApplication _associadoApplication;

        public AssociadoController(IAssociadoApplication associadoApplication)
        {
            _associadoApplication = associadoApplication;
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
                var resultado = _associadoApplication.GetAll();

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

                var resultado = _associadoApplication.GetAssociadoById(id);

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
        [Route("associadoDao/{id:int},{anuidadeId:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetAssociadoDaoById(int id, int anuidadeId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("Id não informado!");

                if (anuidadeId == 0) throw new Exception("anuidadeId não informada!");

                var resultado = _associadoApplication.GetAssociadoDaoById(id, anuidadeId);

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
        [Route("associadoDao/{id:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetAssociadoDaoByPessoaId(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("Id não informado!");

                var resultado = _associadoApplication.GetAssociadoDaoByPessoaId(id);

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
        [Route("Pessoa/{id:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetByPessoaId(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("Id não informado!");

                var resultado = _associadoApplication.GetAssociadoByPessoaId(id);

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
        [Route("NomeFoto/{id:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetNomeFotoByPessoaId(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new Exception("Id não informado!");

                var resultado = _associadoApplication.GetNomeFotoByPessoaId(id);

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
        [Route("SetAssociado")]
        [HttpGet]
        public Task<HttpResponseMessage> SetAssociado()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _associadoApplication.SetAssociado();

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
        [Route("FindByFilters/{nome},{cpf},{sexo},{atcId},{crp},{tipoprofissao},{tipoPublicoId},{estado}," +
            "{cidade},{ativo}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindByFilters(string nome, string cpf,
            string sexo, string atcId, string crp, string tipoProfissao, string tipoPublicoId, string estado,
            string cidade, string ativo)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                bool? _ativo = null;

                if (ativo != null)
                {
                    if (ativo.Equals("true"))
                        _ativo = true;

                    if (ativo.Equals("false"))
                        _ativo = false;
                }
                
                var resultado = _associadoApplication.FindByFilters(nome, cpf, 
                    sexo, Convert.ToInt16(atcId), crp, tipoProfissao, Convert.ToInt32(tipoPublicoId),
                    estado, cidade, _ativo);

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
        [Route("FindIsentoByFilters/{isencaoId},{nome},{cpf},{sexo},{atcId},{crp},{tipoprofissao},{tipoPublicoId},{estado}," +
            "{cidade},{ativo}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindIsentoByFilters(int isencaoId, string nome, string cpf,
            string sexo, string atcId, string crp, string tipoProfissao, string tipoPublicoId, string estado,
            string cidade, string ativo)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                bool? _ativo = null;

                if (ativo != null)
                {
                    if (ativo.Equals("true"))
                        _ativo = true;

                    if (ativo.Equals("false"))
                        _ativo = false;
                }

                var resultado = ""; /*_associadoApplication.FindIsentoByFilters(isencaoId, nome, cpf,
                    sexo, Convert.ToInt16(atcId), crp, tipoProfissao, Convert.ToInt32(tipoPublicoId),
                    estado, cidade, _ativo);*/

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
        [Route("Associado")]
        [HttpPost]
        public Task<HttpResponseMessage> Post(AssociadoDao associado)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            string resultado = "false";

            try
            {
                if (associado == null) throw new ArgumentNullException("O objeto 'Associado' está nulo!");

                resultado = _associadoApplication.Save(associado);

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

        // [Authorize]
        [Route("AssociadoIsento")]
        [HttpPost]
        public Task<HttpResponseMessage> PostIsento(AssociadoIsentoDao associadoIsentoDao)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            string resultado = "false";

            try
            {
                if (associadoIsentoDao == null) throw new ArgumentNullException("O objeto 'AssociadoIsentoDao' está nulo!");

                resultado = ""; // _associadoApplication.SaveIsento(associadoIsentoDao);

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
        [Route("RessetPassword/{id:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> RessetPasswordById(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (id == 0) throw new InvalidOperationException("Id não informado!");

                var resultado = _associadoApplication.RessetPasswordById(id);

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
        [Route("ValidaEmail/{associadoId:int},{eMail}/")]
        [HttpGet]
        public Task<HttpResponseMessage> ValidaEmail(int associadoId, string eMail)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (eMail == "") throw new Exception("eMail não informado!");

                var resultado = _associadoApplication.ValidaEMail(associadoId, eMail);

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

        [Authorize]
        // [Route("FindAssociadoAdimplente?nomeCidade=&nomeAssociado=&tipoPublicoId=&statusCertificacao=")]
        [Route("FindAssociadoAdimplente")]
        [HttpGet]
        public Task<HttpResponseMessage> FindAssociadoAdimplente(int? pageSize, int? numPage, string nomeCidade, string nomeAssociado,
            string tipoPublicoId, bool? statusCertificacao)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {

                int _pageSize = Convert.ToInt32(pageSize != null ? pageSize : 0);
                int _numPage = Convert.ToInt32(numPage != null ? numPage : 0);

                string _nomeCidade = string.IsNullOrEmpty(nomeCidade) ? "" : nomeCidade;
                string _nomeAssociado = string.IsNullOrEmpty(nomeAssociado) ? "" : nomeAssociado;
                string _statusCertificacao = "";
                int _tipoPublicoId = string.IsNullOrEmpty(tipoPublicoId) ? 0 : Convert.ToInt32(tipoPublicoId);

                if (statusCertificacao != null)
                {
                    if (statusCertificacao == true)
                        _statusCertificacao = "Sim";

                    if (statusCertificacao == false)
                        _statusCertificacao = "Não";
                }

                var resultado = _associadoApplication.FindAssociadoAdimplente(_pageSize, _numPage, _nomeCidade, _nomeAssociado,
                   _tipoPublicoId, _statusCertificacao);

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