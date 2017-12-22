using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IEventoRepository
    {
        IEnumerable<Evento> GetAll();

        Evento GetEventoById(int id);

        Evento GetEventoByRecebimentoId(int id);

        string DeleteById(int id);

        string Insert(Evento evento);

        string InsertValorEvento(TipoPublicoValorDao tipoPublicoValorDao);

        string Update(int id, Evento evento);

        string UpdateValorEvento(int id, TipoPublicoValorDao tipoPublicoValorDao);

        IEnumerable<Evento> FindByFilters(string titulo, int ano,
            string tipoEvento);
    }
}
