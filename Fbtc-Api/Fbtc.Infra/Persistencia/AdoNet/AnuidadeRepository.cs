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
    public class AnuidadeRepository : AbstractRepository, IAnuidadeRepository
    {
        private string query;
        private readonly string strConnSql;

        public AnuidadeRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Anuidade> GetAll()
        {
            query = @"SELECT AnuidadeId, Codigo, DtCadastro, Ativo   
                    FROM dbo.AD_Anuidade
                    ORDER BY Codigo Desc";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Anuidade> _collection = GetCollection<Anuidade>(cmd)?.ToList();

            return _collection;
        }

        public Anuidade GetAnuidadeById(int id)
        {
            query = @"SELECT AnuidadeId, Codigo, DtCadastro, Ativo  
                    FROM dbo.AD_Anuidade
                    WHERE AnuidadeId = " + id + "";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            Anuidade _anuidade = GetCollection<Anuidade>(cmd)?.First();

            return _anuidade;
        }

        public AnuidadeDao GetAnuidadeDaoById(int id)
        {
            query = @"SELECT AnuidadeId, Codigo, DtCadastro, Ativo  
                    FROM dbo.AD_Anuidade
                    WHERE AnuidadeId = " + id + "";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            AnuidadeDao _anuidadeDao = GetCollection<AnuidadeDao>(cmd)?.First();

            if(_anuidadeDao != null)
            {
                TipoPublicoRepository tp = new TipoPublicoRepository();

                _anuidadeDao.TiposPublicosValorsAnuidadesDao = tp.GetTipoPublicoValorByAnuidadeId(id);
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
                        "INSERT into dbo.AD_Anuidade (Codigo) " +
                        "VALUES (@Codigo) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Codigo", a.Codigo);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    if (id > 0)
                        _ident = _ident.PadLeft(10 - id.ToString().Length, '0') + id.ToString();

                    _msg = id > 0 ? $"{_ident}Inclusão realizada com sucesso" : $"{_ident}Inclusão Não realizada com sucesso";

                    transaction.Commit();
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
                        "SET Codigo = @Codigo, Ativo = @Ativo " +
                        "WHERE AnuidadeId = @id";

                    command.Parameters.AddWithValue("Codigo", a.Codigo);
                    command.Parameters.AddWithValue("Ativo", a.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realiada com sucesso" : "Atualização NÃO realiada com sucesso";

                    transaction.Commit();
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
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public IEnumerable<Anuidade> FindByFilters(int codigo, bool? ativo)
        {
            query = @"SELECT AnuidadeId, Codigo, DtCadastro, Ativo   
                    FROM dbo.AD_Anuidade
                    WHERE AnuidadeId > 0 ";                    

            if (codigo > 0)
                query = query + $" AND Codigo = {codigo} ";

            if (ativo != null)
                query = query + $" AND Ativo = '{ativo}' ";

            query = query + " ORDER BY Codigo Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Anuidade> _collection = GetCollection<Anuidade>(cmd)?.ToList();

            return _collection;
        }

        public string InsertAnuidadeDao(AnuidadeDao a)
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
                        "INSERT into dbo.AD_Anuidade (Codigo) " +
                        "VALUES (@Codigo) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Codigo", a.Codigo);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    if (id > 0)
                        _ident = _ident.PadLeft(10 - id.ToString().Length, '0') + id.ToString();

                    _msg = id > 0 ? $"{_ident}Inclusão realizada com sucesso" : $"{_ident}Inclusão Não realizada com sucesso";

                    transaction.Commit();

                    // Inserindo os valores na tabela
                    List<string> lstResultados = new List<string>();

                    if (a.TiposPublicosValorsAnuidadesDao != null && id > 0)
                    {
                        foreach (var valor in a.TiposPublicosValorsAnuidadesDao)
                        {
                            valor.AnuidadeId = id;
                            lstResultados.Add(this.InsertValorAnuidade(valor));
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
                        "SET Codigo = @Codigo, Ativo = @Ativo " +
                        "WHERE AnuidadeId = @id";

                    command.Parameters.AddWithValue("Codigo", a.Codigo);
                    command.Parameters.AddWithValue("Ativo", a.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realiada com sucesso" : "Atualização NÃO realiada com sucesso";

                    transaction.Commit();

                    // Atualizando os valores na tabela
                    List<string> lstResultados = new List<string>();

                    if (a.TiposPublicosValorsAnuidadesDao != null && x > 0)
                    {
                        foreach (var valor in a.TiposPublicosValorsAnuidadesDao)
                        {
                            lstResultados.Add(this.UpdateValorAnuidade(valor.ValorAnuidadePublicoId, valor));
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
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        private string UpdateValorAnuidade(int id, TipoPublicoValorAnuidadeDao tipoPublicoValorAnuidadeDao)
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
                    // Atualizando os dados na tabela:

                    command.CommandText = "" +
                        "UPDATE dbo.AD_Valor_Anuidade_Publico " +
                        "SET Valor = @Valor  " +
                        "WHERE ValorAnuidadePublicoId = @id";

                    command.Parameters.AddWithValue("Valor", tipoPublicoValorAnuidadeDao.Valor);
                    command.Parameters.AddWithValue("id", id);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

                    _msg = _resultado ? "Atualização realiada com sucesso" : "Atualização NÃO realiada com sucesso";

                    transaction.Commit();
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
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        private string InsertValorAnuidade(TipoPublicoValorAnuidadeDao tipoPublicoValorAnuidadeDao)
        {
            bool _resultado = false;
            string _msg = "";
            Int32 id = 0;

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
                    // Inserindo os dados na tabela:
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Valor_Anuidade_Publico (AnuidadeId, Valor, TipoPublicoId) " +
                        "VALUES(@AnuidadeId, @Valor, @TipoPublicoId) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("AnuidadeId", tipoPublicoValorAnuidadeDao.AnuidadeId);
                    command.Parameters.AddWithValue("Valor", tipoPublicoValorAnuidadeDao.Valor);
                    command.Parameters.AddWithValue("TipoPublicoId", tipoPublicoValorAnuidadeDao.TipoPublicoId);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    _msg = _resultado ? "Inclusão realiada com sucesso" : "Inclusão Não realiada com sucesso";

                    transaction.Commit();
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
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }
    }
}

