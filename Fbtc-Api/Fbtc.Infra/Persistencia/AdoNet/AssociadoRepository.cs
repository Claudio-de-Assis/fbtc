using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

using Fbtc.Infra.Helpers;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;

using prmToolkit.AccessMultipleDatabaseWithAdoNet;
using prmToolkit.AccessMultipleDatabaseWithAdoNet.Enumerators;


namespace Fbtc.Infra.Persistencia.AdoNet
{
    public class AssociadoRepository : AbstractRepository, IAssociadoRepository
    {
        private string query;
        private readonly string strConnSql;

        public AssociadoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Associado> FindByFilters(string nome, string cpf,  
            string sexo, int atcId, string crp, string tipoProfissao, int tipoPublicoId, string estado, string cidade, bool? ativo)
        {
            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento, P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.IntegraDiretoria, A.IntegraConfi, A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao 
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId ";

            if (!string.IsNullOrEmpty(estado) || !string.IsNullOrEmpty(cidade))
                query = query + "INNER JOIN dbo.AD_Endereco E ON P.PessoaId = E.PessoaId ";

            query = query + "WHERE P.PessoaId > 0 ";

            if (!string.IsNullOrEmpty(nome))
                query = query + $" AND P.Nome Like '%{nome}%' ";

            if (!string.IsNullOrEmpty(cpf))
                query = query + $" AND P.CPF = '{cpf}' ";

            if (!string.IsNullOrEmpty(sexo))
                query = query + $" AND P.Sexo = '{sexo}' ";

            if (atcId != 0)
                query = query + $" AND A.AtcId = {atcId} ";

            if (!string.IsNullOrEmpty(crp))
                query = query + $" AND A.CRP = '{crp}' ";

            if (!string.IsNullOrEmpty(tipoProfissao))
                query = query + $" AND A.TipoProfissao = '{tipoProfissao}' ";

            if (tipoPublicoId != 0)
                query = query + $" AND A.TipoPublicoId = {tipoPublicoId} ";

            if (!string.IsNullOrEmpty(estado))
                query = query + $" AND E.Estado = '{estado}' ";

            if (!string.IsNullOrEmpty(cidade))
                query = query + $" AND E.Cidade = '{cidade}' ";

            if (ativo != null)
                query = query + $" AND P.Ativo = '{ativo}' ";

            query = query + " ORDER BY P.Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Associado> _collection = GetCollection<Associado>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<Associado> GetAll()
        {
            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento, P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.IntegraDiretoria, A.IntegraConfi, A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao 
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId 
                    ORDER BY P.Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Associado> _collection = GetCollection<Associado>(cmd)?.ToList();

            return _collection;
        }

        public Associado GetAssociadoById(int id)
        {
            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.IntegraDiretoria, A.IntegraConfi, A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao 
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId 
                    WHERE AssociadoId = " + id +"";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            Associado associado = GetCollection<Associado>(cmd)?.First();

            // Obtendo um endereco:
            if (associado != null)
            {
                EnderecoRepository _endRep = new EnderecoRepository();

                var ends = _endRep.GetByPessoaId(associado.PessoaId);

                if (ends != null && ends.Count() > 0)
                {
                    associado.EnderecoPessoa = ends.First<Endereco>();
                }
                else
                {
                    associado.EnderecoPessoa = new Endereco() { EnderecoId = 0, PessoaId = associado.PessoaId,
                        Logradouro = "", Numero = "", Complemento = "", Bairro = "",
                        Cidade = "", Estado = "", Cep = "", TipoEndereco = ""
                    };
                }
            }

            return associado;
        }

        public string Insert(Associado associado)
        {
            bool _resultado = false;
            string _msg = "";
            string _msgEnd = "";
            Int32 id = 0;

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("IncluirAssociado");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Inserindo os dados na tabela PESSOA:
                    string _dtNasc = associado.DtNascimento != null ? ", DtNascimento " : "";
                    string _paramDtNasc = associado.DtNascimento != null ? ", @DtNascimento " : "";

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Pessoa (Nome, EMail, CPF, RG, NomeFoto, " +
                        "   Sexo, NrCelular, PasswordHash, " +
                        "   DtCadastro "+ _dtNasc + ") " +
                        "VALUES(@Nome, @EMail, @CPF, @RG, @NomeFoto, " +
                        "   @Sexo, @NrCelular, @PasswordHash, " +
                        "   @DtCadastro "+ _paramDtNasc + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Nome", associado.Nome);
                    command.Parameters.AddWithValue("EMail", associado.EMail);
                    command.Parameters.AddWithValue("CPF", associado.Cpf);
                    command.Parameters.AddWithValue("RG", associado.Rg);
                    command.Parameters.AddWithValue("NomeFoto", associado.NomeFoto);
                    command.Parameters.AddWithValue("Sexo", associado.Sexo);
                    command.Parameters.AddWithValue("NrCelular", associado.NrCelular);
                    command.Parameters.AddWithValue("PasswordHash", associado.PasswordHash);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);

                    if(_dtNasc != "")
                        command.Parameters.AddWithValue("DtNascimento", associado.DtNascimento);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;
                    
                    // Inserindo os dados na tabela ASSOCIADO:
                    string _dtCert = associado.DtCertificacao != null ? ", DtCertificacao " : "";
                    string _paramDtCert = associado.DtCertificacao != null ? ", @DtCertificacao " : ""; 

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Associado (PessoaId, AtcId, TipoPublicoId, " +
                        "   NrMatricula, CRP, CRM, NomeInstFormacao, Certificado, " +
                        "   DivulgarContato, TipoFormaContato, IntegraDiretoria, IntegraConfi, " +
                        "   NrTelDivulgacao, ComprovanteAfiliacaoAtc, TipoProfissao, TipoTitulacao " + _dtCert + ") " +
                        "VALUES (@PessoaId, @AtcId, @TipoPublicoId, " +
                        "   @NrMatricula, @CRP, @CRM, @NomeInstFormacao, @Certificado, " +
                        "   @DivulgarContato, @TipoFormaContato, @IntegraDiretoria, @IntegraConfi, " +
                        "   @NrTelDivulgacao, @ComprovanteAfiliacaoAtc, @TipoProfissao, @TipoTitulacao "+ _paramDtCert +") ";

                    command.Parameters.AddWithValue("PessoaId", id);
                    command.Parameters.AddWithValue("AtcId", associado.ATCId);
                    command.Parameters.AddWithValue("TipoPublicoId", associado.TipoPublicoId);
                    command.Parameters.AddWithValue("NrMatricula", associado.NrMatricula);
                    command.Parameters.AddWithValue("CRP", associado.Crp);
                    command.Parameters.AddWithValue("CRM", associado.Crm);
                    command.Parameters.AddWithValue("NomeInstFormacao", associado.NomeInstFormacao);
                    command.Parameters.AddWithValue("Certificado", associado.Certificado);
                    command.Parameters.AddWithValue("DivulgarContato", associado.DivulgarContato);
                    command.Parameters.AddWithValue("TipoFormaContato", associado.TipoFormaContato);
                    command.Parameters.AddWithValue("IntegraDiretoria", associado.IntegraDiretoria);
                    command.Parameters.AddWithValue("IntegraConfi", associado.IntegraConfi);
                    command.Parameters.AddWithValue("NrTelDivulgacao", associado.NrTelDivulgacao);
                    command.Parameters.AddWithValue("ComprovanteAfiliacaoAtc", associado.ComprovanteAfiliacaoAtc);
                    command.Parameters.AddWithValue("TipoProfissao", associado.TipoProfissao);
                    command.Parameters.AddWithValue("TipoTitulacao", associado.TipoTitulacao);

                    if (_dtCert!="")
                        command.Parameters.AddWithValue("DtCertificacao", associado.DtCertificacao);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Inclusão realiada com sucesso" : "Inclusão Não realiada com sucesso";
                    
                    transaction.Commit();

                    // Inserindo endereco:
                    if (associado.EnderecoPessoa != null)
                    {
                        if (!string.IsNullOrWhiteSpace(associado.EnderecoPessoa.Cep))
                        {
                            EnderecoRepository _endRep = new EnderecoRepository();

                            associado.EnderecoPessoa.PessoaId = id;

                            _msgEnd = _endRep.Insert(associado.EnderecoPessoa);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }
                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                connection.Close();
            }
            return _msg;
        }

        public string Update(int id, Associado associado)
        {
            bool _resultado = false;
            string _msg = "";
            string _msgEnd = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarAssociado");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Atualizando os dados na tabela PESSOA:
                    string _dtNasc = associado.DtNascimento != null ? ", DtNascimento = @DtNascimento " : "";

                    command.CommandText = "" +
                        "UPDATE dbo.AD_Pessoa " +
                        "SET Nome = @nome, EMail = @EMail, NomeFoto = @NomeFoto, CPF = @CPF, RG = @RG, " +
                            "Sexo = @Sexo, NrCelular = @NrCelular, PasswordHash = @PasswordHash, " +
                            "Ativo = @Ativo " + _dtNasc + 
                        "WHERE PessoaId = @id";

                    command.Parameters.AddWithValue("Nome", associado.Nome);
                    command.Parameters.AddWithValue("EMail", associado.EMail);
                    command.Parameters.AddWithValue("CPF", associado.Cpf);
                    command.Parameters.AddWithValue("RG", associado.Rg);
                    command.Parameters.AddWithValue("NomeFoto", associado.NomeFoto);
                    command.Parameters.AddWithValue("Sexo", associado.Sexo);
                    command.Parameters.AddWithValue("NrCelular", associado.NrCelular);
                    command.Parameters.AddWithValue("PasswordHash", associado.PasswordHash);
                    command.Parameters.AddWithValue("Ativo", associado.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    if (_dtNasc != "")
                        command.Parameters.AddWithValue("DtNascimento", associado.DtNascimento);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

                    // Atualizando os dados na tabela Associado:
                    string _dtCert = associado.DtCertificacao != null ? ", DtCertificacao = @DtCertificacao " : "";

                    command.CommandText = "" +
                        "UPDATE dbo.AD_Associado  " +
                        "SET AtcId = @AtcId, TipoPublicoId = @TipoPublicoId, " +
                        "   NrMatricula = @NrMatricula, CRP = @CRP, CRM = @CRM, " +
                        "   NomeInstFormacao = @NomeInstFormacao, Certificado = @Certificado, " +
                        "   DivulgarContato = @DivulgarContato, TipoFormaContato = @TipoFormaContato, " +
                        "   IntegraDiretoria = @IntegraDiretoria, IntegraConfi = @IntegraConfi, " +
                        "   NrTelDivulgacao = @NrTelDivulgacao, ComprovanteAfiliacaoAtc = @ComprovanteAfiliacaoAtc, " +
                        "   TipoProfissao = @TipoProfissao, TipoTitulacao = @TipoTitulacao  " + _dtCert +
                        "WHERE PessoaId = @id";

                    command.Parameters.AddWithValue("AtcId", associado.ATCId);
                    command.Parameters.AddWithValue("TipoPublicoId", associado.TipoPublicoId);
                    command.Parameters.AddWithValue("NrMatricula", associado.NrMatricula);
                    command.Parameters.AddWithValue("CRP", associado.Crp);
                    command.Parameters.AddWithValue("CRM", associado.Crm);
                    command.Parameters.AddWithValue("NomeInstFormacao", associado.NomeInstFormacao);
                    command.Parameters.AddWithValue("Certificado", associado.Certificado);
                    command.Parameters.AddWithValue("DivulgarContato", associado.DivulgarContato);
                    command.Parameters.AddWithValue("TipoFormaContato", associado.TipoFormaContato);
                    command.Parameters.AddWithValue("IntegraDiretoria", associado.IntegraDiretoria);
                    command.Parameters.AddWithValue("IntegraConfi", associado.IntegraConfi);
                    command.Parameters.AddWithValue("NrTelDivulgacao", associado.NrTelDivulgacao);
                    command.Parameters.AddWithValue("ComprovanteAfiliacaoAtc", associado.ComprovanteAfiliacaoAtc);
                    command.Parameters.AddWithValue("TipoProfissao", associado.TipoProfissao);
                    command.Parameters.AddWithValue("TipoTitulacao", associado.TipoTitulacao);

                    if (_dtCert != "")
                        command.Parameters.AddWithValue("DtCertificacao", associado.DtCertificacao);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realiada com sucesso" : "Atualização NÃO realiada com sucesso";

                    transaction.Commit();

                    // Atualizando endereco:
                    if(associado.EnderecoPessoa != null)
                    {
                        if (!string.IsNullOrWhiteSpace(associado.EnderecoPessoa.Cep))
                        {
                            EnderecoRepository _endRep = new EnderecoRepository();

                            associado.EnderecoPessoa.PessoaId = id;

                            if (associado.EnderecoPessoa.EnderecoId != 0)
                            {
                                _msgEnd = _endRep.Update(associado.EnderecoPessoa.EnderecoId, associado.EnderecoPessoa);
                            }
                            else
                            {
                                _msgEnd = _endRep.Insert(associado.EnderecoPessoa);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }
                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                connection.Close();
            }
            return _msg;
        }

        public string GetNomeFotoByAssociadoId(int id)
        {
            String NomeFoto = "_no-foto.png";

            query = @"SELECT P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.IntegraDiretoria, A.IntegraConfi, A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao 
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId 
                    WHERE AssociadoId = " + id + "";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            Associado associado = GetCollection<Associado>(cmd)?.First();

            // Obtendo o nome da foto:
            if (associado != null)
            {
                NomeFoto = associado.NomeFoto;
            }

            return NomeFoto;
        }
    }
}

