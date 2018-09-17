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
                EnderecosPessoa = null,          
             
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

        public IEnumerable<AssociadoIsentoDao> FindIsentoByFilters(int isencaoId, string nome, string cpf, string sexo,
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

            return _associadoService.FindIsentoByFilters(isencaoId, _nome, _cpf, _sexo, atcId, _crp,
                _tipoProfissao, tipoPublicoId, _estado, _cidade, ativo);
        }

        public IEnumerable<Associado> GetAll()
        {
            return _associadoService.GetAll();
        }

        public Associado GetAssociadoById(int id)
        {
            Associado _associado = _associadoService.GetAssociadoById(id)?? this.SetAssociado();

            //Adicionando objeto Endereco caso não exista:
            if (_associado.EnderecosPessoa == null)
            {
                List<Endereco> lstEnd = new List<Endereco>();

                int i = 1;

                while (i < 3)
                {
                    lstEnd.Add(new Endereco()
                    {
                        PessoaId = 0,
                        EnderecoId = 0,
                        Cep = "",
                        Logradouro = "",
                        Numero = "",
                        Complemento = "",
                        Bairro = "",
                        Cidade = "",
                        Estado = "",
                        TipoEndereco = "",
                        OrdemEndereco = i.ToString()
                    });
                }
                _associado.EnderecosPessoa = lstEnd;
            }
            else
            {
                List<Endereco> lstEnd = new List<Endereco>();

                foreach (var end in _associado.EnderecosPessoa)
                {
                    lstEnd.Add(end);
                }

                if (lstEnd.Count < 2)
                {
                    int i = 0;
                    int pessoaId = 0;

                    foreach (var end in lstEnd)
                    {
                        i = int.Parse(end.OrdemEndereco);
                        pessoaId = end.PessoaId;

                        if (i == 1)
                        {
                            i++;
                        }
                        else
                        {
                            i--;
                        }
                    }

                    lstEnd.Add(new Endereco()
                    {
                        PessoaId = pessoaId,
                        EnderecoId = 0,
                        Cep = "",
                        Logradouro = "",
                        Numero = "",
                        Complemento = "",
                        Bairro = "",
                        Cidade = "",
                        Estado = "",
                        TipoEndereco = "",
                        OrdemEndereco = i.ToString()
                    });
                    _associado.EnderecosPessoa = lstEnd;
                }
            }

            return _associado; 
        }
        
        public Associado GetAssociadoByPessoaId(int id)
        {
            Associado _associado = _associadoService.GetAssociadoByPessoaId(id);

            //Adicionando objeto Endereco caso não exista:
            if (_associado.EnderecosPessoa == null)
            {
                List<Endereco> lstEnd = new List<Endereco>();

                int i = 1;

                while (i < 3)
                {
                    lstEnd.Add(new Endereco()
                    {
                        PessoaId = 0,
                        EnderecoId = 0,
                        Cep = "",
                        Logradouro = "",
                        Numero = "",
                        Complemento = "",
                        Bairro = "",
                        Cidade = "",
                        Estado = "",
                        TipoEndereco = "",
                        OrdemEndereco = i.ToString()
                    });
                }
                _associado.EnderecosPessoa = lstEnd;
            }
            else
            {
                List<Endereco> lstEnd = new List<Endereco>();

                foreach (var end in _associado.EnderecosPessoa)
                {
                    lstEnd.Add(end);
                }

                if (lstEnd.Count < 2)
                {
                    int i = 0;
                    int pessoaId = 0;

                    foreach (var end in lstEnd)
                    {
                        i = int.Parse(end.OrdemEndereco);
                        pessoaId = end.PessoaId;

                        if (i == 1)
                        {
                            i++;
                        }
                        else
                        {
                            i--;
                        }
                    }

                    lstEnd.Add(new Endereco()
                    {
                        PessoaId = pessoaId,
                        EnderecoId = 0,
                        Cep = "",
                        Logradouro = "",
                        Numero = "",
                        Complemento = "",
                        Bairro = "",
                        Cidade = "",
                        Estado = "",
                        TipoEndereco = "",
                        OrdemEndereco = i.ToString()
                    });
                    _associado.EnderecosPessoa = lstEnd;
                }
            }

            return _associado;
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
                ATCId = a.ATCId == 0 ? null : a.ATCId,
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
                PerfilId = a.PerfilId
            };

            if (a.EnderecosPessoa != null)
            {
                List<Endereco> lst = new List<Endereco>();

                foreach (var e in a.EnderecosPessoa)
                {
                    Endereco _endereco = new Endereco()
                    {
                        PessoaId = e.PessoaId,
                        EnderecoId = e.EnderecoId,
                        Cep = Functions.AjustaTamanhoString(e.Cep, 10),
                        Logradouro = Functions.AjustaTamanhoString(e.Logradouro, 100),
                        Numero = Functions.AjustaTamanhoString(e.Numero, 10),
                        Complemento = Functions.AjustaTamanhoString(e.Complemento, 100),
                        Bairro = Functions.AjustaTamanhoString(e.Bairro, 100),
                        Cidade = Functions.AjustaTamanhoString(e.Cidade, 100),
                        Estado = Functions.AjustaTamanhoString(e.Estado, 2),
                        TipoEndereco = Functions.AjustaTamanhoString(e.TipoEndereco, 1),
                        OrdemEndereco = Functions.AjustaTamanhoString(e.OrdemEndereco, 1)
                    };
                    lst.Add(e);
                }
                _a.EnderecosPessoa = lst;
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

        public string SaveIsento(AssociadoIsentoDao a)
        {
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                RaiseException.IfTrue(a.IsencaoId == 0, "IsencaoId não informada"),
                RaiseException.IfNull(a.IsencaoId, "IsencaoId está nula"),

                RaiseException.IfTrue(a.AssociadoId == 0, "AssociadoId não informado"),
                RaiseException.IfNull(a.AssociadoId, "AssociadoId está nulo")
            );

            try
            {
                if (a.AssociadoIsentoId == 0)
                {
                    return _associadoService.InsertIsento(a);
                }
                else
                {
                    return _associadoService.DeleteIsentoByAssociadoIsentoId(a.AssociadoIsentoId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNomeFotoByPessoaId(int id)
        {
            return _associadoService.GetNomeFotoByPessoaId(id);
        }

        public string RessetPasswordById(int id)
        {
            return _associadoService.RessetPasswordById(id);
        }

        public string ValidaEMail(int associadoId, string eMail)
        {
            return _associadoService.ValidaEMail(associadoId, eMail);
        }
    }
}
