﻿using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface ITipoPublicoService
    {
        IEnumerable<TipoPublico> GetAll(bool? isAtivo);

        IEnumerable<TipoPublico> GetByTipoAssociacao(bool associado);

        TipoPublico GetTipoPublicoById(int id);

        IEnumerable<TipoPublicoValorDao> GetTipoPublicoValorByEventoId(int id);

        int GetIdTipoPublicoByCodigo(string codigo);

    }
}
