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

        public string DeleteIsentoByAssociadoIsentoId(int AssociadoIsentoId)
        {
            return _associadoRepository.DeleteIsentoByAssociadoIsentoId(AssociadoIsentoId);
        }

        public IEnumerable<Associado> FindByFilters(string nome, string cpf, string sexo, int atcId, 
            string crp, string tipoProfissao, int tipoPublicoId, string estado, string cidade, bool? ativo)
        {
            return _associadoRepository.FindByFilters(nome, cpf, sexo, atcId, crp, 
                tipoProfissao, tipoPublicoId, estado, cidade, ativo);
        }

        public IEnumerable<AssociadoIsentoDao> FindIsentoByFilters(int isencaoId, string nome, string cpf, string sexo, int atcId,
            string crp, string tipoProfissao, int tipoPublicoId, string estado, string cidade, bool? ativo)
        {
            return _associadoRepository.FindIsentoByFilters(isencaoId, nome, cpf, sexo, atcId, crp,
                tipoProfissao, tipoPublicoId, estado, cidade, ativo);
        }

        public IEnumerable<Associado> GetAll()
        {
            return _associadoRepository.GetAll();
        }

        public Associado GetAssociadoById(int id)
        {
            return _associadoRepository.GetAssociadoById(id);
        }

        public Associado GetAssociadoByPessoaId(int id)
        {
            return _associadoRepository.GetAssociadoByPessoaId(id);
        }
        
        public string GetNomeFotoByPessoaId(int id)
        {
            return _associadoRepository.GetNomeFotoByPessoaId(id);
        }

        public string Insert(Associado associado)
        {
            return _associadoRepository.Insert(associado);
        }

        public string InsertIsento(AssociadoIsentoDao associadoIsentoDao)
        {
            return _associadoRepository.InsertIsento(associadoIsentoDao);
        }

        public string RessetPasswordById(int id)
        {
            return _associadoRepository.RessetPasswordById(id);
        }

        public string Update(int id, Associado associado)
        {
            return _associadoRepository.Update(id, associado);
        }

        public string ValidaEMail(int associadoId, string eMail)
        {
            return _associadoRepository.ValidaEMail(associadoId, eMail);
        }
    }
}
