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
    public class ColaboradorRepository : AbstractRepository, IColaboradorRepository
    {
        private string query;
        private readonly string strConnSql;

        public ColaboradorRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colaborador> FindByFilters(string nome, int perfilId, bool? ativo)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            query = @"SELECT Distinct P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento, P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        C.ColaboradorId, P.PerfilId  
                    FROM dbo.AD_Colaborador C 
                    INNER JOIN dbo.AD_Pessoa P on C.PessoaId = P.PessoaId
                    WHERE 1 = 1  ";

            if (!string.IsNullOrEmpty(nome))
            {
                query = query + $" AND P.Nome Like '%'+ @nome +'%' ";

                SqlParameter pnome = new SqlParameter() { ParameterName = "@nome", Value = nome };
                _parametros.Add(pnome);
            }

            if (perfilId > 0)
            {
                query = query + $" AND P.PerfilId = @perfilId ";

                SqlParameter pperfilId = new SqlParameter() { ParameterName = "@perfilId", Value = perfilId };
                _parametros.Add(pperfilId);
            }

            if (ativo != null)
            {
                query = query + $" AND P.Ativo = @ativo ";

                SqlParameter pativo = new SqlParameter() { ParameterName = "@ativo", Value = ativo };
                _parametros.Add(pativo);
            }

            query = query + " ORDER BY P.Nome ";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            IEnumerable<Colaborador> _collection = GetCollection<Colaborador>(cmd)?.ToList();

            return _collection;
        }

        public IEnumerable<Colaborador> GetAll()
        {
            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento, P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        C.ColaboradorId, P.PerfilId  
                    FROM dbo.AD_Colaborador C 
                    INNER JOIN dbo.AD_Pessoa P on C.PessoaId = P.PessoaId
                    ORDER BY P.Nome";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer);

            // Obtém os dados do banco de dados:
            IEnumerable<Colaborador> _collection = GetCollection<Colaborador>(cmd)?.ToList();

            return _collection;
        }

        public Colaborador GetColaboradorById(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pid = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(pid);
            // Fim da definição dos parâmetros

            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento, P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        C.ColaboradorId, P.PerfilId  
                    FROM dbo.AD_Colaborador C 
                    INNER JOIN dbo.AD_Pessoa P on C.PessoaId = P.PessoaId
                    WHERE ColaboradorId = @id";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            Colaborador colaborador = GetCollection<Colaborador>(cmd)?.FirstOrDefault<Colaborador>();

            return colaborador;
        }

        public string Insert(Colaborador colaborador)
        {
            bool _resultado = false;
            string _msg = "";
            Int32 id = 0;
            Int32 colabId = 0;
            string _ident = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("IncluirColaborador");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Inserindo os dados na tabela PESSOA:
                    string _dtNasc = colaborador.DtNascimento != null ? ", DtNascimento " : "";
                    string _paramDtNasc = colaborador.DtNascimento != null ? ", @DtNascimento " : "";

                    command.CommandText = "" +
                        "INSERT into dbo.AD_Pessoa (Nome, EMail, NrCelular, " +
                        "   DtCadastro, PerfilId  " + _dtNasc + ") " +
                        "VALUES(@Nome, @EMail, @NrCelular, " +
                        "   @DtCadastro, @PerfilId " + _paramDtNasc + ") " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("Nome", colaborador.Nome);
                    command.Parameters.AddWithValue("EMail", colaborador.EMail);
                    command.Parameters.AddWithValue("NrCelular", colaborador.NrCelular);
                    command.Parameters.AddWithValue("PerfilId", colaborador.PerfilId);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);

                    if (_dtNasc != "")
                        command.Parameters.AddWithValue("DtNascimento", colaborador.DtNascimento);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    // Inserindo os dados na tabela Colaborador:
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Colaborador (PessoaId) " +
                        "   VALUES (@PessoaId) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("PessoaId", id);

                    colabId = (Int32)command.ExecuteScalar();

                    if (colabId > 0)
                        _ident = _ident.PadLeft(10 - colabId.ToString().Length, '0') + colabId.ToString();

                    _msg = colabId > 0 ? $"{_ident}Inclusão Realizada com sucesso" : $"{_ident}Inclusão Não Realizada com sucesso";

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
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string Update(int id, Colaborador colaborador)
        {
            string _msg = "";

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarColaborador");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Atualizando os dados na tabela PESSOA:
                    string _dtNasc = colaborador.DtNascimento != null ? ", DtNascimento = @DtNascimento " : "";

                    command.CommandText = "" +
                        "UPDATE dbo.AD_Pessoa " +
                        "SET Nome = @nome, EMail = @EMail, NrCelular = @NrCelular,  " +
                            "PerfilId = @PerfilId, Ativo = @Ativo " + _dtNasc +
                        "WHERE PessoaId = @id";

                    command.Parameters.AddWithValue("Nome", colaborador.Nome);
                    command.Parameters.AddWithValue("EMail", colaborador.EMail);
                    command.Parameters.AddWithValue("NrCelular", colaborador.NrCelular);
                    command.Parameters.AddWithValue("Ativo", colaborador.Ativo);
                    command.Parameters.AddWithValue("PerfilId", colaborador.PerfilId);
                    command.Parameters.AddWithValue("id", id);

                    if (_dtNasc != "")
                        command.Parameters.AddWithValue("DtNascimento", colaborador.DtNascimento);

                    int i = command.ExecuteNonQuery();

                    _msg = i > 0 ? "Atualização Realizada com sucesso" : "Atualização NÃO Realizada com sucesso";

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
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string RessetPasswordById(int id)
        {
            bool _sendSucess = false;
            string _msg = "", _newPassword = "", _newPasswordHash = "";
            bool _isBodyHtml = true;

            string _subject, _textBody;

            Colaborador _colaborador = new Colaborador();
            _colaborador = GetColaboradorById(id);

            SendEMail _sendMail = new SendEMail();

            _newPassword = PasswordFunctions.GetNovaSenhaAcesso("");
            _newPasswordHash = PasswordFunctions.CriptografaSenha(_newPassword);

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("RessetSenhaColaborador");

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
                    command.Parameters.AddWithValue("id", _colaborador.PessoaId);

                    int i = command.ExecuteNonQuery();

                    transaction.Commit();

                    if (i > 0)
                    {
                        _subject = "Site FBTC - Troca de Senha - A sua nova senha de acesso chegou!";

                        _textBody = "<html><body> " +
                                    $"<p>Olá {_colaborador.Nome}!</p>" +
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

                        _sendSucess = _sendMail.SendMessage(_colaborador.EMail, _subject, _isBodyHtml, _textBody);

                        _msg = _sendSucess == true ? $"A nova senha foi enviada para o e-mail: { _colaborador.EMail }." : "Houve uma falha no envio da sua senha";
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

        public string ValidaEMail(int pessoaId, string eMail)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter ppessoaId = new SqlParameter() { ParameterName = "@pessoaId", Value = pessoaId };
            _parametros.Add(ppessoaId);

            SqlParameter peMail = new SqlParameter() { ParameterName = "@eMail", Value = eMail };
            _parametros.Add(peMail);
            // Fim da definição dos parâmetros

            string _msg = "OK";

            try
            {
                query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento, P.NrCelular, P.PasswordHash, P.DtCadastro, P.Ativo, 
                        P.PerfilId 
                    FROM dbo.AD_Pessoa P 
                    WHERE P.PessoaId != @pessoaId 
                        AND P.EMail = @eMai";

                // Define o banco de dados que será usando:
                CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

                // Obtém os dados do banco de dados:
                IEnumerable<Colaborador> _collection = GetCollection<Colaborador>(cmd)?.ToList();

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
