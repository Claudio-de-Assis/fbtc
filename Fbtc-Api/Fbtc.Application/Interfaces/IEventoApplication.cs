using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IEventoApplication
    {
        IEnumerable<Evento> GetAll();

        Evento GetEventoById(int id);

        EventoDao GetEventoDaoById(int id);

        Evento GetEventoByRecebimentoId(int id);

        Evento SetEvento();

        string DeleteById(int id);

        string Save(Evento evento);

        string SaveEventoDao(EventoDao eventoDao);

        string SaveValoresEvento(IEnumerable<TipoPublicoValorDao> tiposPublicosValoresDao);

        IEnumerable<Evento> FindByFilters(string titulo, int ano,
            string tipoEvento);

        IEnumerable<EventoDao> FindEventoDaoByFilters(string titulo, int ano,
            string tipoEvento);

        string GetNomeFotoByEventoId(int id);
    }
}
