using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Fbtc.Domain.Services
{
    public class AssinaturaAnuidadeService : IAssinaturaAnuidadeService
    {
        private readonly IAssinaturaAnuidadeRepository _assinaturaAnuidadeRepository;

        public AssinaturaAnuidadeService(IAssinaturaAnuidadeRepository assinaturaAnuidadeRepository)
        {
            _assinaturaAnuidadeRepository = assinaturaAnuidadeRepository;
        }

        public IEnumerable<AssinaturaAnuidadeDao> FindAssinaturaPendenteByFilters(int anuidadeId, string nome, string cpf, bool? ativo)
        {
            return _assinaturaAnuidadeRepository.FindAssinaturaPendenteByFilters(anuidadeId, nome, cpf, ativo);
        }

        public IEnumerable<AssinaturaAnuidadeDao> FindByFilters(int anuidadeId, string nome, string cpf, bool? ativo)
        {
            return _assinaturaAnuidadeRepository.FindByFilters(anuidadeId, nome, cpf, ativo);
        }

        public IEnumerable<AssinaturaAnuidadeDao> FindByPessoaId(int pessoaId)
        {
            return _assinaturaAnuidadeRepository.FindByPessoaId(pessoaId);
        }

        public IEnumerable<AssinaturaAnuidade> GetAll()
        {
            return _assinaturaAnuidadeRepository.GetAll();
        }

        public AssinaturaAnuidadeDao GetAssinaturaAnuidadeById(int id)
        {
            return _assinaturaAnuidadeRepository.GetAssinaturaAnuidadeById(id);
        }

        public string Insert(AssinaturaAnuidade assinaturaAnuidade)
        {
            return _assinaturaAnuidadeRepository.Insert(assinaturaAnuidade);
        }

        public string Update(int id, AssinaturaAnuidade assinaturaAnuidade)
        {
            return _assinaturaAnuidadeRepository.Update(id, assinaturaAnuidade);
        }
    }
}
