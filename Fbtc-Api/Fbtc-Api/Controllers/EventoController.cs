﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/Evento")]
    public class EventoController : ApiController
    {
        private readonly IEventoApplication _eventoApplication;

        public EventoController(IEventoApplication eventoApplication)
        {
            _eventoApplication = eventoApplication;
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
                var resultado = _eventoApplication.GetAll();

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException")
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
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

                var resultado = _eventoApplication.GetEventoById(id);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException")
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
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
        [Route("SetEvento")]
        [HttpGet]
        public Task<HttpResponseMessage> SetAssociado()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _eventoApplication.SetEvento();

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException")
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
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
        [Route("FindByFilters/{titulo},{ano},{tipoEvento}")]
        [HttpGet]
        public Task<HttpResponseMessage> FindByFilters(string titulo, string ano,
            string tipoEvento)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _eventoApplication.FindByFilters(titulo,Convert.ToInt16(ano),
                    tipoEvento);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException")
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
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
        [Route("Evento")]
        [HttpPost]
        public Task<HttpResponseMessage> Post(Evento evento)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            string resultado = "false";

            try
            {
                if (evento == null) throw new ArgumentNullException("O objeto 'Evento' está nulo!");

                resultado = _eventoApplication.Save(evento);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException")
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
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