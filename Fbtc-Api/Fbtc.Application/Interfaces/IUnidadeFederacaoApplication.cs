using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IUnidadeFederacaoApplication
    {
        IEnumerable<UnidadeFederacao> GetAll();

        IEnumerable<UnidadeFederacao> GetDisponiveis(int atcId);

        IEnumerable<UnidadeFederacao> GetUtilizadas();
    }
}
