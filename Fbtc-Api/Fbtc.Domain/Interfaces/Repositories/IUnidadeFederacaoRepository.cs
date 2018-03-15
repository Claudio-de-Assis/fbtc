using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IUnidadeFederacaoRepository
    {
        IEnumerable<UnidadeFederacao> GetAll();

        IEnumerable<UnidadeFederacao> GetDisponiveis(int atcId);

        IEnumerable<UnidadeFederacao> GetUtilizadas();
    }
}
