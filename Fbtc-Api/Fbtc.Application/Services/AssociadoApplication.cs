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

            Associado _a = new Associado();

            _a.PessoaId = 0;
            _a.Nome = "";
            _a.EMail = "";
            _a.NomeFoto = "";
            _a.Sexo = "";
            // _a.DtNascimento;
            _a.NrCelular = "";
            _a.PasswordHash = "";
            _a.Ativo = true;
            _a.PessoaId = 0;
            _a.ATCId = 0;
            _a.TipoPublicoId = 0;
            _a.Cpf = "";
            _a.Rg = "";
            _a.NrMatricula = "";
            _a.Crp = "";
            _a.Crm = "";
            _a.NomeInstFormacao = "";
            _a.Certificado = false;
            // _a.DtCertificacao;
            _a.DivulgarContato = false;
            _a.TipoFormaContato = "";
            _a.IntegraDiretoria = false;
            _a.IntegraConfi = false;
            _a.NrTelDivulgacao = "";
            _a.ComprovanteAfiliacaoAtc = "";
            _a.TipoProfissao = "";
            _a.TipoTitulacao = "";

            return _a;
        }

        public IEnumerable<Associado> FindByFilters(string nome, string cpf, string sexo, int atcId, string crp, string tipoProfissao)
        {
            string _nome, _cpf, _sexo, _crp, _tipoProfissao;

            _nome = nome == "0" ? "" : nome;
            _cpf = cpf == "0" ? "" : cpf;
            _sexo = sexo == "0" ? "" : sexo;
            _crp = crp == "0" ? "" : crp;
            _tipoProfissao = tipoProfissao == "0" ? "" : tipoProfissao;

            return _associadoService.FindByFilters(_nome, _cpf, _sexo, atcId, _crp, _tipoProfissao);
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

            Associado _a = new Associado();

            _a.PessoaId = a.PessoaId;
            _a.Nome = Functions.AjustaTamanhoString(a.Nome, 100);
            _a.EMail = Functions.AjustaTamanhoString(a.EMail, 100);
            _a.NomeFoto = Functions.AjustaTamanhoString(a.NomeFoto, 32);
            _a.Sexo = a.Sexo;
            _a.DtNascimento = a.DtNascimento;
            _a.NrCelular = Functions.AjustaTamanhoString(a.NrCelular, 15);
            _a.PasswordHash = Functions.AjustaTamanhoString(a.PasswordHash, 100);
            _a.Ativo = a.Ativo;
            _a.PessoaId = a.PessoaId;
            _a.ATCId = a.ATCId;
            _a.TipoPublicoId = a.TipoPublicoId;
            _a.Cpf = Functions.AjustaTamanhoString(a.Cpf, 15);
            _a.Rg = Functions.AjustaTamanhoString(a.Rg, 15);
            _a.NrMatricula = Functions.AjustaTamanhoString(a.NrMatricula, 15);
            _a.Crp = Functions.AjustaTamanhoString(a.Crp, 15);
            _a.Crm = Functions.AjustaTamanhoString(a.Crm, 15);
            _a.NomeInstFormacao = Functions.AjustaTamanhoString(a.NomeInstFormacao, 100);
            _a.Certificado = a.Certificado;
            _a.DtCertificacao = a.DtCertificacao;
            _a.DivulgarContato = a.DivulgarContato;
            _a.TipoFormaContato = a.TipoFormaContato;
            _a.IntegraDiretoria = a.IntegraDiretoria;
            _a.IntegraConfi = a.IntegraConfi;
            _a.NrTelDivulgacao = Functions.AjustaTamanhoString(a.NrTelDivulgacao, 15);
            _a.ComprovanteAfiliacaoAtc = Functions.AjustaTamanhoString(a.ComprovanteAfiliacaoAtc, 100);
            _a.TipoProfissao = a.TipoProfissao;
            _a.TipoTitulacao = a.TipoTitulacao;
            
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

        public string Insert(Associado associado)
        {
            return _associadoService.Insert(associado);
        }

        public string Update(int id, Associado associado)
        {
            return _associadoService.Update(id, associado);
        }
    }
}
