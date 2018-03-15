using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Fbtc.Application.Services
{
    public class UnidadeFederacaoApplication : IUnidadeFederacaoApplication
    {
        private readonly IUnidadeFederacaoService _unidadeFederacaoService;

        public UnidadeFederacaoApplication(IUnidadeFederacaoService unidadeFederacaoService)
        {
            _unidadeFederacaoService = unidadeFederacaoService;
        }

        public IEnumerable<UnidadeFederacao> GetAll()
        {
            return _unidadeFederacaoService.GetAll();
        }

        public IEnumerable<UnidadeFederacao> GetDisponiveis(int atcId)
        {
            return _unidadeFederacaoService.GetDisponiveis(atcId);
        }

        public IEnumerable<UnidadeFederacao> GetUtilizadas()
        {
            return _unidadeFederacaoService.GetUtilizadas();
        }
    }
}
