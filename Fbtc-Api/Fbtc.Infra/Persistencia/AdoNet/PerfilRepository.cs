using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Infra.Helpers;
using prmToolkit.AccessMultipleDatabaseWithAdoNet;
using prmToolkit.AccessMultipleDatabaseWithAdoNet.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Infra.Persistencia.AdoNet
{
    public class PerfilRepository : AbstractRepository, IPerfilRepository
    {
        private string query;
        private readonly string strConnSql;

        public PerfilRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }


        public IEnumerable<Perfil> GetAll(bool? isAtivo, string dominio)
        {
            string _where = "";
            string _ativo = "";
            string _dominio = "";

            _ativo = !isAtivo.Equals(null) ? $" where Ativo = '{isAtivo}' " : "";

            if (_ativo != "")
            {
                if (!string.IsNullOrEmpty(dominio))
                    _dominio = !string.IsNullOrEmpty(dominio) ? $" and Dominio = '{dominio}' " : "";
            }
            else
            {
                if (!string.IsNullOrEmpty(dominio))
                    _dominio = !string.IsNullOrEmpty(dominio) ? $" where Dominio = '{dominio}' " : "";
            }

            _where = _ativo + _dominio;

            query = @"SELECT PerfilId, 
                    Nome, TipoPerfil, Dominio, Ativo  
                    FROM dbo.AD_PERFIL " + _where + "" +
                    " ORDER BY Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Perfil> PerfilCollection = GetCollection<Perfil>(cmd)?.ToList();

            return PerfilCollection;
        }

        public Perfil GetPerfilById(int id)
        {
            query = @"SELECT PerfilId, 
                    Nome, TipoPerfil, Dominio, Ativo  
                    FROM dbo.AD_PERFIL  
                    WHERE PerfilId = " + id + "";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            Perfil perfil = GetCollection<Perfil>(cmd)?.First();

            return perfil;
        }
    }
}
