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
    public class TipoPublicoRepository : AbstractRepository, ITipoPublicoRepository
    {
        private string query;
        private readonly string strConnSql;

        private LogRepository logRep;
        private readonly string className;
        private string _instrucaoSql = "";
        private string _result = "";

        public TipoPublicoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");

            className = "TipoPublicoRepository";
            logRep = new LogRepository();
        }

        public IEnumerable<TipoPublico> GetAll(bool? isAtivo)
        {

            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pIsAtivo = new SqlParameter() { ParameterName = "@isAtivo", Value = isAtivo };

            _parametros.Add(pIsAtivo);
            // Fim da definição dos parâmetros


            string _where = "";

            _where = !isAtivo.Equals(null) ? $" where Ativo = @isAtivo " : "";

            query = $@"SELECT TipoPublicoId, 
                        Nome, Ativo, Ordem, Associado, Codigo  
                    FROM dbo.AD_TIPO_PUBLICO {_where} 
                    ORDER BY Ordem";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<TipoPublico> TipoCollection = GetCollection<TipoPublico>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAll",
                "SELECT", "TIPO_PUBLICO", 0, query, TipoCollection != null ? "SUCESSO" : "0");
            // Fim Log

            return TipoCollection;
        }

        public IEnumerable<TipoPublico> GetByTipoAssociacao(bool associado)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pAssociado = new SqlParameter() { ParameterName = "@associado", Value = associado };

            _parametros.Add(pAssociado);
            // Fim da definição dos parâmetros

            query = @"SELECT TipoPublicoId, LEFT(Nome,CHARINDEX(' -', Nome)) as Nome, 
                        Ativo, Ordem, Associado, Codigo  
                    FROM dbo.AD_TIPO_PUBLICO 
                    WHERE Associado = @associado 
                    ORDER BY Ordem";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<TipoPublico> TipoCollection = GetCollection<TipoPublico>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetByTipoAssociacao",
                "SELECT", "TIPO_PUBLICO", 0, query, TipoCollection != null ? "SUCESSO" : "0");
            // Fim Log

            return TipoCollection;
        }

        public TipoPublico GetTipoPublicoById(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter paramId = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(paramId);
            // Fim da definição dos parâmetros

            query = @"SELECT TipoPublicoId, Nome, Ativo, Ordem, Associado, Codigo  
                    FROM dbo.AD_TIPO_PUBLICO 
                    WHERE TipoPublicoId = @id ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            TipoPublico tipo = GetCollection<TipoPublico>(cmd)?.FirstOrDefault<TipoPublico>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetTipoPublicoById",
                "SELECT", "TIPO_PUBLICO", id, query, tipo != null ? "SUCESSO" : "0");
            // Fim Log

            return tipo;
        }

        public IEnumerable<TipoPublicoValorDao> GetTipoPublicoValorByEventoId(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter paramId = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(paramId);
            // Fim da definição dos parâmetros

            query = @"SELECT TP.TipoPublicoId, TP.Nome, TP.DescricaoValor, TP.Ordem, TP.Ativo, TP.Associado, 
		            ISNULL((SELECT VEP.ValorEventoPublicoId FROM
                        dbo.AD_Valor_Evento_Publico VEP
                        WHERE VEP.EventoId = @id AND VEP.TipoPublicoId = TP.TipoPublicoId),0) as ValorEventoPublicoId, @id AS EventoId,  
                    ISNULL((SELECT VEP.Valor FROM
                        dbo.AD_Valor_Evento_Publico VEP
                        WHERE VEP.EventoId = @id AND VEP.TipoPublicoId = TP.TipoPublicoId),0) as Valor,
		            ISNULL((SELECT VEP.Ativo FROM
                        dbo.AD_Valor_Evento_Publico VEP
                        WHERE VEP.EventoId = @id AND VEP.TipoPublicoId = TP.TipoPublicoId),0) as ValorAtivo, Codigo 
                    FROM dbo.AD_Tipo_Publico TP
                    WHERE TP.Ativo = 1
                    ORDER By TP.Ordem";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<TipoPublicoValorDao> tipoCollection = GetCollection<TipoPublicoValorDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetTipoPublicoValorByEventoId",
                "SELECT", "TIPO_PUBLICO", id, query, tipoCollection != null ? "SUCESSO" : "0");
            // Fim Log

            return tipoCollection;
        }

        public int GetIdTipoPublicoByCodigo(string codigo)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pCodigo = new SqlParameter() { ParameterName = "@codigo", Value = codigo };

            _parametros.Add(pCodigo);
            // Fim da definição dos parâmetros

            query = @"SELECT TipoPublicoId, Nome, Ativo, Ordem, Associado, Codigo  
                    FROM dbo.AD_TIPO_PUBLICO 
                    WHERE Codigo = @codigo";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            TipoPublico tipo = GetCollection<TipoPublico>(cmd)?.FirstOrDefault<TipoPublico>();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetIdTipoPublicoByCodigo",
                "SELECT", "TIPO_PUBLICO", 0, query, tipo != null ? "SUCESSO" : "0");
            // Fim Log

            return tipo.TipoPublicoId;
        }
    }
}
