using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;

namespace Fbtc.Domain.Services
{
    public class IsencaoService : IIsencaoService
    {
        private readonly IIsencaoRepository _isencaoRepository;

        public IsencaoService(IIsencaoRepository isencaoRepository)
        {
            _isencaoRepository = isencaoRepository;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Isencao> FindByFilters(string tipoIsencao, string nomeAssociado, string descricao, int ano, int eventoId)
        {
            return _isencaoRepository.FindByFilters(tipoIsencao, nomeAssociado, descricao, ano, eventoId );
        }

        public IEnumerable<IsencaoDao> FindIsencaoByFilters(string tipoIsencao, string nomeAssociado, int ano, string identificacao, string tipoEvento)
        {
            return _isencaoRepository.FindIsencaoByFilters(tipoIsencao, nomeAssociado, ano, identificacao, tipoEvento);
        }

        public IEnumerable<Isencao> GetAll(string tipoIsencao)
        {
            return _isencaoRepository.GetAll(tipoIsencao);
        }

        public Isencao GetIsencaoById(int id)
        {
            return _isencaoRepository.GetIsencaoById(id);
        }

        public string Insert(Isencao isencao)
        {
            return _isencaoRepository.Insert(isencao);
        }

        public string Update(int id, Isencao isencao)
        {
            return _isencaoRepository.Update(id, isencao);
        }
    }
}
