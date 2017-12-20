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
    public class EventoRepository : AbstractRepository, IEventoRepository
    {
        private string query;
        private readonly string strConnSql;

        public EventoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Evento> FindByFilters(string titulo, int ano, string tipoEvento)
        {
            query = @"SELECT EventoId, Titulo, Descricao, Codigo, DtInicio, DtTermino, 
                        DtTerminoInscricao, TipoEvento, AceitaIsencaoAta, Ativo, NomeFoto 
                    FROM dbo.AD_Evento 
                    WHERE EventoId > 0 ";

            if (!string.IsNullOrEmpty(titulo))
                query = query + $" AND Titulo Like '%{titulo}%' ";

            if (ano != 0)
                query = query + $" AND year(DtInicio) = {ano} ";

            if (!string.IsNullOrEmpty(tipoEvento))
                query = query + $" AND TipoEvento = '{tipoEvento}' ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Evento> _collection = GetCollection<Evento>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<Evento> GetAll()
        {
            query = @"SELECT EventoId, Titulo, Descricao, Codigo, DtInicio, DtTermino, 
                        DtTerminoInscricao, TipoEvento, AceitaIsencaoAta, Ativo, NomeFoto 
                    FROM dbo.AD_Evento 
                    ORDER BY Titulo ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Evento> _collection = GetCollection<Evento>(cmd)?.ToList();

            return _collection;
        }

        public Evento GetEventoById(int id)
        {
            query = @"SELECT EventoId, Titulo, Descricao, Codigo, DtInicio, DtTermino, 
                        DtTerminoInscricao, TipoEvento, AceitaIsencaoAta, Ativo, NomeFoto 
                    FROM dbo.AD_Evento 
                    WHERE EventoId = " + id + "";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            Evento evento = GetCollection<Evento>(cmd)?.First();

            return evento;
        }

        public string Insert(Evento evento)
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
                transaction = connection.BeginTransaction("IncluirEvento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Inserindo os dados na tabela:
                    string _dtTermInscr = evento.DtTerminoInscricao != null ? ", DtTerminoInscricao " : "";
                    string _paramDtTermInscr = evento.DtTerminoInscricao != null ? ", @DtTerminoInscricao " : "";

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Evento (Titulo, Descricao, Codigo, DtInicio, DtTermino, " +
                        "   TipoEvento, AceitaIsencaoAta, NomeFoto " + _dtTermInscr + ") " +
                        "VALUES(@Titulo, @Descricao, @Codigo, @DtInicio, @DtTermino, " +
                        "   @TipoEvento, @AceitaIsencaoAta, @NomeFoto " + _paramDtTermInscr + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Titulo", evento.Titulo);
                    command.Parameters.AddWithValue("Descricao", evento.Descricao);
                    command.Parameters.AddWithValue("Codigo", evento.Codigo);
                    command.Parameters.AddWithValue("DtInicio", evento.DtInicio);
                    command.Parameters.AddWithValue("DtTermino", evento.DtTermino);
                    command.Parameters.AddWithValue("TipoEvento", evento.TipoEvento);
                    command.Parameters.AddWithValue("AceitaIsencaoAta", evento.AceitaIsencaoAta);
                    command.Parameters.AddWithValue("NomeFoto", evento.NomeFoto);

                    if (_dtTermInscr != "")
                        command.Parameters.AddWithValue("DtTerminoInscricao", evento.DtTerminoInscricao);

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

        public string Update(int id, Evento evento)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarEvento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Atualizando os dados na tabela PESSOA:
                    string _dtTermInscr = evento.DtTerminoInscricao != null ? ", DtTerminoInscricao = @DtTerminoInscricao " : "";

                    command.CommandText = "" +
                        "UPDATE dbo.AD_Evento " +
                        "SET Titulo = @Titulo, Descricao = @Descricao, Codigo = @Codigo, " +
                            "DtInicio = @DtInicio, DtTermino = @DtTermino, TipoEvento = @TipoEvento, " +
                            "AceitaIsencaoAta = @AceitaIsencaoAta, NomeFoto = @NomeFoto, Ativo = @Ativo " + _dtTermInscr +
                        "WHERE EventoId = @id";

                    command.Parameters.AddWithValue("Titulo", evento.Titulo);
                    command.Parameters.AddWithValue("Descricao", evento.Descricao);
                    command.Parameters.AddWithValue("Codigo", evento.Codigo);
                    command.Parameters.AddWithValue("DtInicio", evento.DtInicio);
                    command.Parameters.AddWithValue("DtTermino", evento.DtTermino);
                    command.Parameters.AddWithValue("TipoEvento", evento.TipoEvento);
                    command.Parameters.AddWithValue("AceitaIsencaoAta", evento.AceitaIsencaoAta);
                    command.Parameters.AddWithValue("NomeFoto", evento.NomeFoto);
                    command.Parameters.AddWithValue("Ativo", evento.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    if (_dtTermInscr != "")
                        command.Parameters.AddWithValue("DtTerminoInscricao", evento.DtTerminoInscricao);

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
                connection.Close();
            }
            return _msg;
        }
    }
}
