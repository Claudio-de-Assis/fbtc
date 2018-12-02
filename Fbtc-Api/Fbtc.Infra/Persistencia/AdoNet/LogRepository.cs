using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

using Fbtc.Infra.Helpers;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;

using prmToolkit.AccessMultipleDatabaseWithAdoNet;
using prmToolkit.AccessMultipleDatabaseWithAdoNet.Enumerators;
using Fbtc.Application.Helper;

namespace Fbtc.Infra.Persistencia.AdoNet
{
    public class LogRepository : AbstractRepository, ILogRepository
    {

        private string query;
        private readonly string strConnSql;

        public LogRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }


        public IEnumerable<Log> GetAll()
        {
            query = @"SELECT LogId, MetodoOrigem, TipoInstrucao,  
                        Entidade, EntidadeId, InstrucaoSQL, Resultado, DtCadastro   
                    FROM dbo.AD_Log 
                    ORDER BY Entidade, EntidadeId ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Log> _collection = GetCollection<Log>(cmd)?.ToList();

            return _collection;
        }

        /// <summary>
        /// Função que registra o log das transações
        /// </summary>
        /// <param name="MetodoOrigem">VARCHAR2(100)</param>
        /// <param name="TipoInstrucao">VARCHAR2(30)</param>
        /// <param name="Entidade">VARCHAR2(100)</param>
        /// <param name="EntidadeId">INTEGER</param>
        /// <param name="InstrucaoSQL">VARCHAR2(2000)</param>
        /// <param name="Resultado">VARCHAR2(2000)</param>
        /// <returns></returns>
        public string SetLogger(string metodoOrigem, string tipoInstrucao, string entidade,
            int? entidadeId, string instrucaoSQL, string resultado)
        {
            string msg = "";

            try
            {
                Log l = new Log()
                {
                    MetodoOrigem = Functions.AjustaTamanhoString(metodoOrigem, 100),
                    TipoInstrucao = Functions.AjustaTamanhoString(tipoInstrucao, 30),
                    Entidade = Functions.AjustaTamanhoString(entidade, 100),
                    EntidadeId = entidadeId,
                    InstrucaoSQL = Functions.AjustaTamanhoString(instrucaoSQL, 2000),
                    Resultado = Functions.AjustaTamanhoString(resultado, 2000)
                };

                msg = this.Save(l);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return msg;
        }

        public string Save(Log l)
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
                transaction = connection.BeginTransaction("IncluirLog");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Log (MetodoOrigem, TipoInstrucao, " +
                        "       Entidade, EntidadeId, InstrucaoSQL, Resultado) " +
                        "VALUES (@MetodoOrigem, @TipoInstrucao, " +
                        "       @Entidade, @EntidadeId, @InstrucaoSQL, @Resultado) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("MetodoOrigem", l.MetodoOrigem);
                    command.Parameters.AddWithValue("TipoInstrucao", l.TipoInstrucao);
                    command.Parameters.AddWithValue("Entidade", l.Entidade);
                    command.Parameters.AddWithValue("EntidadeId", l.EntidadeId);
                    command.Parameters.AddWithValue("InstrucaoSQL", l.InstrucaoSQL);
                    command.Parameters.AddWithValue("Resultado", l.Resultado);

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
    }
}
