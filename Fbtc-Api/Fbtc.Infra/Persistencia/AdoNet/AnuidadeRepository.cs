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
    public class AnuidadeRepository : AbstractRepository, IAnuidadeRepository
    {
        private string query;
        private readonly string strConnSql;

        private LogRepository logRep;
        private readonly string className;
        private string _instrucaoSql = "";
        private string _result = "";

        public AnuidadeRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");

            className = "AnuidadeRepository";
            logRep = new LogRepository();
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Anuidade> GetAll()
        {
            query = @"SELECT AnuidadeId, Exercicio, 
                        DtVencimento, DtInicioVigencia, DtTerminoVigencia, 
                        CobrancaLiberada, DtCobrancaLiberada, 
                        DtCadastro, Ativo   
                    FROM dbo.AD_Anuidade 
                    WHERE Ativo = 1   
                    ORDER BY Exercicio Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Anuidade> _collection = GetCollection<Anuidade>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAll",
                "SELECT", "ANUIDADE", 0, query, _collection.Count<Anuidade>().ToString());
            // Fim Log

            return _collection;
        }

        public IEnumerable<Anuidade> GetAnuidadesPendentesByPessoaId(int pessoaId)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT	distinct A1.AnuidadeId, A1.Exercicio, 
                            A1.DtVencimento, A1.DtInicioVigencia, A1.DtTerminoVigencia, 
                            A1.CobrancaLiberada, A1.DtCobrancaLiberada, A1.DtCadastro, A1.Ativo 
                    FROM	dbo.AD_Anuidade A1 
                    WHERE	A1.Ativo = 1
		                    AND A1.CobrancaLiberada = 1
		                    AND A1.Exercicio >= (	SELECT YEAR(P1.DtCadastro) 
								                    FROM dbo.AD_Pessoa P1 
								                    WHERE P1.PessoaId = @pessoaId )
		
		                    AND A1.Exercicio >=	(	SELECT  ISNULL(MAX(AA.AnoTermino),0)
								                    FROM dbo.AD_Assinatura_Anuidade AA 
								                    INNER JOIN dbo.AD_Associado ASO2 ON AA.AssociadoId = ASO2.AssociadoId
								                    INNER JOIN dbo.AD_Pessoa P ON ASO2.PessoaId = P.PessoaId
								                    WHERE P.PessoaId = @pessoaId )
                    ORDER BY A1.Exercicio Desc";

            // Definição do parâmetros da consulta:
            SqlParameter paramPessoaId = new SqlParameter() { ParameterName = "@pessoaId", Value = pessoaId };

            _parametros.Add(paramPessoaId);
            // Fim da definição dos parâmetros

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<Anuidade> _collection = GetCollection<Anuidade>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAnuidadesPendentesByAssociadoId",
                "SELECT", "ANUIDADE", 0, query, _collection.Count<Anuidade>().ToString());
            // Fim Log

            return _collection;
        }

        public Anuidade GetAnuidadeById(int id)
        {

            try
            {
                List<DbParameter> _parametros = new List<DbParameter>();

                query = @"SELECT AnuidadeId, Exercicio, 
                        DtVencimento, DtInicioVigencia, DtTerminoVigencia, 
                        CobrancaLiberada, DtCobrancaLiberada, 
                        DtCadastro, Ativo   
                    FROM dbo.AD_Anuidade
                    WHERE AnuidadeId = @id";

                // Definição do parâmetros da consulta:
                SqlParameter paramId = new SqlParameter() { ParameterName = "@id", Value = id };

                _parametros.Add(paramId);
                // Fim da definição dos parâmetros

                // Define o banco de dados que será usando:
                CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

                // Obtém os dados do banco de dados:
                Anuidade _anuidade = GetCollection<Anuidade>(cmd)?.First();

                // Log da consulta:
                string log = logRep.SetLogger(className + "/GetAnuidadeById",
                    "SELECT", "ANUIDADE", id, query, _anuidade != null ? "SUCESSO" : "0");
                // Fim Log

                return _anuidade;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AnuidadeDao GetAnuidadeDaoById(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT AnuidadeId, Exercicio, 
                        DtVencimento, DtInicioVigencia, DtTerminoVigencia, 
                        CobrancaLiberada, DtCobrancaLiberada, 
                        DtCadastro, Ativo  
                    FROM dbo.AD_Anuidade
                    WHERE AnuidadeId = @id ";

            // Definição do parâmetros da consulta:
            SqlParameter paramId = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(paramId);
            // Fim da definição dos parâmetros

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            AnuidadeDao _anuidadeDao = GetCollection<AnuidadeDao>(cmd)?.FirstOrDefault<AnuidadeDao>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAnuidadeDaoById",
                "SELECT", "ANUIDADE", id, query, _anuidadeDao != null ? "SUCESSO" : "0");
            // Fim Log

            if (_anuidadeDao != null)
            {
                _anuidadeDao.AnuidadesTiposPublicosDao = GetAnuidadeTipoPublicoDaoByAnuidadeId(id);
            }
            return _anuidadeDao;
        }

        public string Insert(Anuidade a)
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
                transaction = connection.BeginTransaction("IncluirAnuidade");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Anuidade (Exercicio, DtVencimento, DtInicioVigencia, " +
                        "       DtTerminoVigencia, CobrancaLiberada, DtCobrancaLiberada) " +
                        "VALUES (@Exercicio, @DtVencimento, @DtInicioVigencia, " +
                        "       @DtTerminoVigencia, @CobrancaLiberada, @DtCobrancaLiberada) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Exercicio", a.Exercicio);
                    command.Parameters.AddWithValue("DtVencimento", a.DtVencimento);
                    command.Parameters.AddWithValue("DtInicioVigencia", a.DtInicioVigencia);
                    command.Parameters.AddWithValue("DtTerminoVigencia", a.DtTerminoVigencia);
                    command.Parameters.AddWithValue("CobrancaLiberada", a.CobrancaLiberada);
                    command.Parameters.AddWithValue("DtCobrancaLiberada", a.DtCobrancaLiberada);

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
                        "INSERT", "ANUIDADE", id, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR ANUIDADE: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                        //throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/Insert",
                        "INSERT", "ANUIDADE", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR ANUIDADE: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                    // throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string Update(int id, Anuidade a)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarAnuidade");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "UPDATE dbo.AD_Anuidade " +
                        "SET Exercicio = @Exercicio, DtVencimento = @DtVencimento, " +
                        "   DtInicioVigencia = @DtInicioVigencia, DtTerminoVigencia = @DtTerminoVigencia, " +
                        "   CobrancaLiberada = @CobrancaLiberada, DtCobrancaLiberada = @DtCobrancaLiberada, " +
                        "   Ativo = @Ativo " +
                        "WHERE AnuidadeId = @id";

                    command.Parameters.AddWithValue("Exercicio", a.Exercicio);
                    command.Parameters.AddWithValue("DtVencimento", a.DtVencimento);
                    command.Parameters.AddWithValue("DtInicioVigencia", a.DtInicioVigencia);
                    command.Parameters.AddWithValue("DtTerminoVigencia", a.DtTerminoVigencia);
                    command.Parameters.AddWithValue("CobrancaLiberada", a.CobrancaLiberada);
                    command.Parameters.AddWithValue("DtCobrancaLiberada", a.DtCobrancaLiberada);
                    command.Parameters.AddWithValue("Ativo", a.Ativo);
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
                        "UPDATE", "ANUIDADE", id, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR ANUIDADE: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                        // throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "ANUIDADE", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR ANUIDADE: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                    // throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public IEnumerable<Anuidade> FindByFilters(int exercicio, bool? ativo)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT AnuidadeId, Exercicio, 
                        DtVencimento, DtInicioVigencia, DtTerminoVigencia, 
                        CobrancaLiberada, DtCobrancaLiberada, 
                        DtCadastro, Ativo    
                    FROM dbo.AD_Anuidade
                    WHERE AnuidadeId > 0 ";                    

            if (exercicio > 0)
            {
                query = query + $" AND Exercicio = @exercicio ";
                SqlParameter paramExercicio = new SqlParameter() { ParameterName = "@exercicio", Value = exercicio };
                _parametros.Add(paramExercicio);
            }

            if (ativo != null)
            {
                query = query + $" AND Ativo = @ativo ";
                SqlParameter paramAtivo = new SqlParameter() { ParameterName = "@ativo", Value = ativo };
                _parametros.Add(paramAtivo);
            }

            query = query + " ORDER BY Exercicio Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<Anuidade> _collection = GetCollection<Anuidade>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindByFilters",
                "SELECT", "ANUIDADE", 0, query, _collection.Count<Anuidade>().ToString());
            // Fim Log

            return _collection;
        }

        public string InsertAnuidadeDao(AnuidadeDao a)
        {
            bool _resultado = false;
            string _msg = "";
            string _msg2= "";
            Int32 id = 0;
            string _ident = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("IncluirAnuidade");

                command.Connection = connection;
                command.Transaction = transaction;

                string _dtCobrancaLiberada = a.DtCobrancaLiberada != null ? ", DtCobrancaLiberada " : "";
                string _paramDtCobrancaLiberada = a.DtCobrancaLiberada != null ? ", @DtCobrancaLiberada " : "";

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Anuidade (Exercicio, DtVencimento, DtInicioVigencia, " +
                        "       DtTerminoVigencia, CobrancaLiberada"+ _dtCobrancaLiberada +") " +
                        "VALUES (@Exercicio, @DtVencimento, @DtInicioVigencia, " +
                        "       @DtTerminoVigencia, @CobrancaLiberada"+ _paramDtCobrancaLiberada + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Exercicio", a.Exercicio);
                    command.Parameters.AddWithValue("DtVencimento", a.DtVencimento);
                    command.Parameters.AddWithValue("DtInicioVigencia", a.DtInicioVigencia);
                    command.Parameters.AddWithValue("DtTerminoVigencia", a.DtTerminoVigencia);
                    command.Parameters.AddWithValue("CobrancaLiberada", a.CobrancaLiberada);

                    if(_paramDtCobrancaLiberada != "")
                        command.Parameters.AddWithValue("DtCobrancaLiberada", a.DtCobrancaLiberada);

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

                    string log = logRep.SetLogger(className + "/InsertAnuidadeDao",
                      "INSERT", "ANUIDADE", id, _instrucaoSql, _result);
                    //Fim do Log

                    // Inserindo na tabela ANUIDADE_TIPO_PUBLICO:

                    if (a.AnuidadesTiposPublicosDao != null)
                    {
                        foreach (var atp in a.AnuidadesTiposPublicosDao)
                        {
                            atp.AnuidadeId = id;

                            if (atp.AnuidadeTipoPublicoId == 0)
                                _msg2 = this.InsertAnuidadeTipoPublico(atp);
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

                    string log = logRep.SetLogger(className + "/InsertAnuidadeDao",
                        "INSERT", "ANUIDADE", 0, ex.Message, "FALHA");

                    if (ex.Message.Contains("Violation of UNIQUE KEY constraint 'AK_AD_Anuidade_Exercicio'. Cannot insert duplicate key in object 'dbo.AD_Anuidade'"))
                    {
                        _msg = "Atenção: Já há um registro previamente cadastrado para o exercício informado";
                    }
                    else
                    {
                        _msg = "Atenção: Não foi possível gravar o registro";
                    }

                    // throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string UpdateAnuidadeDao(int id, AnuidadeDao a)
        {
            bool _resultado = false;
            string _msg = "";
            string _msg2 = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarAnuidade");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    string _dtCobrancaLiberada = a.DtCobrancaLiberada != null ? "DtCobrancaLiberada = @DtCobrancaLiberada, " : "";

                    command.CommandText = "" +
                        "UPDATE dbo.AD_Anuidade " +
                        "SET Exercicio = @Exercicio, DtVencimento = @DtVencimento, " +
                        "   DtInicioVigencia = @DtInicioVigencia, DtTerminoVigencia = @DtTerminoVigencia, " +
                        "   CobrancaLiberada = @CobrancaLiberada, "+ _dtCobrancaLiberada  +" " +
                        "   Ativo = @Ativo " +
                        "WHERE AnuidadeId = @id";

                    command.Parameters.AddWithValue("Exercicio", a.Exercicio);
                    command.Parameters.AddWithValue("DtVencimento", a.DtVencimento);
                    command.Parameters.AddWithValue("DtInicioVigencia", a.DtInicioVigencia);
                    command.Parameters.AddWithValue("DtTerminoVigencia", a.DtTerminoVigencia);
                    command.Parameters.AddWithValue("CobrancaLiberada", a.CobrancaLiberada);
                    command.Parameters.AddWithValue("Ativo", a.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    if(_dtCobrancaLiberada != "")
                        command.Parameters.AddWithValue("DtCobrancaLiberada", a.DtCobrancaLiberada);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

                    transaction.Commit();

                    // Log do UPDATE PESSOA:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = x > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/UpdateAnuidadeDao",
                        "UPDATE", "ANUIDADE", id, _instrucaoSql, _result);
                    //Fim do Log

                    // Atualizando os valores da anuidade:
                    if (a.AnuidadesTiposPublicosDao != null)
                    {
                        foreach (var atp in a.AnuidadesTiposPublicosDao)
                        {
                            _msg2 = this.UpdateAnuidadeTipoPublico(atp.AnuidadeTipoPublicoId, atp);
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

                    string log = logRep.SetLogger(className + "/UpdateAnuidadeDao",
                        "UPDATE", "ANUIDADE", 0, ex.Message, "FALHA");

                    if (ex.Message.Contains("Violation of UNIQUE KEY constraint 'AK_AD_Anuidade_Exercicio'. Cannot insert duplicate key in object 'dbo.AD_Anuidade'"))
                    {
                        _msg = "Atenção: Já há um registro previamente cadastrado para o exercício informado";
                    }
                    else
                    {
                        _msg = "Atenção: Não foi possível gravar o registro";
                    }

                    // throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public IEnumerable<AnuidadeTipoPublicoDao> GetAnuidadeTipoPublicoDaoByAnuidadeId(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT A.AnuidadeTipoPublicoId, A.AnuidadeId, A.TipoPublicoId,
                    T.Nome as NomeTipoPublico, T.Codigo  
                    FROM dbo.AD_Anuidade_Tipo_Publico A 
                    INNER JOIN dbo.AD_TIPO_PUBLICO T ON A.TipoPublicoId = T.TipoPublicoId    
                    WHERE A.AnuidadeId = @id ORDER BY T.Ordem ASC";

            // Definição do parâmetros da consulta:
            SqlParameter paramId = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(paramId);
            // Fim da definição dos parâmetros

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<AnuidadeTipoPublicoDao> _collection = GetCollection<AnuidadeTipoPublicoDao>(cmd)?.ToList();

            // Log da consulta:
            /* Log comentado para aumentar a performance da consulta de anuidade
            string log = logRep.SetLogger(className + "/GetAnuidadeTipoPublicoDaoByAnuidadeId",
                "SELECT", "ANUIDADE_TIPO_ANUIDADE", 0, query, _collection.Count<AnuidadeTipoPublicoDao>().ToString());*/
            // Fim Log

            if (_collection != null)
            {
                foreach (var atp in _collection)
                {
                    atp.ValoresAnuidades = GetValoresAnuidadesByAnuidadeTipoPublicoId(atp.AnuidadeTipoPublicoId);
                }
            }
            return _collection;
        }

        public IEnumerable<ValorAnuidade> GetValoresAnuidadesByAnuidadeTipoPublicoId(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT ValorAnuidadeId , Valor, TipoAnuidade, AnuidadeTipoPublicoId 
                    FROM dbo.AD_Valor_Anuidade
                    WHERE AnuidadeTipoPublicoId = @id ORDER BY TipoAnuidade ASC ";

            // Definição do parâmetros da consulta:
            SqlParameter pId = new SqlParameter() { ParameterName = "@id", Value = id };
            _parametros.Add(pId);
            // Fim da definição dos parâmetros

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<ValorAnuidade> _collection = GetCollection<ValorAnuidade>(cmd)?.ToList();

            // Log da consulta:
            /* Log comentado para aumentar a performance da consulta de anuidade
            string log = logRep.SetLogger(className + "/GetValoresAnuidadesByAnuidadeTipoPublicoId",
                "SELECT", "VALOR_ANUIDADE", 0, query, _collection.Count<ValorAnuidade>().ToString());*/
            // Fim Log

            return _collection;
        }

        private string InsertAnuidadeTipoPublico(AnuidadeTipoPublicoDao a)
        {
            bool _resultado = false;
            string _msg = "";
            string _msg2 = "";
            Int32 id = 0;
            string _ident = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("IncluirAnuidadeTipoPublico");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Anuidade_Tipo_Publico (AnuidadeId, TipoPublicoId) " +
                        "VALUES (@AnuidadeId, @TipoPublicoId) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("AnuidadeId", a.AnuidadeId);
                    command.Parameters.AddWithValue("TipoPublicoId", a.TipoPublicoId);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    if (id > 0)
                        _ident = _ident.PadLeft(10 - id.ToString().Length, '0') + id.ToString();

                    _msg = id > 0 ? $"{_ident}Inclusão realizada com sucesso" : $"{_ident}Inclusão Não realizada com sucesso";

                    transaction.Commit();

                    // Log da Inserção Anuidade_Tipo_Publico:
                    /*Log comentado para aumentar a performance do cadastro de anuidade:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = id > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/InsertAnuidadeTipoPublico",
                        "INSERT", "ANUIDADE_TIPO_PUBLICO", id, _instrucaoSql, _result);*/
                    //Fim do Log

                    // Inserindo na tabela VALOR_ANUIDADE:
                    if (a.ValoresAnuidades != null)
                    {
                        foreach (var v in a.ValoresAnuidades)
                        {
                            v.AnuidadeTipoPublicoId = id;

                            if(v.ValorAnuidadeId == 0)
                                _msg2 = this.InsertValorAnuidade(v);
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

                    string log = logRep.SetLogger(className + "/InsertAnuidadeTipoPublico",
                        "INSERT", "ANUIDADE_TIPO_PUBLICO", 0, ex.Message, "FALHA");

                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        private string InsertValorAnuidade(ValorAnuidade v)
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
                transaction = connection.BeginTransaction("IncluirValorAnuidade");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Valor_Anuidade (Valor, TipoAnuidade, AnuidadeTipoPublicoId) " +
                        "VALUES (@Valor, @TipoAnuidade, @AnuidadeTipoPublicoId) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Valor",v.Valor);
                    command.Parameters.AddWithValue("TipoAnuidade", v.TipoAnuidade);
                    command.Parameters.AddWithValue("AnuidadeTipoPublicoId", v.AnuidadeTipoPublicoId);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    if (id > 0)
                        _ident = _ident.PadLeft(10 - id.ToString().Length, '0') + id.ToString();

                    _msg = id > 0 ? $"{_ident}Inclusão realizada com sucesso" : $"{_ident}Inclusão Não realizada com sucesso";

                    transaction.Commit();

                    // Log da Inserção VALOR_ANUIDADE:
                    /* Log comentado para aumentar a performance do cadastro de anuidade:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = id > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/InsertValorAnuidade",
                        "INSERT", "VALOR_ANUIDADE", id, _instrucaoSql, _result);*/
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
                        throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/InsertValorAnuidade",
                        "INSERT", "VALOR_ANUIDADE", 0, ex.Message, "FALHA");

                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        private string UpdateAnuidadeTipoPublico(int id, AnuidadeTipoPublico a)
        {
            string _msg = "";

            if (a.ValoresAnuidades != null)
            {
                foreach (var v in a.ValoresAnuidades)
                {
                    _msg = this.UpdateValorAnuidade(v.ValorAnuidadeId, v);
                }
            }
            return _msg;
        }

        private string UpdateValorAnuidade(int id, ValorAnuidade v)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarValorAnuidade");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "UPDATE dbo.AD_Valor_Anuidade " +
                        "SET Valor = @Valor " +
                        "WHERE ValorAnuidadeId = @id";

                    command.Parameters.AddWithValue("Valor", v.Valor);
                    command.Parameters.AddWithValue("id", id);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

                    transaction.Commit();

                    // Log do UPDATE VALOR_ANUIDADE:
                    /* Log comentado para melhorar a performance do cadastro de anuidade
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = x > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/UpdateValorAnuidade",
                        "UPDATE", "VALOR_ANUIDADE", id, _instrucaoSql, _result);*/
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
                        throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/UpdateValorAnuidade",
                        "UPDATE", "VALOR_ANUIDADE", 0, ex.Message, "FALHA");

                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public IEnumerable<TipoPublico> GetTiposPublicosToAnuidade()
        {
            TipoPublicoRepository tp = new TipoPublicoRepository();

            IEnumerable<TipoPublico> _collection = tp.GetAll(true);

            return _collection;
        }

        public IEnumerable<AnuidadeTipoPublicoDao> GetAnuidadeTipoPublicoDaoByAnuidadeIdTipoPublicoId(int id, int tipoPublicoId)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT A.AnuidadeTipoPublicoId, A.AnuidadeId, A.TipoPublicoId,
                    T.Nome as NomeTipoPublico, T.Codigo  
                    FROM dbo.AD_Anuidade_Tipo_Publico A 
                    INNER JOIN dbo.AD_TIPO_PUBLICO T ON A.TipoPublicoId = T.TipoPublicoId    
                    WHERE A.AnuidadeId = @id  
                        AND T.TipoPublicoId = @tipoPublicoId ORDER BY T.Ordem ASC";

            // Definição do parâmetros da consulta:
            SqlParameter paramId = new SqlParameter() { ParameterName = "@id", Value = id };
            SqlParameter paramTipoPublicoId = new SqlParameter() { ParameterName = "@tipoPublicoId", Value = tipoPublicoId };

            _parametros.Add(paramId);
            _parametros.Add(paramTipoPublicoId);
            // Fim da definição dos parâmetros


            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<AnuidadeTipoPublicoDao> _collection = GetCollection<AnuidadeTipoPublicoDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAnuidadeTipoPublicoDaoByAnuidadeIdTipoPublicoId",
                "SELECT", "ANUIDADE_TIPO_ANUIDADE", 0, query, _collection.Count<AnuidadeTipoPublicoDao>().ToString());
            // Fim Log

            if (_collection != null)
            {
                foreach (var atp in _collection)
                {
                    atp.ValoresAnuidades = GetValoresAnuidadesByAnuidadeTipoPublicoId(atp.AnuidadeTipoPublicoId);
                }
            }
            return _collection;
        }

        public AnuidadeDao GetAnuidadeDaoByIdTipoPublicoId(int id, int tipoPublicoId)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT AnuidadeId, Exercicio, 
                        DtVencimento, DtInicioVigencia, DtTerminoVigencia, 
                        CobrancaLiberada, DtCobrancaLiberada, 
                        DtCadastro, Ativo  
                    FROM dbo.AD_Anuidade
                    WHERE AnuidadeId = @id ";

            // Definição do parâmetros da consulta:
            SqlParameter paramId = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(paramId);
            // Fim da definição dos parâmetros

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            AnuidadeDao _anuidadeDao = GetCollection<AnuidadeDao>(cmd)?.FirstOrDefault<AnuidadeDao>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAnuidadeDaoByIdTipoPublicoId",
                "SELECT", "ANUIDADE", id, query, _anuidadeDao != null ? "SUCESSO" : "0");
            // Fim Log

            if (_anuidadeDao != null)
            {
                _anuidadeDao.AnuidadesTiposPublicosDao = GetAnuidadeTipoPublicoDaoByAnuidadeIdTipoPublicoId(id, tipoPublicoId);
            }
            return _anuidadeDao;

        }
    }
}

