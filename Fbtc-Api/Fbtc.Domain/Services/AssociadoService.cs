using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;

namespace Fbtc.Domain.Services
{
    public class AssociadoService : IAssociadoService
    {
        private readonly IAssociadoRepository _associadoRepository;

        public AssociadoService(IAssociadoRepository associadoRepository)
        {
            _associadoRepository = associadoRepository;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Associado> FindByFilters(string nome, string cpf, string sexo, int atcId, string crp, string tipoProfissao)
        {
            return _associadoRepository.FindByFilters(nome, cpf, sexo, atcId, crp, tipoProfissao);
        }

        public IEnumerable<Associado> GetAll()
        {
            return _associadoRepository.GetAll();
        }

        public Associado GetAssociadoById(int id)
        {
            return _associadoRepository.GetAssociadoById(id);
        }

        public string Insert(Associado associado)
        {
            return _associadoRepository.Insert(associado);
        }

        public string Update(int id, Associado associado)
        {
            return _associadoRepository.Update(id, associado);
        }
    }
}
