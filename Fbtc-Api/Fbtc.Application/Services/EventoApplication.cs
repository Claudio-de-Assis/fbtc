using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Application.Interfaces;

using prmToolkit.Validation;
using Fbtc.Application.Helper;

namespace Fbtc.Application.Services
{
    public class EventoApplication : IEventoApplication
    {
        private readonly IEventoService _eventoService;

        public EventoApplication(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Evento> FindByFilters(string titulo, int ano, string tipoEvento)
        {
            string _titulo, _tipoEvento;

            _titulo = titulo == "0" ? "" : titulo;
            _tipoEvento = tipoEvento == "0" ? "" : tipoEvento;

            return _eventoService.FindByFilters(_titulo, ano, _tipoEvento);
        }

        public IEnumerable<Evento> GetAll()
        {
            return _eventoService.GetAll();
        }

        public Evento GetEventoById(int id)
        {
            return _eventoService.GetEventoById(id);
        }

        public Evento GetEventoByRecebimentoId(int id)
        {
            return _eventoService.GetEventoByRecebimentoId(id);
        }

        public string GetNomeFotoByEventoId(int id)
        {
            return _eventoService.GetNomeFotoByEventoId(id);
        }

        public string Save(Evento e)
        {
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
               RaiseException.IfNullOrEmpty(e.Titulo, "Título não informado"),
               RaiseException.IfNullOrEmpty(e.Descricao, "Descrição não informada"),
               RaiseException.IfTrue(DateTime.Equals(e.DtInicio, DateTime.MinValue), "Data de Início não informada"),
               RaiseException.IfTrue(DateTime.Equals(e.DtTermino, DateTime.MinValue), "Data de Término não informada"),
               RaiseException.IfNullOrEmpty(e.TipoEvento, "Tipo de Evento não informado")
            );

            Evento _e = new Evento()
            {
                EventoId = e.EventoId,
                Titulo = Functions.AjustaTamanhoString(e.Titulo, 100),
                Descricao = Functions.AjustaTamanhoString(e.Descricao, 2000),
                Codigo = Functions.AjustaTamanhoString(e.Codigo, 60),
                DtInicio = e.DtInicio,
                DtTermino = e.DtTermino,
                DtTerminoInscricao = e.DtTerminoInscricao,
                TipoEvento = e.TipoEvento,
                AceitaIsencaoAta = e.AceitaIsencaoAta,
                NomeFoto = e.NomeFoto,
                Ativo = e.Ativo
            };

            try
            {
                if (_e.EventoId == 0)
                {
                    return _eventoService.Insert(_e);
                }
                else
                {
                    return _eventoService.Update(e.EventoId, _e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveValoresEvento(IEnumerable<TipoPublicoValorDao> tiposPublicosValoresDao)
        {
            string msg = "";
            try
            {
                foreach (var vtp in tiposPublicosValoresDao)
                {
                    if (vtp.ValorEventoPublicoId == 0)
                    {
                        msg = _eventoService.InsertValorEvento(vtp);
                    }
                    else
                    {
                        msg = _eventoService.UpdateValorEvento(vtp.ValorEventoPublicoId, vtp);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return msg;
        }

        public Evento SetEvento()
        {
            Evento e = new Evento() {
                EventoId = 0,
                Titulo = "",
                Descricao = "",
                Codigo = "",
                DtInicio = null,
                DtTermino = null,
                DtTerminoInscricao = null,
                TipoEvento = "",
                AceitaIsencaoAta = false,
                Ativo = true,
                NomeFoto = ""
            };
            return e;
        }
    }
}
