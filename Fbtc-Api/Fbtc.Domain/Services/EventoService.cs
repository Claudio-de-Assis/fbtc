using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;

namespace Fbtc.Domain.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Evento> FindByFilters(string titulo, int ano, string tipoEvento)
        {
            return _eventoRepository.FindByFilters(titulo, ano, tipoEvento);
        }

        public IEnumerable<EventoDao> FindEventoDaoByFilters(string titulo, int ano, string tipoEvento)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Evento> GetAll()
        {
            return _eventoRepository.GetAll();
        }

        public Evento GetEventoById(int id)
        {
            return _eventoRepository.GetEventoById(id);
        }

        public Evento GetEventoByRecebimentoId(int id)
        {
            return _eventoRepository.GetEventoByRecebimentoId(id);
        }

        public EventoDao GetEventoDaoById(int id)
        {
            return _eventoRepository.GetEventoDaoById(id);
        }

        public string GetNomeFotoByEventoId(int id)
        {
            return _eventoRepository.GetNomeFotoByEventoId(id);
        }

        public string Insert(Evento evento)
        {
            return _eventoRepository.Insert(evento);
        }

        public string InsertEventoDao(EventoDao eventoDao)
        {
            return _eventoRepository.InsertEventoDao(eventoDao);
        }

        public string InsertValorEvento(TipoPublicoValorDao tipoPublicoValorDao)
        {
            return _eventoRepository.InsertValorEvento(tipoPublicoValorDao);
        }

        public string SaveValoresEvento(IEnumerable<TipoPublicoValorDao> tiposPublicosValoresDao)
        {
            throw new NotImplementedException();
        }

        public Evento SetEvento()
        {
            throw new NotImplementedException();
        }

        public string Update(int id, Evento evento)
        {
            return _eventoRepository.Update(id, evento);
        }

        public string UpdateEventoDao(int id, EventoDao eventoDao)
        {
            return _eventoRepository.UpdateEventoDao(id, eventoDao);
        }

        public string UpdateValorEvento(int id, TipoPublicoValorDao tipoPublicoValorDao)
        {
            return _eventoRepository.UpdateValorEvento(id, tipoPublicoValorDao);
        }
    }
}
