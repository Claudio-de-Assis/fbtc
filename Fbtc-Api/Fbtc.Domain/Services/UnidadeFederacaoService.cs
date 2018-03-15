using System.Collections.Generic;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;

namespace Fbtc.Domain.Services
{
    public class UnidadeFederacaoService : IUnidadeFederacaoService
    {
        private readonly IUnidadeFederacaoRepository _unidadeFederacaoRepository;

        public UnidadeFederacaoService(IUnidadeFederacaoRepository unidadeFederacaoRepository)
        {
            _unidadeFederacaoRepository = unidadeFederacaoRepository;
        }

        public IEnumerable<UnidadeFederacao> GetAll()
        {
            return _unidadeFederacaoRepository.GetAll();   
        }

        public IEnumerable<UnidadeFederacao> GetDisponiveis(int atcId)
        {
            return _unidadeFederacaoRepository.GetDisponiveis(atcId);
        }

        public IEnumerable<UnidadeFederacao> GetUtilizadas()
        {
            return _unidadeFederacaoRepository.GetUtilizadas();
        }
    }
}
