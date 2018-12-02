using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Infra.Helpers;
using prmToolkit.AccessMultipleDatabaseWithAdoNet;
using prmToolkit.AccessMultipleDatabaseWithAdoNet.Enumerators;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Infra.Persistencia.AdoNet
{
    public class PerfilRepository : AbstractRepository, IPerfilRepository
    {
        private string query;
        private readonly string strConnSql;

        private LogRepository logRep;
        private readonly string className;
        private string _instrucaoSql = "";
        private string _result = "";

        public PerfilRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");

            className = "PerfilRepository";
            logRep = new LogRepository();
        }


        public IEnumerable<Perfil> GetAll(bool? isAtivo, string dominio)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pisAtivo = new SqlParameter() { ParameterName = "@isAtivo", Value = isAtivo };
            _parametros.Add(pisAtivo);

            SqlParameter pdominio = new SqlParameter() { ParameterName = "@dominio", Value = dominio };
            _parametros.Add(pdominio);
            // Fim da definição dos parâmetros

            string _where = "";
            string _ativo = "";
            string _dominio = "";

            _ativo = !isAtivo.Equals(null) ? $" where Ativo = @isAtivo " : "";

            if (_ativo != "")
            {
                if (!string.IsNullOrEmpty(dominio))
                    _dominio = !string.IsNullOrEmpty(dominio) ? $" and Dominio = @dominio " : "";
            }
            else
            {
                if (!string.IsNullOrEmpty(dominio))
                    _dominio = !string.IsNullOrEmpty(dominio) ? $" where Dominio = @dominio " : "";
            }

            _where = _ativo + _dominio;

            query = @"SELECT PerfilId, 
                    Nome, TipoPerfil, Dominio, Ativo  
                    FROM dbo.AD_PERFIL " + _where + "" +
                    " ORDER BY Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<Perfil> perfilCollection = GetCollection<Perfil>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAll",
                "SELECT", "PERFIL", 0, query, perfilCollection.Count<Perfil>().ToString());
            // Fim Log

            return perfilCollection;
        }

        public Perfil GetPerfilById(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pid = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(pid);
            // Fim da definição dos parâmetros

            query = @"SELECT PerfilId, 
                    Nome, TipoPerfil, Dominio, Ativo  
                    FROM dbo.AD_PERFIL  
                    WHERE PerfilId = @id";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            Perfil perfil = GetCollection<Perfil>(cmd)?.FirstOrDefault<Perfil>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetPerfilById",
                "SELECT", "PERFIL", id, query, perfil != null ? "SUCESSO" : "FALHA");
            // Fim Log

            return perfil;
        }

        public int GetPerfilIdByNomePerfil(string nomePerfil)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pnomePerfil = new SqlParameter() { ParameterName = "@nomePerfil", Value = nomePerfil };

            _parametros.Add(pnomePerfil);
            // Fim da definição dos parâmetros

            int id = 0;

            query = @"SELECT PerfilId 
                    FROM dbo.AD_PERFIL  
                    WHERE Nome = @nomePerfil";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            Perfil perfil = GetCollection<Perfil>(cmd)?.FirstOrDefault<Perfil>();

            id = perfil != null ? perfil.PerfilId : 0;

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetPerfilIdByNomePerfil",
                "SELECT", "PERFIL", id, query, perfil != null ? "SUCESSO" : "FALHA");
            // Fim Log

            return id;
        }
    }
}
