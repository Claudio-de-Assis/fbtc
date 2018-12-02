using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

using Fbtc.Infra.Helpers;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;

using prmToolkit.AccessMultipleDatabaseWithAdoNet;
using prmToolkit.AccessMultipleDatabaseWithAdoNet.Enumerators;
using System.Text;
using System.Data.Common;

namespace Fbtc.Infra.Persistencia.AdoNet
{
    public class DescontoAnuidadeAtcRepository : AbstractRepository, IDescontoAnuidadeAtcRepository
    {
        private string query;
        private readonly string strConnSql;

        private LogRepository logRep;
        private readonly string className;
        private string _instrucaoSql = "";
        private string _result = "";

        public DescontoAnuidadeAtcRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");

            className = "DescontoAnuidadeAtcRepository";
            logRep = new LogRepository();

        }

        public IEnumerable<DescontoAnuidadeAtc> GetAll()
        {
            query = @"SELECT DescontoAnuidadeAtcId, AssociadoId, ColaboradorId, 
		                AnuidadeId, AtcId, Observacao, NomeArquivoComprovante, 
		                DtDesconto, DtCadastro, Ativo 
                    FROM	dbo.AD_Desconto_Anuidade_Atc 
                    ORDER By 1"; 

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<DescontoAnuidadeAtc> _collection = GetCollection<DescontoAnuidadeAtc>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAll",
                "SELECT", "DESCONTO_ANUIDADE_ATC", 0, query, _collection.Count<DescontoAnuidadeAtc>().ToString());
            // Fim Log

