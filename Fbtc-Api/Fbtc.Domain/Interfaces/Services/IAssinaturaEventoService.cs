using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IAssinaturaEventoService
    {
        IEnumerable<AssinaturaEvento> GetAll();

        AssinaturaEventoDao GetAssinaturaEventoById(int id);

        // string Save(AssinaturaEvento assinaturaEvento);

        string Insert(AssinaturaEvento assinaturaEvento);

        string Update(int id, AssinaturaEvento assinaturaEvento);

        IEnumerable<AssinaturaEventoDao> FindByFilters(int eventoId, bool? ativo);
    }
}
