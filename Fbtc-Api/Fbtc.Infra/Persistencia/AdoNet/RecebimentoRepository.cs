﻿using System;
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
    public class RecebimentoRepository : AbstractRepository, IRecebimentoRepository
    {
        private string query;
        private readonly string strConnSql;

        public RecebimentoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recebimento> FindByFilters(string objetivoPagamento, string nome, string cpf, string crp, 
            string crm, string status, int ano, int mes, bool ativo, int eventoId, int tipoPublicoId)
        {
            query = @"SELECT R.RecebimentoId, R.AssociadoId, R.ValorEventoPublicoId, R.ValorAnuidadePublicoId, 
                        R.AssociadoIsentoId, R.ObjetivoPagamento, R.DtCadastro, R.DtVencimento, R.DtPagamento, 
                        R.DtNotificacao, R.StatusPagamento, R.FormaPagamento, R.NrDocCobranca, R.ValorPago, 
                        R.Observacao, R.TokenPagamento, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                        INNER JOIN dbo.AD_Associado A ON R.AssociadoId = A.AssociadoId 
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId ";

            if (eventoId != 0)
                query = query + " " +
                    "INNER JOIN dbo.AD_Valor_Evento_Publico VEP ON R.ValorEventoPublicoId = VEP.ValorEventoPublicoId " +
                    "INNER JOIN dbo.AD_Evento E ON VEP.EventoId = E.EventoId ";

            query = query + " WHERE R.ObjetivoPagamento = '" + objetivoPagamento + "' ";

            if (!string.IsNullOrEmpty(nome))
                query = query + $" AND P.Nome Like '%{nome}%' ";

            if (!string.IsNullOrEmpty(cpf))
                query = query + $" AND P.CPF = '{cpf}' ";

            if (!string.IsNullOrEmpty(crp))
                query = query + $" AND A.CRP = '{crp}' ";

            if (!string.IsNullOrEmpty(crm))
                query = query + $" AND A.CRM = '{crm}' ";

            if (!string.IsNullOrEmpty(status))
                query = query + $" AND R.StatusPagamento = '{status}' ";

            if (ano != 0)
                query = query + $" AND Year(R.DtVencimento) = {ano} ";

            if (mes != 0)
                query = query + $" AND Month(R.DtVencimento) = {mes} ";

            if (tipoPublicoId != 0)
                query = query + $" AND A.TipoPublicoId = {tipoPublicoId} ";

            if (eventoId != 0)
                query = query + $" AND E.EventoId = {eventoId} ";

            query = query + $" AND R.Ativo = '{ativo}'  Order by P.Nome "; 

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<Recebimento> GetAll(string objetivoPagamento)
        {
            query = @"SELECT R.RecebimentoId, R.AssociadoId, R.ValorEventoPublicoId, R.ValorAnuidadePublicoId, 
                        R.AssociadoIsentoId, R.ObjetivoPagamento, R.DtCadastro, R.DtVencimento, R.DtPagamento, 
                        R.DtNotificacao, R.StatusPagamento, R.FormaPagamento, R.NrDocCobranca, R.ValorPago, 
                        R.Observacao, R.TokenPagamento, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                        INNER JOIN dbo.AD_Associado A ON R.AssociadoId = A.AssociadoId 
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId 
                    WHERE R.ObjetivoPagamento = '" + objetivoPagamento + "' Order by P.Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<Recebimento> GetRecebimentoByAnuidadeId(int id)
        {
            query = @"SELECT R.RecebimentoId, R.AssociadoId, R.ValorEventoPublicoId, R.ValorAnuidadePublicoId, 
                        R.AssociadoIsentoId, R.ObjetivoPagamento, R.DtCadastro, R.DtVencimento, R.DtPagamento, 
                        R.DtNotificacao, R.StatusPagamento, R.FormaPagamento, R.NrDocCobranca, R.ValorPago, 
                        R.Observacao, R.TokenPagamento, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                        INNER JOIN dbo.AD_Associado A ON R.AssociadoId = A.AssociadoId 
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId 
                        INNER JOIN dbo.AD_Valor_Anuidade_Publico V ON R.ValorAnuidadePublicoId = V.ValorAnuidadePublicoId 
                        INNER JOIN dbo.AD_Anuidade AN ON V.AnuidadeId = AN.AnuidadeId 
                    WHERE AN.AnuidadeId = " + id + " Order by P.Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<Recebimento> GetRecebimentoByEventoId(int id)
        {
            query = @"SELECT R.RecebimentoId, R.AssociadoId, R.ValorEventoPublicoId, R.ValorAnuidadePublicoId, 
                        R.AssociadoIsentoId, R.ObjetivoPagamento, R.DtCadastro, R.DtVencimento, R.DtPagamento, 
                        R.DtNotificacao, R.StatusPagamento, R.FormaPagamento, R.NrDocCobranca, R.ValorPago, 
                        R.Observacao, R.TokenPagamento, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                        INNER JOIN dbo.AD_Associado A ON R.AssociadoId = A.AssociadoId 
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId 
                        INNER JOIN dbo.AD_Valor_Evento_Publico V ON R.ValorEventoPublicoId = V.ValorEventoPublicoId 
                        INNER JOIN dbo.AD_Evento E ON V.EventoId = E.EventoId 
                    WHERE E.EventoId = " + id + " Order by P.Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();

            return _collection;
        }

        public Recebimento GetRecebimentoById(int id)
        {
            query = @"SELECT R.RecebimentoId, R.AssociadoId, R.ValorEventoPublicoId, R.ValorAnuidadePublicoId, 
                        R.AssociadoIsentoId, R.ObjetivoPagamento, R.DtCadastro, R.DtVencimento, R.DtPagamento, 
                        R.DtNotificacao, R.StatusPagamento, R.FormaPagamento, R.NrDocCobranca, R.ValorPago, 
                        R.Observacao, R.TokenPagamento, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                        INNER JOIN dbo.AD_Associado A ON R.AssociadoId = A.AssociadoId 
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId 
                    WHERE R.RecebimentoId = " + id + " ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            Recebimento _recebimento = GetCollection<Recebimento>(cmd)?.First();

            return _recebimento;
        }

        public IEnumerable<Recebimento> GetRecebimentoByPessoaId(string objetivoPagamento, int id)
        {
            query = @"SELECT R.RecebimentoId, R.AssociadoId, R.ValorEventoPublicoId, R.ValorAnuidadePublicoId, 
                        R.AssociadoIsentoId, R.ObjetivoPagamento, R.DtCadastro, R.DtVencimento, R.DtPagamento, 
                        R.DtNotificacao, R.StatusPagamento, R.FormaPagamento, R.NrDocCobranca, R.ValorPago, 
                        R.Observacao, R.TokenPagamento, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                        INNER JOIN dbo.AD_Associado A ON R.AssociadoId = A.AssociadoId 
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId 
                    WHERE P.PessoaId = " + id + " AND R.ObjetivoPagamento = '"+ objetivoPagamento  + "' Order by P.Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();

            return _collection;
        }

        public string Insert(Recebimento r)
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
                transaction = connection.BeginTransaction("IncluirRecebimento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    string _atributos = "";
                    string _values = "";

                    // Inserindo os dados na tabela:
                    if (r.AssociadoId != null) {
                        _atributos = _atributos + ", AssociadoId ";
                        _values = _values + ", @AssociadoId ";
                        command.Parameters.AddWithValue("AssociadoId", r.AssociadoId);
                    }

                    if (r.AssociadoIsentoId != null) {
                        _atributos = _atributos + ", AssociadoIsentoId ";
                        _values = _values + ", @AssociadoIsentoId ";
                        command.Parameters.AddWithValue("AssociadoIsentoId", r.AssociadoIsentoId);
                    }

                    if (r.ValorAnuidadePublicoId != null) {
                        _atributos = _atributos + ", ValorAnuidadePublicoId ";
                        _values = _values + ", @ValorAnuidadePublicoId ";
                        command.Parameters.AddWithValue("ValorAnuidadePublicoId", r.ValorAnuidadePublicoId);
                    }

                    if (r.ValorEventoPublicoId != null) {
                        _atributos = _atributos + ", ValorEventoPublicoId ";
                        _values = _values + ", @ValorAnuidadePublicoId ";
                        command.Parameters.AddWithValue("ValorAnuidadePublicoId", r.ValorAnuidadePublicoId);
                    }

                    if (r.DtNotificacao != null) {
                        _atributos = _atributos + ", DtNotificacao ";
                        _values = _values + ", @DtNotificacao ";
                        command.Parameters.AddWithValue("DtNotificacao", r.DtNotificacao);
                    }

                    if (r.DtPagamento != null) {
                        _atributos = _atributos + ", DtPagamento ";
                        _values = _values + ", @DtPagamento ";
                        command.Parameters.AddWithValue("DtPagamento", r.DtPagamento);
                    }

                    if (r.DtVencimento != null) {
                        _atributos = _atributos + ", DtVencimento ";
                        _values = _values + ", @DtVencimento ";
                        command.Parameters.AddWithValue("DtVencimento", r.DtVencimento);
                    }

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Recebimento (ObjetivoPagamento, " +
                        "   StatusPagamento, FormaPagamento, NrDocCobranca, ValorPago, " +
                        "   Observacao, TokenPagamento, Ativo, " +
                        "   DtCadastro " + _atributos + ") " +
                        "VALUES(@ObjetivoPagamento, " +
                        "   @StatusPagamento, @FormaPagamento, @NrDocCobranca, @ValorPago, " +
                        "   @Observacao, @TokenPagamento, @Ativo, " +
                        "   @DtCadastro" + _values + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("ObjetivoPagamento", r.ObjetivoPagamento);
                    command.Parameters.AddWithValue("StatusPagamento", r.StatusPagamento);
                    command.Parameters.AddWithValue("FormaPagamento", r.FormaPagamento);
                    command.Parameters.AddWithValue("NrDocCobranca", r.NrDocCobranca);
                    command.Parameters.AddWithValue("ValorPago", r.ValorPago);
                    command.Parameters.AddWithValue("Observacao", r.Observacao);
                    command.Parameters.AddWithValue("TokenPagamento", r.TokenPagamento);
                    command.Parameters.AddWithValue("Ativo", r.Ativo);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);

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

        public string Update(int id, Recebimento r)
        {
            bool _resultado = false;
            string _msg = "";
            
            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarRecebimento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    string _atributos = "";

                    // Inserindo os dados na tabela:
                    if (r.AssociadoId != null) {
                        _atributos = _atributos + ", AssociadoId = @AssociadoId ";
                        command.Parameters.AddWithValue("AssociadoId", r.AssociadoId);
                    }

                    if (r.AssociadoIsentoId != null) {
                        _atributos = _atributos + ", AssociadoIsentoId = @AssociadoIsentoId ";
                        command.Parameters.AddWithValue("AssociadoIsentoId", r.AssociadoIsentoId);
                    }

                    if (r.ValorAnuidadePublicoId != null) {
                        _atributos = _atributos + ", ValorAnuidadePublicoId = @ValorAnuidadePublicoId ";
                        command.Parameters.AddWithValue("ValorAnuidadePublicoId", r.ValorAnuidadePublicoId);
                    }

                    if (r.ValorEventoPublicoId != null) {
                        _atributos = _atributos + ", ValorEventoPublicoId = @ValorAnuidadePublicoId ";
                        command.Parameters.AddWithValue("ValorAnuidadePublicoId", r.ValorAnuidadePublicoId);
                    }

                    if (r.DtNotificacao != null) {
                        _atributos = _atributos + ", DtNotificacao = @DtNotificacao ";
                        command.Parameters.AddWithValue("DtNotificacao", r.DtNotificacao);
                    }

                    if (r.DtPagamento != null) {
                        _atributos = _atributos + ", DtPagamento = @DtPagamento ";
                        command.Parameters.AddWithValue("DtPagamento", r.DtPagamento);
                    }

                    if (r.DtVencimento != null) {
                        _atributos = _atributos + ", DtVencimento = @DtVencimento ";
                        command.Parameters.AddWithValue("DtVencimento", r.DtVencimento);
                    }

                    command.CommandText = "" +
                        "Update dbo.AD_Recebimento  Set ObjetivoPagamento = @ObjetivoPagamento, " +
                        "   StatusPagamento = @StatusPagamento, FormaPagamento = @FormaPagamento, " +
                        "   NrDocCobranca = @NrDocCobranca, ValorPago = @ValorPago, " +
                        "   Observacao = @Observacao, TokenPagamento = @TokenPagamento, Ativo = @Ativo " +
                        "   " + _atributos + 
                        "WHERE RecebimentoId = @id ";

                    command.Parameters.AddWithValue("ObjetivoPagamento", r.ObjetivoPagamento);
                    command.Parameters.AddWithValue("StatusPagamento", r.StatusPagamento);
                    command.Parameters.AddWithValue("FormaPagamento", r.FormaPagamento);
                    command.Parameters.AddWithValue("NrDocCobranca", r.NrDocCobranca);
                    command.Parameters.AddWithValue("ValorPago", r.ValorPago);
                    command.Parameters.AddWithValue("Observacao", r.Observacao);
                    command.Parameters.AddWithValue("TokenPagamento", r.TokenPagamento);
                    command.Parameters.AddWithValue("Ativo", r.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

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