using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IUnidadeFederacaoService
    {
        IEnumerable<UnidadeFederacao> GetAll();

        IEnumerable<UnidadeFederacao> GetDisponiveis(int atcId);

        IEnumerable<UnidadeFederacao> GetUtilizadas();
    }
}
