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
            query = @"SELECT Distinct P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
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

        public IEnumerable<AssociadoIsentoDao> FindIsentoByFilters(int isencaoId, string nome, string cpf,
            string sexo, int atcId, string crp, string tipoProfissao, int tipoPublicoId, string estado, string cidade, bool? ativo)
        {
            query = @"SELECT AssociadoIsentoId, IsencaoId, AssociadoId, Nome, Cpf, Crp, AtcId, TipoPublicoId, Ativo from 
                        (   SELECT  0 as AssociadoIsentoId, 0 as IsencaoId, A.AssociadoId, P.Nome, P.Cpf, A.Crp, A.AtcId, A.TipoPublicoId, P.Ativo 
                            FROM dbo.AD_Associado A 
                            INNER JOIN  dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId 
                            WHERE A.AssociadoId not in (SELECT AI2.AssociadoId FROM dbo.AD_Associado_Isento AI2 WHERE AI2.IsencaoId = " + isencaoId + ") ";
            query = query + @"UNION 
                            SELECT  AI.AssociadoIsentoId, AI.IsencaoId, A.AssociadoId, P.Nome, P.Cpf, A.Crp,  A.AtcId, A.TipoPublicoId, P.Ativo 
                            FROM dbo.AD_Associado A 
                            INNER JOIN  dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId 
                            LEFT JOIN dbo.AD_Associado_Isento AI ON A.AssociadoId = AI.AssociadoId 
                            WHERE AI.IsencaoId =  " + isencaoId + " ) AS TAB WHERE AssociadoId IS NOT NULL ";

            if (!string.IsNullOrEmpty(nome))
                query = query + $" AND Nome Like '%{nome}%' ";

            if (!string.IsNullOrEmpty(cpf))
                query = query + $" AND CPF = '{cpf}' ";

            /* if (!string.IsNullOrEmpty(sexo))
                query = query + $" AND Sexo = '{sexo}' ";*/

            if (atcId != 0)
                query = query + $" AND AtcId = {atcId} ";

            if (!string.IsNullOrEmpty(crp))
                query = query + $" AND CRP = '{crp}' ";

            /*if (!string.IsNullOrEmpty(tipoProfissao))
                query = query + $" AND A.TipoProfissao = '{tipoProfissao}' ";*/

            if (tipoPublicoId != 0)
                query = query + $" AND TipoPublicoId = {tipoPublicoId} ";

            /*if (!string.IsNullOrEmpty(estado))
                query = query + $" AND Estado = '{estado}' ";*/

            /*if (!string.IsNullOrEmpty(cidade))
                query = query + $" AND Cidade = '{cidade}' ";*/

            if (ativo != null)
                query = query + $" AND Ativo = '{ativo}' ";

            query = query + " ORDER BY Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<AssociadoIsentoDao> _collection = GetCollection<AssociadoIsentoDao>(cmd)?.ToList();

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
                    WHERE AssociadoId = " + id + "";

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
                    associado.EnderecosPessoa = ends;
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
            Int32 assocId = 0;
            string _ident = "";

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
                        "   Sexo, NrCelular, " +
                        "   DtCadastro " + _dtNasc + ") " +
                        "VALUES(@Nome, @EMail, @CPF, @RG, @NomeFoto, " +
                        "   @Sexo, @NrCelular, " +
                        "   @DtCadastro " + _paramDtNasc + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Nome", associado.Nome);
                    command.Parameters.AddWithValue("EMail", associado.EMail);
                    command.Parameters.AddWithValue("CPF", associado.Cpf);
                    command.Parameters.AddWithValue("RG", associado.Rg);
                    command.Parameters.AddWithValue("NomeFoto", associado.NomeFoto);
                    command.Parameters.AddWithValue("Sexo", associado.Sexo);
                    command.Parameters.AddWithValue("NrCelular", associado.NrCelular);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);

                    if (_dtNasc != "")
                        command.Parameters.AddWithValue("DtNascimento", associado.DtNascimento);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    // Inserindo os dados na tabela ASSOCIADO:
                    string _dtCert = associado.DtCertificacao != null ? ", DtCertificacao " : "";
                    string _paramDtCert = associado.DtCertificacao != null ? ", @DtCertificacao " : "";

                    string _atc = associado.ATCId != null ? ", AtcId " : "";
                    string _paramAtac = associado.ATCId != null ? ", @AtcId " : "";


                    command.CommandText = "" +
                        "INSERT into dbo.AD_Associado (PessoaId "+ _atc +", TipoPublicoId, " +
                        "   NrMatricula, CRP, CRM, NomeInstFormacao, Certificado, " +
                        "   DivulgarContato, TipoFormaContato, IntegraDiretoria, IntegraConfi, " +
                        "   NrTelDivulgacao, ComprovanteAfiliacaoAtc, TipoProfissao, TipoTitulacao " + _dtCert + ") " +
                        "VALUES (@PessoaId "+ _paramAtac +", @TipoPublicoId, " +
                        "   @NrMatricula, @CRP, @CRM, @NomeInstFormacao, @Certificado, " +
                        "   @DivulgarContato, @TipoFormaContato, @IntegraDiretoria, @IntegraConfi, " +
                        "   @NrTelDivulgacao, @ComprovanteAfiliacaoAtc, @TipoProfissao, @TipoTitulacao " + _paramDtCert + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("PessoaId", id);
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

                    if(_atc != "")
                        command.Parameters.AddWithValue("AtcId", associado.ATCId);

                    assocId = (Int32)command.ExecuteScalar();

                    _resultado = assocId > 0;


                    if (assocId > 0)
                        _ident = _ident.PadLeft(10 - assocId.ToString().Length, '0') + assocId.ToString();

                    _msg = assocId > 0 ? $"{_ident}Inclusão realiada com sucesso" : $"{_ident}Inclusão Não realiada com sucesso";

                    transaction.Commit();

                    // Inserindo endereco:
                    if (associado.EnderecosPessoa != null)
                    {
                        foreach (var end in associado.EnderecosPessoa)
                        {
                            EnderecoRepository _endRep = new EnderecoRepository();

                            end.PessoaId = id;

                            if (end.EnderecoId != 0)
                            {
                                _msgEnd = _endRep.Update(end.EnderecoId, end);
                            }
                            else
                            {
                                _msgEnd = _endRep.Insert(end);
                            }
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
                            "Sexo = @Sexo, NrCelular = @NrCelular, " +
                            "Ativo = @Ativo " + _dtNasc +
                        "WHERE PessoaId = @id";

                    command.Parameters.AddWithValue("Nome", associado.Nome);
                    command.Parameters.AddWithValue("EMail", associado.EMail);
                    command.Parameters.AddWithValue("CPF", associado.Cpf);
                    command.Parameters.AddWithValue("RG", associado.Rg);
                    command.Parameters.AddWithValue("NomeFoto", associado.NomeFoto);
                    command.Parameters.AddWithValue("Sexo", associado.Sexo);
                    command.Parameters.AddWithValue("NrCelular", associado.NrCelular);
                    command.Parameters.AddWithValue("Ativo", associado.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    if (_dtNasc != "")
                        command.Parameters.AddWithValue("DtNascimento", associado.DtNascimento);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

                    // Atualizando os dados na tabela Associado:
                    string _dtCert = associado.DtCertificacao != null ? ", DtCertificacao = @DtCertificacao " : "";
                    string _atc = associado.ATCId != null ? " AtcId = @AtcId, " : "";

                    command.CommandText = "" +
                        "UPDATE dbo.AD_Associado  " +
                        "SET TipoPublicoId = @TipoPublicoId , " + _atc +
                        "   NrMatricula = @NrMatricula, CRP = @CRP, CRM = @CRM, " +
                        "   NomeInstFormacao = @NomeInstFormacao, Certificado = @Certificado, " +
                        "   DivulgarContato = @DivulgarContato, TipoFormaContato = @TipoFormaContato, " +
                        "   IntegraDiretoria = @IntegraDiretoria, IntegraConfi = @IntegraConfi, " +
                        "   NrTelDivulgacao = @NrTelDivulgacao, ComprovanteAfiliacaoAtc = @ComprovanteAfiliacaoAtc, " +
                        "   TipoProfissao = @TipoProfissao, TipoTitulacao = @TipoTitulacao  " + _dtCert +
                        "WHERE PessoaId = @id";

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

                    if (_atc != "")
                        command.Parameters.AddWithValue("AtcId", associado.ATCId);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

                    transaction.Commit();

                    // Atualizando endereco:
                    if (associado.EnderecosPessoa != null)
                    {
                        foreach (var end in associado.EnderecosPessoa)
                        {
                            // if (!string.IsNullOrWhiteSpace(end.Cep))
                            // {
                            EnderecoRepository _endRep = new EnderecoRepository();

                            end.PessoaId = id;

                            if (end.EnderecoId != 0)
                            {
                                _msgEnd = _endRep.Update(end.EnderecoId, end);
                            }
                            else
                            {
                                _msgEnd = _endRep.Insert(end);
                            }
                            //  }
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

        public string InsertIsento(AssociadoIsentoDao a)
        {
            RecebimentoRepository recebimentoRep = new RecebimentoRepository();

            string _msg = "";
            Int32 id = 0;

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("IncluirAssociadoIsento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Associado_Isento (IsencaoId, AssociadoId) " +
                        "VALUES(@IsencaoId, @AssociadoId) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("IsencaoId", a.IsencaoId);
                    command.Parameters.AddWithValue("AssociadoId", a.AssociadoId);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);

                    id = (Int32)command.ExecuteScalar();

                    transaction.Commit();

                    if (id > 0)
                    {
                        string res = recebimentoRep.InsertIsento(a.AssociadoId, id, a.TipoIsencao, a.TipoIsencao);
                    }

                    _msg = id > 0 ? "Inclusão realiada com sucesso" : "Inclusão Não realiada com sucesso";

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

        public string DeleteIsentoByAssociadoIsentoId(int AssociadoIsentoId)
        {
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("DeleteAssociadoIsento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    RecebimentoRepository recebimentoRep = new RecebimentoRepository();

                    string _result = recebimentoRep.DeleteByAssociadoIsentoId(AssociadoIsentoId);

                    if (_result.Equals("SUCESSO"))
                    {
                        command.CommandText = "" +
                            "DELETE " +
                            "From dbo.AD_Associado_Isento " +
                            "WHERE AssociadoIsentoId = @AssociadoIsentoId ";

                        command.Parameters.AddWithValue("@AssociadoIsentoId", AssociadoIsentoId);

                        int i = command.ExecuteNonQuery();

                        _msg = i > 0 ? "Exclusão realizada com sucesso" : "Exclusão NÃO realizada com sucesso";

                        transaction.Commit();
                    }
                    else
                    {
                        _msg = "Exclusão NÃO realizada com sucesso";
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

        public string RessetPasswordById(int id)
        {
            bool _sendSucess = false;
            string _msg = "", _newPassword = "", _newPasswordHash = "";
            bool _isBodyHtml = true;

            string _subject, _textBody;

            Associado _associado = new Associado();
            _associado = GetAssociadoById(id);

            SendEMail _sendMail = new SendEMail();

            _newPassword = Functions.GetNovaSenhaAcesso("");
            _newPasswordHash = Functions.CriptografaSenha(_newPassword);

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("RessetSenhaAssociado");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Atualizando a senha na tabela PESSOA:
                    command.CommandText = "" +
                        "UPDATE dbo.AD_Pessoa " +
                        "SET PasswordHash = @PasswordHash " +
                        "WHERE PessoaId = @id";

                    command.Parameters.AddWithValue("PasswordHash", _newPasswordHash);
                    command.Parameters.AddWithValue("id", _associado.PessoaId);

                    int i = command.ExecuteNonQuery();

                    transaction.Commit();

                    if (i > 0)
                    {
                        _subject = "Site FBTC - Troca de Senha - A sua nova senha de acesso chegou!";

                        _textBody = "<html><body> " +
                                    $"<p>Olá {_associado.Nome}!</p>" +
                                    "<p>Esta mensagem foi gerada pelo sistema Troca de Senha do Site FBTC.</p>" +
                                    "<p>Conforme solicitação através do site, a sua senha de acesso a sua conta no site fbtc.org.br foi reiniciada.</br></br>" +
                                        "Para você logar-se, por favor, informe o seu e-mail e a senha abaixo:</br></br></br>" +
                                        $"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>{_newPassword}</b></br></br></br>" +
                                        "Por favor, para seu segurança, troque-a no seu próximo acesso.</br></br></br>" +
                                        "<a href='http://www.fbtc.org.br/Account/Login' target='_blank'>fbtc.org.br - Acessar sua Conta</a></br>" +
                                    "</p>" +
                                    "<p><i>2018 - FBTC Federação Brasileira de Terapias Cognitivas - Direitos reservados.</i></p> " +
                                     "<p>Este é um e-mail automático da FBTC, por favor não o responda.</p> " +
                                    "</body></html> ";

                        _sendSucess = _sendMail.SendMessage(_associado.EMail, _subject, _isBodyHtml, _textBody);

                        _msg = _sendSucess == true ? $"A nova senha foi enviada para o e-mail: { _associado.EMail }." : "Houve uma falha no envio da sua senha";
                    }
                    else
                    {
                        _msg = "Atualização NÃO realiada com sucesso";
                        _sendSucess = false;
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

        public string ValidaEMail(int associadoId, string eMail)
        {
            string _msg = "OK";

            try
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
                    WHERE AssociadoId != " + associadoId + " " +
                        " AND P.EMail = '" + eMail + "' ";

                // Define o banco de dados que será usando:
                CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

                // Obtém os dados do banco de dados:
                IEnumerable<Associado> _collection = GetCollection<Associado>(cmd)?.ToList();

                // Verificando se há registro:
                foreach (var item in _collection)
                {
                    if (item.PessoaId > 0)
                        _msg = $"Atenção: O EMail {eMail} já está um uso por outro usuário";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception Type:{ex.GetType()}. Erro:{ex.Message}");
            }
            return _msg;
        }
    }
}

