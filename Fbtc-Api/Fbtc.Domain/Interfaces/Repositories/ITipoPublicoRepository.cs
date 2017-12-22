using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface ITipoPublicoRepository
    {
        IEnumerable<TipoPublico> GetAll();

        TipoPublico GetTipoPublicoById(int id);

        IEnumerable<TipoPublicoValorDao> GetTipoPublicoValorByEventoId(int id);
    }
}
