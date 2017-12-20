using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Application.Interfaces;

using prmToolkit.Validation;
using Fbtc.Application.Helper;

namespace Fbtc.Application.Services
{
    public class AssociadoApplication : IAssociadoApplication
    {
        private readonly IAssociadoService _associadoService;

        public AssociadoApplication(IAssociadoService associadoService)
        {
            _associadoService = associadoService;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Associado SetAssociado() {

            Associado _a = new Associado() {
                PessoaId = 0,
                Nome = "",
                EMail = "",
                NomeFoto = "",
                Sexo = "",
                DtNascimento = null,
                NrCelular = "",
                PasswordHash = "",
                Ativo = true,
                DtCadastro = null,
                ATCId = 0,
                TipoPublicoId = 0,
                Cpf = "",
                Rg = "",
                NrMatricula = "",
                Crp = "",
                Crm = "",
                NomeInstFormacao = "",
                Certificado = false,
                DtCertificacao = null,
                DivulgarContato = false,
                TipoFormaContato = "",
                IntegraDiretoria = false,
                IntegraConfi = false,
                NrTelDivulgacao = "",
                ComprovanteAfiliacaoAtc = "",
                TipoProfissao = "",
                TipoTitulacao = "",
                EnderecoPessoa = new Endereco()
                {
                    PessoaId = 0,
                    EnderecoId = 0,
                    Logradouro = "",
                    Numero = "",
                    Complemento = "",
                    Bairro = "",
                    Cidade = "",
                    Estado = "",
                    Cep = "",
                    TipoEndereco = ""
                }
            };
            return _a;
        }

        public IEnumerable<Associado> FindByFilters(string nome, string cpf, string sexo, 
            int atcId, string crp, string tipoProfissao, int tipoPublicoId, string estado, string cidade, bool? ativo)
        {
            string _nome, _cpf, _sexo, _crp, _tipoProfissao, _estado, _cidade;

            _nome = nome == "0" ? "" : nome;
            _cpf = cpf == "0" ? "" : cpf;
            _sexo = sexo == "0" ? "" : sexo;
            _crp = crp == "0" ? "" : crp;
            _tipoProfissao = tipoProfissao == "0" ? "" : tipoProfissao;
            _estado = estado == "0" ? "" : estado;
            _cidade = cidade == "0" ? "" : cidade;

            if (_nome.IndexOf("%20") > 0)
                _nome = _nome.Replace("%20", " ");
            
            if (_cidade.IndexOf("%20") > 0)
                _cidade = _cidade.Replace("%20", " ");
            
            return _associadoService.FindByFilters(_nome, _cpf, _sexo, atcId, _crp, 
                _tipoProfissao, tipoPublicoId, _estado, _cidade, ativo);
        }

        public IEnumerable<Associado> GetAll()
        {
            return _associadoService.GetAll();
        }

        public Associado GetAssociadoById(int id)
        {
            return _associadoService.GetAssociadoById(id);
        }

        public string Save(Associado a)
        {
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                RaiseException.IfNullOrEmpty(a.Nome, "Nome do Associado não informado"),
                RaiseException.IfNotEmail(a.EMail, "E-Mail inválido"),
                RaiseException.IfNullOrEmpty(a.NrCelular, "NrCelular não informado"),
                RaiseException.IfEqualsZero(a.TipoPublicoId, "Tipo de Publico não informado")
            );

            Associado _a = new Associado() {
                PessoaId = a.PessoaId,
                Nome = Functions.AjustaTamanhoString(a.Nome, 100),
                EMail = Functions.AjustaTamanhoString(a.EMail, 100),
                NomeFoto = Functions.AjustaTamanhoString(a.NomeFoto, 32),
                Sexo = a.Sexo,
                DtNascimento = a.DtNascimento,
                NrCelular = Functions.AjustaTamanhoString(a.NrCelular, 15),
                PasswordHash = Functions.AjustaTamanhoString(a.PasswordHash, 100),
                Ativo = a.Ativo,
                ATCId = a.ATCId,
                TipoPublicoId = a.TipoPublicoId,
                Cpf = Functions.AjustaTamanhoString(a.Cpf, 15),
                Rg = Functions.AjustaTamanhoString(a.Rg, 15),
                NrMatricula = Functions.AjustaTamanhoString(a.NrMatricula, 15),
                Crp = Functions.AjustaTamanhoString(a.Crp, 15),
                Crm = Functions.AjustaTamanhoString(a.Crm, 15),
                NomeInstFormacao = Functions.AjustaTamanhoString(a.NomeInstFormacao, 100),
                Certificado = a.Certificado,
                DtCertificacao = a.DtCertificacao,
                DivulgarContato = a.DivulgarContato,
                TipoFormaContato = a.TipoFormaContato,
                IntegraDiretoria = a.IntegraDiretoria,
                IntegraConfi = a.IntegraConfi,
                NrTelDivulgacao = Functions.AjustaTamanhoString(a.NrTelDivulgacao, 15),
                ComprovanteAfiliacaoAtc = Functions.AjustaTamanhoString(a.ComprovanteAfiliacaoAtc, 100),
                TipoProfissao = a.TipoProfissao,
                TipoTitulacao = a.TipoTitulacao,
            };

            if (!string.IsNullOrWhiteSpace(a.EnderecoPessoa.Cep))
            {
                _a.EnderecoPessoa = new Endereco()
                {
                    PessoaId = a.EnderecoPessoa.PessoaId,
                    EnderecoId = a.EnderecoPessoa.EnderecoId,
                    Cep = Functions.AjustaTamanhoString(a.EnderecoPessoa.Cep, 10),
                    Logradouro = Functions.AjustaTamanhoString(a.EnderecoPessoa.Logradouro, 100),
                    Numero = Functions.AjustaTamanhoString(a.EnderecoPessoa.Numero, 10),
                    Complemento = Functions.AjustaTamanhoString(a.EnderecoPessoa.Complemento, 100),
                    Bairro = Functions.AjustaTamanhoString(a.EnderecoPessoa.Bairro, 100),
                    Cidade = Functions.AjustaTamanhoString(a.EnderecoPessoa.Cidade, 100),
                    Estado = Functions.AjustaTamanhoString(a.EnderecoPessoa.Estado, 2),
                    TipoEndereco = Functions.AjustaTamanhoString(a.EnderecoPessoa.TipoEndereco, 1)
                };
            }

            try
            {
                if (_a.PessoaId == 0)
                {
                    return _associadoService.Insert(_a);
                }
                else
                {
                    return _associadoService.Update(a.PessoaId, _a);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
