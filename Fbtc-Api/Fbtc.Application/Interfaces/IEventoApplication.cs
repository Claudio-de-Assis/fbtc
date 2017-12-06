using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IEventoApplication
    {
        IEnumerable<Evento> GetAll();

        Evento GetEventoById(int id);

        Evento SetEvento();

        string DeleteById(int id);

        string Save(Evento evento);

        IEnumerable<Evento> FindByFilters(string titulo, int ano,
            string tipoEvento);
    }
}
