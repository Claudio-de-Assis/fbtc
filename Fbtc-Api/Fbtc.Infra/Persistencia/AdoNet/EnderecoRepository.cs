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
    public class EnderecoRepository : AbstractRepository, IEnderecoRepository
    {
        private string query;
        private readonly string strConnSql;

        public EnderecoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
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
            query = @"SELECT EnderecoId, PessoaId, Logradouro, Numero,  
                        Complemento, Bairro, Cidade, Estado, CEP, TipoEndereco 
                    FROM dbo.AD_Endereco  
                    WHERE PessoaId = " + id + " ORDER BY TipoEndereco";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Endereco> _collection = GetCollection<Endereco>(cmd)?.ToList();

            return _collection;
        }

        public Endereco GetEnderecoById(int id)
        {
            query = @"SELECT EnderecoId, PessoaId, Logradouro, Numero,  
                        Complemento, Bairro, Cidade, Estado, CEP, TipoEndereco 
                    FROM dbo.AD_Endereco  
                    WHERE PessoaId = " + id + " ORDER BY TipoEndereco";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            Endereco _endereco = GetCollection<Endereco>(cmd)?.First();

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
                        "   Complemento, Bairro, Cidade, Estado, CEP, TipoEndereco) " +
                        "VALUES(@PessoaId, @Logradouro, @Numero, " +
                        "   @Complemento, @Bairro, @Cidade, @Estado, @CEP, @TipoEndereco) " +
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

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    _msg = id > 0 ? "Inclusão realiada com sucesso" : "Inclusão Não realiada com sucesso";

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
                connection.Close();
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
                        "   Estado = @Estado, CEP = @CEP, TipoEndereco = @TipoEndereco " +
                        "WHERE EnderecoId = @id";

                    command.Parameters.AddWithValue("Logradouro", endereco.Logradouro);
                    command.Parameters.AddWithValue("Numero", endereco.Numero);
                    command.Parameters.AddWithValue("Complemento", endereco.Complemento);
                    command.Parameters.AddWithValue("Bairro", endereco.Bairro);
                    command.Parameters.AddWithValue("Cidade", endereco.Cidade);
                    command.Parameters.AddWithValue("Estado", endereco.Estado);
                    command.Parameters.AddWithValue("CEP", endereco.Cep);
                    command.Parameters.AddWithValue("TipoEndereco", endereco.TipoEndereco);
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
                connection.Close();
            }
            return _msg;
        }

        public IEnumerable<EstadoEnderecoCepDAO> GetAllNomesEstados()
        {
            query = @"SELECT Distinct Estado 
                    FROM dbo.AD_Endereco  
                    ORDER BY Estado";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<EstadoEnderecoCepDAO> _collection = GetCollection<EstadoEnderecoCepDAO>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<CidadeEnderecoCepDAO> GetNomesCidadesByEstado(string nomeEstado)
        {
            query = @"SELECT Distinct Cidade 
                    FROM dbo.AD_Endereco 
                    WHERE Estado = "+ nomeEstado + " ORDER BY Cidade";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<CidadeEnderecoCepDAO> _collection = GetCollection<CidadeEnderecoCepDAO>(cmd)?.ToList();

            return _collection;
        }
    }
}
