using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

using Fbtc.Infra.Helpers;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Application.Helper;

using prmToolkit.AccessMultipleDatabaseWithAdoNet;
using prmToolkit.AccessMultipleDatabaseWithAdoNet.Enumerators;
using System.Text;
using System.Data.Common;

namespace Fbtc.Infra.Persistencia.AdoNet
{
    public class AssociadoRepository : AbstractRepository, IAssociadoRepository
    {
        private string query;
        private readonly string strConnSql;

        private LogRepository logRep;
        private readonly string className;
        private string _instrucaoSql = "";
        private string _result = "";
        
        public AssociadoRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");

            className = "AssociadoRepository";
            logRep = new LogRepository();
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Associado> FindByFilters(string nome, string cpf,
            string sexo, int atcId, string crp, string tipoProfissao, int tipoPublicoId, string estado, string cidade, bool? ativo)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT Distinct P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento, P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao 
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId ";

            if (!string.IsNullOrEmpty(estado) || !string.IsNullOrEmpty(cidade))
                query = query + "INNER JOIN dbo.AD_Endereco E ON P.PessoaId = E.PessoaId ";

            query = query + "WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(nome))
            {
                query = query + $" AND P.Nome Like '%'+ @nome +'%' ";
                SqlParameter pNome = new SqlParameter() { ParameterName = "@nome", Value = nome };
                _parametros.Add(pNome);
            }

            if (!string.IsNullOrEmpty(cpf))
            {
                query = query + $" AND P.CPF = @cpf ";
                SqlParameter pCpf = new SqlParameter() { ParameterName = "@cpf", Value = cpf };
                _parametros.Add(pCpf);
            }

            if (!string.IsNullOrEmpty(sexo))
            {
                query = query + $" AND P.Sexo = @sexo ";
                SqlParameter pSexo = new SqlParameter() { ParameterName = "@sexo", Value = sexo };
                _parametros.Add(pSexo);
            }
            if (atcId != 0)
            {
                query = query + $" AND A.AtcId = @atcId ";
                SqlParameter pAtcId = new SqlParameter() { ParameterName = "@atcId", Value = atcId };
                _parametros.Add(pAtcId);

            }
            if (!string.IsNullOrEmpty(crp))
            {
                query = query + $" AND A.CRP = @crp ";
                SqlParameter pCrp = new SqlParameter() { ParameterName = "@crp", Value = crp };
                _parametros.Add(pCrp);
            }
            if (!string.IsNullOrEmpty(tipoProfissao))
            {
                query = query + $" AND A.TipoProfissao = @tipoProfissao ";
                SqlParameter pTipoProfissao = new SqlParameter() { ParameterName = "@tipoProfissao", Value = tipoProfissao };
                _parametros.Add(pTipoProfissao);
            }
            if (tipoPublicoId != 0)
            {
                query = query + $" AND A.TipoPublicoId = @tipoPublicoId ";
                SqlParameter pTipoPublicoId = new SqlParameter() { ParameterName = "@tipoPublicoId", Value = tipoPublicoId };
                _parametros.Add(pTipoPublicoId);
            }
            if (!string.IsNullOrEmpty(estado))
            {
                query = query + $" AND E.Estado = @estado ";
                SqlParameter pEstado = new SqlParameter() { ParameterName = "@estado", Value = estado };
                _parametros.Add(pEstado);
            }
            if (!string.IsNullOrEmpty(cidade))
            {
                query = query + $" AND E.Cidade = @cidade ";
                SqlParameter pCidade = new SqlParameter() { ParameterName = "@cidade", Value = cidade };
                _parametros.Add(pCidade);
            }
            if (ativo != null)
            {
                query = query + $" AND P.Ativo = @ativo ";
                SqlParameter pAtivo = new SqlParameter() { ParameterName = "@ativo", Value = ativo };
                _parametros.Add(pAtivo);
            }

            query = query + " ORDER BY P.Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<Associado> _collection = GetCollection<Associado>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindByFilters",
                "SELECT", "ASSOCIADO", 0, query, _collection.Count<Associado>().ToString());
            // Fim Log

