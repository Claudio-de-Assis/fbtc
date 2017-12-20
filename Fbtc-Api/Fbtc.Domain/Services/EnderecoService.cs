using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;

namespace Fbtc.Domain.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository; 
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public string DeleteByPessoaId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EstadoEnderecoCepDao> GetAllNomesEstados()
        {
            return _enderecoRepository.GetAllNomesEstados();
        }

        public IEnumerable<Endereco> GetByPessoaId(int id)
        {
            return _enderecoRepository.GetByPessoaId(id);
        }

        public Endereco GetEnderecoById(int id)
        {
            return _enderecoRepository.GetEnderecoById(id);
        }

        public IEnumerable<CidadeEnderecoCepDao> GetNomesCidadesByEstado(string nomeEstado)
        {
            return _enderecoRepository.GetNomesCidadesByEstado(nomeEstado);
        }

        public string Insert(Endereco endereco)
        {
            return _enderecoRepository.Insert(endereco);
        }

        public string Update(int id, Endereco endereco)
        {
            return _enderecoRepository.Update(id, endereco);
        }

    }
}
