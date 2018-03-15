using System.Collections.Generic;
using System.Linq;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Infra.Helpers;
using prmToolkit.AccessMultipleDatabaseWithAdoNet;
using prmToolkit.AccessMultipleDatabaseWithAdoNet.Enumerators;

namespace Fbtc.Infra.Persistencia.AdoNet
{
    public class UnidadeFederacaoRepository : AbstractRepository, IUnidadeFederacaoRepository
    {
        private string query;
        private readonly string strConnSql;

        public UnidadeFederacaoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        public IEnumerable<UnidadeFederacao> GetAll()
        {
            query = @"SELECT UnidadeFederacaoId, SiglaUF, Nome 
                    FROM dbo.AD_UNIDADE_FEDERACAO
                    ORDER BY Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<UnidadeFederacao> UFCollection = GetCollection<UnidadeFederacao>(cmd)?.ToList();

            return UFCollection;
        }

        public IEnumerable<UnidadeFederacao> GetDisponiveis(int atcId)
        {
            query = @"SELECT UnidadeFederacaoId, SiglaUF, Nome 
                        FROM dbo.AD_UNIDADE_FEDERACAO
                        WHERE SiglaUF NOT IN (  SELECT DISTINCT UF 
                                                FROM AD_ATC 
                                                WHERE UF NOT IN (   SELECT UF 
                                                                    FROM AD_ATC A 
                                                                    WHERE A.AtcId = "+ atcId +")) " +
                     "ORDER BY Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<UnidadeFederacao> UFCollection = GetCollection<UnidadeFederacao>(cmd)?.ToList();

            return UFCollection;
        }

        public IEnumerable<UnidadeFederacao> GetUtilizadas()
        {
            query = @"SELECT UnidadeFederacaoId, SiglaUF, Nome 
                        FROM dbo.AD_UNIDADE_FEDERACAO
                        WHERE SiglaUF IN (  SELECT DISTINCT UF 
                                                FROM AD_ATC)
                     ORDER BY Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<UnidadeFederacao> UFCollection = GetCollection<UnidadeFederacao>(cmd)?.ToList();

            return UFCollection;
        }
    }
}