            return _collection;
        }

        public IEnumerable<AssociadoIsentoDao> FindIsentoByFilters(int isencaoId, string nome, string cpf,
            string sexo, int atcId, string crp, string tipoProfissao, int tipoPublicoId, string estado, string cidade, bool? ativo)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            SqlParameter pisencaoId = new SqlParameter() { ParameterName = "@isencaoId", Value = isencaoId };
            _parametros.Add(pisencaoId);

            query = @"SELECT AssociadoIsentoId, IsencaoId, AssociadoId, Nome, Cpf, Crp, AtcId, TipoPublicoId, Ativo from 
                        (   SELECT  0 as AssociadoIsentoId, 0 as IsencaoId, A.AssociadoId, P.Nome, P.Cpf, A.Crp, A.AtcId, A.TipoPublicoId, P.Ativo  
                            FROM dbo.AD_Associado A 
                            INNER JOIN  dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId 
                            WHERE A.AssociadoId not in (SELECT AI2.AssociadoId FROM dbo.AD_Associado_Isento AI2 WHERE AI2.IsencaoId = @isencaoId) 
                    UNION 
                            SELECT  AI.AssociadoIsentoId, AI.IsencaoId, A.AssociadoId, P.Nome, P.Cpf, A.Crp,  A.AtcId, A.TipoPublicoId, P.Ativo 
                            FROM dbo.AD_Associado A 
                            INNER JOIN  dbo.AD_Pessoa P ON A.PessoaId = P.PessoaId 
                            LEFT JOIN dbo.AD_Associado_Isento AI ON A.AssociadoId = AI.AssociadoId 
                            WHERE AI.IsencaoId =  @isencaoId) AS TAB 
                    WHERE AssociadoId IS NOT NULL ";

            if (!string.IsNullOrEmpty(nome))
            {
                query = query + $" AND Nome Like '%'+ @nome +'%' ";
                SqlParameter pNome = new SqlParameter() { ParameterName = "@nome", Value = nome };
                _parametros.Add(pNome);
            }

            if (!string.IsNullOrEmpty(cpf))
            {
                query = query + $" AND CPF = @cpf ";
                SqlParameter pCpf = new SqlParameter() { ParameterName = "@cpf", Value = cpf };
                _parametros.Add(pCpf);
            }

            /* if (!string.IsNullOrEmpty(sexo))
            {    
                query = query + $" AND Sexo = '{sexo}' ";
                SqlParameter pSexo = new SqlParameter() { ParameterName = "@sexo", Value = sexo };
                _parametros.Add(pSexo);
             }
             */

            if (atcId != 0)
            {
                query = query + $" AND AtcId = @atcId ";
                SqlParameter pAtcId = new SqlParameter() { ParameterName = "@atcId", Value = atcId };
                _parametros.Add(pAtcId);
            }

            if (!string.IsNullOrEmpty(crp))
            {
                query = query + $" AND CRP = @crp ";
                SqlParameter pCrp = new SqlParameter() { ParameterName = "@crp", Value = crp };
                _parametros.Add(pCrp);
            }

            /*if (!string.IsNullOrEmpty(tipoProfissao))
             {
                query = query + $" AND A.TipoProfissao = '{tipoProfissao}' ";
                SqlParameter pTipoProfissao = new SqlParameter() { ParameterName = "@tipoProfissao", Value = tipoProfissao };
                _parametros.Add(pTipoProfissao);   
             */

            if (tipoPublicoId != 0)
            {
                query = query + $" AND TipoPublicoId = @tipoPublicoId ";
                SqlParameter pTipoPublicoId = new SqlParameter() { ParameterName = "@tipoPublicoId", Value = tipoPublicoId };
                _parametros.Add(pTipoPublicoId);
            }

            /*if (!string.IsNullOrEmpty(estado))
             {
                query = query + $" AND Estado = '{estado}' ";
                SqlParameter pEstado = new SqlParameter() { ParameterName = "@estado", Value = estado };
                _parametros.Add(pEstado);   
             }

             if (!string.IsNullOrEmpty(cidade))
             {
                query = query + $" AND Cidade = '{cidade}' ";
                SqlParameter pCidade = new SqlParameter() { ParameterName = "@cidade", Value = cidade };
                _parametros.Add(pCidade);   
              }*/

            if (ativo != null)
            {
                query = query + $" AND Ativo = @ativo ";
                SqlParameter pAtivo = new SqlParameter() { ParameterName = "@ativo", Value = ativo };
                _parametros.Add(pAtivo);
            }

            query = query + " ORDER BY Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<AssociadoIsentoDao> _collection = GetCollection<AssociadoIsentoDao>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/FindIsentoByFilters",
                "SELECT", "ASSOCIADO", 0, query, _collection.Count<AssociadoIsentoDao>().ToString());
            // Fim Log

            return _collection;
        }

        public IEnumerable<Associado> GetAll()
        {
            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento, P.NrCelular, P.PasswordHash, P.DtCadastro, P.PerfilId, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao 
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId
                    ORDER BY P.Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Associado> _collection = GetCollection<Associado>(cmd)?.ToList();

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAll",
                "SELECT", "ASSOCIADO", 0, query, _collection.Count<Associado>().ToString());
            // Fim Log

            return _collection;
        }

        public Associado GetAssociadoById(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pid = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(pid);
            // Fim da definição dos parâmetros

            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.PerfilId, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao 
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId 
                    WHERE A.AssociadoId = @id";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            Associado associado = GetCollection<Associado>(cmd)?.FirstOrDefault<Associado>();

            // Obtendo um endereco:
            if (associado != null)
            {
                EnderecoRepository _endRep = new EnderecoRepository();

                var ends = _endRep.GetByPessoaId(associado.PessoaId);

                if (ends != null && ends.Count() > 0)
                {
                    associado.EnderecosPessoa = ends;
                }
            }

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAssociadoById",
                "SELECT", "ASSOCIADO", id, query, associado != null ? "SUCESSO" : "0");
            // Fim Log

            return associado;
        }

        public AssociadoDao GetAssociadoDaoById(int id, int anuidadeId)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pId = new SqlParameter() { ParameterName = "@id", Value = id };
            _parametros.Add(pId);

            SqlParameter pAnuidadeId = new SqlParameter() { ParameterName = "@anuidadeId", Value = anuidadeId };
            _parametros.Add(pAnuidadeId);
            // Fim da definição dos parâmetros

            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.PerfilId, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao,
	                    (SELECT 'AnuidadeAtcOk' =   
                            Case 
	                            WHEN Count(DAA.DescontoAnuidadeAtcId) > 0 THEN 'TRUE' 
	                            ELSE 'FALSE' 
                            END 
                        FROM dbo.AD_Desconto_Anuidade_Atc DAA 
                        WHERE DAA.AssociadoId = A.AssociadoId AND DAA.AnuidadeId = @anuidadeId )  AS AnuidadeAtcOk, 
	                    (SELECT 'MembroDiretoria' = 
	                        Case 
		                        WHEN Count(GE.GestaoId) > 0 THEN 'TRUE' 
		                        ELSE 'FALSE' 
	                        END  
                        FROM dbo.AD_Gestao GE 
                            INNER JOIN dbo.AD_Membro_Gestao MG ON GE.GestaoId = MG.GestaoId
                        WHERE MG.AssociadoId = A.AssociadoId  and (GE.AnoInicial <= (SELECT AN.Exercicio FROM dbo.AD_Anuidade AN where AN.AnuidadeId = @anuidadeId) 
                                and GE.AnoFinal >= (SELECT A.Exercicio FROM dbo.AD_Anuidade A where A.AnuidadeId = @anuidadeId ))
                        )  AS MembroDiretoria,
                        (SELECT 'MembroConfi' = 
	                        Case 
		                        WHEN Count(G.GestaoId) > 0 THEN 'TRUE' 
		                        ELSE 'FALSE' 
	                        END  
                        FROM dbo.AD_Gestao G 
                        INNER JOIN dbo.AD_Membro_Gestao MG ON G.GestaoId = MG.GestaoId
                        INNER JOIN dbo.AD_Cargo_Gestao CG ON MG.CargoGestaoId = CG.CargoGestaoId
                        WHERE UPPER(CG.Nome) = 'PRESIDENTE'
                        AND MG.AssociadoId = @id 
                        ) as MembroConfi
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId 
                    WHERE A.AssociadoId = @id";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            AssociadoDao associadoDao = GetCollection<AssociadoDao>(cmd)?.FirstOrDefault<AssociadoDao>();

            // Obtendo um endereco:
            if (associadoDao != null)
            {
                EnderecoRepository _endRep = new EnderecoRepository();

                var ends = _endRep.GetByPessoaId(associadoDao.PessoaId);

                if (ends != null && ends.Count() > 0)
                {
                    associadoDao.EnderecosPessoa = ends;
                }
            }

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAssociadoDaoById",
                "SELECT", "ASSOCIADO", id, query, associadoDao != null ? "SUCESSO" : "0");
            // Fim Log

            return associadoDao;
        }
        
        public AssociadoDao GetAssociadoDaoByPessoaId(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pId = new SqlParameter() { ParameterName = "@id", Value = id };
            _parametros.Add(pId);
            // Fim da definição dos parâmetros

            // As informações AnuidadeAtcOk e MembroDiretoria são obtidas através da data atual
            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.PerfilId, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao,
	                    (SELECT 'AnuidadeAtcOk' =   
                            Case 
	                            WHEN Count(DAA.DescontoAnuidadeAtcId) > 0 THEN 'TRUE' 
	                            ELSE 'FALSE' 
                            END 
                        FROM dbo.AD_Desconto_Anuidade_Atc DAA 
                        WHERE DAA.AssociadoId = A.AssociadoId AND DAA.AnuidadeId = (select AAA.AnuidadeId from dbo.AD_Anuidade AAA WHERE AAA.Exercicio = YEAR(GETDATE())))  AS AnuidadeAtcOk, 
	                    (SELECT 'MembroDiretoria' = 
	                        Case 
		                        WHEN Count(GE.GestaoId) > 0 THEN 'TRUE' 
		                        ELSE 'FALSE' 
	                        END  
                        FROM dbo.AD_Gestao GE 
                            INNER JOIN dbo.AD_Membro_Gestao MG ON GE.GestaoId = MG.GestaoId
                        WHERE MG.AssociadoId = A.AssociadoId  and (GE.AnoInicial <= (SELECT AN.Exercicio FROM dbo.AD_Anuidade AN where AN.AnuidadeId = (select AAA.AnuidadeId from dbo.AD_Anuidade AAA WHERE AAA.Exercicio = YEAR(GETDATE()))) 
                                and GE.AnoFinal >= (SELECT A.Exercicio FROM dbo.AD_Anuidade A where A.AnuidadeId = (select AAA.AnuidadeId from dbo.AD_Anuidade AAA WHERE AAA.Exercicio = YEAR(GETDATE()))))
                        )  AS MembroDiretoria,
                        (SELECT 'MembroConfi' = 
	                        Case 
		                        WHEN Count(G.GestaoId) > 0 THEN 'TRUE' 
		                        ELSE 'FALSE' 
	                        END  
                        FROM dbo.AD_Gestao G 
                        INNER JOIN dbo.AD_Membro_Gestao MG ON G.GestaoId = MG.GestaoId
                        INNER JOIN dbo.AD_Cargo_Gestao CG ON MG.CargoGestaoId = CG.CargoGestaoId
                        INNER JOIN dbo.AD_Associado A ON MG.AssociadoId = A.AssociadoId
                        WHERE UPPER(CG.Nome) = 'PRESIDENTE'
                        AND A.PessoaId = @id 
                        ) as MembroConfi
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId 
                    WHERE A.PessoaId = @id";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            AssociadoDao associadoDao = GetCollection<AssociadoDao>(cmd)?.FirstOrDefault<AssociadoDao>();

            // Obtendo um endereco:
            if (associadoDao != null)
            {
                EnderecoRepository _endRep = new EnderecoRepository();

                var ends = _endRep.GetByPessoaId(associadoDao.PessoaId);

                if (ends != null && ends.Count() > 0)
                {
                    associadoDao.EnderecosPessoa = ends;
                }
            }

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAssociadoDaoByPessoaId",
                "SELECT", "ASSOCIADO", id, query, associadoDao != null ? "SUCESSO" : "0");
            // Fim Log

            return associadoDao;
        }

        public Associado GetAssociadoByPessoaId(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pId = new SqlParameter() { ParameterName = "@id", Value = id };
            _parametros.Add(pId);
            // Fim da definição dos parâmetros

            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.PerfilId, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao 
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId 
                    WHERE P.PessoaId = @id";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            Associado associado = GetCollection<Associado>(cmd)?.FirstOrDefault<Associado>();

            // Obtendo um endereco:
            if (associado != null)
            {
                EnderecoRepository _endRep = new EnderecoRepository();

                var ends = _endRep.GetByPessoaId(associado.PessoaId);

                if (ends != null && ends.Count() > 0)
                {
                    associado.EnderecosPessoa = ends;
                }
            }

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetAssociadoByPessoaId",
                "SELECT", "ASSOCIADO", id, query, associado != null ? "SUCESSO" : "0");
            // Fim Log

            return associado;
        }

        public string Insert(Associado associado)
        {
            bool _resultado = false;
            string _msg = "";
            string _msgEnd = "";
            Int32 id = 0;
            Int32 assocId = 0;
            string _ident = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("IncluirAssociado");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Inserindo os dados na tabela PESSOA:
                    string _dtNasc = associado.DtNascimento != null ? ", DtNascimento " : "";
                    string _paramDtNasc = associado.DtNascimento != null ? ", @DtNascimento " : "";

                    if(associado.PerfilId == 0)
                    {
                        PerfilRepository perRep = new PerfilRepository();

                        associado.PerfilId = perRep.GetPerfilIdByNomePerfil("Cliente Externo");
                    }

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Pessoa (Nome, EMail, CPF, RG, NomeFoto, " +
                        "   Sexo, NrCelular, PerfilId, " +
                        "   DtCadastro " + _dtNasc + ") " +
                        "VALUES(@Nome, @EMail, @CPF, @RG, @NomeFoto, " +
                        "   @Sexo, @NrCelular, @PerfilId, " +
                        "   @DtCadastro " + _paramDtNasc + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Nome", associado.Nome);
                    command.Parameters.AddWithValue("EMail", associado.EMail);
                    command.Parameters.AddWithValue("CPF", associado.Cpf);
                    command.Parameters.AddWithValue("RG", associado.Rg);
                    command.Parameters.AddWithValue("NomeFoto", associado.NomeFoto);
                    command.Parameters.AddWithValue("Sexo", associado.Sexo);
                    command.Parameters.AddWithValue("NrCelular", associado.NrCelular);
                    command.Parameters.AddWithValue("PerfilId", associado.PerfilId);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);

                    if (_dtNasc != "")
                        command.Parameters.AddWithValue("DtNascimento", associado.DtNascimento);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    // Log da Inserção PESSOA:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = id > 0 ? "SUCESSO" : "FALHA";

                    string log = logRep.SetLogger(className + "/Insert",
                      "INSERT", "PESSOA", id, _instrucaoSql, _result);
                    //Fim do Log

                    // Inserindo os dados na tabela ASSOCIADO:
                    string _dtCert = associado.DtCertificacao != null ? ", DtCertificacao " : "";
                    string _paramDtCert = associado.DtCertificacao != null ? ", @DtCertificacao " : "";

                    string _atc = associado.ATCId != null ? ", AtcId " : "";
                    string _paramAtac = associado.ATCId != null ? ", @AtcId " : "";

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Associado (PessoaId "+ _atc +", TipoPublicoId, " +
                        "   NrMatricula, CRP, CRM, NomeInstFormacao, Certificado, " +
                        "   DivulgarContato, TipoFormaContato,  " +
                        "   NrTelDivulgacao, ComprovanteAfiliacaoAtc, TipoProfissao, TipoTitulacao " + _dtCert + ") " +
                        "VALUES (@PessoaId "+ _paramAtac +", @TipoPublicoId, " +
                        "   @NrMatricula, @CRP, @CRM, @NomeInstFormacao, @Certificado, " +
                        "   @DivulgarContato, @TipoFormaContato, " +
                        "   @NrTelDivulgacao, @ComprovanteAfiliacaoAtc, @TipoProfissao, @TipoTitulacao " + _paramDtCert + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("PessoaId", id);
                    command.Parameters.AddWithValue("TipoPublicoId", associado.TipoPublicoId);
                    command.Parameters.AddWithValue("NrMatricula", associado.NrMatricula);
                    command.Parameters.AddWithValue("CRP", associado.Crp);
                    command.Parameters.AddWithValue("CRM", associado.Crm);
                    command.Parameters.AddWithValue("NomeInstFormacao", associado.NomeInstFormacao);
                    command.Parameters.AddWithValue("Certificado", associado.Certificado);
                    command.Parameters.AddWithValue("DivulgarContato", associado.DivulgarContato);
                    command.Parameters.AddWithValue("TipoFormaContato", associado.TipoFormaContato);
                    command.Parameters.AddWithValue("NrTelDivulgacao", associado.NrTelDivulgacao);
                    command.Parameters.AddWithValue("ComprovanteAfiliacaoAtc", associado.ComprovanteAfiliacaoAtc);
                    command.Parameters.AddWithValue("TipoProfissao", associado.TipoProfissao);
                    command.Parameters.AddWithValue("TipoTitulacao", associado.TipoTitulacao);

                    if (_dtCert != "")
                        command.Parameters.AddWithValue("DtCertificacao", associado.DtCertificacao);

                    if(_atc != "")
                        command.Parameters.AddWithValue("AtcId", associado.ATCId);

                    assocId = (Int32)command.ExecuteScalar();

                    _resultado = assocId > 0;

                    if (id > 0)
                        _ident = _ident.PadLeft(10 - id.ToString().Length, '0') + id.ToString();

                    _msg = id > 0 ? $"{_ident}Inclusão realizada com sucesso" : $"{_ident}Inclusão Não realizada com sucesso";

                    transaction.Commit();

                    // Log da Inserção ASSOCIADO:
                    sb.Clear();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = assocId > 0 ? "SUCESSO" : "FALHA";

                    log = logRep.SetLogger(className + "/Insert",
                      "INSERT", "ASSOCIADO", assocId, _instrucaoSql, _result);
                    //Fim do Log

                    // Inserindo endereco:

                    EnderecoRepository _endRep = new EnderecoRepository();

                    if (associado.EnderecosPessoa != null)
                    {
                        foreach (var end in associado.EnderecosPessoa)
                        {

                            end.PessoaId = id;

                            if (end.EnderecoId != 0)
                            {
                                _msgEnd = _endRep.Update(end.EnderecoId, end);
                            }
                            else
                            {
                                _msgEnd = _endRep.Insert(end);
                            }
                        }
                    }
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
                    string log = logRep.SetLogger(className + "/Insert",
                        "INSERT", "PESSOA/ASSOCIADO", 0, ex.Message, "FALHA");

                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string Update(int id, Associado associado)
        {
            bool _resultado = false;
            string _msg = "";
            string _msgEnd = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarAssociado");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Atualizando os dados na tabela PESSOA:
                    string _dtNasc = associado.DtNascimento != null ? ", DtNascimento = @DtNascimento " : "";

                    if (associado.PerfilId == 0)
                    {
                        PerfilRepository perRep = new PerfilRepository();

                        associado.PerfilId = perRep.GetPerfilIdByNomePerfil("Cliente Externo");
                    }

                    command.CommandText = "" +
                        "UPDATE dbo.AD_Pessoa " +
                        "SET Nome = @nome, EMail = @EMail, NomeFoto = @NomeFoto, CPF = @CPF, RG = @RG, " +
                            "Sexo = @Sexo, NrCelular = @NrCelular, PerfilId = @PerfilId, " +
                            "Ativo = @Ativo " + _dtNasc +
                        "WHERE PessoaId = @id";

                    command.Parameters.AddWithValue("Nome", associado.Nome);
                    command.Parameters.AddWithValue("EMail", associado.EMail);
                    command.Parameters.AddWithValue("CPF", associado.Cpf);
                    command.Parameters.AddWithValue("RG", associado.Rg);
                    command.Parameters.AddWithValue("NomeFoto", associado.NomeFoto);
                    command.Parameters.AddWithValue("Sexo", associado.Sexo);
                    command.Parameters.AddWithValue("NrCelular", associado.NrCelular);
                    command.Parameters.AddWithValue("PerfilId", associado.PerfilId);
                    command.Parameters.AddWithValue("Ativo", associado.Ativo);
                    command.Parameters.AddWithValue("id", id);

                    if (_dtNasc != "")
                        command.Parameters.AddWithValue("DtNascimento", associado.DtNascimento);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

                    // Log do UPDATE PESSOA:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = i > 0 ? "SUCESSO" : "FALHA";


                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "PESSOA", id, _instrucaoSql, _result);
                    //Fim do Log

                    // Atualizando os dados na tabela Associado:
                    string _dtCert = associado.DtCertificacao != null ? ", DtCertificacao = @DtCertificacao " : "";
                    string _atc = associado.ATCId != null ? " AtcId = @AtcId, " : "";

                    command.CommandText = "" +
                        "UPDATE dbo.AD_Associado  " +
                        "SET TipoPublicoId = @TipoPublicoId , " + _atc +
                        "   NrMatricula = @NrMatricula, CRP = @CRP, CRM = @CRM, " +
                        "   NomeInstFormacao = @NomeInstFormacao, Certificado = @Certificado, " +
                        "   DivulgarContato = @DivulgarContato, TipoFormaContato = @TipoFormaContato, " +
                        "   NrTelDivulgacao = @NrTelDivulgacao, ComprovanteAfiliacaoAtc = @ComprovanteAfiliacaoAtc, " +
                        "   TipoProfissao = @TipoProfissao, TipoTitulacao = @TipoTitulacao  " + _dtCert +
                        "WHERE PessoaId = @id";

                    command.Parameters.AddWithValue("TipoPublicoId", associado.TipoPublicoId);
                    command.Parameters.AddWithValue("NrMatricula", associado.NrMatricula);
                    command.Parameters.AddWithValue("CRP", associado.Crp);
                    command.Parameters.AddWithValue("CRM", associado.Crm);
                    command.Parameters.AddWithValue("NomeInstFormacao", associado.NomeInstFormacao);
                    command.Parameters.AddWithValue("Certificado", associado.Certificado);
                    command.Parameters.AddWithValue("DivulgarContato", associado.DivulgarContato);
                    command.Parameters.AddWithValue("TipoFormaContato", associado.TipoFormaContato);
                    command.Parameters.AddWithValue("NrTelDivulgacao", associado.NrTelDivulgacao);
                    command.Parameters.AddWithValue("ComprovanteAfiliacaoAtc", associado.ComprovanteAfiliacaoAtc);
                    command.Parameters.AddWithValue("TipoProfissao", associado.TipoProfissao);
                    command.Parameters.AddWithValue("TipoTitulacao", associado.TipoTitulacao);

                    if (_dtCert != "")
                        command.Parameters.AddWithValue("DtCertificacao", associado.DtCertificacao);

                    if (_atc != "")
                        command.Parameters.AddWithValue("AtcId", associado.ATCId);

                    int x = command.ExecuteNonQuery();
                    _resultado = x > 0;

                    _msg = x > 0 ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

                    transaction.Commit();

                    // Log do UPDATE ASSOCIADO:
                    sb.Clear();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value +", "); 
                    }

                    _instrucaoSql = sb.ToString();
                    _result = x > 0 ? "SUCESSO" : "FALHA";

                    log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "ASSOCIADO", id, _instrucaoSql, _result);
                    // Fim do log

                    // Atualizando endereco:
                    if (associado.EnderecosPessoa != null)
                    {
                        foreach (var end in associado.EnderecosPessoa)
                        {
                            EnderecoRepository _endRep = new EnderecoRepository();

                            end.PessoaId = id;

                            if (end.EnderecoId != 0)
                            {
                                _msgEnd = _endRep.Update(end.EnderecoId, end);
                            }
                            else
                            {
                                _msgEnd = _endRep.Insert(end);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _result = ex.Message;
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }

                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "PESSOA/ASSOCIADO", 0, ex.Message, "FALHA");

                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string GetNomeFotoByPessoaId(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pId = new SqlParameter() { ParameterName = "@id", Value = id };
            _parametros.Add(pId);
            // Fim da definição dos parâmetros
            
            String NomeFoto = "_no-foto.png";

            query = @"SELECT P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao 
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId 
                    WHERE P.PessoaId = @id";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            Associado associado = GetCollection<Associado>(cmd)?.FirstOrDefault<Associado>();

            // Obtendo o nome da foto:
            if (associado != null)
            {
                NomeFoto = associado.NomeFoto;
            }

            // Log da consulta:
            string log = logRep.SetLogger(className + "/GetNomeFotoByPessoaId",
                "SELECT", "ASSOCIADO", 0, query, associado != null ? "SUCESSO" : "0");
            // Fim Log

            return NomeFoto;
        }

        public string RessetPasswordById(int id)
        {
            bool _sendSucess = false;
            string _msg = "", _newPassword = "", _newPasswordHash = "";
            bool _isBodyHtml = true;

            string _subject, _textBody;

            Associado _associado = new Associado();
            _associado = GetAssociadoByPessoaId(id);

            SendEMail _sendMail = new SendEMail();

            _newPassword = PasswordFunctions.GetNovaSenhaAcesso("");
            _newPasswordHash = PasswordFunctions.CriptografaSenha(_newPassword);

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("RessetSenhaAssociado");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Atualizando a senha na tabela PESSOA:
                    command.CommandText = "" +
                        "UPDATE dbo.AD_Pessoa " +
                        "SET PasswordHash = @PasswordHash " +
                        "WHERE PessoaId = @id";

                    command.Parameters.AddWithValue("PasswordHash", _newPasswordHash);
                    command.Parameters.AddWithValue("id", _associado.PessoaId);

                    int i = command.ExecuteNonQuery();

                    transaction.Commit();

                    // Log do UPDATE PESSOA:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Parâmetros: ");

                    for (int z = 0; z < command.Parameters.Count; z++)
                    {
                        sb.Append(command.Parameters[z].ParameterName + ": " + command.Parameters[z].Value + ", ");
                    }

                    _instrucaoSql = sb.ToString();
                    _result = i > 0 ? "SUCESSO" : "FALHA";


                    string log = logRep.SetLogger(className + "/Update",
                        "UPDATE", "PESSOA", id, _instrucaoSql, _result);
                    //Fim do Log

                    if (i > 0)
                    {
                        _subject = "Site FBTC - Troca de Senha - A sua nova senha de acesso chegou!";

                        _textBody = "<html><body> " +
                                    $"<p>Olá {_associado.Nome}!</p>" +
                                    "<p>Esta mensagem foi gerada pelo sistema Troca de Senha do Site FBTC.</p>" +
                                    "<p>Conforme solicitação através do site, a sua senha de acesso a sua conta no site fbtc.org.br foi reiniciada.</br></br>" +
                                        "Para você logar-se, por favor, informe o seu e-mail e a senha abaixo:</br></br></br>" +
                                        $"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>{_newPassword}</b></br></br></br>" +
                                        "Por favor, para seu segurança, troque-a no seu próximo acesso.</br></br></br>" +
                                        "<a href='http://administrativo.fbtc.org.br' target='_blank'>http://administrativo.fbtc.org.br - Acessar sua Conta</a></br>" +
                                    "</p>" +
                                    "<p><i>2018 - FBTC Federação Brasileira de Terapias Cognitivas - Direitos reservados.</i></p> " +
                                     "<p>Este é um e-mail automático da FBTC, por favor não o responda.</p> " +
                                    "</body></html> ";

                        _sendSucess = _sendMail.SendMessage(_associado.EMail, _subject, _isBodyHtml, _textBody);

                        _msg = _sendSucess == true ? $"A nova senha foi enviada para o e-mail: { _associado.EMail }." : "Houve uma falha no envio da sua senha";
                    }
                    else
                    {
                        _msg = "Atualização NÃO Realizada com sucesso";
                        _sendSucess = false;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("Mail") < 0)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                        }
                    }

                    if (ex.Message.IndexOf("System.Net.Mail.SmtpException") > 0)
                    {
                        return "ATENÇÃO: Não foi possível enviar o e-mail com a nova senha agora. Por favor, tente novamente mais tarde.";
                    }
                    else
                    {
                        throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string ValidaEMail(int associadoId, string eMail)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pAssociadoId = new SqlParameter() { ParameterName = "@associadoId", Value = associadoId };
            _parametros.Add(pAssociadoId);

            SqlParameter pEMail = new SqlParameter() { ParameterName = "@eMail", Value = eMail };
            _parametros.Add(pEMail);
            // Fim da definição dos parâmetros

            string _msg = "OK";

            try
            {
                query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        A.AssociadoId, A.PessoaId, A.AtcId, A.TipoPublicoId, P.CPF, P.RG, 
                        A.NrMatricula, A.CRP, A.CRM, A.NomeInstFormacao, A.Certificado, 
                        A.DtCertificacao, A.DivulgarContato, A.TipoFormaContato, 
                        A.NrTelDivulgacao, 
                        A.ComprovanteAfiliacaoAtc, A.TipoProfissao, A.TipoTitulacao 
                    FROM dbo.AD_Associado A 
                    INNER JOIN dbo.AD_Pessoa P on A.PessoaId = P.PessoaId 
                    WHERE AssociadoId != @associadoId
                        AND P.EMail = @eMail";

                // Define o banco de dados que será usando:
                CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

                // Obtém os dados do banco de dados:
                IEnumerable<Associado> _collection = GetCollection<Associado>(cmd)?.ToList();

                // Verificando se há registro:
                foreach (var item in _collection)
                {
                    if (item.PessoaId > 0)
                        _msg = $"Atenção: O EMail {eMail} já está um uso por outro usuário";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception Type:{ex.GetType()}. Erro:{ex.Message}");
            }
            return _msg;
        }
    }
}

