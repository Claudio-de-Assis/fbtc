using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface ITipoPublicoApplication
    {
        IEnumerable<TipoPublico> GetAll();

        TipoPublico GetTipoPublicoById(int id);
    }
}
