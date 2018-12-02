using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IAssinaturaEventoApplication
    {
        IEnumerable<AssinaturaEvento> GetAll();

        AssinaturaEventoDao GetAssinaturaEventoById(int id);

        string Save(AssinaturaEventoDao assinaturaEventoDao);

        IEnumerable<AssinaturaEventoDao> FindByFilters(int eventoId, bool? ativo);

    }
}
