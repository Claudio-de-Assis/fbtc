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

namespace Fbtc.Infra.Persistencia.AdoNet
{
    public class AssinaturaEventoRepository : AbstractRepository, IAssinaturaEventoRepository
    {
        private string query;
        private readonly string strConnSql;

        private LogRepository logRep;
        private readonly string className;
        private string _instrucaoSql = "";
        private string _result = "";

        public AssinaturaEventoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");

            className = "AssinaturaEventoRepository";
            logRep = new LogRepository();
        }

        public IEnumerable<AssinaturaEventoDao> FindByFilters(int eventoId, bool? ativo)
        {
            query = @"SELECT AE.AssinaturaEventoId, AE.AssociadoId, AE.ValorEventoPublicoId, AE.PercentualDesconto, 
                        AE.TipoDesconto, AE.DtAssinatura, AE.DtAtualizacao, AE.Ativo, P.Nome as NomePessoa, 
                        P.CPF, TP.Nome as NomeTP, E.Titulo, VEP.Valor  
                    FROM dbo.AD_Assinatura_Evento AE 
		                INNER JOIN dbo.AD_Valor_Evento_Publico VEP ON AE.ValorEventoPublicoId  = VEP.ValorEventoPublicoId  
                        INNER JOIN dbo.AD_Evento E ON VEP.EventoId = E.EventoId 
                        INNER JOIN dbo.AD_Associado ASO ON AE.AssociadoId = ASO.AssociadoId 
                        INNER JOIN dbo.AD_Pessoa P ON ASO.PessoaId = P.PessoaId 
                        INNER JOIN dbo.AD_Tipo_Publico TP ON VEP.TipoPublicoId = TP.TipoPublicoId 
                    WHERE AE.AssinaturaEventoId > 0 ";
            
            if (eventoId > 0)
                query = query + $" AND E.EventoId = {eventoId} ";

            if (ativo != null)
                query = query + $" AND AE.Ativo = '{ativo}' ";

            query = query + " ORDER BY AE.DtAssinatura Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<AssinaturaEventoDao> _collection = GetCollection<AssinaturaEventoDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindByFilters",
                "SELECT", "ASSINATURA_EVENTO", 0, query, _collection.Count<AssinaturaEventoDao>().ToString());
            // Fim Log

            return _collection;
        }

        public IEnumerable<AssinaturaEvento> GetAll()
        {
            query = @"SELECT AE.AssinaturaEventoId, AE.AssociadoId, AE.ValorEventoPublicoId, AE.PercentualDesconto, 
                        AE.TipoDesconto, AE.DtAssinatura, AE.DtAtualizacao, AE.Ativo  
                    FROM dbo.AD_Assinatura_Evento AE
                    ORDER BY AE.AssociadoId, AE.AssinaturaEventoId ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<AssinaturaEvento> _collection = GetCollection<AssinaturaEvento>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAll",
                "SELECT", "ASSINATURA_EVENTO", 0, query, _collection.Count<AssinaturaEvento>().ToString());
            // Fim Log

            return _collection;
        }

        public AssinaturaEventoDao GetAssinaturaEventoById(int id)
        {
            query = @"SELECT AE.AssinaturaEventoId, AE.AssociadoId, AE.ValorEventoPublicoId, AE.PercentualDesconto, 
                        AE.TipoDesconto, AE.DtAssinatura, AE.DtAtualizacao, AE.Ativo, P.Nome as NomePessoa, 
                        P.CPF, TP.Nome as NomeTP, E.Titulo, VEP.Valor 
                    FROM dbo.AD_Assinatura_Evento AE 
                        INNER JOIN dbo.AD_Valor_Evento_Publico VEP ON AE.ValorEventoPublicoId  = VEP.ValorEventoPublicoId  
                        INNER JOIN dbo.AD_Evento E ON VEP.EventoId = E.EventoId 
                        INNER JOIN dbo.AD_Associado ASO ON AE.AssociadoId = ASO.AssociadoId 
                        INNER JOIN dbo.AD_Pessoa P ON ASO.PessoaId = P.PessoaId 
                        INNER JOIN dbo.AD_Tipo_Publico TP ON VEP.TipoPublicoId = TP.TipoPublicoId
                    WHERE AE.AssinaturaEventoId > " + id + " ORDER BY AE.AssociadoId, AE.AssinaturaEventoId ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            AssinaturaEventoDao assinaturaAssociadoDao = GetCollection<AssinaturaEventoDao>(cmd)?.FirstOrDefault<AssinaturaEventoDao>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAssinaturaEventoById",
                "SELECT", "ASSINATURA_EVENTO", id, query, assinaturaAssociadoDao != null ? "SUCESSO" : "0");
            // Fim Log

            return assinaturaAssociadoDao;
        }

        public string Insert(AssinaturaEvento a)
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
                transaction = connection.BeginTransaction("IncluirAssinaturaEvento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Assinatura_Evento (AssociadoId, ValorEventoPublicoId, PercentualDesconto, " +
                        "       TipoDesconto, DtAssinatura, DtAtualizacao ) " +
                        "VALUES (@AssociadoId, @ValorEventoPublicoId, @PercentualDesconto, " +
                        "       @TipoDesconto, @DtAssinatura, @DtAtualizacao) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("AssociadoId", a.AssociadoId);
                    command.Parameters.AddWithValue("ValorEventoPublicoId", a.ValorEventoPublicoId);
                    command.Parameters.AddWithValue("PercentualDesconto", a.PercentualDesconto);
                    command.Parameters.AddWithValue("TipoDesconto", a.TipoDesconto);
                    command.Parameters.AddWithValue("DtAssinatura", DateTime.Now);
                    command.Parameters.AddWithValue("DtAtualizacao", DateTime.Now);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    if (id > 0)
                        _ident = _ident.PadLeft(10 - id.ToString().Length, '0') + id.ToString();

                    _msg = id > 0 ? $"{_ident}Inclusão realizada com sucesso" : $"{_ident}Inclusão Não realizada com sucesso";

                    transaction.Commit();

                    // Log da Inserção ASSINATURA_EVENTO:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = id > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/Insert",
                        "INSERT", "ASSINATURA_EVENTO", id, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR ASSINATURA_EVENTO: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                        //throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/Insert",
                        "INSERT", "ASSINATURA_EVENTO", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR ASSINATURA_EVENTO: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                    // throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string Update(int id, AssinaturaEvento a)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarAssinaturaEvento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "UPDATE dbo.AD_Assinatura_Evento " +
                        "SET PercentualDesconto  = @PercentualDesconto, TipoDesconto = @TipoDesconto, " +
                        "   DtAtualizacao = @DtAtualizacao, Ativo = @Ativo " +
                        "WHERE AssinaturaEventoId = @id";

                    command.Parameters.AddWithValue("PercentualDesconto", a.PercentualDesconto);
                    command.Parameters.AddWithValue("TipoDesconto", a.TipoDesconto);
                    command.Parameters.AddWithValue("DtAtualizacao", DateTime.Now);
                    command.Parameters.AddWithValue("Ativo", a.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

                    transaction.Commit();

                    // Log do UPDATE ASSINATURA_EVENTO:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = x > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "ASSINATURA_EVENTO", id, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR ASSINATURA_EVENTO: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                        // throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "ASSINATURA_EVENTO", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR ASSINATURA_EVENTO: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                    // throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
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

