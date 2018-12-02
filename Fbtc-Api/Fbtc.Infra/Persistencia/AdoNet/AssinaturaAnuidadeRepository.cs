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
    public class AssinaturaAnuidadeRepository : AbstractRepository, IAssinaturaAnuidadeRepository
    {
        private string query;
        private readonly string strConnSql;

        private LogRepository logRep;
        private readonly string className;
        private string _instrucaoSql = "";
        private string _result = "";

        public AssinaturaAnuidadeRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");

            className = "AssinaturaAnuidadeRepository";
            logRep = new LogRepository();
        }

        public IEnumerable<AssinaturaAnuidadeDao> FindByFilters(int anuidadeId, string nome, string cpf, bool? ativo)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT AA.AssinaturaAnuidadeId, AA.AssociadoId, AA.ValorAnuidadeId, AA.AnoInicio, 
                        AA.AnoTermino, AA.PercentualDesconto, AA.TipoDesconto, AA.Valor, AA.DtAssinatura, 
                        AA.DtAtualizacao, AA.Ativo, AA.CodePS, AA.DtCodePS, AA.Reference, AA.EmProcessoPagamento,
                        AA.DtInicioProcessamento, P.Nome as NomePessoa, P.CPF, TP.Nome as NomeTP, 
                        A.Exercicio, VA.ValorAnuidadeId, VA.ValorAnuidadeId as ValorAnuidadeIdOriginal, VA.TipoAnuidade, VA.Valor as ValorTipoAnuidade, A.AnuidadeId, TP.TipoPublicoId, 
	                    (SELECT 'AnuidadeAtcOk' =   
		                    Case 
			                    WHEN Count(DAA.DescontoAnuidadeAtcId) > 0 THEN 'TRUE' 
			                    ELSE 'FALSE' 
		                    END 
	                    FROM dbo.AD_Desconto_Anuidade_Atc DAA 
	                    WHERE DAA.AssociadoId = ASO.AssociadoId AND DAA.AnuidadeId = A.AnuidadeId)  AS AnuidadeAtcOk, 
	                    (SELECT 'MembroDiretoria' = 
		                    Case 
			                    WHEN Count(GE.GestaoId) > 0 THEN 'TRUE' 
			                    ELSE 'FALSE' 
		                    END                         
	                    FROM dbo.AD_Gestao GE  
	                    INNER JOIN dbo.AD_Membro_Gestao MG ON GE.GestaoId  = MG.GestaoId 
                        WHERE MG.AssociadoId = ASO.AssociadoId AND (GE.AnoInicial <= A.Exercicio AND GE.AnoFinal >= A.Exercicio) 
                            )  AS MembroDiretoria,
                        (SELECT 'MembroConfi' = 
	                        Case 
		                        WHEN Count(G.GestaoId) > 0 THEN 'TRUE' 
		                        ELSE 'FALSE' 
	                        END  
                        FROM dbo.AD_Gestao G 
                        INNER JOIN dbo.AD_Membro_Gestao MG ON G.GestaoId = MG.GestaoId 
                        INNER JOIN dbo.AD_Cargo_Gestao CG ON MG.CargoGestaoId = CG.CargoGestaoId 
                        WHERE UPPER(CG.Nome) = 'PRESIDENTE' 
                        AND MG.AssociadoId = AA.AssociadoId 
                        ) AS MembroConfi                       
                    FROM dbo.AD_Assinatura_Anuidade AA 
                        INNER JOIN dbo.AD_Valor_Anuidade VA ON AA.ValorAnuidadeId = VA.ValorAnuidadeId 
                        INNER JOIN dbo.AD_Anuidade_Tipo_Publico ATP ON VA.AnuidadeTipoPublicoId = ATP.AnuidadeTipoPublicoId 
                        INNER JOIN dbo.AD_Anuidade A ON ATP.AnuidadeId = A.AnuidadeId 
                        INNER JOIN dbo.AD_Associado ASO ON AA.AssociadoId = ASO.AssociadoId 
                        INNER JOIN dbo.AD_Pessoa P ON ASO.PessoaId = P.PessoaId 
                        INNER JOIN dbo.AD_Tipo_Publico TP ON ATP.TipoPublicoId = TP.TipoPublicoId 
                    WHERE AA.AssinaturaAnuidadeId > 0 ";

            if (anuidadeId > 0)
            {
                query = query + $" AND A.AnuidadeId = @anuidadeId ";
                SqlParameter paramAnuidadeId = new SqlParameter() { ParameterName = "@anuidadeId", Value = anuidadeId };
                _parametros.Add(paramAnuidadeId);
            }

            if (!string.IsNullOrEmpty(nome))
            {
                query = query + $" AND P.Nome Like '%'+ @nome +'%' ";
                SqlParameter paramNome = new SqlParameter() { ParameterName = "@nome", Value = nome };
                _parametros.Add(paramNome);
            }

            if (!string.IsNullOrEmpty(cpf))
            {
                query = query + $" AND P.CPF = @cpf ";
                SqlParameter paramCpf = new SqlParameter() { ParameterName = "@cpf", Value = cpf };
                _parametros.Add(paramCpf);
            }

            if (ativo != null)
            {
                query = query + $" AND AA.Ativo = @ativo ";
                SqlParameter paramAtivo = new SqlParameter() { ParameterName = "@ativo", Value = ativo };
                _parametros.Add(paramAtivo);
            }

            query = query + " ORDER BY AA.DtAssinatura Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<AssinaturaAnuidadeDao> _collection = GetCollection<AssinaturaAnuidadeDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindByFilters",
                "SELECT", "ASSINATURA_ANUIDADE", 0, query, _collection.Count<AssinaturaAnuidadeDao>().ToString());
            // Fim Log

            return _collection;
        }

        public IEnumerable<AssinaturaAnuidade> GetAll()
        {
            query = @"SELECT AssinaturaAnuidadeId, AssociadoId, ValorAnuidadeId, AnoInicio, AnoTermino, 
                        PercentualDesconto, TipoDesconto, Valor, DtVencimentoPagamento, DtAssinatura, 
                        DtAtualizacao, Ativo, CodePS, DtCodePS, Reference, EmProcessoPagamento,
                        DtInicioProcessamento 
                    FROM dbo.AD_Assinatura_Anuidade 
                    ORDER BY AssociadoId, AssinaturaAnuidadeId ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<AssinaturaAnuidade> _collection = GetCollection<AssinaturaAnuidade>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAll",
                "SELECT", "ASSINATURA_ANUIDADE", 0, query, _collection.Count<AssinaturaAnuidade>().ToString());
            // Fim Log

            return _collection;
        }

        public AssinaturaAnuidadeDao GetAssinaturaAnuidadeById(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT AA.AssinaturaAnuidadeId, AA.AssociadoId, AA.ValorAnuidadeId, AA.AnoInicio, 
                        AA.AnoTermino, AA.PercentualDesconto, AA.TipoDesconto, AA.Valor, AA.DtVencimentoPagamento, 
                        AA.DtAssinatura, AA.DtAtualizacao, AA.Ativo, AA.CodePS, AA.DtCodePS, AA.Reference, AA.EmProcessoPagamento,
                        AA.DtInicioProcessamento, P.Nome as NomePessoa, P.CPF, TP.Nome as NomeTP, 
                        A.Exercicio, VA.ValorAnuidadeId, VA.ValorAnuidadeId as ValorAnuidadeIdOriginal, VA.TipoAnuidade, VA.Valor as ValorTipoAnuidade, A.AnuidadeId, TP.TipoPublicoId, 
	                    (SELECT 'AnuidadeAtcOk' =   
		                    Case 
			                    WHEN Count(DAA.DescontoAnuidadeAtcId) > 0 THEN 'TRUE' 
			                    ELSE 'FALSE' 
		                    END 
	                    FROM dbo.AD_Desconto_Anuidade_Atc DAA 
	                    WHERE DAA.AssociadoId = ASO.AssociadoId AND DAA.AnuidadeId = A.AnuidadeId)  AS AnuidadeAtcOk, 
	                    (SELECT 'MembroDiretoria' = 
		                    Case 
			                    WHEN Count(GE.GestaoId) > 0 THEN 'TRUE' 
			                    ELSE 'FALSE' 
		                    END                         
	                    FROM dbo.AD_Gestao GE  
	                    INNER JOIN dbo.AD_Membro_Gestao MG ON GE.GestaoId  = MG.GestaoId 
                        WHERE MG.AssociadoId = ASO.AssociadoId AND (GE.AnoInicial <= A.Exercicio AND GE.AnoFinal >= A.Exercicio) 
                            )  AS MembroDiretoria,
                        (SELECT 'MembroConfi' = 
	                        Case 
		                        WHEN Count(G.GestaoId) > 0 THEN 'TRUE' 
		                        ELSE 'FALSE' 
	                        END  
                        FROM dbo.AD_Gestao G 
                        INNER JOIN dbo.AD_Membro_Gestao MG ON G.GestaoId = MG.GestaoId 
                        INNER JOIN dbo.AD_Cargo_Gestao CG ON MG.CargoGestaoId = CG.CargoGestaoId 
                        WHERE UPPER(CG.Nome) = 'PRESIDENTE' 
                        AND MG.AssociadoId = AA.AssociadoId 
                        ) AS MembroConfi 
                    FROM dbo.AD_Assinatura_Anuidade AA 
                        INNER JOIN dbo.AD_Valor_Anuidade VA ON AA.ValorAnuidadeId = VA.ValorAnuidadeId 
                        INNER JOIN dbo.AD_Anuidade_Tipo_Publico ATP ON VA.AnuidadeTipoPublicoId = ATP.AnuidadeTipoPublicoId 
                        INNER JOIN dbo.AD_Anuidade A ON ATP.AnuidadeId = A.AnuidadeId 
                        INNER JOIN dbo.AD_Associado ASO ON AA.AssociadoId = ASO.AssociadoId 
                        INNER JOIN dbo.AD_Pessoa P ON ASO.PessoaId = P.PessoaId 
                        INNER JOIN dbo.AD_Tipo_Publico TP ON ATP.TipoPublicoId = TP.TipoPublicoId 
                    WHERE AA.AssinaturaAnuidadeId = @id ORDER BY AA.AssociadoId, AA.AssinaturaAnuidadeId ";


            // Definição do parâmetros da consulta:
            SqlParameter paramId = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(paramId);
            // Fim da definição dos parâmetros


            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            AssinaturaAnuidadeDao assinaturaAssociadoDao = GetCollection<AssinaturaAnuidadeDao>(cmd)?.FirstOrDefault<AssinaturaAnuidadeDao>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAssinaturaAnuidadeById",
                "SELECT", "ASSINATURA_ANUIDADE", id, query, assinaturaAssociadoDao != null ? "SUCESSO" : "0");
            // Fim Log

            return assinaturaAssociadoDao;
        }

        public string Insert(AssinaturaAnuidade a)
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
                transaction = connection.BeginTransaction("IncluirAssinaturaAnuidade");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Assinatura_Anuidade (AssociadoId , ValorAnuidadeId,  AnoInicio, " +
                        "       AnoTermino, PercentualDesconto, TipoDesconto, Valor, DtVencimentoPagamento, DtAssinatura, DtAtualizacao, " +
                        "       CodePS, DtCodePS, Reference) " +
                        "VALUES (@AssociadoId , @ValorAnuidadeId,  @AnoInicio, " +
                        "       @AnoTermino, @PercentualDesconto, @TipoDesconto, @Valor, @DtVencimentoPagamento, @DtAssinatura, @DtAtualizacao, " +
                        "       @CodePS, @DtCodePS, @Reference) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("AssociadoId", a.AssociadoId);
                    command.Parameters.AddWithValue("ValorAnuidadeId", a.ValorAnuidadeId);
                    command.Parameters.AddWithValue("AnoInicio", a.AnoInicio);
                    command.Parameters.AddWithValue("AnoTermino", a.AnoTermino);
                    command.Parameters.AddWithValue("PercentualDesconto", a.PercentualDesconto);
                    command.Parameters.AddWithValue("TipoDesconto", a.TipoDesconto);
                    command.Parameters.AddWithValue("Valor", a.Valor);
                    command.Parameters.AddWithValue("DtVencimentoPagamento", a.DtVencimentoPagamento);
                    command.Parameters.AddWithValue("DtAssinatura", DateTime.Now);
                    command.Parameters.AddWithValue("DtAtualizacao", DateTime.Now);
                    command.Parameters.AddWithValue("CodePS", a.CodePS);
                    command.Parameters.AddWithValue("DtCodePS", a.DtCodePS);
                    command.Parameters.AddWithValue("Reference", a.Reference);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    if (id > 0)
                        _ident = _ident.PadLeft(10 - id.ToString().Length, '0') + id.ToString();

                    _msg = id > 0 ? $"{_ident}Inclusão realizada com sucesso" : $"{_ident}Inclusão Não realizada com sucesso";

                    transaction.Commit();

                    // Log da Inserção ASSINATURA_ANUIDADE:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = id > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/Insert",
                        "INSERT", "ASSINATURA_ANUIDADE", id, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR ASSINATURA_ANUIDADE: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                        //throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/Insert",
                        "INSERT", "ASSINATURA_ANUIDADE", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR ASSINATURA_ANUIDADE: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                    // throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string Update(int id, AssinaturaAnuidade a)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarAssinaturaAnuidade");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    string _data = "";
                    if (a.DtInicioProcessamento != null)
                        _data = " DtInicioProcessamento = @DtInicioProcessamento, ";


                    command.CommandText = "" +
                        "UPDATE dbo.AD_Assinatura_Anuidade " +
                        "SET ValorAnuidadeId = @ValorAnuidadeId, " +
                        "   AnoInicio = @AnoInicio, AnoTermino = @AnoTermino, " +
                        "   PercentualDesconto  = @PercentualDesconto, TipoDesconto = @TipoDesconto, " +
                        "   Valor = @Valor, DtVencimentoPagamento = @DtVencimentoPagamento, " +
                        "   DtAtualizacao = @DtAtualizacao, CodePS = @CodePS, DtCodePS = @DtCodePS, " +
                        "   Reference = @Reference, EmProcessoPagamento = @EmProcessoPagamento, "                    +
                        "   "+ _data +"Ativo = @Ativo " +
                        "WHERE AssinaturaAnuidadeId = @id";

                    command.Parameters.AddWithValue("ValorAnuidadeId", a.ValorAnuidadeId);
                    command.Parameters.AddWithValue("AnoInicio", a.AnoInicio);
                    command.Parameters.AddWithValue("AnoTermino", a.AnoTermino);
                    command.Parameters.AddWithValue("PercentualDesconto", a.PercentualDesconto);
                    command.Parameters.AddWithValue("TipoDesconto", a.TipoDesconto);
                    command.Parameters.AddWithValue("Valor", a.Valor);
                    command.Parameters.AddWithValue("DtVencimentoPagamento", a.DtVencimentoPagamento);
                    command.Parameters.AddWithValue("DtAtualizacao", DateTime.Now);
                    command.Parameters.AddWithValue("CodePS", a.CodePS);
                    command.Parameters.AddWithValue("DtCodePS", a.DtCodePS);
                    command.Parameters.AddWithValue("Reference", a.Reference);
                    command.Parameters.AddWithValue("EmProcessoPagamento", a.EmProcessoPagamento);
                    command.Parameters.AddWithValue("Ativo", a.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    if(_data != "")
                        command.Parameters.AddWithValue("DtInicioProcessamento", a.DtInicioProcessamento);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

                    transaction.Commit();

                    // Log do UPDATE ASSINATURA_ANUIDADE:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = x > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "ASSINATURA_ANUIDADE", id, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR ASSINATURA_ANUIDADE: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                        // throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "ASSINATURA_ANUIDADE", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR ASSINATURA_ANUIDADE: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                    // throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public IEnumerable<AssinaturaAnuidadeDao> FindAssinaturaPendenteByFilters(int anuidadeId, string nome, string cpf, bool? ativo)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT AssinaturaAnuidadeId, AssociadoId, ValorAnuidadeId, AnoInicio, AnoTermino, PercentualDesconto, TipoDesconto, Valor,
                        CodePS, DtCodePS, Reference, EmProcessoPagamento, DtInicioProcessamento, 
                        DtAssinatura, DtAtualizacao, Ativo, NomePessoa, CPF, NomeTP, Exercicio, ValorAnuidadeId, ValorAnuidadeIdOriginal, TipoAnuidade, ValorTipoAnuidade,
                        AnuidadeId, TipoPublicoId, AnuidadeAtcOk, MembroDiretoria

                        FROM (

                        SELECT 0 as AssinaturaAnuidadeId, ASO.AssociadoId, 0 as ValorAnuidadeId, 0 as ValorAnuidadeIdOriginal,  
                        0 as AnoInicio, 0 as AnoTermino, 0 as PercentualDesconto, 0 as TipoDesconto, 0 as Valor, 
                        '' as CodePS, null as DtCodePS, '' as Reference, 0 as EmProcessoPagamento, null as DtInicioProcessamento, 
                        null as DtVencimentoPagamento, null as DtAssinatura, null as DtAtualizacao, 0 as Ativo, P.Nome as NomePessoa, 
                        P.CPF, TP.Nome as NomeTP, 
                        (   SELECT  Exercicio 
	                        FROM    dbo.AD_Anuidade An 
	                        WHERE   An.AnuidadeId = @anuidadeId)	as Exercicio, 
	                        0 as TipoAnuidade, 0 as ValorTipoAnuidade, 0 as AnuidadeId, 
	                        TP.TipoPublicoId, NULL as AnuidadeAtcOk, NULL as MembroDiretoria 
                        FROM dbo.AD_Pessoa P
	                        INNER JOIN dbo.AD_Associado ASO ON P.PessoaId = ASO.PessoaId
	                        INNER JOIN dbo.AD_Tipo_Publico TP ON ASO.TipoPublicoId = TP.TipoPublicoId
                        WHERE	TP.Associado = 1 
                        AND P.Ativo = 1
                        AND Year(P.DtCadastro) <= (SELECT AAA.Exercicio FROM dbo.AD_Anuidade AAA WHERE AAA.AnuidadeId = @anuidadeId) 
                        AND ASO.AssociadoId NOT IN (	SELECT AssociadoId
								                        FROM dbo.AD_Assinatura_Anuidade AA2) 

                        UNION

                        SELECT 0 as AssinaturaAnuidadeId, ASO.AssociadoId, 0 as ValorAnuidadeId, 0 as ValorAnuidadeIdOriginal, 
                        0 as AnoInicio, 0 as AnoTermino, 0 as PercentualDesconto, 0 as TipoDesconto, 0 as Valor, 
                        '' as CodePS, null as DtCodePS, '' as Reference, 0 as EmProcessoPagamento, null as DtInicioProcessamento, 
                        null as DtVencimentoPagamento, null as DtAssinatura, null as DtAtualizacao, 0 as Ativo, P.Nome as NomePessoa, 
                        P.CPF, TP.Nome as NomeTP, 
                        (   SELECT  Exercicio 
	                        FROM    dbo.AD_Anuidade An 
	                        WHERE   An.AnuidadeId = @anuidadeId) as Exercicio, 
	                        0 as TipoAnuidade, 0 as ValorTipoAnuidade, 0 as AnuidadeId, 
	                        TP.TipoPublicoId, NULL as AnuidadeAtcOk, NULL as MembroDiretoria 
                        FROM dbo.AD_Pessoa P
	                        INNER JOIN dbo.AD_Associado ASO ON P.PessoaId = ASO.PessoaId
	                        INNER JOIN dbo.AD_Tipo_Publico TP ON ASO.TipoPublicoId = TP.TipoPublicoId
                        WHERE	TP.Associado = 1 
                        AND P.Ativo = 1
                        AND Year(P.DtCadastro) <= (SELECT AAA.Exercicio FROM dbo.AD_Anuidade AAA WHERE AAA.AnuidadeId = @anuidadeId)
                        AND ASO.AssociadoId IN (	SELECT ASS.AssociadoId 
								                        FROM  dbo.AD_Associado ASS INNER JOIN 
								                        dbo.AD_Assinatura_Anuidade AA2 ON ASS.AssociadoId = AA2.AssociadoId
								                        AND AA2.AnoTermino = (SELECT A2.Exercicio FROM dbo.AD_Anuidade A2 WHERE A2.AnuidadeId = @anuidadeId)) 

                        AND ASO.AssociadoId NOT IN (	SELECT ASS.AssociadoId 
								                        FROM  dbo.AD_Associado ASS INNER JOIN 
								                        dbo.AD_Assinatura_Anuidade AA2 ON ASS.AssociadoId = AA2.AssociadoId
								                        AND AA2.AnoInicio = (SELECT A2.Exercicio FROM dbo.AD_Anuidade A2 WHERE A2.AnuidadeId = @anuidadeId)) 

                        )  as tab
                        Where 1 = 1  ";

            // Definição do parâmetros da consulta:
            SqlParameter paramAnuidadeId = new SqlParameter() { ParameterName = "@anuidadeId", Value = anuidadeId };

            _parametros.Add(paramAnuidadeId);
            // Fim da definição dos parâmetros

            if (!string.IsNullOrEmpty(nome))
            {
                query = query + @" AND NomePessoa Like '%'+ @nome + '%'";
                SqlParameter paramNome = new SqlParameter() { ParameterName = "@nome", Value = nome };
                _parametros.Add(paramNome);
            }

            if (!string.IsNullOrEmpty(cpf))
            {
                query = query + " AND CPF = @cpf ";
                SqlParameter paramCpf = new SqlParameter() { ParameterName = "@cpf", Value = cpf };
                _parametros.Add(paramCpf);
            }

            query = query + " ORDER BY NomePessoa ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<AssinaturaAnuidadeDao> _collection = GetCollection<AssinaturaAnuidadeDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindAssinaturaPendenteByFilters",
                "SELECT", "ASSINATURA_ANUIDADE", 0, query, _collection.Count<AssinaturaAnuidadeDao>().ToString());
            // Fim Log

            return _collection;
        }

        public IEnumerable<AssinaturaAnuidadeDao> FindByPessoaId(int pessoaId)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT AA.AssinaturaAnuidadeId, AA.AssociadoId, AA.ValorAnuidadeId, AA.AnoInicio, 
                        AA.AnoTermino, AA.PercentualDesconto, AA.TipoDesconto, AA.Valor, AA.DtAssinatura, 
                        AA.DtAtualizacao, AA.Ativo, AA.CodePS, AA.DtCodePS, AA.Reference, AA.EmProcessoPagamento,
                        AA.DtInicioProcessamento, P.Nome as NomePessoa, P.CPF, TP.Nome as NomeTP, 
                        A.Exercicio, VA.ValorAnuidadeId, VA.ValorAnuidadeId as ValorAnuidadeIdOriginal, VA.TipoAnuidade, VA.Valor as ValorTipoAnuidade, A.AnuidadeId, TP.TipoPublicoId, 
	                    (SELECT 'AnuidadeAtcOk' =   
		                    Case 
			                    WHEN Count(DAA.DescontoAnuidadeAtcId) > 0 THEN 'TRUE' 
			                    ELSE 'FALSE' 
		                    END 
	                    FROM dbo.AD_Desconto_Anuidade_Atc DAA 
	                    WHERE DAA.AssociadoId = ASO.AssociadoId AND DAA.AnuidadeId = A.AnuidadeId)  AS AnuidadeAtcOk, 
	                    (SELECT 'MembroDiretoria' = 
		                    Case 
			                    WHEN Count(GE.GestaoId) > 0 THEN 'TRUE' 
			                    ELSE 'FALSE' 
		                    END                         
	                    FROM dbo.AD_Gestao GE  
	                    INNER JOIN dbo.AD_Membro_Gestao MG ON GE.GestaoId  = MG.GestaoId 
                        WHERE MG.AssociadoId = ASO.AssociadoId AND (GE.AnoInicial <= A.Exercicio AND GE.AnoFinal >= A.Exercicio) 
                            )  AS MembroDiretoria,
                        (SELECT 'MembroConfi' = 
	                        Case 
		                        WHEN Count(G.GestaoId) > 0 THEN 'TRUE' 
		                        ELSE 'FALSE' 
	                        END  
                        FROM dbo.AD_Gestao G 
                        INNER JOIN dbo.AD_Membro_Gestao MG ON G.GestaoId = MG.GestaoId 
                        INNER JOIN dbo.AD_Cargo_Gestao CG ON MG.CargoGestaoId = CG.CargoGestaoId 
                        WHERE UPPER(CG.Nome) = 'PRESIDENTE' 
                        AND MG.AssociadoId = AA.AssociadoId 
                        ) AS MembroConfi                       
                    FROM dbo.AD_Assinatura_Anuidade AA 
                        INNER JOIN dbo.AD_Valor_Anuidade VA ON AA.ValorAnuidadeId = VA.ValorAnuidadeId 
                        INNER JOIN dbo.AD_Anuidade_Tipo_Publico ATP ON VA.AnuidadeTipoPublicoId = ATP.AnuidadeTipoPublicoId 
                        INNER JOIN dbo.AD_Anuidade A ON ATP.AnuidadeId = A.AnuidadeId 
                        INNER JOIN dbo.AD_Associado ASO ON AA.AssociadoId = ASO.AssociadoId 
                        INNER JOIN dbo.AD_Pessoa P ON ASO.PessoaId = P.PessoaId 
                        INNER JOIN dbo.AD_Tipo_Publico TP ON ATP.TipoPublicoId = TP.TipoPublicoId 
                    WHERE AA.AssinaturaAnuidadeId > 0 
					AND P.PessoaId = @pessoaId ORDER BY A.Exercicio DESC ";

            // Definição do parâmetros da consulta:
            SqlParameter paramPessoaId = new SqlParameter() { ParameterName = "@pessoaId", Value = pessoaId };

            _parametros.Add(paramPessoaId);
            // Fim da definição dos parâmetros

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<AssinaturaAnuidadeDao> _collection = GetCollection<AssinaturaAnuidadeDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindByPessoaId",
                "SELECT", "ASSINATURA_ANUIDADE", 0, query, _collection.Count<AssinaturaAnuidadeDao>().ToString());
            // Fim Log

            return _collection;
        }

        public AssinaturaAnuidade GetAssinaturaAnuidadeByReference(string reference)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT AssinaturaAnuidadeId, AssociadoId, ValorAnuidadeId, AnoInicio, AnoTermino, 
                        PercentualDesconto, TipoDesconto, Valor, DtVencimentoPagamento, DtAssinatura, 
                        DtAtualizacao, Ativo, CodePS, DtCodePS, Reference, EmProcessoPagamento,
                        DtInicioProcessamento 
                    FROM dbo.AD_Assinatura_Anuidade 
                    WHERE Reference = @reference ";

            // Definição do parâmetros da consulta:
            SqlParameter paramRef = new SqlParameter() { ParameterName = "@reference", Value = reference };

            _parametros.Add(paramRef);
            // Fim da definição dos parâmetros

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            AssinaturaAnuidade assinaturaAnuidade = GetCollection<AssinaturaAnuidade>(cmd)?.FirstOrDefault<AssinaturaAnuidade>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAssinaturaAnuidadeByReference",
                "SELECT", "ASSINATURA_ANUIDADE", 0, query, assinaturaAnuidade != null ? "SUCESSO" : "0");
            // Fim Log

            return assinaturaAnuidade;
        }

        public string SetInicioPagamentoPagSeguro(string reference, bool emProcessoPagamento)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SetInicioPagamentoPagSeguro");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "UPDATE dbo.AD_Assinatura_Anuidade " +
                        "SET EmProcessoPagamento = @EmProcessoPagamento, " +
                        "   DtInicioProcessamento = @DtInicioProcessamento " +
                        "WHERE Reference = @Reference";

                    command.Parameters.AddWithValue("EmProcessoPagamento", emProcessoPagamento);
                    command.Parameters.AddWithValue("DtInicioProcessamento", DateTime.Now);
                    command.Parameters.AddWithValue("Reference", reference);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

                    transaction.Commit();

                    // Log do UPDATE ASSINATURA_ANUIDADE:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = x > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/SetInicioPagamentoPagSeguro",
                        "UPDATE", "ASSINATURA_ANUIDADE", 0, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR ASSINATURA_ANUIDADE: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                    }

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "ASSINATURA_ANUIDADE", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR ASSINATURA_ANUIDADE: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }
    }
}
