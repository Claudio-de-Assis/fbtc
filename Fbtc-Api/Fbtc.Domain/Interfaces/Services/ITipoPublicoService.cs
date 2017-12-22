using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface ITipoPublicoService
    {
        IEnumerable<TipoPublico> GetAll();

        TipoPublico GetTipoPublicoById(int id);

        IEnumerable<TipoPublicoValorDao> GetTipoPublicoValorByEventoId(int id);
    }
}
