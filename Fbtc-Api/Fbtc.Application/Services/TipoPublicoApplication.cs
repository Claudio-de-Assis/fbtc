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

        public IEnumerable<TipoPublico> GetAll(bool? isAtivo)
        {
            return _tipoPublicoService.GetAll(isAtivo);
        }

        public IEnumerable<TipoPublico> GetByTipoAssociacao(bool? associado)
        {
            if (associado == null) throw new Exception("Tipo de Associação não informada!");

            return _tipoPublicoService.GetByTipoAssociacao(associado == true ? true : false);
        }

        public TipoPublico GetTipoPublicoById(int id)
        {
            return _tipoPublicoService.GetTipoPublicoById(id);
        }

        public IEnumerable<TipoPublicoValorDao> GetTipoPublicoValorByEventoId(int id)
        {
            return _tipoPublicoService.GetTipoPublicoValorByEventoId(id);
        }

        public IEnumerable<TipoPublicoValorAnuidadeDao> GetTipoPublicoValorByAnuidadeId(int id)
        {
            return _tipoPublicoService.GetTipoPublicoValorByAnuidadeId(id);
        }
    }
}
