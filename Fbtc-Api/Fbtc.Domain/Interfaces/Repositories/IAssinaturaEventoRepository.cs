using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IAssinaturaEventoRepository
    {
        IEnumerable<AssinaturaEvento> GetAll();

        AssinaturaEventoDao GetAssinaturaEventoById(int id);

        string Insert(AssinaturaEvento assinaturaEvento);

        string Update(int id, AssinaturaEvento assinaturaEvento);

        IEnumerable<AssinaturaEventoDao> FindByFilters(int eventoId, bool? ativo);
    }
}
