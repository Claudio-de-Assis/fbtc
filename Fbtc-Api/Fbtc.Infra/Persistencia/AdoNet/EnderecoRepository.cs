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
    public class EnderecoRepository : AbstractRepository, IEnderecoRepository
    {
        private string query;
        private readonly string strConnSql;

        private LogRepository logRep;
        private readonly string className;
        private string _instrucaoSql = "";
        private string _result = "";

        public EnderecoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");

            className = "EnderecoRepository";
            logRep = new LogRepository();
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public string DeleteByPessoaId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Endereco> GetByPessoaId(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pid = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(pid);
            // Fim da definição dos parâmetros

            query = @"SELECT EnderecoId, PessoaId, Logradouro, Numero,  
                        Complemento, Bairro, Cidade, Estado, CEP, TipoEndereco, OrdemEndereco 
                    FROM dbo.AD_Endereco  
                    WHERE PessoaId = @id 
                    ORDER BY OrdemEndereco";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<Endereco> _collection = GetCollection<Endereco>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetByPessoaId",
                "SELECT", "ENDERECO", id, query, _collection.Count<Endereco>().ToString());
            // Fim Log

            return _collection;
        }

        public Endereco GetEnderecoById(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pid = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(pid);
            // Fim da definição dos parâmetros

            query = @"SELECT EnderecoId, PessoaId, Logradouro, Numero,  
                        Complemento, Bairro, Cidade, Estado, CEP, TipoEndereco, OrdemEndereco 
                    FROM dbo.AD_Endereco  
                    WHERE PessoaId = @id 
                    ORDER BY OrdemEndereco";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            Endereco _endereco = GetCollection<Endereco>(cmd)?.FirstOrDefault<Endereco>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetEnderecoById",
                "SELECT", "ENDERECO", id, query, _endereco != null ? "SUCESSO" : "0");
            // Fim Log

            return _endereco;
        }

        public string Insert(Endereco endereco)
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
                transaction = connection.BeginTransaction("IncluirEndereco");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Endereco (PessoaId, Logradouro, Numero, " +  
                        "   Complemento, Bairro, Cidade, Estado, CEP, TipoEndereco, OrdemEndereco) " +
                        "VALUES(@PessoaId, @Logradouro, @Numero, " +
                        "   @Complemento, @Bairro, @Cidade, @Estado, @CEP, @TipoEndereco, @OrdemEndereco) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("PessoaId", endereco.PessoaId );
                    command.Parameters.AddWithValue("Logradouro", endereco.Logradouro);
                    command.Parameters.AddWithValue("Numero", endereco.Numero);
                    command.Parameters.AddWithValue("Complemento", endereco.Complemento);
                    command.Parameters.AddWithValue("Bairro", endereco.Bairro);
                    command.Parameters.AddWithValue("Cidade", endereco.Cidade);
                    command.Parameters.AddWithValue("Estado", endereco.Estado);
                    command.Parameters.AddWithValue("CEP", endereco.Cep);
                    command.Parameters.AddWithValue("TipoEndereco", endereco.TipoEndereco);
                    command.Parameters.AddWithValue("OrdemEndereco", endereco.OrdemEndereco);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    _msg = id > 0 ? "Inclusão Realizada com sucesso" : "Inclusão Não Realizada com sucesso";

                    transaction.Commit();

                    // Log da Inserção PESSOA:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ":" + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = id > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/Insert",
                      "INSERT", "ENDERECO", id, _instrucaoSql, _result);
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

                    string log = logRep.SetLogger(className + "/Insert",
                      "INSERT", "ENDERECO", 0, ex.Message, "FALHA");

                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string Update(int id, Endereco endereco)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarEndereco");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "UPDATE dbo.AD_Endereco " +
                        "SET Logradouro = @Logradouro, Numero = @Numero, " +
                        "   Complemento = @Complemento, Bairro = @Bairro, Cidade = @Cidade, " +
                        "   Estado = @Estado, CEP = @CEP, TipoEndereco = @TipoEndereco, OrdemEndereco = @OrdemEndereco " +
                        "WHERE EnderecoId = @id";

                    command.Parameters.AddWithValue("Logradouro", endereco.Logradouro);
                    command.Parameters.AddWithValue("Numero", endereco.Numero);
                    command.Parameters.AddWithValue("Complemento", endereco.Complemento);
                    command.Parameters.AddWithValue("Bairro", endereco.Bairro);
                    command.Parameters.AddWithValue("Cidade", endereco.Cidade);
                    command.Parameters.AddWithValue("Estado", endereco.Estado);
                    command.Parameters.AddWithValue("CEP", endereco.Cep);
                    command.Parameters.AddWithValue("TipoEndereco", endereco.TipoEndereco.Trim());
                    command.Parameters.AddWithValue("OrdemEndereco", endereco.OrdemEndereco);
                    command.Parameters.AddWithValue("id", id);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização Realizada com sucesso" : "Atualização NÃO Realizada com sucesso";

                    transaction.Commit();


                    // Log do UPDATE PESSOA:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ":" + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = x > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "ENDERECO", id, _instrucaoSql, _result);
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
                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "ENDERECO", 0, ex.Message, "FALHA");

                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public IEnumerable<EstadoEnderecoCepDao> GetAllNomesEstados()
        {
            query = @"SELECT Distinct Estado 
                    FROM dbo.AD_Endereco  
                    ORDER BY Estado";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<EstadoEnderecoCepDao> _collection = GetCollection<EstadoEnderecoCepDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAllNomesEstados",
                "SELECT", "ENDERECO", 0, query, _collection.Count<EstadoEnderecoCepDao>().ToString());
            // Fim Log

            return _collection;
        }

        public IEnumerable<CidadeEnderecoCepDao> GetNomesCidadesByEstado(string nomeEstado)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pnomeEstado = new SqlParameter() { ParameterName = "@nomeEstado", Value = nomeEstado };

            _parametros.Add(pnomeEstado);
            // Fim da definição dos parâmetros


            query = @"SELECT Distinct Cidade 
                    FROM dbo.AD_Endereco 
                    WHERE Estado = @nomeEstado 
                    ORDER BY Cidade";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<CidadeEnderecoCepDao> _collection = GetCollection<CidadeEnderecoCepDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetNomesCidadesByEstado",
                "SELECT", "ENDERECO", 0, query, _collection.Count<CidadeEnderecoCepDao>().ToString());
            // Fim Log

            return _collection;
        }
    }
}
