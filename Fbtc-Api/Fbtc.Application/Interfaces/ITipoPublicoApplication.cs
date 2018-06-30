using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface ITipoPublicoApplication
    {
        IEnumerable<TipoPublico> GetAll(bool? isAtivo);

        IEnumerable<TipoPublico> GetByTipoAssociacao(bool? associado);

        TipoPublico GetTipoPublicoById(int id);

        IEnumerable<TipoPublicoValorDao> GetTipoPublicoValorByEventoId(int id);
    }
}
