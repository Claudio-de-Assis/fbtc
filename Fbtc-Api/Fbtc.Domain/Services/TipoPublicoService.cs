using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;

namespace Fbtc.Domain.Services
{
    public class TipoPublicoService : ITipoPublicoService
    {
        private readonly ITipoPublicoRepository _tipoPublicoRepository;

        public TipoPublicoService(ITipoPublicoRepository tipoPublicoRepository)
        {
            _tipoPublicoRepository = tipoPublicoRepository;
        }

        public IEnumerable<TipoPublico> GetAll()
        {
            return _tipoPublicoRepository.GetAll();
        }

        public TipoPublico GetTipoPublicoById(int id)
        {
            return _tipoPublicoRepository.GetTipoPublicoById(id);
        }

        public IEnumerable<TipoPublicoValorDao> GetTipoPublicoValorByEventoId(int id)
        {
            return _tipoPublicoRepository.GetTipoPublicoValorByEventoId(id);
        }
    }
}
