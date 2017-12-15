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
    public class ColaboradorRepository : AbstractRepository, IColaboradorRepository
    {
        private string query;
        private readonly string strConnSql;

        public ColaboradorRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colaborador> FindByFilters(string nome, string tipoPerfil, bool? ativo)
        {
            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento, P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        C.ColaboradorId, C.TipoPerfil  
                    FROM dbo.AD_Colaborador C 
                    INNER JOIN dbo.AD_Pessoa P on C.PessoaId = P.PessoaId 
                    WHERE P.PessoaId > 0 ";

            if (!string.IsNullOrEmpty(nome))
                query = query + $" AND P.Nome Like '%{nome}%' ";

            if (!string.IsNullOrEmpty(tipoPerfil))
                query = query + $" AND C.TipoPerfil = '{tipoPerfil}' ";

            if (ativo != null)
                query = query + $" AND P.Ativo = '{ativo}' ";

            query = query + " ORDER BY P.Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Colaborador> _collection = GetCollection<Colaborador>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<Colaborador> GetAll()
        {
            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento, P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        C.ColaboradorId, C.TipoPerfil  
                    FROM dbo.AD_Colaborador C 
                    INNER JOIN dbo.AD_Pessoa P on C.PessoaId = P.PessoaId 
                    ORDER BY P.Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Colaborador> _collection = GetCollection<Colaborador>(cmd)?.ToList();

            return _collection;
        }

        public Colaborador GetColaboradorById(int id)
        {
            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento, P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        C.ColaboradorId, C.TipoPerfil  
                    FROM dbo.AD_Colaborador C 
                    INNER JOIN dbo.AD_Pessoa P on C.PessoaId = P.PessoaId 
                    WHERE ColaboradorId= " + id + "";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            Colaborador colaborador = GetCollection<Colaborador>(cmd)?.First();

            return colaborador;
        }

        public string Insert(Colaborador colaborador)
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
                transaction = connection.BeginTransaction("IncluirColaborador");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Inserindo os dados na tabela PESSOA:
                    string _dtNasc = colaborador.DtNascimento != null ? ", DtNascimento " : "";
                    string _paramDtNasc = colaborador.DtNascimento != null ? ", @DtNascimento " : "";

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Pessoa (Nome, EMail, NrCelular, PasswordHash, " +
                        "   DtCadastro " + _dtNasc + ") " +
                        "VALUES(@Nome, @EMail, @NrCelular, @PasswordHash, " +
                        "   @DtCadastro " + _paramDtNasc + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Nome", colaborador.Nome);
                    command.Parameters.AddWithValue("EMail", colaborador.EMail);
                    command.Parameters.AddWithValue("NrCelular", colaborador.NrCelular);
//                  command.Parameters.AddWithValue("NomeFoto", colaborador.NomeFoto);
//                  command.Parameters.AddWithValue("Sexo", colaborador.Sexo);
                    command.Parameters.AddWithValue("PasswordHash", colaborador.PasswordHash);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);

                    if (_dtNasc != "")
                        command.Parameters.AddWithValue("DtNascimento", colaborador.DtNascimento);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    // Inserindo os dados na tabela ASSOCIADO:

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Colaborador (PessoaId, TipoPerfil) " +
                        "VALUES (@PessoaId, @TipoPerfil) ";

                    command.Parameters.AddWithValue("PessoaId", id);
                    command.Parameters.AddWithValue("TipoPerfil", colaborador.TipoPerfil);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Inclusão realiada com sucesso" : "Inclusão Não realiada com sucesso";

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

        public string Update(int id, Colaborador colaborador)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarColaborador");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Atualizando os dados na tabela PESSOA:
                    string _dtNasc = colaborador.DtNascimento != null ? ", DtNascimento = @DtNascimento " : "";

                    command.CommandText = "" +
                        "UPDATE dbo.AD_Pessoa " +
                        "SET Nome = @nome, EMail = @EMail, NrCelular = @NrCelular, PasswordHash = @PasswordHash, " +
                            "Ativo = @Ativo " + _dtNasc +
                        "WHERE PessoaId = @id";

                    command.Parameters.AddWithValue("Nome", colaborador.Nome);
                    command.Parameters.AddWithValue("EMail", colaborador.EMail);
//                  command.Parameters.AddWithValue("NomeFoto", colaborador.NomeFoto);
//                  command.Parameters.AddWithValue("Sexo", colaborador.Sexo);
                    command.Parameters.AddWithValue("NrCelular", colaborador.NrCelular);
                    command.Parameters.AddWithValue("PasswordHash", colaborador.PasswordHash);
                    command.Parameters.AddWithValue("Ativo", colaborador.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    if (_dtNasc != "")
                        command.Parameters.AddWithValue("DtNascimento", colaborador.DtNascimento);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

                    command.CommandText = "" +
                        "UPDATE dbo.AD_Colaborador  " +
                        "SET TipoPerfil = @TipoPerfil " +
                        "WHERE PessoaId = @id";

                    command.Parameters.AddWithValue("TipoPerfil", colaborador.TipoPerfil);

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
    }
}
