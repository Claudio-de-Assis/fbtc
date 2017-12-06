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
            query = @"SELECT TipoPublicoId, Nome, Ativo, Ordem 
                    FROM dbo.AD_TIPO_PUBLICO 
                    ORDER BY Ordem";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<TipoPublico> TipoCollection = GetCollection<TipoPublico>(cmd)?.ToList();

            return TipoCollection;
        }

        public TipoPublico GetTipoPublicoById(int id)
        {
            query = @"SELECT TipoPublicoId, Nome, Ativo, Ordem 
                    FROM dbo.AD_TIPO_PUBLICO 
                    WHERE TipoPublicoId = " + id + "";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            TipoPublico tipo = GetCollection<TipoPublico>(cmd)?.First();

            return tipo;
        }
    }
}
