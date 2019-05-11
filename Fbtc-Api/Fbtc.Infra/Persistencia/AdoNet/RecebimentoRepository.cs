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
    public class RecebimentoRepository : AbstractRepository, IRecebimentoRepository
    {
        private string query;
        private readonly string strConnSql;

        private LogRepository logRep;
        private readonly string className;
        private string _instrucaoSql = "";
        private string _result = "";

        public RecebimentoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");

            className = "RecebimentoRepository";
            logRep = new LogRepository();
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
                finally
                {
                    connection.Close();
                }
            }
            return _msg;

        }
       
        public IEnumerable<RecebimentoAssociadoDao> FindAnuidadeByFilters(string nome, string cpf, string crp, 
            string crm, int statusPS, int ano, int mes, bool? ativo, int tipoPublicoId)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT '' as Titulo, AN.Exercicio as Anuidade , P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.AssinaturaAnuidadeId, RE.AssinaturaEventoId, RE.TypePS, 
                        RE.StatusPS, RE.LastEventDatePS, RE.Ativo, RE.NotificationCodePS,  
                        RE.TypePaymentMethodPS, RE.codePaymentMethodPS, 
                        RE.GrossAmountPS, RE.DiscountAmountPS, RE.FeeAmountPS, RE.NetAmountPS, RE.ExtraAmountPS, 
                        RE.Observacao, 
                        RE.StatusFBTC, RE.DtStatusFBTC, RE.OrigemEmissaoTitulo,  RE.DtVencimento, 
                        P.EMail, P.NrCelular, P.Ativo as AtivoAssociado 
                    FROM dbo.AD_Recebimento RE
                        INNER JOIN dbo.AD_Assinatura_Anuidade AA ON RE.AssinaturaAnuidadeId = AA.AssinaturaAnuidadeId
                        INNER JOIN dbo.AD_Associado A ON AA.AssociadoId = A.AssociadoId    
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                        INNER JOIN dbo.AD_Valor_Anuidade VA ON AA.ValorAnuidadeId = VA.ValorAnuidadeId
                        INNER JOIN dbo.AD_Anuidade_Tipo_Publico ATP ON VA.AnuidadeTipoPublicoId = ATP.AnuidadeTipoPublicoId
                        INNER JOIN dbo.AD_Anuidade AN ON ATP.AnuidadeId = AN.AnuidadeId
                        INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                    WHERE A.AssociadoId is not null ";

            if (!string.IsNullOrEmpty(nome))
            {
                query = query + $" AND P.Nome Like '%'+ @nome +'%' ";
                SqlParameter pNome = new SqlParameter() { ParameterName = "@nome", Value = nome };
                _parametros.Add(pNome);
            }
            /*if (!string.IsNullOrEmpty(cpf))
                query = query + $" AND P.CPF = '{cpf}' ";*/
            if (!string.IsNullOrEmpty(crp))
            {
                query = query + $" AND A.CRP = @crp ";
                SqlParameter pCrp = new SqlParameter() { ParameterName = "@crp", Value = crp };
                _parametros.Add(pCrp);
            }
            if (!string.IsNullOrEmpty(crm))
            {
                query = query + $" AND A.CRM = @crm ";
                SqlParameter pCrm = new SqlParameter() { ParameterName = "@crm", Value = crm };
                _parametros.Add(pCrm);
            }
            if (statusPS >= 0 && statusPS <= 9)
            {
                query = query + $" AND RE.StatusPS = @statusPS ";
                SqlParameter pStatusPS = new SqlParameter() { ParameterName = "@statusPS", Value = statusPS };
                _parametros.Add(pStatusPS);
            }
            if (ano != 0)
            {
                query = query + $" AND Year(RE.LastEventDatePS) = @ano ";
                SqlParameter pAno = new SqlParameter() { ParameterName = "@ano", Value = ano };
                _parametros.Add(pAno);
            }
            if (mes != 0)
            {
                query = query + $" AND Month(RE.LastEventDatePS) = @mes ";
                SqlParameter pMes = new SqlParameter() { ParameterName = "@mes", Value = mes };
                _parametros.Add(pMes);
            }
            if (tipoPublicoId != 0)
            {
                query = query + $" AND TP.TipoPublicoId = @tipoPublicoId ";
                SqlParameter pTPId = new SqlParameter() { ParameterName = "@tipoPublicoId", Value = tipoPublicoId };
                _parametros.Add(pTPId);
            }
            if (ativo != null)
            {
                query = query + $" AND RE.Ativo = @ativo ";
                SqlParameter pAtivo = new SqlParameter() { ParameterName = "@ativo", Value = ativo };
                _parametros.Add(pAtivo);
            }
            query = query + " Order by RE.LastEventDatePS Desc "; 

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<RecebimentoAssociadoDao> _collection = GetCollection<RecebimentoAssociadoDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindAnuidadeByFilters",
                "SELECT", "RECEBIMENTO", 0, query, _collection != null ? "SUCESSO" : "0");
            // Fim Log

            return _collection;
        }
 
        public IEnumerable<RecebimentoAssociadoDao> FindByAnuidadeIdFilters(int anuidadeId, string nome, string cpf,
            string crp, string crm, int statusPS, int ano, int mes, bool? ativo, int tipoPublicoId)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            SqlParameter pAnuidadeId = new SqlParameter() { ParameterName = "@anuidadeId", Value = anuidadeId };
            _parametros.Add(pAnuidadeId);

            query = @"SELECT '' as Titulo, AN.Exercicio as Anuidade , P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.AssinaturaAnuidadeId, RE.AssinaturaEventoId, RE.TypePS, 
                        RE.StatusPS, RE.LastEventDatePS, RE.Ativo, RE.NotificationCodePS, AA.Reference, 
                        RE.TypePaymentMethodPS, RE.codePaymentMethodPS, 
                        RE.GrossAmountPS, RE.DiscountAmountPS, RE.FeeAmountPS, RE.NetAmountPS, RE.ExtraAmountPS, 
                        RE.Observacao, 
                        RE.StatusFBTC, RE.DtStatusFBTC, RE.OrigemEmissaoTitulo,  RE.DtVencimento, 
                        P.EMail, P.NrCelular, P.Ativo as AtivoAssociado 
                    FROM dbo.AD_Associado A
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId

                        INNER JOIN dbo.AD_Assinatura_Anuidade AA ON A.AssociadoId = AA.AssociadoId
                        INNER JOIN dbo.AD_Recebimento RE ON AA.AssinaturaAnuidadeId = RE.AssinaturaAnuidadeId
                        INNER JOIN dbo.AD_Valor_Anuidade VA ON AA.ValorAnuidadeId = VA.ValorAnuidadeId
                        INNER JOIN dbo.AD_Anuidade_Tipo_Publico ATP ON VA.AnuidadeTipoPublicoId = ATP.AnuidadeTipoPublicoId
                        INNER JOIN dbo.AD_Anuidade AN ON ATP.AnuidadeId = AN.AnuidadeId

                        INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                        INNER JOIN dbo.AD_Recebimento RE on A.AssociadoId = RE.AssociadoId
                    WHERE AN.AnuidadeId = @anuidadeId ";

            if (!string.IsNullOrEmpty(nome))
            {
                query = query + $" AND P.Nome Like '%'+ @nome +'%' ";
                SqlParameter pNome = new SqlParameter() { ParameterName = "@nome", Value = nome };
                _parametros.Add(pNome);
            }
            if (!string.IsNullOrEmpty(crp))
            {
                query = query + $" AND A.CRP = @crp ";
                SqlParameter pCrp = new SqlParameter() { ParameterName = "@crp", Value = crp };
                _parametros.Add(pCrp);
            }
            if (!string.IsNullOrEmpty(crm))
            {
                query = query + $" AND A.CRM = @crm ";
                SqlParameter pCrm = new SqlParameter() { ParameterName = "@crm", Value = crm };
                _parametros.Add(pCrm);
            }
            if (statusPS >= 0 && statusPS <= 9)
            {
                query = query + $" AND StatusPS = @statusPS ";
                SqlParameter pStatusPS = new SqlParameter() { ParameterName = "@statusPS", Value = statusPS };
                _parametros.Add(pStatusPS);
            }
            if (ano != 0)
            {
                query = query + $" AND Year(RE.LastEventDatePS) = @ano ";
                SqlParameter pAno = new SqlParameter() { ParameterName = "@ano", Value = ano };
                _parametros.Add(pAno);
            }
            if (mes != 0)
            {
                query = query + $" AND Month(RE.LastEventDatePS) = @mes ";
                SqlParameter pMes = new SqlParameter() { ParameterName = "@mes", Value = mes };
                _parametros.Add(pMes);
            }
            if (tipoPublicoId != 0)
            {
                query = query + $" AND A.TipoPublicoId = @tipoPublicoId ";
                SqlParameter pTPId = new SqlParameter() { ParameterName = "@tipoPublicoId", Value = tipoPublicoId };
                _parametros.Add(pTPId);
            }
            if (ativo != null)
            {
                query = query + $" AND P.Ativo = @ativo ";
                SqlParameter pAtivo = new SqlParameter() { ParameterName = "@ativo", Value = ativo };
                _parametros.Add(pAtivo);
            }

            query = query + " Order by RE.LastEventDatePS Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<RecebimentoAssociadoDao> _collection = GetCollection<RecebimentoAssociadoDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindByAnuidadeIdFilters",
                "SELECT", "RECEBIMENTO", 0, query, _collection != null ? "SUCESSO" : "0");
            // Fim Log

            return _collection;
        }

        public IEnumerable<RecebimentoAssociadoDao> FindEventoByFilters(string nome, string cpf, string crp, 
            string crm, int statusPS, int ano, int mes, bool? ativo, string tipoEvento, int tipoPublicoId)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT EV.Titulo, 0 as Anuidade , P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.AssinaturaAnuidadeId, RE.AssinaturaEventoId, RE.TypePS, 
                        RE.StatusPS, RE.LastEventDatePS, RE.Ativo, RE.NotificationCodePS,  
                        RE.TypePaymentMethodPS, RE.codePaymentMethodPS, 
                        RE.GrossAmountPS, RE.DiscountAmountPS, RE.FeeAmountPS, RE.NetAmountPS, RE.ExtraAmountPS, 
                        RE.Observacao, 
                        RE.StatusFBTC, RE.DtStatusFBTC, RE.OrigemEmissaoTitulo,  RE.DtVencimento, 
                        P.EMail, P.NrCelular, P.Ativo as AtivoAssociado 
                    FROM dbo.AD_Associado A
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId

                        INNER JOIN dbo.AD_Assinatura_Evento AE ON A.AssociadoId = AE.AssociadoId
                        INNER JOIN dbo.AD_Recebimento RE ON AE.AssinaturaEventoId = RE.AssinaturaEventoId
                        INNER JOIN dbo.AD_Valor_Evento_Publico VEP ON AE.ValorEventoPublicoId = VEP.ValorEventoPublicoId

                        INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                        INNER JOIN dbo.AD_Evento EV ON VEP.EventoId = EV.EventoId
                    WHERE TP.Associado = 'true' ";

            if (!string.IsNullOrEmpty(nome))
            {
                query = query + $" AND P.Nome Like '%'+ @nome +'%' ";
                SqlParameter pNome = new SqlParameter() { ParameterName = "@nome", Value = nome };
                _parametros.Add(pNome);
            }
            /*if (!string.IsNullOrEmpty(cpf))
                query = query + $" AND P.CPF = '{cpf}' ";*/
            if (!string.IsNullOrEmpty(crp))
            {
                query = query + $" AND P.CRP = @crp ";
                SqlParameter pCrp = new SqlParameter() { ParameterName = "@crp", Value = crp };
                _parametros.Add(pCrp);
            }
            if (!string.IsNullOrEmpty(crm))
            {
                query = query + $" AND P.CRM = @crm ";
                SqlParameter pCrm = new SqlParameter() { ParameterName = "@crm", Value = crm };
                _parametros.Add(pCrm);
            }
            if (statusPS >= 0 && statusPS <= 9)
            {
                query = query + $" AND RE.StatusPS = @statusPS ";
                SqlParameter pStatusPS = new SqlParameter() { ParameterName = "@statusPS", Value = statusPS };
                _parametros.Add(pStatusPS);
            }
            if (ano != 0)
            {
                query = query + $" AND Year(EV.DtInicio) = @ano ";
                SqlParameter pAno = new SqlParameter() { ParameterName = "@ano", Value = ano };
                _parametros.Add(pAno);
            }
            if (tipoPublicoId != 0)
            {
                query = query + $" AND TP.TipoPublicoId = @tipoPublicoId ";
                SqlParameter pTPId = new SqlParameter() { ParameterName = "@tipoPublicoId", Value = tipoPublicoId };
                _parametros.Add(pTPId);
            }
            if (!string.IsNullOrEmpty(tipoEvento))
            {
                query = query + $" AND EV.TipoEvento = @tipoEvento ";
                SqlParameter pTipoEvento = new SqlParameter() { ParameterName = "@tipoEvento", Value = tipoEvento };
                _parametros.Add(pTipoEvento);
            }
            if (ativo != null)
            {
                query = query + $" AND RE.Ativo = @ativo ";
                SqlParameter pAtivo = new SqlParameter() { ParameterName = "@ativo", Value = ativo };
                _parametros.Add(pAtivo);
            }
            query = query + " Order by RE.LastEventDatePS Desc, P.Nome, EV.Titulo ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<RecebimentoAssociadoDao> _collection = GetCollection<RecebimentoAssociadoDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindEventoByFilters",
                "SELECT", "RECEBIMENTO", 0, query, _collection != null ? "SUCESSO" : "0");
            // Fim Log

            return _collection;
        }
        
        public IEnumerable<RecebimentoAssociadoDao> FindByEventoIdFilters(int eventoId, string nome, string cpf,
            string crp, string crm, int statusPS, int ano, int mes, bool? ativo,
            string tipoEvento, int tipoPublicoId)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            SqlParameter pEventoId = new SqlParameter() { ParameterName = "@eventoId", Value = eventoId };
            _parametros.Add(pEventoId);

            query = @"SELECT EV.Titulo, 0 as Anuidade , P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.AssinaturaAnuidadeId, RE.AssinaturaEventoId, RE.TypePS, 
                        RE.StatusPS, RE.LastEventDatePS, RE.Ativo, RE.NotificationCodePS, 
                        RE.TypePaymentMethodPS, RE.codePaymentMethodPS, 
                        RE.GrossAmountPS, RE.DiscountAmountPS, RE.FeeAmountPS, RE.NetAmountPS, RE.ExtraAmountPS, 
                        RE.Observacao, 
                        RE.StatusFBTC, RE.DtStatusFBTC, RE.OrigemEmissaoTitulo,  RE.DtVencimento, 
                        P.EMail, P.NrCelular, P.Ativo as AtivoAssociado 
                    FROM dbo.AD_Associado A
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId

                        INNER JOIN dbo.AD_Assinatura_Evento AE ON A.AssociadoId = AE.AssociadoId
                        INNER JOIN dbo.AD_Recebimento RE ON AE.AssinaturaEventoId = RE.AssinaturaEventoId
                        INNER JOIN dbo.AD_Valor_Evento_Publico VEP ON AE.ValorEventoPublicoId = VEP.ValorEventoPublicoId

                        INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                        INNER JOIN dbo.AD_Evento EV ON VEP.EventoId = EV.EventoId
                    WHERE VEP.EventoId = @eventoId ";

            if (!string.IsNullOrEmpty(nome))
            {
                query = query + $" AND P.Nome Like '%'+ @nome +'%' ";
                SqlParameter pNome = new SqlParameter() { ParameterName = "@nome", Value = nome };
                _parametros.Add(pNome);
            }
            /*if (!string.IsNullOrEmpty(cpf))
                query = query + $" AND P.CPF = '{cpf}' ";*/
            if (!string.IsNullOrEmpty(crp))
            {
                query = query + $" AND A.CRP = @crp ";
                SqlParameter pCrp = new SqlParameter() { ParameterName = "@crp", Value = crp };
                _parametros.Add(pCrp);
            }
            if (!string.IsNullOrEmpty(crm))
            {
                query = query + $" AND A.CRM = @crm ";
                SqlParameter pCrm = new SqlParameter() { ParameterName = "@crm", Value = crm };
                _parametros.Add(pCrm);
            }
            if (statusPS >= 0 && statusPS <= 9)
            {
                query = query + $" AND RE.StatusPS = @statusPS ";
                SqlParameter pStatusPS = new SqlParameter() { ParameterName = "@statusPS", Value = statusPS };
                _parametros.Add(pStatusPS);
            }
            if (ano != 0)
            {
                query = query + $" AND Year(E.DtInicio) = @ano ";
                SqlParameter pAno = new SqlParameter() { ParameterName = "@ano", Value = ano };
                _parametros.Add(pAno);
            }
            if (mes != 0)
            {
                query = query + $" AND Month(E.DtInicio) = @mes ";
                SqlParameter pMes = new SqlParameter() { ParameterName = "@mes", Value = mes };
                _parametros.Add(pMes);
            }
            if (tipoPublicoId != 0)
            {
                query = query + $" AND A.TipoPublicoId = @tipoPublicoId ";
                SqlParameter pTPId = new SqlParameter() { ParameterName = "@tipoPublicoId", Value = tipoPublicoId };
                _parametros.Add(pTPId);
            }
            if (!string.IsNullOrEmpty(tipoEvento))
            {
                query = query + $" AND EV.TipoEvento = @tipoEvento ";
                SqlParameter pTEId = new SqlParameter() { ParameterName = "@tipoEvento", Value = tipoEvento };
                _parametros.Add(pTEId);
            }
            if (ativo != null)
            {
                query = query + $" AND P.Ativo = @ativo ";
                SqlParameter pAtivo = new SqlParameter() { ParameterName = "@ativo", Value = ativo };
                _parametros.Add(pAtivo);
            }
            query = query + " Order by RE.LastEventDatePS Desc, P.Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<RecebimentoAssociadoDao> _collection = GetCollection<RecebimentoAssociadoDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindByEventoIdFilters",
                "SELECT", "RECEBIMENTO", 0, query, _collection != null ? "SUCESSO" : "0");
            // Fim Log

            return _collection;
        }

        public IEnumerable<RecebimentoAssociadoDao> FindPagamentosByPessoaIdIdFilters(int pessoaId,
            string objetivoPagamento, int ano, int statusPS)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            SqlParameter pPessoaId = new SqlParameter() { ParameterName = "@pessoaId", Value = pessoaId };
            _parametros.Add(pPessoaId);

            // objetivoPagamento = 1: Evento; objetivoPagamento = 2: Anuidade
            // string _addWhere = objetivoPagamento == "1" ? " AssinaturaAnuidadeId is null " : " AssinaturaEventoId is null ";

            if (objetivoPagamento == "1")
            {
                // Evento:
                query = @"SELECT EV.Titulo, 0 as Anuidade, P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.AssinaturaAnuidadeId, RE.AssinaturaEventoId, RE.TypePS, 
                        RE.StatusPS, RE.LastEventDatePS, RE.Ativo, RE.NotificationCodePS, 
                        RE.TypePaymentMethodPS, RE.codePaymentMethodPS, 
                        RE.GrossAmountPS, RE.DiscountAmountPS, RE.FeeAmountPS, RE.NetAmountPS, RE.ExtraAmountPS, 
                        RE.Observacao, 
                        RE.StatusFBTC, RE.DtStatusFBTC, RE.OrigemEmissaoTitulo,  RE.DtVencimento, 
                        P.EMail, P.NrCelular, P.Ativo as AtivoAssociado 
                    FROM dbo.AD_Associado A
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                        INNER JOIN dbo.AD_Assinatura_Evento AE ON A.AssociadoId = AE.AssociadoId
                        INNER JOIN dbo.AD_Recebimento RE ON AE.AssinaturaEventoId = RE.AssinaturaEventoId
                        INNER JOIN dbo.AD_Valor_Evento_Publico VEP ON AE.ValorEventoPublicoId = VEP.ValorEventoPublicoId
                        INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                        INNER JOIN dbo.AD_Evento EV ON VEP.EventoId = EV.EventoId
                    WHERE TP.Associado = 'true' 
	                    AND P.PessoaId = @pessoaId ";
            }
            else
            {
                // Anuidade:
                query = @"SELECT '' as Titulo, AN.Exercicio as Anuidade , P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.AssinaturaAnuidadeId, RE.AssinaturaEventoId, RE.TypePS, 
                        RE.StatusPS, RE.LastEventDatePS, RE.Ativo, RE.NotificationCodePS, AA.Reference, 
                        RE.TypePaymentMethodPS, RE.codePaymentMethodPS, 
                        RE.GrossAmountPS, RE.DiscountAmountPS, RE.FeeAmountPS, RE.NetAmountPS, RE.ExtraAmountPS, 
                        RE.Observacao, 
                        RE.StatusFBTC, RE.DtStatusFBTC, RE.OrigemEmissaoTitulo,  RE.DtVencimento, 
                        P.EMail, P.NrCelular, P.Ativo as AtivoAssociado
                    FROM dbo.AD_Associado A
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                        INNER JOIN dbo.AD_Assinatura_Anuidade AA ON A.AssociadoId = AA.AssociadoId
                        INNER JOIN dbo.AD_Recebimento RE ON AA.AssinaturaAnuidadeId = RE.AssinaturaAnuidadeId
                        INNER JOIN dbo.AD_Valor_Anuidade VA ON AA.ValorAnuidadeId = VA.ValorAnuidadeId 
                        INNER JOIN dbo.AD_Anuidade_Tipo_Publico ATP ON VA.AnuidadeTipoPublicoId = ATP.AnuidadeTipoPublicoId
                        INNER JOIN dbo.AD_Anuidade AN ON ATP.AnuidadeId = AN.AnuidadeId
                        INNER JOIN dbo.AD_Tipo_Publico TP ON ATP.TipoPublicoId = TP.TipoPublicoId
                    WHERE TP.Associado = 'true' 
	                    AND P.PessoaId = @pessoaId ";
            }

            if (statusPS >= 0 && statusPS <= 9)
            {
                query = query + $" AND RE.StatusPS = @statusPS ";
                SqlParameter pStatusPS = new SqlParameter() { ParameterName = "@statusPS", Value = statusPS };
                _parametros.Add(pStatusPS);
            }
            if (ano != 0)
            {
                query = query + $" AND Year(RE.DtVencimento) = @ano ";
                SqlParameter pAno = new SqlParameter() { ParameterName = "@ano", Value = ano };
                _parametros.Add(pAno);
            }

            query = query + " Order by RE.LastEventDatePS Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<RecebimentoAssociadoDao> _collection = GetCollection<RecebimentoAssociadoDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindPagamentosByPessoaIdIdFilters",
                "SELECT", "RECEBIMENTO", 0, query, _collection != null ? "SUCESSO" : "0");
            // Fim Log

            return _collection;
        }

        public IEnumerable<Recebimento> GetAll(string objetivoPagamento)
        {
            // objetivoPagamento = 1: Evento; objetivoPagamento = 2: Anuidade
            string _addWhere = objetivoPagamento == "1" ? " AssinaturaAnuidadeId is null " : " AssinaturaEventoId is null ";

            query = @"SELECT R.RecebimentoId, R.AssinaturaAnuidadeId, R.AssinaturaEventoId, 
                        R.Observacao, R.NotificationCodePS, R.TypePS, R.StatusPS, 
                        R.LastEventDatePS, R.TypePaymentMethodPS, R.CodePaymentMethodPS, 
                        R.GrossAmountPS, R.DiscountAmountPS, R.FeeAmountPS, R.NetAmountPS, R.ExtraAmountPS, 
                        R.DtAtualizacaoPS, R.DtVencimento, R.StatusFBTC, R.DtStatusFBTC, 
                        R.OrigemEmissaoTitulo, R.DtCadastro, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                    WHERE " + _addWhere + " Order by R.LastEventDatePS Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAll",
                "SELECT", "RECEBIMENTO", 0, query, _collection != null ? "SUCESSO" : "0");
            // Fim Log

            return _collection;
        }

        public IEnumerable<Recebimento> GetRecebimentoByAnuidadeId(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            SqlParameter pId = new SqlParameter() { ParameterName = "@id", Value = id };
            _parametros.Add(pId);

            query = @"SELECT R.RecebimentoId, R.AssinaturaAnuidadeId, R.AssinaturaEventoId, 
                        R.Observacao, R.NotificationCodePS , R.TypePS, R.StatusPS, 
                        R.LastEventDatePS, R.TypePaymentMethodPS, R.CodePaymentMethodPS, 
                        R.GrossAmountPS, R.DiscountAmountPS, R.FeeAmountPS, R.NetAmountPS, R.ExtraAmountPS, 
                        R.DtAtualizacaoPS, R.DtVencimento, R.StatusFBTC, R.DtStatusFBTC, 
                        R.OrigemEmissaoTitulo, R.DtCadastro, R.Ativo 
                    FROM dbo.AD_Recebimento R  
                        INNER JOIN dbo.AD_Assinatura_Anuidade AA ON R.AssinaturaAnuidadeId = AA.AssinaturaAnuidadeId 
                        INNER JOIN dbo.AD_Valor_Anuidade VA ON AA.ValorAnuidadeId = VA.ValorAnuidadeId
                        INNER JOIN dbo.AD_Anuidade_Tipo_Publico ATP ON VA.AnuidadeTipoPublicoId = ATP.AnuidadeTipoPublicoId
                        INNER JOIN dbo.AD_Anuidade AN ON ATP.AnuidadeId = AN.AnuidadeId
                    WHERE AN.AnuidadeId = @id Order by R.LastEventDatePS Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetRecebimentoByAnuidadeId",
                "SELECT", "RECEBIMENTO", id, query, _collection != null ? "SUCESSO" : "0");
            // Fim Log

            return _collection;
        }

        public IEnumerable<Recebimento> GetRecebimentoByEventoId(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            SqlParameter pId = new SqlParameter() { ParameterName = "@id", Value = id };
            _parametros.Add(pId);

            query = @"SELECT R.RecebimentoId, R.AssinaturaAnuidadeId, R.AssinaturaEventoId, 
                        R.Observacao, R.NotificationCodePS, R.TypePS, R.StatusPS, 
                        R.LastEventDatePS, R.TypePaymentMethodPS, R.CodePaymentMethodPS, 
                        R.GrossAmountPS, R.DiscountAmountPS, R.FeeAmountPS, R.NetAmountPS, R.ExtraAmountPS, 
                        R.DtAtualizacaoPS, R.DtVencimento, R.StatusFBTC, R.DtStatusFBTC, 
                        R.OrigemEmissaoTitulo, R.DtCadastro, R.Ativo 
                    FROM dbo.AD_Recebimento R  
                        INNER JOIN dbo.AD_Assinatura_Evento AE ON R.AssinaturaEventoId  = AE.AssinaturaEventoId 
                        INNER JOIN dbo.AD_Valor_Evento_Publico VEP ON AE.ValorEventoPublicoId  = VEP.ValorEventoPublicoId 
                        INNER JOIN dbo.AD_Evento EV ON VEP.EventoId = EV.EventoId
                    WHERE EV.EventoId = @id Order by R.LastEventDatePS Desc ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetRecebimentoByEventoId",
                "SELECT", "RECEBIMENTO", id, query, _collection != null ? "SUCESSO" : "0");
            // Fim Log

            return _collection;
        }

        public Recebimento GetRecebimentoById(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            SqlParameter pId = new SqlParameter() { ParameterName = "@id", Value = id };
            _parametros.Add(pId);

            query = @"SELECT R.RecebimentoId, R.AssinaturaAnuidadeId, R.AssinaturaEventoId, 
                        R.Observacao, R.NotificationCodePS, R.TypePS, R.StatusPS, 
                        R.LastEventDatePS, R.TypePaymentMethodPS, R.CodePaymentMethodPS, 
                        R.GrossAmountPS, R.DiscountAmountPS, R.FeeAmountPS, R.NetAmountPS, R.ExtraAmountPS, 
                        R.DtAtualizacaoPS, R.DtVencimento, R.StatusFBTC, R.DtStatusFBTC, 
                        R.OrigemEmissaoTitulo, R.DtCadastro, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                    WHERE R.RecebimentoId = @id ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            Recebimento _recebimento = GetCollection<Recebimento>(cmd)?.FirstOrDefault<Recebimento>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetRecebimentoById",
                "SELECT", "RECEBIMENTO", id, query, _recebimento != null ? "SUCESSO" : "0");
            // Fim Log

            return _recebimento;
        }

        public IEnumerable<Recebimento> GetRecebimentoByPessoaId(string objetivoPagamento, int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            SqlParameter pId = new SqlParameter() { ParameterName = "@id", Value = id };
            _parametros.Add(pId);

            // objetivoPagamento = 1: Evento; objetivoPagamento = 2: Anuidade
            if (objetivoPagamento == "1")
            {
                // Evento:
                query = @"SELECT R.RecebimentoId, R.AssinaturaAnuidadeId, R.AssinaturaEventoId, 
                        R.Observacao, R.NotificationCodePS, R.TypePS, R.StatusPS, 
                        R.LastEventDatePS, R.TypePaymentMethodPS, R.CodePaymentMethodPS, 
                        R.GrossAmountPS, R.DiscountAmountPS, R.FeeAmountPS, R.NetAmountPS, R.ExtraAmountPS, 
                        R.DtAtualizacaoPS, R.DtVencimento, R.StatusFBTC, R.DtStatusFBTC, 
                        R.OrigemEmissaoTitulo, R.DtCadastro, R.Ativo 
                    FROM dbo.AD_Recebimento R  
                        INNER JOIN dbo.AD_Assinatura_Evento AE ON R.AssinaturaEventoId = AE.AssinaturaEventoId 
                        INNER JOIN dbo.AD_Associado A ON AE.AssociadoId = A.AssociadoId
                        INNER JOIN dbo.AD_Pessoal P ON A.PessoaId = P.PessoaId
                    WHERE P.PessoaId = @id Order by R.LastEventDatePS Desc";
            }
            else
            {
                // Anuidade:
                query = @"SELECT R.RecebimentoId, R.AssinaturaAnuidadeId, R.AssinaturaEventoId, 
                        R.Observacao, R.NotificationCodePS, R.TypePS, R.StatusPS, 
                        R.LastEventDatePS, R.TypePaymentMethodPS, R.CodePaymentMethodPS, 
                        R.GrossAmountPS, R.DiscountAmountPS, R.FeeAmountPS, R.NetAmountPS, R.ExtraAmountPS, 
                        R.DtAtualizacaoPS, R.DtVencimento, R.StatusFBTC, R.DtStatusFBTC, 
                        R.OrigemEmissaoTitulo, R.DtCadastro, R.Ativo 
                    FROM dbo.AD_Recebimento R  
                        INNER JOIN dbo.AD_Assinatura_Anuidade AA ON R.AssinaturaAnuidadeId = AA.AssinaturaAnuidadeId 
                        INNER JOIN dbo.AD_Associado A ON AA.AssociadoId = A.AssociadoId
                        INNER JOIN dbo.AD_Pessoal P ON A.PessoaId = P.PessoaId
                    WHERE P.PessoaId = @id Order by R.LastEventDatePS Desc";
            }

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<Recebimento> _collection = GetCollection<Recebimento>(cmd)?.ToList();
            
            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetRecebimentoByPessoaId",
                "SELECT", "RECEBIMENTO", id, query, _collection != null ? "SUCESSO" : "0");
            // Fim Log

            return _collection;
        }

        public string Insert(Recebimento r, string lastEventDate)
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
                transaction = connection.BeginTransaction("IncluirRecebimento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    string _atributos = "";
                    string _values = "";

                    // Formantando a data e hora de acordo com o valor passado pelo PagSeguro
                    string _dtEvent, _hrEvent;
                    _dtEvent = lastEventDate.Substring(0, lastEventDate.IndexOf("T")) + " " + lastEventDate.Substring(lastEventDate.IndexOf("T") + 1, 12);
                    _hrEvent = lastEventDate.Substring(lastEventDate.IndexOf("T"));

                    // Inserindo os dados na tabela:

                    if (r.AssinaturaAnuidadeId != null) {
                        _atributos = _atributos + ", AssinaturaAnuidadeId ";
                        _values = _values + ", @AssinaturaAnuidadeId ";
                        command.Parameters.AddWithValue("AssinaturaAnuidadeId", r.AssinaturaAnuidadeId);
                    }

                    if (r.AssinaturaEventoId != null)
                    {
                        _atributos = _atributos + ", AssinaturaEventoId ";
                        _values = _values + ", @AssinaturaEventoId ";
                        command.Parameters.AddWithValue("AssinaturaEventoId", r.AssinaturaEventoId);
                    }

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Recebimento ( " +
                        "   NotificationCodePS, TypePS, StatusPS, LastEventDatePS, TypePaymentMethodPS, " +
                        "   CodePaymentMethodPS, GrossAmountPS, DiscountAmountPS, FeeAmountPS, " +
                        "   NetAmountPS, ExtraAmountPS, DtCadastro, DtVencimento, StatusFBTC, " +
                        "   DtStatusFBTC, OrigemEmissaoTitulo, DtAtualizacaoPS " +
                        "    " + _atributos + ") " +
                        "VALUES( " +
                        "   @NotificationCodePS, @TypePS, @StatusPS, @LastEventDatePS, @TypePaymentMethodPS, " +
                        "   @CodePaymentMethodPS, @GrossAmountPS, @DiscountAmountPS, @FeeAmountPS, @NetAmountPS, @ExtraAmountPS, " +
                        "   @DtCadastro, @DtVencimento, @StatusFBTC, " +
                        "   @DtStatusFBCT, @OrigemEmissaoTitulo, @DtAtualizacaoPS " + 
                        "    " + _values + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("NotificationCodePS", r.NotificationCodePS);
                    command.Parameters.AddWithValue("TypePS", r.TypePS);
                    command.Parameters.AddWithValue("StatusPS", r.StatusPS);
                    command.Parameters.AddWithValue("LastEventDatePS", _dtEvent);
                    command.Parameters.AddWithValue("LastEventHourTZDPS", _hrEvent);
                    command.Parameters.AddWithValue("TypePaymentMethodPS", r.TypePaymentMethodPS);
                    command.Parameters.AddWithValue("CodePaymentMethodPS", r.CodePaymentMethodPS);
                    command.Parameters.AddWithValue("GrossAmountPS", r.GrossAmountPS);
                    command.Parameters.AddWithValue("DiscountAmountPS", r.DiscountAmountPS);
                    command.Parameters.AddWithValue("FeeAmountPS", r.FeeAmountPS);
                    command.Parameters.AddWithValue("NetAmountPS", r.NetAmountPS);
                    command.Parameters.AddWithValue("ExtraAmountPS", r.ExtraAmountPS);
                    command.Parameters.AddWithValue("DtVencimento", r.DtVencimento);
                    command.Parameters.AddWithValue("StatusFBTC", r.StatusFBTC);
                    command.Parameters.AddWithValue("DtStatusFBCT", DateTime.Now);
                    command.Parameters.AddWithValue("OrigemEmissaoTitulo", r.OrigemEmissaoTitulo);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);
                    command.Parameters.AddWithValue("DtAtualizacaoPS", DateTime.Now);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    if (id > 0)
                        _ident = _ident.PadLeft(10 - id.ToString().Length, '0') + id.ToString();

                    _msg = _resultado ? $"{_ident}Inclusão realizada com sucesso" : $"{_ident}Inclusão Não realizada com sucesso";

                    transaction.Commit();

                    // Log da Inserção RECEBIMENTO:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = id > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/Insert",
                        "INSERT", "RECEBIMENTO", id, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR RECEBIMENTO: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                    }

                    string log = logRep.SetLogger(className + "/Insert",
                        "INSERT", "ANUIDADE", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR RECEBIMENTO: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                }
                finally
                {
                    connection.Close();
                }
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


                    // Log do UPDATE RECEBIMENTO:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = i > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "RECEBIMENTO", id, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR RECEBIMENTO: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                        // throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "RECEBIMENTO", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR RECEBIMENTO: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                    // throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        /// <summary>
        /// Insere os dados na tabela de Recebimento para os títulos isentos de pagamento
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public string InsertRecebimentoIsencao(Recebimento r)
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
                transaction = connection.BeginTransaction("IncluirRecebimento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    string _atributos = "";
                    string _values = "";

                    // Inserindo os dados na tabela:
                    if (r.AssinaturaAnuidadeId != null)
                    {
                        _atributos = _atributos + ", AssinaturaAnuidadeId ";
                        _values = _values + ", @AssinaturaAnuidadeId ";
                        command.Parameters.AddWithValue("AssinaturaAnuidadeId", r.AssinaturaAnuidadeId);
                    }

                    if (r.AssinaturaEventoId != null)
                    {
                        _atributos = _atributos + ", AssinaturaEventoId ";
                        _values = _values + ", @AssinaturaEventoId ";
                        command.Parameters.AddWithValue("AssinaturaEventoId", r.AssinaturaEventoId);
                    }

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Recebimento ( " +
                        "   StatusPS, " +
                        "   GrossAmountPS, DiscountAmountPS, FeeAmountPS, " +
                        "   NetAmountPS, ExtraAmountPS, DtCadastro, DtVencimento, StatusFBTC, " +
                        "   DtStatusFBTC, OrigemEmissaoTitulo, LastEventDatePS, NotificationCodePS, Ativo " +
                        "    " + _atributos + ") " +
                        "VALUES( " +
                        "   @StatusPS, " +
                        "   @GrossAmountPS, @DiscountAmountPS, @FeeAmountPS, @NetAmountPS, @ExtraAmountPS, " +
                        "   @DtCadastro, @DtVencimento, @StatusFBTC, " +
                        "   @DtStatusFBCT, @OrigemEmissaoTitulo, @LastEventDatePS, @NotificationCodePS, @Ativo " +
                        "    " + _values + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    // command.Parameters.AddWithValue("Observacao", r.Observacao);
                    command.Parameters.AddWithValue("StatusPS", r.StatusPS);
                    command.Parameters.AddWithValue("GrossAmountPS", r.GrossAmountPS);
                    command.Parameters.AddWithValue("DiscountAmountPS", r.DiscountAmountPS);
                    command.Parameters.AddWithValue("FeeAmountPS", r.FeeAmountPS);
                    command.Parameters.AddWithValue("NetAmountPS", r.NetAmountPS);
                    command.Parameters.AddWithValue("ExtraAmountPS", r.ExtraAmountPS);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);
                    command.Parameters.AddWithValue("DtVencimento", r.DtVencimento);
                    command.Parameters.AddWithValue("StatusFBTC", r.StatusFBTC);
                    command.Parameters.AddWithValue("DtStatusFBCT", DateTime.Now);
                    command.Parameters.AddWithValue("OrigemEmissaoTitulo", r.OrigemEmissaoTitulo);
                    command.Parameters.AddWithValue("LastEventDatePS", DateTime.Now);
                    command.Parameters.AddWithValue("NotificationCodePS", r.NotificationCodePS);
                    command.Parameters.AddWithValue("Ativo", r.Ativo);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    if (id > 0)
                        _ident = _ident.PadLeft(10 - id.ToString().Length, '0') + id.ToString();

                    _msg = _resultado ? $"{_ident}Inclusão realizada com sucesso" : $"{_ident}Inclusão Não realizada com sucesso";

                    transaction.Commit();

                    // Log da Inserção RECEBIMENTO:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = id > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/InsertRecebimentoIsencao",
                        "INSERT", "RECEBIMENTO", id, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR RECEBIMENTO ISENTO: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                    }

                    string log = logRep.SetLogger(className + "/Insert",
                        "INSERT", "RECEBIMENTO", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar INCLUIR RECEBIMENTO ISENTO: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string UpdateRecebimentoIsencao(int id, Recebimento r)
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
                        "Update dbo.AD_Recebimento Set DtVencimento = @DtVencimento, Ativo = @Ativo  " +
                        "WHERE StatusPS = 0 and StatusFBTC = '3' and AssinaturaAnuidadeId = @AssinaturaAnuidadeId ";

                    // command.Parameters.AddWithValue("Observacao", r.Observacao);
                    command.Parameters.AddWithValue("DtVencimento ", r.DtVencimento);
                    command.Parameters.AddWithValue("Ativo", r.Ativo);
                    command.Parameters.AddWithValue("AssinaturaAnuidadeId", r.AssinaturaAnuidadeId);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

                    transaction.Commit();

                    _msg = _resultado ? "Atualização realizada com sucesso" : InsertRecebimentoIsencao(r);

                    // Log do UPDATE RECEBIMENTO:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = i > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/UpdateRecebimentoIsencao",
                        "UPDATE", "RECEBIMENTO", id, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR RECEBIMENTO: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                    }

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "RECEBIMENTO", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR RECEBIMENTO: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string DeleteRecebimentoIsencao(int assinaturaAnuidadeId, int statusPS)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("ExcluirRecebimento");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Atualizando os dados na tabela:
                    command.CommandText = "" +
                        "Delete FROM dbo.AD_Recebimento WHERE StatusPS = @StatusPS and AssinaturaAnuidadeId = @AssinaturaAnuidadeId ";

                    // command.Parameters.AddWithValue("Observacao", r.Observacao);
                    command.Parameters.AddWithValue("StatusPS ", statusPS);
                    command.Parameters.AddWithValue("AssinaturaAnuidadeId", assinaturaAnuidadeId);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

                    transaction.Commit();

                    _msg = _resultado ? "Exclusão realizada com sucesso" : "Exclusão não realizada com sucesso";

                    // Log da EXCLUSÃO RECEBIMENTO:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = i > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/DeleteRecebimentoIsencao",
                        "DELETE", "RECEBIMENTO", assinaturaAnuidadeId, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar EXCLUIR RECEBIMENTO: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                    }

                    string log = logRep.SetLogger(className + "/Delete",
                        "DELETE", "RECEBIMENTO", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar EXCLUIR RECEBIMENTO: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }
                     
        public string UpdateRecebimentoPagSeguro(int recebimentoId, Recebimento r,  string lastEventDate)
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

                // Formantando a data e hora de acordo com o valor passado pelo PagSeguro
                string _dtEvent, _hrEvent;
                _dtEvent = lastEventDate.Substring(0, lastEventDate.IndexOf("T")) + " " + lastEventDate.Substring(lastEventDate.IndexOf("T") + 1, 12);
                _hrEvent = lastEventDate.Substring(lastEventDate.IndexOf("T"));

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
                        "   GrossAmountPS = @GrossAmountPS, " +
                        "   DiscountAmountPS = @DiscountAmountPS, " +
                        "   FeeAmountPS = @FeeAmountPS, " +
                        "   NetAmountPS = @NetAmountPS, " +
                        "   ExtraAmountPS = @ExtraAmountPS, " +
                        "   DtAtualizacaoPS = @DtAtualizacaoPS " +
                        "WHERE NotificationCodePS = @NotificationCodePS ";

                    command.Parameters.AddWithValue("TypePS", r.TypePS);
                    command.Parameters.AddWithValue("StatusPS",  r.StatusPS);
                    command.Parameters.AddWithValue("LastEventDatePS", _dtEvent);
                    command.Parameters.AddWithValue("LastEventHourTZDPS", _hrEvent);
                    command.Parameters.AddWithValue("TypePaymentMethodPS", r.TypePaymentMethodPS);
                    command.Parameters.AddWithValue("CodePaymentMethodPS", r.CodePaymentMethodPS);
                    command.Parameters.AddWithValue("GrossAmountPS", r.GrossAmountPS);
                    command.Parameters.AddWithValue("DiscountAmountPS", r.DiscountAmountPS);
                    command.Parameters.AddWithValue("FeeAmountPS", r.FeeAmountPS);
                    command.Parameters.AddWithValue("NetAmountPS", r.NetAmountPS);
                    command.Parameters.AddWithValue("ExtraAmountPS", r.ExtraAmountPS);
                    command.Parameters.AddWithValue("DtAtualizacaoPS", DateTime.Now);
                    command.Parameters.AddWithValue("NotificationCodePS", r.NotificationCodePS);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

                    _msg = _resultado ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

                    transaction.Commit();

                    // Log do UpdateRecebimentoPagSeguro:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = i > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/UpdateRecebimentoPagSeguro",
                        "UPDATE", "RECEBIMENTO", 0, _instrucaoSql, _result);
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
                        _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR RECEBIMENTO PAG SEGURO: Commit Exception Type:{ex2.GetType()}. Erro:{ex2.Message}";
                        // throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }
                    string log = logRep.SetLogger(className + "/UpdateRecebimentoPagSeguro",
                        "UPDATE", "RECEBIMENTO", 0, ex.Message, "FALHA");

                    _msg = $"ATENÇÃO: Ocorreu um erro ao tentar ATUALIZAR RECEBIMENTO PAG SEGURO: Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}";
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public RecebimentoAssociadoDao GetRecebimentoAssociadoDaoByRecebimentoId(int id)
        {
            Recebimento rec = this.GetRecebimentoById(id);

            if (rec == null)
                return null;

            List<DbParameter> _parametros = new List<DbParameter>();

            SqlParameter pId = new SqlParameter() { ParameterName = "@id", Value = id };
            _parametros.Add(pId);

            if (rec.AssinaturaEventoId != null)
            {
                //Evento:
                query = @"SELECT EV.Titulo, 0 as Anuidade , P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.AssinaturaAnuidadeId, RE.AssinaturaEventoId, RE.TypePS, 
                        RE.StatusPS, RE.LastEventDatePS, RE.Ativo, RE.NotificationCodePS,  
                        RE.TypePaymentMethodPS, RE.codePaymentMethodPS, RE.GrossAmountPS, 
                        RE.DiscountAmountPS, RE.FeeAmountPS, RE.NetAmountPS, RE.ExtraAmountPS, RE.Observacao, 
                        RE.StatusFBTC, RE.DtStatusFBTC, RE.OrigemEmissaoTitulo,  RE.DtVencimento, 
                        P.EMail, P.NrCelular, P.Ativo as AtivoAssociado  
                    FROM dbo.AD_Associado A
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                        INNER JOIN dbo.AD_Assinatura_Evento AE ON A.AssociadoId = AE.AssociadoId
                        INNER JOIN dbo.AD_Recebimento RE ON AE.AssinaturaEventoId = RE.AssinaturaEventoId
                        INNER JOIN dbo.AD_Valor_Evento_Publico VEP ON AE.ValorEventoPublicoId = VEP.ValorEventoPublicoId
                        INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                        INNER JOIN dbo.AD_Evento EV ON VEP.EventoId = EV.EventoId
                    WHERE TP.Associado = 'true' 
                        AND RE.RecebimentoId = @id ";
            }
            else
            {
                //Anuidade:
                query = @"SELECT '' as Titulo, AN.Exercicio as Anuidade , P.Nome, P.CPF, TP.Nome as NomeTP, 
	                    RE.RecebimentoId, RE.AssinaturaAnuidadeId, RE.AssinaturaEventoId, RE.TypePS, 
                        RE.StatusPS, RE.LastEventDatePS, RE.Ativo, RE.NotificationCodePS, 
                        RE.TypePaymentMethodPS, RE.codePaymentMethodPS, RE.GrossAmountPS, 
                        RE.DiscountAmountPS, RE.FeeAmountPS, RE.NetAmountPS, RE.ExtraAmountPS, RE.Observacao, 
                        RE.StatusFBTC, RE.DtStatusFBTC, RE.OrigemEmissaoTitulo,  RE.DtVencimento, 
                        P.EMail, P.NrCelular, P.Ativo as AtivoAssociado  
                    FROM dbo.AD_Associado A
                        INNER JOIN dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId
                        INNER JOIN dbo.AD_Assinatura_Anuidade AA ON A.AssociadoId = AA.AssociadoId
                        INNER JOIN dbo.AD_Recebimento RE ON AA.AssinaturaAnuidadeId = RE.AssinaturaAnuidadeId
                        INNER JOIN dbo.AD_Valor_Anuidade VA ON AA.ValorAnuidadeId = VA.ValorAnuidadeId
                        INNER JOIN dbo.AD_Anuidade_Tipo_Publico ATP ON VA.AnuidadeTipoPublicoId = ATP.AnuidadeTipoPublicoId
                        INNER JOIN dbo.AD_Anuidade AN ON ATP.AnuidadeId = AN.AnuidadeId
                        INNER JOIN dbo.AD_Tipo_Publico TP ON A.TipoPublicoId = TP.TipoPublicoId
                    WHERE TP.Associado = 'true' 
                        AND RE.RecebimentoId = @id ";
            }

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            RecebimentoAssociadoDao _recebimentoAssociadoDao = GetCollection<RecebimentoAssociadoDao>(cmd)?.FirstOrDefault<RecebimentoAssociadoDao>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetRecebimentoAssociadoDaoByRecebimentoId",
                "SELECT", "RECEBIMENTO", id, query, _recebimentoAssociadoDao != null ? "SUCESSO" : "0");
            // Fim Log

            return _recebimentoAssociadoDao;
        }

        public string SaveDadosRecebimentoFromTransacaoPagSeguro(TransacaoPagSeguro transacaoPagSeguro)
        {
            throw new NotImplementedException();
        }

        public Recebimento GetRecebimentoByReference(string reference)
        {
            query = @"SELECT R.RecebimentoId, R.AssinaturaAnuidadeId, R.AssinaturaEventoId, 
                        R.Observacao, R.NotificationCodePS, R.TypePS, R.StatusPS, 
                        R.LastEventDatePS, R.TypePaymentMethodPS, R.CodePaymentMethodPS, 
                        R.GrossAmountPS, R.DiscountAmountPS, R.FeeAmountPS, R.NetAmountPS, R.ExtraAmountPS, 
                        R.DtAtualizacaoPS, R.DtVencimento, R.StatusFBTC, R.DtStatusFBTC, 
                        R.OrigemEmissaoTitulo, R.DtCadastro, R.Ativo 
                    FROM dbo.AD_Recebimento R 
                    INNER JOIN dbo.AD_Assinatura_Anuidade AA ON R.AssinaturaAnuidadeId = AA.AssinaturaAnuidadeId 
                    WHERE AA.Reference = @Reference";

            // Definição do parâmetros da consulta:
            SqlParameter paramId = new SqlParameter() { ParameterName = "@Reference", Value = reference };

            List<DbParameter> _parametros = new List<DbParameter>();
            _parametros.Add(paramId);
            // Fim da definição dos parâmetros

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros:_parametros);

            // Obtém os dados do banco de dados:
            Recebimento _recebimento = GetCollection<Recebimento>(cmd)?.FirstOrDefault<Recebimento>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetRecebimentoByReference",
                "SELECT", "RECEBIMENTO", 0, query, _recebimento != null ? "SUCESSO" : "0");
            // Fim Log

            return _recebimento;
        }

        public string DesativarByAssinaturaAnuidadeId(int assinaturaAnuidadeId)
        {
            throw new NotImplementedException();
        }
    }
}
