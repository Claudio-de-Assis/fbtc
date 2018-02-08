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
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("DeleteRecebimento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "DELETE " +
                        "From dbo.AD_Recebimento " +
                        "WHERE RecebimentoId = @RecebimentoId ";

                    command.Parameters.AddWithValue("@RecebimentoId", id);

                    int i = command.ExecuteNonQuery();

                    _msg = i > 0 ? "Exclusão realizada com sucesso" : "Exclusão NÃO realizada com sucesso";

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

        IEnumerable<RecebimentoAssociadoDao> IRecebimentoRepository.FindByEventoIdFilters(int eventoId, string nome, string cpf, string crp, string crm, int statusPS, int ano, int mes, bool? ativo, string tipoEvento, int tipoPublicoId)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<RecebimentoAssociadoDao> FindAnuidadeByFilters(string nome, string cpf, string crp, 
            string crm, int statusPS, int ano, int mes, bool? ativo, int tipoPublicoId)
        {
            query = @"SELECT AssociadoId, Titulo, Anuidade , Nome, CPF, NomeTP, RecebimentoId, 
                        StatusPS, LastEventDatePS, AtivoRec,
	                    IsencaoId, TipoPublicoId FROM (
                    SELECT	A.AssociadoId, '' as Titulo, AN.Codigo as Anuidade , P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.StatusPS, RE.LastEventDatePS, RE.Ativo as AtivoRec,
	                    null as IsencaoId, TP.TipoPublicoId
                    FROM dbo.AD_Associado A
                    INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                    INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                    INNER JOIN dbo.AD_Recebimento RE on A.AssociadoId = RE.AssociadoId
                    INNER JOIN dbo.AD_Valor_Anuidade_Publico VAP ON RE.ValorAnuidadePublicoId = VAP.ValorAnuidadePublicoId
                    INNER JOIN dbo.AD_Anuidade AN ON VAP.AnuidadeId = AN.AnuidadeId
                    WHERE TP.Associado = 'true' 
	                    AND RE.ObjetivoPagamento = 2
	                    AND RE.AssociadoIsentoId is null				

                    UNION

                    SELECT	DISTINCT A.AssociadoId, '' as Titulo, AN.Codigo as Anuidade , P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.StatusPS, RE.LastEventDatePS, RE.Ativo as AtivoRec,
	                    I.IsencaoId, TP.TipoPublicoId
                    FROM dbo.AD_Associado A
                    INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                    INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                    INNER JOIN dbo.AD_Recebimento RE on A.AssociadoId = RE.AssociadoId
                    INNER JOIN dbo.AD_Associado_Isento AI ON RE.AssociadoIsentoId = AI.AssociadoIsentoId
                    INNER JOIN dbo.AD_Isencao I ON AI.IsencaoId = I.IsencaoId
                    INNER JOIN dbo.AD_Anuidade AN ON I.AnuidadeId = AN.AnuidadeId

                    WHERE TP.Associado = 'true' 
	                    AND RE.ObjetivoPagamento = 2
	                    AND RE.AssociadoIsentoId is not null) AS TAB
                    WHERE AssociadoId is not null ";
            

            if (!string.IsNullOrEmpty(nome))
                query = query + $" AND Nome Like '%{nome}%' ";

            if (!string.IsNullOrEmpty(cpf))
                query = query + $" AND CPF = '{cpf}' ";

            if (!string.IsNullOrEmpty(crp))
                query = query + $" AND CRP = '{crp}' ";

            if (!string.IsNullOrEmpty(crm))
                query = query + $" AND CRM = '{crm}' ";

            if (statusPS >= 0 && statusPS <= 9)
                query = query + $" AND StatusPS = {statusPS} ";

            if (ano != 0)
                query = query + $" AND Year(LastEventDatePS) = {ano} ";

            if (mes != 0)
                query = query + $" AND Month(LastEventDatePS) = {mes} ";

            if (tipoPublicoId != 0)
                query = query + $" AND TipoPublicoId = {tipoPublicoId} ";

            if (ativo != null)
                query = query + $" AND AtivoRec = '{ativo}' ";

            query = query + " Order by Nome, Anuidade "; 

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<RecebimentoAssociadoDao> _collection = GetCollection<RecebimentoAssociadoDao>(cmd)?.ToList();
         
            return _collection;
        }
 
        public IEnumerable<RecebimentoAssociadoDao> FindByAnuidadeIdFilters(int anuidadeId, string nome, string cpf,
            string crp, string crm, int statusPS, int ano, int mes, bool? ativo, int tipoPublicoId)
        {
            query = @"SELECT A.AssociadoId, '' as Titulo, 
                        (   SELECT Anu.Codigo FROM dbo.AD_Anuidade Anu WHERE Anu.AnuidadeId = " + anuidadeId + @") as Anuidade, 
	                    P.Nome, P.CPF, TP.Nome as NomeTP, 
                        IsNull((    SELECT R2.RecebimentoId FROM dbo.AD_Recebimento R2
                                    INNER JOIN dbo.AD_Valor_Anuidade_Publico V1 
                                        ON R2.ValorAnuidadePublicoId = V1.ValorAnuidadePublicoId
                                    WHERE R2.AssociadoId = A.AssociadoId
                                        AND R2.ObjetivoPagamento = '2'
                                        AND V1.AnuidadeId = " + anuidadeId + @"),0) as RecebimentoId,
                	    IsNull((    SELECT R1.StatusPS FROM dbo.AD_Recebimento R1
                                    INNER JOIN dbo.AD_Valor_Anuidade_Publico V1 ON R1.ValorAnuidadePublicoId = V1.ValorAnuidadePublicoId
                                    WHERE R1.AssociadoId = A.AssociadoId
                                        AND R1.ObjetivoPagamento = '2'
                                        AND V1.AnuidadeId = " + anuidadeId + @"),9) as StatusPS,
            		    (   SELECT R4.LastEventDatePS FROM dbo.AD_Recebimento R4
                            INNER JOIN dbo.AD_Valor_Anuidade_Publico V1 
                                ON R4.ValorAnuidadePublicoId = V1.ValorAnuidadePublicoId
                            WHERE R4.AssociadoId = A.AssociadoId
                                AND R4.ObjetivoPagamento = '2'
                                AND V1.AnuidadeId = " + anuidadeId + @") as LastEventDatePS,
            		    (   SELECT R5.Ativo FROM dbo.AD_Recebimento R5
                            INNER JOIN dbo.AD_Valor_Anuidade_Publico V1 
                                ON R5.ValorAnuidadePublicoId = V1.ValorAnuidadePublicoId
                            WHERE R5.AssociadoId = A.AssociadoId
                                AND R5.ObjetivoPagamento = '2'
                                AND V1.AnuidadeId = " + anuidadeId + @") as AtivoRec,
	                    IsNull((    SELECT I.IsencaoId FROM dbo.AD_Isencao I 
                                    INNER JOIN dbo.AD_Associado_Isento AI 
                                        ON I.IsencaoId = AI.IsencaoId
                                    WHERE I.AnuidadeId =  = " + anuidadeId + @" 
                                        AND AI.AssociadoId = A.AssociadoId),0) as IsencaoId
                    FROM dbo.AD_Associado A
                    INNER JOIN dbo.AD_Pessoa P 
                        ON A.PessoaId = P.PessoaId
                    INNER JOIN dbo.AD_Tipo_Publico TP 
                        ON A.TipoPublicoId = TP.TipoPublicoId
                    WHERE TP.Associado = 'true' ";

            if (!string.IsNullOrEmpty(nome))
                query = query + $" AND P.Nome Like '%{nome}%' ";

            if (!string.IsNullOrEmpty(cpf))
                query = query + $" AND P.CPF = '{cpf}' ";

            if (!string.IsNullOrEmpty(crp))
                query = query + $" AND A.CRP = '{crp}' ";

            if (!string.IsNullOrEmpty(crm))
                query = query + $" AND A.CRM = '{crm}' ";

            if (statusPS >= 0 && statusPS <= 9)
                query = query + $" AND StatusPS = {statusPS} ";

            if (ano != 0)
                query = query + $" AND Year(RE.LastEventDatePS) = {ano} ";

            if (mes != 0)
                query = query + $" AND Month(RE.LastEventDatePS) = {mes} ";

            if (tipoPublicoId != 0)
                query = query + $" AND A.TipoPublicoId = {tipoPublicoId} ";

            if (ativo != null)
                query = query + $" AND P.Ativo = '{ativo}' ";

            query = query + " Order by P.Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<RecebimentoAssociadoDao> _collection = GetCollection<RecebimentoAssociadoDao>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<RecebimentoAssociadoDao> FindEventoByFilters(string nome, string cpf, string crp, 
            string crm, int statusPS, int ano, int mes, bool? ativo, string tipoEvento, int tipoPublicoId)
        {
            query = @"SELECT AssociadoId, Titulo, Anuidade , Nome, CPF, NomeTP, RecebimentoId, StatusPS, LastEventDatePS,
                        AtivoRec, IsencaoId, DtInicio, TipoPublicoId, TipoEvento  FROM (
                    SELECT	A.AssociadoId, EV.Titulo, null as Anuidade , P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.StatusPS, RE.LastEventDatePS, RE.Ativo as AtivoRec,
	                    null as IsencaoId, EV.DtInicio, TP.TipoPublicoId, EV.TipoEvento
                    FROM dbo.AD_Associado A
                    INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                    INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                    INNER JOIN dbo.AD_Recebimento RE on A.AssociadoId = RE.AssociadoId
                    INNER JOIN dbo.AD_Valor_Evento_Publico VEP ON RE.ValorEventoPublicoId = VEP.ValorEventoPublicoId
                    INNER JOIN dbo.AD_Evento EV ON VEP.EventoId = EV.EventoId
                    WHERE TP.Associado = 'true' 
	                    AND RE.ObjetivoPagamento = 1
	                    AND RE.AssociadoIsentoId is null				

                    UNION

                    SELECT	DISTINCT A.AssociadoId, EV.Titulo, null as Anuidade , P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.StatusPS, RE.LastEventDatePS, RE.Ativo as AtivoRec,
	                    I.IsencaoId, EV.DtInicio, TP.TipoPublicoId, EV.TipoEvento
                    FROM dbo.AD_Associado A
                    INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                    INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                    INNER JOIN dbo.AD_Recebimento RE on A.AssociadoId = RE.AssociadoId
                    INNER JOIN dbo.AD_Associado_Isento AI ON RE.AssociadoIsentoId = AI.AssociadoIsentoId
                    INNER JOIN dbo.AD_Isencao I ON AI.IsencaoId = I.IsencaoId
                    INNER JOIN dbo.AD_Evento EV ON I.EventoId = EV.EventoId

                    WHERE TP.Associado = 'true' 
	                    AND RE.ObjetivoPagamento = 1
	                    AND RE.AssociadoIsentoId is not null) AS TAB
                    WHERE AssociadoId is not null ";

            if (!string.IsNullOrEmpty(nome))
                query = query + $" AND Nome Like '%{nome}%' ";

            if (!string.IsNullOrEmpty(cpf))
                query = query + $" AND CPF = '{cpf}' ";

            if (!string.IsNullOrEmpty(crp))
                query = query + $" AND CRP = '{crp}' ";

            if (!string.IsNullOrEmpty(crm))
                query = query + $" AND CRM = '{crm}' ";

            if (statusPS >= 0 && statusPS <= 9)
                query = query + $" AND StatusPS = {statusPS} ";

            if (ano != 0)
                query = query + $" AND Year(DtInicio) = {ano} ";

            if (tipoPublicoId != 0)
                query = query + $" AND TipoPublicoId = {tipoPublicoId} ";

            if (!string.IsNullOrEmpty(tipoEvento))
                query = query + $" AND TipoEvento = '{tipoEvento}' ";

            if (ativo != null)
                query = query + $" AND AtivoRec = '{ativo}' ";

            query = query + " Order by Nome, Titulo ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<RecebimentoAssociadoDao> _collection = GetCollection<RecebimentoAssociadoDao>(cmd)?.ToList();

            return _collection;
        }
        
        IEnumerable<RecebimentoAssociadoDao> FindByEventoIdFilters(int eventoId, string nome, string cpf,
            string crp, string crm, int statusPS, int ano, int mes, bool? ativo,
            string tipoEvento, int tipoPublicoId)
        {
            query = @"SELECT A.AssociadoId, 
                        (SELECT EVE.Titulo FROM dbo.AD_Evento EVE WHERE EVE.EventoId = 3) as Titulo,
	                    null as Anuidade, P.Nome, P.CPF, TP.Nome as NomeTP, 
                        IsNull((    SELECT R2.RecebimentoId 
                                    FROM dbo.AD_Recebimento R2
                                    INNER JOIN dbo.AD_Valor_Evento_Publico V1 
                                        ON R2.ValorEventoPublicoId = V1.ValorEventoPublicoId
                                    WHERE R2.AssociadoId = A.AssociadoId
                                        AND R2.ObjetivoPagamento = '1'
                                        AND V1.EventoId = "+ eventoId + @"),0) as RecebimentoId,
                        IsNull((    SELECT R1.StatusPS FROM dbo.AD_Recebimento R1
                                    INNER JOIN dbo.AD_Valor_Evento_Publico V1 
                                        ON R1.ValorEventoPublicoId = V1.ValorEventoPublicoId
                                    WHERE R1.AssociadoId = A.AssociadoId
                                        AND R1.ObjetivoPagamento = '1'
                                        AND V1.EventoId = " + eventoId + @"),9) as StatusPS,
            		    (   SELECT R4.LastEventDatePS 
                            FROM dbo.AD_Recebimento R4
                            INNER JOIN dbo.AD_Valor_Evento_Publico V1 
                                ON R4.ValorEventoPublicoId = V1.ValorEventoPublicoId
                            WHERE R4.AssociadoId = A.AssociadoId
                                AND R4.ObjetivoPagamento = '1'
                                AND V1.EventoId = " + eventoId + @") as LastEventDatePS,
            		    (   SELECT R5.Ativo FROM dbo.AD_Recebimento R5
                            INNER JOIN dbo.AD_Valor_Evento_Publico V1 
                                ON R5.ValorEventoPublicoId = V1.ValorEventoPublicoId
                            WHERE R5.AssociadoId = A.AssociadoId
                                AND R5.ObjetivoPagamento = '1'
                                AND V1.EventoId = " + eventoId + @") as AtivoRec,
	                    IsNull((    SELECT I.IsencaoId 
                                    FROM dbo.AD_Isencao I 
                                    INNER JOIN dbo.AD_Associado_Isento AI 
                                        ON I.IsencaoId = AI.IsencaoId
                                    WHERE I.EventoId = " + eventoId + @" 
                                        AND AI.AssociadoId = A.AssociadoId),0) as IsencaoId
                    FROM dbo.AD_Associado A
                    INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                    INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                    WHERE P.AssociadoId != 0 ";

            if (!string.IsNullOrEmpty(nome))
                query = query + $" AND P.Nome Like '%{nome}%' ";

            if (!string.IsNullOrEmpty(cpf))
                query = query + $" AND P.CPF = '{cpf}' ";

            if (!string.IsNullOrEmpty(crp))
                query = query + $" AND A.CRP = '{crp}' ";

            if (!string.IsNullOrEmpty(crm))
                query = query + $" AND A.CRM = '{crm}' ";

            if (statusPS >= 0 && statusPS <= 9)
                query = query + $" AND RE.StatusPS = {statusPS} ";

            if (ano != 0)
                query = query + $" AND Year(E.DtInicio) = {ano} ";

            if (mes != 0)
                query = query + $" AND Month(E.DtInicio) = {mes} ";

            if (tipoPublicoId != 0)
                query = query + $" AND A.TipoPublicoId = {tipoPublicoId} ";

            if (!string.IsNullOrEmpty(tipoEvento))
                query = query + $" AND EV.TipoEvento = '{tipoEvento}' ";

            if (ativo != null)
                query = query + $" AND P.Ativo = '{ativo}' ";

            query = query + " Order by P.Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<RecebimentoAssociadoDao> _collection = GetCollection<RecebimentoAssociadoDao>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<Recebimento> GetAll(string objetivoPagamento)
        {
            query = @"SELECT R.RecebimentoId, R.AssociadoId, R.ValorEventoPublicoId, R.ObjetivoPagamento,
                        R.DtNotificacao, R.Observacao, R.AssociadoIsentoId, R.ValorAnuidadePublicoId, 
                        R.CodePS, R.ReferencePS, R.TypePS, R.StatusPS, R.LastEventDatePS, 
                        R.TypePaymentMethodPS, R.CodePaymentMethodPS, R.NetAmountPS, 
                        R.DtCadastro, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                    WHERE R.ObjetivoPagamento = '" + objetivoPagamento + "' Order by R.DtCadastro Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();

            if(_collection != null && _collection.Count() > 0)
            {
                AssociadoRepository _associadoRep = new AssociadoRepository();

                Associado _associado = new Associado();

                List<Recebimento> _recebimentos = new List<Recebimento>();

                foreach (var rec in _collection)
                {
                    var assoc = _associadoRep.GetAssociadoById(rec.AssociadoId);

                    if (assoc != null)
                    {
                        rec.Associado = assoc;
                        _recebimentos.Add(rec);
                    }
                }

                if (_recebimentos.Count() > 0)
                {
                    _collection = _recebimentos;
                }
            }
            return _collection;
        }

        public IEnumerable<Recebimento> GetRecebimentoByAnuidadeId(int id)
        {
            query = @"SELECT R.RecebimentoId, R.AssociadoId, R.ValorEventoPublicoId, R.ObjetivoPagamento,
                        R.DtNotificacao, R.Observacao, R.AssociadoIsentoId, R.ValorAnuidadePublicoId, 
                        R.CodePS, R.ReferencePS, R.TypePS, R.StatusPS, R.LastEventDatePS, 
                        R.TypePaymentMethodPS, R.CodePaymentMethodPS, R.NetAmountPS, 
                        R.DtCadastro, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                        INNER JOIN dbo.AD_Valor_Anuidade_Publico V ON R.ValorAnuidadePublicoId = V.ValorAnuidadePublicoId 
                        INNER JOIN dbo.AD_Anuidade AN ON V.AnuidadeId = AN.AnuidadeId 
                    WHERE AN.AnuidadeId = " + id + " Order by R.DtCadastro Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<Recebimento> GetRecebimentoByEventoId(int id)
        {
            query = @"SELECT R.RecebimentoId, R.AssociadoId, R.ValorEventoPublicoId, R.ObjetivoPagamento,
                        R.DtNotificacao, R.Observacao, R.AssociadoIsentoId, R.ValorAnuidadePublicoId, 
                        R.CodePS, R.ReferencePS, R.TypePS, R.StatusPS, R.LastEventDatePS, 
                        R.TypePaymentMethodPS, R.CodePaymentMethodPS, R.NetAmountPS, 
                        R.DtCadastro, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                        INNER JOIN dbo.AD_Valor_Evento_Publico V ON R.ValorEventoPublicoId = V.ValorEventoPublicoId 
                        INNER JOIN dbo.AD_Evento E ON V.EventoId = E.EventoId 
                    WHERE E.EventoId = " + id + " Order by R.DtCadastro Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();

            return _collection;
        }

        public Recebimento GetRecebimentoById(int id)
        {
            query = @"SELECT R.RecebimentoId, R.AssociadoId, R.ValorEventoPublicoId, R.ObjetivoPagamento,
                        R.DtNotificacao, R.Observacao, R.AssociadoIsentoId, R.ValorAnuidadePublicoId, 
                        R.CodePS, R.ReferencePS, R.TypePS, R.StatusPS, R.LastEventDatePS, 
                        R.TypePaymentMethodPS, R.CodePaymentMethodPS, R.NetAmountPS, 
                        R.DtCadastro, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                    WHERE R.RecebimentoId = " + id + " ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            Recebimento _recebimento = GetCollection<Recebimento>(cmd)?.First();

            // Obtendo uma Associado:
            if (_recebimento != null)
            {
                AssociadoRepository _associadoRep = new AssociadoRepository();

                var assoc = _associadoRep.GetAssociadoById(_recebimento.AssociadoId);

                if (assoc != null)
                {
                    _recebimento.Associado = assoc;
                }
                else
                {
                    throw new ArgumentNullException("Atenção: Não foi encontrado Associado vinculado do recebimento");
                }
            }
            return _recebimento;
        }

        public IEnumerable<Recebimento> GetRecebimentoByPessoaId(string objetivoPagamento, int id)
        {
            query = @"SELECT R.RecebimentoId, R.AssociadoId, R.ValorEventoPublicoId, R.ObjetivoPagamento,
                        R.DtNotificacao, R.Observacao, R.AssociadoIsentoId, R.ValorAnuidadePublicoId, 
                        R.CodePS, R.ReferencePS, R.TypePS, R.StatusPS, R.LastEventDatePS, 
                        R.TypePaymentMethodPS, R.CodePaymentMethodPS, R.NetAmountPS, 
                        R.DtCadastro, R.Ativo 
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

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Recebimento (AssociadoId, ObjetivoPagamento, Observacao, " +
                        "   CodePS, ReferencePS, TypePS, StatusPS, LastEventDatePS, TypePaymentMethod, " +
                        "   CodePaymentMethod,  NetAmountPS, DtCadastro, Ativo " +
                        "    " + _atributos + ") " +
                        "VALUES(@AssociadoId, @ObjetivoPagamento, @Observacao, " +
                        "   @CodePS, @ReferencePS, @TypePS, @StatusPS, @LastEventDatePS, @TypePaymentMethod, " +
                        "   @CodePaymentMethod,  @NetAmountPS, @DtCadastro, @Ativo " +
                        "    " + _values + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("AssociadoId", r.AssociadoId);
                    command.Parameters.AddWithValue("ObjetivoPagamento", r.ObjetivoPagamento);
                    command.Parameters.AddWithValue("Observacao", r.Observacao);
                    command.Parameters.AddWithValue("CodePS", r.CodePS);
                    command.Parameters.AddWithValue("ReferencePS", r.ReferencePS);
                    command.Parameters.AddWithValue("TypePS", r.TypePS);
                    command.Parameters.AddWithValue("StatusPS", r.StatusPS);
                    command.Parameters.AddWithValue("LastEventDatePS", r.LastEventDatePS);
                    command.Parameters.AddWithValue("TypePaymentMethod", r.TypePaymentMethodPS);
                    command.Parameters.AddWithValue("CodePaymentMethod", r.CodePaymentMethodPS);
                    command.Parameters.AddWithValue("NetAmountPS", r.NetAmountPS);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);
                    command.Parameters.AddWithValue("Ativo", r.Ativo);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    _msg = _resultado ? "Inclusão realizada com sucesso" : "Inclusão Não realizada com sucesso";

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
                    // Atualizando os dados na tabela:
       
                    command.CommandText = "" +
                        "Update dbo.AD_Recebimento Set Observacao = @Observacao  " +
                        "WHERE RecebimentoId = @id ";

                    command.Parameters.AddWithValue("Observacao", r.Observacao);
                    command.Parameters.AddWithValue("id", id);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

                    _msg = _resultado ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

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

        public string InsertIsento(int associadoId, int associadoIsentoId, string ojetivoPagamento, string tipoIsencao)
        {
            bool _resultado = false;
            string _msg = "";
            Int32 id = 0;

            string _obs, _netAmountPS, _referencePS, _codePS, _statusPS, _typePS, _typePaymentMethodPS, _codePaymentMethodPS;
            DateTime _date = DateTime.Now;

            if (tipoIsencao.Equals("1"))
            {
                _obs = "Isento do pagamento do evento";
            }
            else
            {
                _obs = "Isento do pagamento da anuidade";
            }
            _netAmountPS = "0";
            _referencePS = "N/A";
            _codePS = "N/A";
            _statusPS = "0"; //Indica que o usuário é Isento
            _typePS = "0";
            _codePaymentMethodPS = "0";
            _typePaymentMethodPS = "0";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("IncluirRecebimentoIsento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Recebimento (AssociadoId, ObjetivoPagamento, Observacao, AssociadoIsentoId, " +
                        "    CodePS, ReferencePS, TypePS, StatusPS, LastEventDatePS,  TypePaymentMethodPS,  " +
                        "   CodePaymentMethodPS, NetAmountPS, DtCadastro, Ativo) " +
                        "VALUES(@AssociadoId, @ObjetivoPagamento, @Observacao, @AssociadoIsentoId, " +
                        "    @CodePS, @ReferencePS, @TypePS, @StatusPS, @LastEventDatePS, @TypePaymentMethodPS,  " +
                        "   @CodePaymentMethodPS, @NetAmountPS, @DtCadastro, @Ativo) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("AssociadoId", associadoId);
                    command.Parameters.AddWithValue("ObjetivoPagamento", tipoIsencao);
                    command.Parameters.AddWithValue("Observacao", _obs);
                    command.Parameters.AddWithValue("AssociadoIsentoId", associadoIsentoId);
                    command.Parameters.AddWithValue("CodePS", _codePS);
                    command.Parameters.AddWithValue("ReferencePS", _referencePS);
                    command.Parameters.AddWithValue("TypePS", _typePS);
                    command.Parameters.AddWithValue("StatusPS", _statusPS);
                    command.Parameters.AddWithValue("LastEventDatePS", _date);
                    command.Parameters.AddWithValue("TypePaymentMethodPS", _typePaymentMethodPS);
                    command.Parameters.AddWithValue("CodePaymentMethodPS", _codePaymentMethodPS);
                    command.Parameters.AddWithValue("NetAmountPS", _netAmountPS);
                    command.Parameters.AddWithValue("DtCadastro", _date);
                    command.Parameters.AddWithValue("Ativo", true);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    _msg = _resultado ? "Inclusão realizada com sucesso" : "Inclusão Não realizada com sucesso";

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

        public string DeleteByAssociadoIsentoId(int id)
        {
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("DeleteRecebimentoIsento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "DELETE " +
                        "From dbo.AD_Recebimento " +
                        "WHERE AssociadoIsentoId = @AssociadoIsentoId ";

                    command.Parameters.AddWithValue("@AssociadoIsentoId", id);

                    int i = command.ExecuteNonQuery();

                    _msg = i > 0 ? "SUCESSO" : "FALHA";

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

        public string UpdateRecebimentoPagSeguro(string code, string reference, 
            int type, int status, string lasteventdate, int TypePaymentoMethod, int CodePaymentoMethod, 
            decimal NetAmountPS)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarRecebimentoPagSeguro");

                command.Connection = connection;
                command.Transaction = transaction;

                string _dtEvent, _hrEvent;

                _dtEvent = lasteventdate.Substring(0,lasteventdate.IndexOf("T")) +" "+ lasteventdate.Substring(lasteventdate.IndexOf("T") + 1,12);
                _hrEvent = lasteventdate.Substring(lasteventdate.IndexOf("T")); 

                try
                {
                    // Atualizando os dados na tabela:

                    command.CommandText = "" +
                        "Update dbo.AD_Recebimento Set " +
                        "   TypePS = @TypePS, " +
                        "   StatusPS = @StatusPS," +
                        "   LastEventDatePS = @LastEventDatePS, " +
                        "   LastEventHourTZDPS = @LastEventHourTZDPS, " +
                        "   TypePaymentMethodPS = @TypePaymentMethodPS, " +
                        "   CodePaymentMethodPS = @CodePaymentMethodPS, " +
                        "   NetAmountPS = @NetAmountPS, " +
                        "   DtAtualizacaoPS = @DtAtualizacaoPS " +
                        "WHERE CodePS = @CodePS AND ReferencePS = @ReferencePS";

                    command.Parameters.AddWithValue("TypePS", type);
                    command.Parameters.AddWithValue("StatusPS", status);
                    command.Parameters.AddWithValue("LastEventDatePS", _dtEvent);
                    command.Parameters.AddWithValue("LastEventHourTZDPS", _hrEvent);
                    command.Parameters.AddWithValue("TypePaymentMethodPS", TypePaymentoMethod);
                    command.Parameters.AddWithValue("CodePaymentMethodPS", CodePaymentoMethod);
                    command.Parameters.AddWithValue("NetAmountPS", NetAmountPS);
                    command.Parameters.AddWithValue("DtAtualizacaoPS", DateTime.Now);
                    command.Parameters.AddWithValue("CodePS", code);
                    command.Parameters.AddWithValue("ReferencePS", reference);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

                    _msg = _resultado ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

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
