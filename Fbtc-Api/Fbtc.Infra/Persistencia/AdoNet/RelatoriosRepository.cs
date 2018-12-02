using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

using Fbtc.Infra.Helpers;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;

using prmToolkit.AccessMultipleDatabaseWithAdoNet;
using prmToolkit.AccessMultipleDatabaseWithAdoNet.Enumerators;
using System.Data.Common;

namespace Fbtc.Infra.Persistencia.AdoNet
{
    public class RelatoriosRepository : AbstractRepository, IRelatoriosRepository
    {
        private string query;
        private readonly string strConnSql;

        public RelatoriosRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        public IEnumerable<RptTotalAssociadosDAO> GetRptAssociadosAno(int ano)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pAno = new SqlParameter() { ParameterName = "@ano", Value = ano };

            _parametros.Add(pAno);
            // Fim da definição dos parâmetros

            query = @"SELECT T.Nome as NomeTipoAssociado, T.Ordem,
                    (	SELECT COUNT(A1.AssociadoId) 
	                    FROM dbo.AD_Associado A1 
	                    INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
	                    WHERE P.Ativo = 1 
	                    AND YEAR(P.DtCadastro) = @ano
                       AND A1.TipoPublicoId = T.TipoPublicoId) Qtd, 
                    (	SELECT COUNT(A1.AssociadoId) 
	                    FROM dbo.AD_Associado A1 
	                    INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
	                    WHERE P.Ativo = 1 
                    AND YEAR(P.DtCadastro) = @ano) QtdTotal 
                    FROM dbo.AD_Tipo_Publico T 
                    ORDER BY T.Ordem ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<RptTotalAssociadosDAO> _collection = GetCollection<RptTotalAssociadosDAO>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<RptAssociadosEstadosDAO> GetRptAssociadosEstados()
        {
            query = @"SELECT U.Nome as NomeUF,
                        (   SELECT Count(P.PessoaId)
                            FROM dbo.AD_Associado A1 
                            INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
                            INNER JOIN dbo.AD_Endereco E ON P.PessoaId = E.PessoaId
                            INNER JOIN dbo.AD_Tipo_Publico TP ON A1.TipoPublicoId = TP.TipoPublicoId
                            WHERE P.Ativo = 1 
                            AND TP.Nome = 'Profissional - Associado'
                            AND E.Estado = U.SiglaUF ) ProfissionalAssociado,
                        (   SELECT Count(P.PessoaId)
                            FROM dbo.AD_Associado A1 
                            INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
                            INNER JOIN dbo.AD_Endereco E ON P.PessoaId = E.PessoaId
                            INNER JOIN dbo.AD_Tipo_Publico TP ON A1.TipoPublicoId = TP.TipoPublicoId
                            WHERE P.Ativo = 1 
                            AND TP.Nome = 'Profissional - Não Associado'
                            AND E.Estado = U.SiglaUF ) ProfissionalNaoAssociado,
                        (   SELECT Count(P.PessoaId)
                            FROM dbo.AD_Associado A1 
                            INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
                            INNER JOIN dbo.AD_Endereco E ON P.PessoaId = E.PessoaId
                            INNER JOIN dbo.AD_Tipo_Publico TP ON A1.TipoPublicoId = TP.TipoPublicoId
                            WHERE P.Ativo = 1 
                            AND TP.Nome = 'Estudante de Pós - Associado'
                            AND E.Estado = U.SiglaUF ) EstudantePosAssociado,
                        (   SELECT Count(P.PessoaId)
                            FROM dbo.AD_Associado A1 
                            INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
                            INNER JOIN dbo.AD_Endereco E ON P.PessoaId = E.PessoaId
                            INNER JOIN dbo.AD_Tipo_Publico TP ON A1.TipoPublicoId = TP.TipoPublicoId
                            WHERE P.Ativo = 1 
                            AND TP.Nome = 'Estudante de Pós - Não Associado'
                            AND E.Estado = U.SiglaUF ) EstudantePosNaoAssociado,
                        (   SELECT Count(P.PessoaId)
                            FROM dbo.AD_Associado A1 
                            INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
                            INNER JOIN dbo.AD_Endereco E ON P.PessoaId = E.PessoaId
                            INNER JOIN dbo.AD_Tipo_Publico TP ON A1.TipoPublicoId = TP.TipoPublicoId
                            WHERE P.Ativo = 1 
                            AND TP.Nome = 'Estudante - Associado'
                            AND E.Estado = U.SiglaUF ) EstudanteAssociado,
                        (   SELECT Count(P.PessoaId)
                            FROM dbo.AD_Associado A1 
                            INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
                            INNER JOIN dbo.AD_Endereco E ON P.PessoaId = E.PessoaId
                            INNER JOIN dbo.AD_Tipo_Publico TP ON A1.TipoPublicoId = TP.TipoPublicoId
                            WHERE P.Ativo = 1 
                            AND TP.Nome = 'Estudante - Não Associado'
                            AND E.Estado = U.SiglaUF ) EstudanteNaoAssociado
                    FROM dbo.AD_Unidade_Federacao U
					WHERE U.SiglaUF IN (SELECT E.Estado FROM dbo.AD_Endereco E)
                    ORDER By U.Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<RptAssociadosEstadosDAO> _collection = GetCollection<RptAssociadosEstadosDAO>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<RptAssociadoFaixaDAO> GetRptAssociadosFaixa()
        {
            query = @"SELECT	
                    (   SELECT Count(P.PessoaId) 
                        from dbo.AD_Pessoa P 
                        INNER JOIN dbo.AD_Associado A ON P.PessoaId = A.PessoaId
                        WHERE DATEDIFF(Year, P.DtNascimento, GetDate()) < 31) as 'FaixaAte30',
                    (   SELECT Count(P.PessoaId) 
                        from dbo.AD_Pessoa P 
                        INNER JOIN dbo.AD_Associado A ON P.PessoaId = A.PessoaId
                        WHERE DATEDIFF(Year, P.DtNascimento, GetDate()) > 30 
                        AND DATEDIFF(Year, P.DtNascimento, GetDate()) < 41) as 'Faixa31a40',
                    (   SELECT Count(P.PessoaId) 
                        from dbo.AD_Pessoa P 
                        INNER JOIN dbo.AD_Associado A ON P.PessoaId = A.PessoaId
                        WHERE DATEDIFF(Year, P.DtNascimento, GetDate()) > 40 
                        AND DATEDIFF(Year, P.DtNascimento, GetDate()) < 51) as 'Faixa41a50',
                    (   SELECT Count(P.PessoaId) 
                        from dbo.AD_Pessoa P 
                        INNER JOIN dbo.AD_Associado A ON P.PessoaId = A.PessoaId
                        WHERE DATEDIFF(Year, P.DtNascimento, GetDate()) > 50 
                        AND DATEDIFF(Year, P.DtNascimento, GetDate()) < 61) as 'Faixa51a60',
                    (   SELECT Count(P.PessoaId) 
                        from dbo.AD_Pessoa P 
                        INNER JOIN dbo.AD_Associado A ON P.PessoaId = A.PessoaId
                        WHERE DATEDIFF(Year, P.DtNascimento, GetDate()) > 60 ) as 'FaixaApos60'";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<RptAssociadoFaixaDAO> _collection = GetCollection<RptAssociadoFaixaDAO>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<RptTotalAssociadosDAO> GetRptAssociadosGenero(string genero)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pGenero = new SqlParameter() { ParameterName = "@genero", Value = genero };

            _parametros.Add(pGenero);
            // Fim da definição dos parâmetros

            query = @"SELECT T.Nome as NomeTipoAssociado, T.Ordem,
                        (	SELECT COUNT(A1.AssociadoId) 
	                        FROM dbo.AD_Associado A1 
	                        INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
	                        WHERE P.Ativo = 1 
	                        AND Sexo = @genero
                            AND A1.TipoPublicoId = T.TipoPublicoId) Qtd, 
                        (	SELECT COUNT(A1.AssociadoId) 
	                        FROM dbo.AD_Associado A1 
	                        INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
	                        WHERE P.Ativo = 1
	                        AND Sexo = @genero) QtdTotal 
                            FROM dbo.AD_Tipo_Publico T 
                        ORDER BY T.Ordem";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<RptTotalAssociadosDAO> _collection = GetCollection<RptTotalAssociadosDAO>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<RptRecebimentoStatusDAO> GetRptRecebimentoStatus(int objetivoPagamento, int anoEventoPS, int statusPS)
        {
            string R1status = "";
            string Status = "";

            if (statusPS != 0)
            {
                R1status = " AND R1.StatusPS = @statusPS ";
                Status = " AND R.StatusPS = @statusPS ";
            }

            List<DbParameter> _parametros = new List<DbParameter>();
            
            // Definição do parâmetros da consulta:
            SqlParameter pAnoEventoPS = new SqlParameter() { ParameterName = "@anoEventoPS", Value = anoEventoPS };
            SqlParameter pStatusPS = new SqlParameter() { ParameterName = "@statusPS", Value = statusPS };

            _parametros.Add(pAnoEventoPS);
            _parametros.Add(pStatusPS);
            // Fim da definição dos parâmetros

            if (objetivoPagamento == 2)
            {
                //Objetivo: Assinatura Anuidade
                query = $@"SELECT 
                        CASE StatusPS
	                        WHEN '1' THEN 'Aguardando pagamento'
	                        WHEN '2' THEN 'Em Análise'
	                        WHEN '3' THEN 'Paga'
	                        WHEN '4' THEN 'Disponível'
	                        WHEN '5' THEN 'Em Disputa'
	                        WHEN '6' THEN 'Devolvida'
	                        WHEN '7' THEN 'Cancelada'
	                        WHEN '8' THEN 'Debitado'
	                        WHEN '9' THEN 'Retenção Temporária'
	                        ELSE 'Não Identificado'
                        END AS StatusPagamento,
                        Count(RecebimentoId) Qtd,
                        Sum(NetAmountPS) ValorPorStatus,
                        (	Select Sum(R1.NetAmountPS) 
	                        FROM dbo.AD_Recebimento R1 
                            INNER JOIN dbo.AD_Assinatura_Anuidade AA1 ON R1.AssinaturaAnuidadeId = AA1.AssinaturaAnuidadeId
                            INNER JOIN dbo.AD_Valor_Anuidade VA1 ON AA1.ValorAnuidadeId = VA1.ValorAnuidadeId
                            INNER JOIN dbo.AD_Anuidade_Tipo_Publico ATP1 ON VA1.AnuidadeTipoPublicoId = ATP1.AnuidadeTipoPublicoId
                            INNER JOIN dbo.AD_Anuidade A1 ON ATP1.AnuidadeId = A1.AnuidadeId
	                        WHERE A1.Exercicio = @anoEventoPS {R1status}) ValorTotal 
                        FROM dbo.AD_Recebimento R 
                            INNER JOIN dbo.AD_Assinatura_Anuidade AA ON R.AssinaturaAnuidadeId = AA.AssinaturaAnuidadeId
                            INNER JOIN dbo.AD_Valor_Anuidade VA ON AA.ValorAnuidadeId = VA.ValorAnuidadeId
                            INNER JOIN dbo.AD_Anuidade_Tipo_Publico ATP ON VA.AnuidadeTipoPublicoId = ATP.AnuidadeTipoPublicoId
                            INNER JOIN dbo.AD_Anuidade A ON ATP.AnuidadeId = A.AnuidadeId
                        WHERE A.Exercicio = @anoEventoPS {Status}
                        Group By R.StatusPS";
            }
            else
            {
                //Objetivo: Assinatura Evento
                query = $@"SELECT 
                        CASE StatusPS
	                        WHEN '1' THEN 'Aguardando pagamento'
	                        WHEN '2' THEN 'Em Análise'
	                        WHEN '3' THEN 'Paga'
	                        WHEN '4' THEN 'Disponível'
	                        WHEN '5' THEN 'Em Disputa'
	                        WHEN '6' THEN 'Devolvida'
	                        WHEN '7' THEN 'Cancelada'
	                        WHEN '8' THEN 'Debitado'
	                        WHEN '9' THEN 'Retenção Temporária'
	                        ELSE 'Não Identificado'
                        END AS StatusPagamento,
                        Count(RecebimentoId) Qtd,
                        Sum(NetAmountPS) ValorPorStatus,
                        (	Select Sum(R1.NetAmountPS) 
	                        FROM dbo.AD_Recebimento R1
                            INNER JOIN dbo.AD_Assinatura_Evento AE1 ON R1.AssinaturaEventoId = AE1.AssinaturaEventoId
                            INNER JOIN dbo.AD_Valor_Evento_Publico VEP1 ON AE1.ValorEventoPublicoId = VEP1.ValorEventoPublicoId
                            INNER JOIN dbo.AD_Evento E1 ON VEP1.EventoId = E1.EventoId
	                        WHERE YEAR(E1.DtTermino) = @anoEventoPS {R1status}) ValorTotal 
                        FROM dbo.AD_Recebimento R
                            INNER JOIN dbo.AD_Assinatura_Evento AE ON R.AssinaturaEventoId = AE.AssinaturaEventoId
                            INNER JOIN dbo.AD_Valor_Evento_Publico VEP ON AE.ValorEventoPublicoId = VEP.ValorEventoPublicoId
                            INNER JOIN dbo.AD_Evento E ON VEP.EventoId = E.EventoId
                        WHERE YEAR(E.DtTermino) = @anoEventoPS {Status}
                        Group By R.StatusPS";
            }

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<RptRecebimentoStatusDAO> _collection = GetCollection<RptRecebimentoStatusDAO>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<RptTotalAssociadosDAO> GetRptTotalAssociadosTipo()
        {
            query = @"SELECT T.Nome as NomeTipoAssociado, T.Ordem,
                        (	SELECT COUNT(A1.AssociadoId) 
	                        FROM dbo.AD_Associado A1 
	                        INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
	                        WHERE P.Ativo = 1 
                            AND A1.TipoPublicoId = T.TipoPublicoId) Qtd,
                        (	SELECT COUNT(A1.AssociadoId) 
	                        FROM dbo.AD_Associado A1 
	                        INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
	                        WHERE P.Ativo = 1) QtdTotal
                    FROM dbo.AD_Tipo_Publico T 
                    ORDER BY T.Ordem";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<RptTotalAssociadosDAO> _collection = GetCollection<RptTotalAssociadosDAO>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<RptReceitaAnualDAO> GetRptReceitaAnual()
        {
            query = @"SELECT Ano, NomeObjetivoPagamento, ValorPrevisto, ValorRealizado, 
                    QtdIsentos from (
                        --Assinatura anuidade:
                        SELECT YEAR(R.DtCadastro) Ano, 
	                        CASE R.AssinaturaAnuidadeId 
		                        WHEN NULL THEN 'Eventos'
		                        ELSE 'Anuidades'
	                        END as NomeObjetivoPagamento,
	                        (	SELECT Isnull(SUM(NetAmountPS),0) 
		                        FROM dbo.AD_Recebimento 
		                        WHERE YEAR(DtCadastro) = YEAR(R.DtCadastro)
		                        AND AssinaturaAnuidadeId Is Not Null
		                        AND StatusPS in ('1','2','3','4','5')) as ValorPrevisto,

	                        (	SELECT Isnull(SUM(NetAmountPS),0) 
		                        FROM dbo.AD_Recebimento 
		                        WHERE YEAR(DtCadastro) = YEAR(R.DtCadastro)
		                        AND AssinaturaAnuidadeId Is Not Null
		                        AND StatusPS in ('3','4')) as ValorRealizado,
	
	                        (	SELECT COUNT(AssinaturaAnuidadeId) 
		                        FROM dbo.AD_Assinatura_Anuidade 
		                        WHERE YEAR(DtCadastro) = YEAR(R.DtCadastro)
		                        AND PercentualDesconto = 100) as QtdIsentos
	
                        FROM  dbo.AD_Recebimento R
                        WHERE R.Ativo = 1 AND R.AssinaturaAnuidadeId is not null

                        UNION 

                        --Assinatura Evento:
                        SELECT YEAR(R.DtCadastro) Ano, 
	                        CASE R.AssinaturaEventoId 
		                        WHEN NULL THEN 'Anuidades'
		                        ELSE 'Eventos'
	                        END as NomeObjetivoPagamento,
	                        (	SELECT Isnull(SUM(NetAmountPS),0) 
		                        FROM dbo.AD_Recebimento 
		                        WHERE YEAR(DtCadastro) = YEAR(R.DtCadastro)
		                        AND AssinaturaEventoId is not null
		                        AND StatusPS in ('1','2','3','4','5')) as ValorPrevisto,

	                        (	SELECT Isnull(SUM(NetAmountPS),0) 
		                        FROM dbo.AD_Recebimento 
		                        WHERE YEAR(DtCadastro) = YEAR(R.DtCadastro)
		                        AND AssinaturaEventoId is not null
		                        AND StatusPS in ('3','4')) as ValorRealizado,
	
	                        (	SELECT COUNT(AssinaturaEventoId) 
		                        FROM dbo.AD_Assinatura_Evento 
		                        WHERE YEAR(DtCadastro) = YEAR(R.DtCadastro)
		                        AND PercentualDesconto = 100) as QtdIsentos
	
                        FROM  dbo.AD_Recebimento R
                        WHERE R.Ativo = 1 AND R.AssinaturaEventoId is not null ) as TAB
                        ORDER BY Ano DESC, NomeObjetivoPagamento";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<RptReceitaAnualDAO> _collection = GetCollection<RptReceitaAnualDAO>(cmd)?.ToList();

            return _collection;
        }
    }
}
