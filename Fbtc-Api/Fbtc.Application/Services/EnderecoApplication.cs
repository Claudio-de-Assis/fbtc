using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Application.Interfaces;

using prmToolkit.Validation;
using Fbtc.Application.Helper;

namespace Fbtc.Application.Services
{
    public class EnderecoApplication : IEnderecoApplication
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoApplication(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
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
            return _enderecoService.GetAllNomesEstados();
        }

        public IEnumerable<Endereco> GetByPessoaId(int id)
        {
            return _enderecoService.GetByPessoaId(id);
        }

        public Endereco GetEnderecoById(int id)
        {
            return _enderecoService.GetEnderecoById(id);
        }

        public IEnumerable<CidadeEnderecoCepDao> GetNomesCidadesByEstado(string nomeEstado)
        {
            return _enderecoService.GetNomesCidadesByEstado(nomeEstado);
        }

        public string Save(Endereco e)
        {

            Endereco _e = new Endereco() {
                EnderecoId = e.EnderecoId,
                PessoaId = e.PessoaId,
                Logradouro = Functions.AjustaTamanhoString(e.Logradouro, 100),
                Numero = Functions.AjustaTamanhoString(e.Numero, 10),
                Complemento = Functions.AjustaTamanhoString(e.Complemento, 100),
                Bairro = Functions.AjustaTamanhoString(e.Bairro, 100),
                Cidade = Functions.AjustaTamanhoString(e.Cidade, 100),
                Estado = Functions.AjustaTamanhoString(e.Estado, 2),
                Cep = Functions.AjustaTamanhoString(e.Cep, 10),
                TipoEndereco = e.TipoEndereco
            };

            try
            {
                if (_e.EnderecoId == 0)
                {
                    return _enderecoService.Insert(_e);
                }
                else
                {
                    return _enderecoService.Update(e.EnderecoId, _e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Endereco SetEndereco()
        {
            Endereco e = new Endereco() {
                EnderecoId = 0,
                PessoaId = 0,
                Logradouro = "",
                Numero = "",
                Complemento = "",
                Bairro = "",
                Cidade = "",
                Estado = "",
                Cep = "",
                TipoEndereco = ""
            };

            return e;
        }
    }
}
