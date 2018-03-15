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
            query = @"SELECT T.Nome as NomeTipoAssociado, T.Ordem,
                    (	SELECT COUNT(A1.AssociadoId) 
	                    FROM dbo.AD_Associado A1 
	                    INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
	                    WHERE P.Ativo = 1 
	                    AND YEAR(P.DtCadastro) = "+ ano +" " +
                    "   AND A1.TipoPublicoId = T.TipoPublicoId) Qtd, "+
                    "(	SELECT COUNT(A1.AssociadoId) "+
	                "    FROM dbo.AD_Associado A1 "+
	                "    INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId "+
	                "    WHERE P.Ativo = 1" +
                    "AND YEAR(P.DtCadastro) = " + ano + ") QtdTotal "+
                    "FROM dbo.AD_Tipo_Publico T "+
                    "ORDER BY T.Ordem";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

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
            query = @"SELECT T.Nome as NomeTipoAssociado, T.Ordem,
                        (	SELECT COUNT(A1.AssociadoId) 
	                        FROM dbo.AD_Associado A1 
	                        INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
	                        WHERE P.Ativo = 1 
	                        AND Sexo = '" + genero + "' ";
            query = query + @"AND A1.TipoPublicoId = T.TipoPublicoId) Qtd, 
                        (	SELECT COUNT(A1.AssociadoId) 
	                        FROM dbo.AD_Associado A1 
	                        INNER JOIN dbo.AD_Pessoa P ON A1.PessoaId = P.PessoaId 
	                        WHERE P.Ativo = 1
	                        AND Sexo = '" + genero + "' ) QtdTotal ";
            query = query + @"FROM dbo.AD_Tipo_Publico T 
                        ORDER BY T.Ordem";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

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
                R1status = " AND R1.StatusPS = " + statusPS + " ";
                Status = " AND StatusPS = " + statusPS + " ";
            }

            query = @"SELECT 
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
		                    WHERE R1.ObjetivoPagamento = " + objetivoPagamento + " " +
                        "AND YEAR(R1.LastEventDatePS) = "+ anoEventoPS + R1status + ") ValorTotal " +
                        "FROM dbo.AD_Recebimento WHERE ObjetivoPagamento = " + objetivoPagamento + " " +
                        "AND ReferencePS is not null AND YEAR(LastEventDatePS) = " + anoEventoPS + Status + " " +
                        "Group By StatusPS";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

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
            query = @"SELECT YEAR(R.DtCadastro) Ano, 
	                    CASE R.ObjetivoPagamento
		                    WHEN 1 THEN 'Eventos'
		                    WHEN 2 THEN 'Mensalidades'
	                    END as NomeObjetivoPagamento,
	                    (	SELECT Isnull(SUM(NetAmountPS),0) 
		                    FROM dbo.AD_Recebimento 
		                    WHERE AssociadoIsentoId is null 
		                    AND YEAR(DtCadastro) = YEAR(R.DtCadastro)
		                    AND ObjetivoPagamento = R.ObjetivoPagamento
		                    AND StatusPS in ('1','2','3','4','5')) as ValorPrevisto,

	                    (	SELECT Isnull(SUM(NetAmountPS),0) 
		                    FROM dbo.AD_Recebimento 
		                    WHERE AssociadoIsentoId is null 
		                    AND YEAR(DtCadastro) = YEAR(R.DtCadastro)
		                    AND ObjetivoPagamento = R.ObjetivoPagamento
		                    AND StatusPS in ('3','4')) as ValorRealizado,
	
	                    (	SELECT COUNT(RecebimentoId) 
		                    FROM dbo.AD_Recebimento 
		                    WHERE AssociadoIsentoId is not null 
		                    AND YEAR(DtCadastro) = YEAR(R.DtCadastro)
		                    AND ObjetivoPagamento = R.ObjetivoPagamento) as QtdIsentos
	
	                    FROM  dbo.AD_Recebimento R
	                    WHERE R.AssociadoIsentoId is null
	                    GROUP BY YEAR(R.DtCadastro), R.ObjetivoPagamento
	                    ORDER BY YEAR(R.DtCadastro) DESC, R.ObjetivoPagamento";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<RptReceitaAnualDAO> _collection = GetCollection<RptReceitaAnualDAO>(cmd)?.ToList();

            return _collection;
        }
    }
}
