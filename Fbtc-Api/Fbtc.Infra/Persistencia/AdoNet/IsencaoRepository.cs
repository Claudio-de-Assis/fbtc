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
    public class IsencaoRepository : AbstractRepository, IIsencaoRepository
    {
        private string query;
        private readonly string strConnSql;

        public IsencaoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Isencao> FindByFilters(string tipoIsencao, string nomeAssociado, string descricao, int ano, int eventoId)
        {
            query = @"SELECT I.IsencaoId, I.AnuidadeId, I.EventoId, 
                        I.Descricao, I.DtAta, I.AnoEvento, I.TipoIsencao, I.Ativo 
                    FROM dbo.AD_Isencao I 
                    LEFT JOIN dbo.AD_Associado_Isento AI ON I.IsencaoId = AI.IsencaoId
                    LEFT JOIN dbo.AD_Associado A ON AI.AssociadoId = A.AssociadoId
					LEFT JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                    WHERE I.IsencaoId > 0 AND I.TipoIsencao = '"+ tipoIsencao + "' ";

            if (!string.IsNullOrEmpty(nomeAssociado))
                query = query + $" AND P.Nome Like '%{nomeAssociado}%' ";

            if (!string.IsNullOrEmpty(descricao))
                query = query + $" AND I.Descricao like '%{descricao}%' ";

            if (ano != 0)
                query = query + $" AND I.AnoEvento = '{ano}' ";

            if (eventoId != 0)
                query = query + $" AND I.EventoId = {eventoId} ";

             query = query + " ORDER BY I.Descricao ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Isencao> _collection = GetCollection<Isencao>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<Isencao> GetAll(string tipoIsencao)
        {
            query = @"SELECT I.IsencaoId, I.AnuidadeId, I.EventoId, 
                        I.Descricao, I.DtAta, I.AnoEvento, I.TipoIsencao, I.Ativo 
                    FROM dbo.AD_Isencao I 
                    WHERE I.TipoIsencao = '" + tipoIsencao  + "' ORDER BY I.Descricao ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Isencao> _collection = GetCollection<Isencao>(cmd)?.ToList();

            return _collection;
        }

        public Isencao GetIsencaoById(int id)
        {
            query = @"SELECT I.IsencaoId, I.AnuidadeId, I.EventoId, 
                        I.Descricao, I.DtAta, I.AnoEvento, I.TipoIsencao, I.Ativo 
                    FROM dbo.AD_Isencao I 
                    WHERE I.IsencaoId = " + id + " ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            Isencao _collection = GetCollection<Isencao>(cmd)?.First();

            return _collection;
        }

        public string Insert(Isencao i)
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
                transaction = connection.BeginTransaction("IncluirIsencao");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    string _atributos = "";
                    string _values = "";

                    // Inserindo os dados na tabela:
                    if (i.AnuidadeId != null) {
                        _atributos = _atributos + ", AnuidadeId ";
                        _values = _values + ", @AnuidadeId ";
                        command.Parameters.AddWithValue("AnuidadeId", i.AnuidadeId);
                    }

                    if (i.EventoId != null) {
                        _atributos = _atributos + ", EventoId ";
                        _values = _values + ", @EventoId ";
                        command.Parameters.AddWithValue("EventoId", i.EventoId);
                    }

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Isencao (Descricao, " +
                        "   DtAta, AnoEvento, TipoIsencao, Ativo " + _atributos + ") " +
                        "VALUES(@Descricao, " +
                        "   @DtAta, @AnoEvento, @TipoIsencao, @Ativo " + _values + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Descricao", i.Descricao);
                    command.Parameters.AddWithValue("DtAta", i.DtAta);
                    command.Parameters.AddWithValue("AnoEvento", i.AnoEvento);
                    command.Parameters.AddWithValue("TipoIsencao", i.TipoIsencao);
                    command.Parameters.AddWithValue("Ativo", i.Ativo);

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
                connection.Close();
            }
            return _msg;
        }

        public string Update(int id, Isencao i)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarIsencao");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    string _atributos = "";

                    // Inserindo os dados na tabela:
                    if (i.AnuidadeId != null) {
                        _atributos = _atributos + ", AnuidadeId = @AnuidadeId ";
                        command.Parameters.AddWithValue("AnuidadeId", i.AnuidadeId);
                    }

                    if (i.EventoId != null) {
                        _atributos = _atributos + ", EventoId = @EventoId ";
                        command.Parameters.AddWithValue("EventoId", i.EventoId);
                    }

                    command.CommandText = "" +
                        "Update dbo.AD_Isencao Set Descricao = @Descricao, " +
                        "   DtAta = @DtAta, AnoEvento = @AnoEvento, " +
                        "   TipoIsencao = @TipoIsencao, Ativo = @Ativo " +
                        "   " + _atributos +
                        "WHERE IsencaoId = @id ";

                    command.Parameters.AddWithValue("Descricao", i.Descricao);
                    command.Parameters.AddWithValue("DtAta", i.DtAta);
                    command.Parameters.AddWithValue("AnoEvento", i.AnoEvento);
                    command.Parameters.AddWithValue("TipoIsencao", i.TipoIsencao);
                    command.Parameters.AddWithValue("Ativo", i.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = _resultado ? "Atualização realiada com sucesso" : "Atualização NÃO realiada com sucesso";

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
    }
}