            return _collection;
        }

        public DescontoAnuidadeAtc GetDescontoAnuidadeAtcById(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pid = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(pid);
            // Fim da definição dos parâmetros

            query = @"SELECT DescontoAnuidadeAtcId, AssociadoId, 
                        ColaboradorId, AnuidadeId, AtcId, Observacao, NomeArquivoComprovante, 
                        DtDesconto, DtCadastro, Ativo 
                    FROM dbo.AD_Desconto_Anuidade_Atc 
                    WHERE DescontoAnuidadeAtcId= @id";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            DescontoAnuidadeAtc desconto = GetCollection<DescontoAnuidadeAtc>(cmd)?.FirstOrDefault<DescontoAnuidadeAtc>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetDescontoAnuidadeAtcById",
                "SELECT", "DESCONTO_ANUIDADE_ATC", id, query, desconto != null ? "SUCESSO" : "0");
            // Fim Log

            return desconto;
        }

        public DescontoAnuidadeAtcDao GetDescontoAnuidadeAtcDaoById(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pid = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(pid);
            // Fim da definição dos parâmetros

            query = @"SELECT	DAA.DescontoAnuidadeAtcId, DAA.AssociadoId, DAA.ColaboradorId, 
		                DAA.AnuidadeId, DAA.AtcId, DAA.Observacao, DAA.NomeArquivoComprovante,
		                DAA.DtDesconto, DAA.DtCadastro, DAA.Ativo,
		                P.Nome as NomePessoa,
		                (	SELECT P2.Nome 
			                FROM dbo.AD_Pessoa P2 
			                INNER JOIN dbo.AD_Colaborador C2 ON P2.PessoaId = C2.PessoaId
			                WHERE C2.ColaboradorId = DAA.ColaboradorId
		                ) as NomeColaborador,
		                Atc.Nome as NomeAtc,
		                Anu.Exercicio
                    FROM	dbo.AD_Desconto_Anuidade_Atc DAA
                    INNER JOIN dbo.AD_ATC Atc ON DAA.AtcId = Atc.AtcId
                    INNER JOIN dbo.AD_Associado A ON DAA.AssociadoId = A.AssociadoId
                    INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                    INNER JOIN dbo.AD_Anuidade Anu ON DAA.AnuidadeId = Anu.AnuidadeId
                    WHERE DAA.DescontoAnuidadeAtcId = @id 
                    ORDER By P.Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            DescontoAnuidadeAtcDao desconto = GetCollection<DescontoAnuidadeAtcDao>(cmd)?.FirstOrDefault<DescontoAnuidadeAtcDao>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetDescontoAnuidadeAtcDaoById",
                "SELECT", "DESCONTO_ANUIDADE_ATC", id, query, desconto != null ? "SUCESSO" : "0");
            // Fim Log

            return desconto;
        }

        public DescontoAnuidadeAtcDao GetDescontoAnuidadeAtcDaoByPessoaId(int pessoaId)
        {
            throw new NotImplementedException();
        }

        public DescontoAnuidadeAtcDao GetDadosNovoDescontoAnuidadeAtcDao(int associadoId, int anuidadeId, int colaboradorPessoaId)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter passociadoId = new SqlParameter() { ParameterName = "@associadoId", Value = associadoId };
            _parametros.Add(passociadoId);

            SqlParameter panuidadeId = new SqlParameter() { ParameterName = "@anuidadeId", Value = anuidadeId };
            _parametros.Add(panuidadeId);

            SqlParameter pcolaboradorPessoaId = new SqlParameter() { ParameterName = "@colaboradorPessoaId", Value = colaboradorPessoaId };
            _parametros.Add(pcolaboradorPessoaId);
            // Fim da definição dos parâmetros

            query = @"SELECT	0 as DescontoAnuidadeAtcId, A.AssociadoId,  
                                (	SELECT C2.ColaboradorId  
			                        FROM dbo.AD_Pessoa P2 
				                        INNER JOIN dbo.AD_Colaborador C2 ON P2.PessoaId = C2.PessoaId 
			                        WHERE P2.PessoaId = @colaboradorPessoaId 
                                ) as ColaboradorId, @anuidadeId as AnuidadeId, At.AtcId, 
                                '' as Observacao, '' as NomeArquivoComprovante, 
		                        null as DtDesconto, null as DtCadastro, 'true' as Ativo, 
		                        P.Nome as NomePessoa, 
		                        (	SELECT P2.Nome 
			                        FROM dbo.AD_Pessoa P2 
				                        INNER JOIN dbo.AD_Colaborador C2 ON P2.PessoaId = C2.PessoaId 
			                        WHERE P2.PessoaId = @colaboradorPessoaId 
		                        ) as NomeColaborador, 
		                        At.Nome as NomeAtc, 
		                        (SELECT Exercicio FROM dbo.AD_Anuidade WHERE AnuidadeId = @anuidadeId ) as Exercicio 
                    FROM	dbo.AD_Associado A  
                    INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId 
                    INNER JOIN dbo.AD_Atc At ON A.AtcId = At.AtcId 
                    WHERE A.AssociadoId = @associadoId ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            DescontoAnuidadeAtcDao desconto = GetCollection<DescontoAnuidadeAtcDao>(cmd)?.FirstOrDefault<DescontoAnuidadeAtcDao>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetDadosNovoDescontoAnuidadeAtcDao",
                "SELECT", "DESCONTO_ANUIDADE_ATC", associadoId, query, desconto != null ? "SUCESSO" : "0");
            // Fim Log

            return desconto;
        }

        public string Insert(DescontoAnuidadeAtc d)
        {
            bool _resultado = false;
            string _msg = "";
            Int32 id = 0;
            string _ident = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("IncluirDescontoAnuidadeAtc");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Desconto_Anuidade_Atc (AssociadoId, ColaboradorId, "+
                        "   AnuidadeId, AtcId, Observacao, NomeArquivoComprovante, " +
		                "   DtDesconto, DtCadastro, Ativo) " +
                        "VALUES (@AssociadoId, @ColaboradorId, " +
                        "        @AnuidadeId, @AtcId, @Observacao, @NomeArquivoComprovante, " +
		                "        @DtDesconto, @DtCadastro, @Ativo) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("AssociadoId", d.AssociadoId);
                    command.Parameters.AddWithValue("ColaboradorId", d.ColaboradorId);
                    command.Parameters.AddWithValue("AnuidadeId", d.AnuidadeId);
                    command.Parameters.AddWithValue("AtcId", d.AtcId);
                    command.Parameters.AddWithValue("Observacao", d.Observacao);
                    command.Parameters.AddWithValue("NomeArquivoComprovante", d.NomeArquivoComprovante);
                    command.Parameters.AddWithValue("DtDesconto", DateTime.Now);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);
                    command.Parameters.AddWithValue("Ativo", d.Ativo);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    if (id > 0)
                        _ident = _ident.PadLeft(10 - id.ToString().Length, '0') + id.ToString();

                    _msg = id > 0 ? $"{_ident}Inclusão realizada com sucesso" : $"{_ident}Inclusão Não realizada com sucesso";

                    transaction.Commit();

                    // Log da Inserção ANUIDADE:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = id > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/Insert",
                        "INSERT", "DESCONTO_ANUIDADE_ATC", id, _instrucaoSql, _result);
                    //Fim do Log
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR DESCONTO_ANUIDADE_ATC: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                        //throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/Insert",
                        "INSERT", "DESCONTO_ANUIDADE_ATC", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR DESCONTO_ANUIDADE_ATC: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                    // throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string Update(int id, DescontoAnuidadeAtc d)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarDescontoAnuidadeAtc");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "UPDATE dbo.AD_Desconto_Anuidade_Atc " +
                        "SET Observacao = @Observacao, NomeArquivoComprovante = @NomeArquivoComprovante, " +
                        "   Ativo = @Ativo " +
                        "WHERE DescontoAnuidadeAtcId = @id";

                    command.Parameters.AddWithValue("Observacao", d.Observacao);
                    command.Parameters.AddWithValue("NomeArquivoComprovante", d.NomeArquivoComprovante);
                    command.Parameters.AddWithValue("Ativo", d.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

                    transaction.Commit();

                    // Log do UPDATE ANUIDADE:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = x > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "DESCONTO_ANUIDADE_ATC", id, _instrucaoSql, _result);
                    //Fim do Log
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR DESCONTO_ANUIDADE_ATC: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                        // throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "DESCONTO_ANUIDADE_ATC", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR DESCONTO_ANUIDADE_ATC: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                    // throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;

        }

        public IEnumerable<DescontoAnuidadeAtcDao> FindByFilters(int anuidadeId, string nomePessoa, bool? ativo, bool? comDesconto)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter panuidadeId = new SqlParameter() { ParameterName = "@anuidadeId", Value = anuidadeId };
            _parametros.Add(panuidadeId);

            SqlParameter pcomDesconto = new SqlParameter() { ParameterName = "@comDesconto", Value = comDesconto };
            _parametros.Add(pcomDesconto);
            // Fim da definição dos parâmetros

            if (comDesconto == true)
            {
                query = @"SELECT	DAA.DescontoAnuidadeAtcId, DAA.AssociadoId, DAA.ColaboradorId, 
		                DAA.AnuidadeId, DAA.AtcId, DAA.Observacao, DAA.NomeArquivoComprovante,
		                DAA.DtDesconto, DAA.DtCadastro, DAA.Ativo,
		                P.Nome as NomePessoa,
		                (	SELECT P2.Nome 
			                FROM dbo.AD_Pessoa P2 
			                INNER JOIN dbo.AD_Colaborador C2 ON P2.PessoaId = C2.PessoaId
			                WHERE C2.ColaboradorId = DAA.ColaboradorId
		                ) as NomeColaborador,
		                Atc.Nome as NomeAtc,
		                Anu.Exercicio
                FROM	dbo.AD_Desconto_Anuidade_Atc DAA
                INNER JOIN dbo.AD_ATC Atc ON DAA.AtcId = Atc.AtcId
                INNER JOIN dbo.AD_Associado A ON DAA.AssociadoId = A.AssociadoId
                INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                INNER JOIN dbo.AD_Anuidade Anu ON DAA.AnuidadeId = Anu.AnuidadeId
                WHERE DAA.AnuidadeId = @anuidadeId ";

                if (!string.IsNullOrEmpty(nomePessoa))
                {
                    query = query + $" AND P.Nome Like '%'+ @nomePessoa +'%' ";

                    SqlParameter pnomePessoa = new SqlParameter() { ParameterName = "@nomePessoa", Value = nomePessoa };
                    _parametros.Add(pnomePessoa);
                }
                if (ativo != null)
                {
                    query = query + $" AND DAA.Ativo = @ativo ";

                    SqlParameter pativo = new SqlParameter() { ParameterName = "@ativo", Value = ativo };
                    _parametros.Add(pativo);
                }

                query = query + " ORDER By P.Nome";
            }
            else
            {
                query = @"SELECT	0 as DescontoAnuidadeAtcId, A.AssociadoId, 0 as ColaboradorId, 
		                        @anuidadeId as AnuidadeId, At.AtcId, '' as Observacao, '' as NomeArquivoComprovante,
		                        null as DtDesconto, null as DtCadastro, 'false' as Ativo,
		                        P.Nome as NomePessoa,
		                        '' as NomeColaborador,
		                        At.Nome as NomeAtc,
		                        (SELECT Exercicio FROM dbo.AD_Anuidade WHERE AnuidadeId = @anuidadeId) as Exercicio
                        FROM	dbo.AD_Associado A  
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                        INNER JOIN dbo.AD_Atc At ON A.AtcId = At.AtcId
                        INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId 
                        WHERE TP.Associado = 1
                        AND A.AssociadoId Not in ( SELECT	A.AssociadoId
                                                    FROM	dbo.AD_Desconto_Anuidade_Atc DAA
                                                    INNER JOIN dbo.AD_ATC Atc ON DAA.AtcId = Atc.AtcId
                                                    INNER JOIN dbo.AD_Associado A ON DAA.AssociadoId = A.AssociadoId
                                                    INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                                                    INNER JOIN dbo.AD_Anuidade Anu ON DAA.AnuidadeId = Anu.AnuidadeId
                                                    WHERE DAA.AnuidadeId = @anuidadeId)
                        AND YEAR(P.DtCadastro) <= ( SELECT A4.Exercicio 
                                                    FROM dbo.AD_Anuidade A4 
                                                    WHERE A4.AnuidadeId = @anuidadeId) ";


                if (!string.IsNullOrEmpty(nomePessoa))
                {
                    query = query + $" AND P.Nome Like '%'+ @nomePessoa +'%' ";

                    SqlParameter pnomePessoa = new SqlParameter() { ParameterName = "@nomePessoa", Value = nomePessoa };
                    _parametros.Add(pnomePessoa);
                }

                query = query + " ORDER By P.Nome";
            }

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<DescontoAnuidadeAtcDao> _collection = GetCollection<DescontoAnuidadeAtcDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindByFilters",
                "SELECT", "DESCONTO_ANUIDADE_ATC", 0, query, _collection.Count<DescontoAnuidadeAtcDao>().ToString());
            // Fim Log

            return _collection;
        }

        public IEnumerable<DescontoAnuidadeAtcDao> GetDescontoAnuidadeAtcDaoByAnuidadeId(int anuidadeId)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter panuidadeId = new SqlParameter() { ParameterName = "@anuidadeId", Value = anuidadeId };
            _parametros.Add(panuidadeId);

            // Fim da definição dos parâmetros


            query = @"SELECT	DAA.DescontoAnuidadeAtcId, DAA.AssociadoId, DAA.ColaboradorId, 
		                DAA.AnuidadeId, DAA.AtcId, DAA.Observacao, DAA.NomeArquivoComprovante,
		                DAA.DtDesconto, DAA.DtCadastro, DAA.Ativo,
		                P.Nome as NomePessoa,
		                (	SELECT P2.Nome 
			                FROM dbo.AD_Pessoa P2 
			                INNER JOIN dbo.AD_Colaborador C2 ON P2.PessoaId = C2.PessoaId
			                WHERE C2.ColaboradorId = DAA.ColaboradorId
		                ) as NomeColaborador,
		                Atc.Nome as NomeAtc,
		                Anu.Exercicio
                FROM	dbo.AD_Desconto_Anuidade_Atc DAA
                INNER JOIN dbo.AD_ATC Atc ON DAA.AtcId = Atc.AtcId
                INNER JOIN dbo.AD_Associado A ON DAA.AssociadoId = A.AssociadoId
                INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                INNER JOIN dbo.AD_Anuidade Anu ON DAA.AnuidadeId = Anu.AnuidadeId
                WHERE DAA.AnuidadeId = @anuidadeId 
                ORDER By P.Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<DescontoAnuidadeAtcDao> _collection = GetCollection<DescontoAnuidadeAtcDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetDescontoAnuidadeAtcDaoByAnuidadeId",
                "SELECT", "DESCONTO_ANUIDADE_ATC", 0, query, _collection.Count<DescontoAnuidadeAtcDao>().ToString());
            // Fim Log

            return _collection;
        }
    }
}
