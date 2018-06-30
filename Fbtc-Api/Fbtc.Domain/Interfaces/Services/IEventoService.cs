using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IEventoService
    {
        IEnumerable<Evento> GetAll();

        Evento GetEventoById(int id);

        EventoDao GetEventoDaoById(int id);

        Evento GetEventoByRecebimentoId(int id);

        Evento SetEvento();

        string DeleteById(int id);

        string Insert(Evento evento);

        string Update(int id, Evento evento);

        string InsertEventoDao(EventoDao eventoDao);

        string UpdateEventoDao(int id, EventoDao eventoDao);

        string SaveValoresEvento(IEnumerable<TipoPublicoValorDao> tiposPublicosValoresDao);

        IEnumerable<Evento> FindByFilters(string titulo, int ano,
            string tipoEvento);

        IEnumerable<EventoDao> FindEventoDaoByFilters(string titulo, int ano,
            string tipoEvento);

        string GetNomeFotoByEventoId(int id);
    }
}
