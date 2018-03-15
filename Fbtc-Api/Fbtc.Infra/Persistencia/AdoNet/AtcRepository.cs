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
    public class AtcRepository : AbstractRepository, IAtcRepository
    {
        private string query;
        private readonly string strConnSql;

        public AtcRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Atc> GetAll()
        {
            query = @"SELECT AtcId, Nome, UF, NomePres, NomeVPres, NomePSec, NomeSSec,
                        NomePTes, NomeSTes, Site, SiteDiretoria, Ativo, Codigo
                    FROM dbo.AD_ATC
                    ORDER BY Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Atc> AtcCollection = GetCollection<Atc>(cmd)?.ToList();

            return AtcCollection;
        }

        public Atc GetAtcById(int id)
        {
            query = @"SELECT AtcId, Nome, UF, NomePres, NomeVPres, NomePSec, NomeSSec,
                        NomePTes, NomeSTes, Site, SiteDiretoria, Ativo, Codigo
                    FROM dbo.AD_ATC 
                    WHERE AtcId = " + id + "";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            Atc atc = GetCollection<Atc>(cmd)?.First();

            return atc;
        }

        public string Insert(Atc atc)
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
                transaction = connection.BeginTransaction("IncluirATC");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_ATC (Nome, UF, NomePres, NomeVPres, NomePSec, NomeSSec, " +
                        "   NomePTes, NomeSTes, Site, SiteDiretoria, Codigo) " +
                        "VALUES(@Nome, @UF, @NomePres, @NomeVPres, @NomePSec, @NomeSSec, " +
                        "  @NomePTes, @NomeSTes, @Site, @SiteDiretoria, @Codigo) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Nome", atc.Nome);
                    command.Parameters.AddWithValue("UF", atc.UF);
                    command.Parameters.AddWithValue("NomePres", atc.NomePres);
                    command.Parameters.AddWithValue("NomeVPres", atc.NomeVPres);
                    command.Parameters.AddWithValue("NomePSec", atc.NomePSec);
                    command.Parameters.AddWithValue("NomeSSec", atc.NomeSSec);
                    command.Parameters.AddWithValue("NomePTes", atc.NomePTes);
                    command.Parameters.AddWithValue("NomeSTes", atc.NomeSTes);
                    command.Parameters.AddWithValue("Site", atc.Site);
                    command.Parameters.AddWithValue("SiteDiretoria", atc.SiteDiretoria);
                    command.Parameters.AddWithValue("Codigo", atc.Codigo);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    _msg = id > 0 ? "Inclusão realiada com sucesso" : "Inclusão Não realiada com sucesso";

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

        public string Update(int id, Atc atc)
        {
            bool _resultado = false;
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarATC");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "UPDATE dbo.AD_Atc " +
                        "SET Nome = @Nome, UF = @UF, NomePres = @NomePres, NomeVPres = @NomeVPres, " +
                        "   NomePSec = @NomePSec, NomeSSec = @NomeSSec, NomePTes = @NomePTes, " +
                        "   NomeSTes = @NomeSTes, Site = @Site, SiteDiretoria = @SiteDiretoria, " +
                        "   Codigo = @Codigo, Ativo = @Ativo " +
                        "WHERE AtcId = @id";

                    command.Parameters.AddWithValue("Nome", atc.Nome);
                    command.Parameters.AddWithValue("UF", atc.UF);
                    command.Parameters.AddWithValue("NomePres", atc.NomePres);
                    command.Parameters.AddWithValue("NomeVPres", atc.NomeVPres);
                    command.Parameters.AddWithValue("NomePSec", atc.NomePSec);
                    command.Parameters.AddWithValue("NomeSSec", atc.NomeSSec);
                    command.Parameters.AddWithValue("NomePTes", atc.NomePTes);
                    command.Parameters.AddWithValue("NomeSTes", atc.NomeSTes);
                    command.Parameters.AddWithValue("Site", atc.Site);
                    command.Parameters.AddWithValue("SiteDiretoria", atc.SiteDiretoria);
                    command.Parameters.AddWithValue("Codigo", atc.Codigo);
                    command.Parameters.AddWithValue("Ativo", atc.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realiada com sucesso" : "Atualização NÃO realiada com sucesso";

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

        public IEnumerable<AtcDao> GetAllLst()
        {
            query = @"SELECT AtcId, Nome
                    FROM dbo.AD_ATC 
                    Where Ativo = '1' 
                    ORDER BY Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<AtcDao> AtcCollection = GetCollection<AtcDao>(cmd)?.ToList();

            return AtcCollection;
        }


        public IEnumerable<Atc> FindByFilters(string siglaUF)
        {
            query = @"SELECT AtcId, Nome, UF, NomePres, NomeVPres, NomePSec, NomeSSec,
                        NomePTes, NomeSTes, Site, SiteDiretoria, Ativo, Codigo
                    FROM dbo.AD_ATC
                    WHERE AtcId > 0 ";

            if (!siglaUF.Equals("0"))
                query = query + $" AND UF = '{siglaUF}' ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Atc> _collection = GetCollection<Atc>(cmd)?.ToList();

            return _collection;
        }
    }
}
