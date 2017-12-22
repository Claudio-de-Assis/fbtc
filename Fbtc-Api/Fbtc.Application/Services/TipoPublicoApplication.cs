using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Application.Interfaces;

using prmToolkit.Validation;
using Fbtc.Application.Helper;

namespace Fbtc.Application.Services
{
    public class TipoPublicoApplication : ITipoPublicoApplication
    {
        private readonly ITipoPublicoService _tipoPublicoService;

        public TipoPublicoApplication(ITipoPublicoService tipoPublicoService)
        {
            _tipoPublicoService = tipoPublicoService;
        }

        public IEnumerable<TipoPublico> GetAll()
        {
            return _tipoPublicoService.GetAll();
        }

        public TipoPublico GetTipoPublicoById(int id)
        {
            return _tipoPublicoService.GetTipoPublicoById(id);
        }

        public IEnumerable<TipoPublicoValorDao> GetTipoPublicoValorByEventoId(int id)
        {
            return _tipoPublicoService.GetTipoPublicoValorByEventoId(id);
        }
    }
}
