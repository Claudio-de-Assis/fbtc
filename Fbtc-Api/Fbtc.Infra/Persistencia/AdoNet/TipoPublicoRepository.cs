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
    public class TipoPublicoRepository : AbstractRepository, ITipoPublicoRepository
    {
        private string query;
        private readonly string strConnSql;

        public TipoPublicoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        public IEnumerable<TipoPublico> GetAll()
        {
            query = @"SELECT TipoPublicoId, 
                    Nome, Ativo, Ordem, Associado 
                    FROM dbo.AD_TIPO_PUBLICO 
                    ORDER BY Ordem";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<TipoPublico> TipoCollection = GetCollection<TipoPublico>(cmd)?.ToList();

            return TipoCollection;
        }

        public IEnumerable<TipoPublico> GetByTipoAssociacao(bool associado)
        {
            query = @"SELECT TipoPublicoId, LEFT(Nome,CHARINDEX(' -', Nome)) as Nome, Ativo, Ordem, Associado 
                    FROM dbo.AD_TIPO_PUBLICO 
                    WHERE Associado = '" + associado + "' ORDER BY Ordem";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<TipoPublico> TipoCollection = GetCollection<TipoPublico>(cmd)?.ToList();

            return TipoCollection;
        }

        public TipoPublico GetTipoPublicoById(int id)
        {
            query = @"SELECT TipoPublicoId, Nome, Ativo, Ordem, Associado 
                    FROM dbo.AD_TIPO_PUBLICO 
                    WHERE TipoPublicoId = " + id + "";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            TipoPublico tipo = GetCollection<TipoPublico>(cmd)?.First();

            return tipo;
        }

        public IEnumerable<TipoPublicoValorDao> GetTipoPublicoValorByEventoId(int id)
        {
            query = @"SELECT TP.TipoPublicoId, TP.Nome, TP.DescricaoValor, TP.Ordem, TP.Ativo, TP.Associado, 
		            ISNULL((SELECT VEP.ValorEventoPublicoId FROM
                        dbo.AD_Valor_Evento_Publico VEP
                        WHERE VEP.EventoId = " + id + @" AND VEP.TipoPublicoId = TP.TipoPublicoId),0) as ValorEventoPublicoId, " + id + @" AS EventoId,  
                    ISNULL((SELECT VEP.Valor FROM
                        dbo.AD_Valor_Evento_Publico VEP
                        WHERE VEP.EventoId = " + id + @" AND VEP.TipoPublicoId = TP.TipoPublicoId),0) as Valor,
		            ISNULL((SELECT VEP.Ativo FROM
                        dbo.AD_Valor_Evento_Publico VEP
                        WHERE VEP.EventoId = " + id + @" AND VEP.TipoPublicoId = TP.TipoPublicoId),0) as ValorAtivo
                    FROM dbo.AD_Tipo_Publico TP
                    WHERE TP.Ativo = 1
                    ORDER By TP.Ordem";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<TipoPublicoValorDao> tipoCollection = GetCollection<TipoPublicoValorDao>(cmd)?.ToList();

            return tipoCollection;
        }
    }
}
