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
    public class UserProfileRepository : AbstractRepository, IUserProfileRepository
    {
        private string query;
        private readonly string strConnSql;

        public UserProfileRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }
        
        public UserProfile GetByPessoaId(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter paramId = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(paramId);
            // Fim da definição dos parâmetros
            
            try
            {
                query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.PerfilId, P.Ativo 
                    FROM dbo.AD_Pessoa P  
                    WHERE P.PessoaId = @id";

                // Define o banco de dados que será usando:
                CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

                // Obtém os dados do banco de dados:
                UserProfile userProfile = GetCollection<UserProfile>(cmd)?.FirstOrDefault<UserProfile>();

                return userProfile;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public string GetNomeFotoByPessoaId(int id)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pid = new SqlParameter() { ParameterName = "@id", Value = id };

            _parametros.Add(pid);
            // Fim da definição dos parâmetros

            String NomeFoto = "_no-foto.png";

            query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.PerfilId,P.Ativo 
                    FROM dbo.AD_Pessoa P  
                    WHERE P.PessoaId = @id";

            // Define o banco de dados que será usando:
            CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

            // Obtém os dados do banco de dados:
            UserProfile userProfile = GetCollection<UserProfile>(cmd)?.FirstOrDefault<UserProfile>();

            // Obtendo um endereco:
            if (userProfile != null)
            {
                NomeFoto = userProfile.NomeFoto;
            }

            return NomeFoto;
        }

        public string RessetPasswordByPessoaId(int id)
        {
            bool _sendSucess = false;
            string _msg = "", _newPassword = "", _newPasswordHash = "";
            bool _isBodyHtml = true;

            string _subject, _textBody;

            UserProfile _userProfile = new UserProfile();
            _userProfile = GetByPessoaId(id);

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
                    command.Parameters.AddWithValue("id", id);

                    int i = command.ExecuteNonQuery();

                    transaction.Commit();

                    if (i > 0)
                    {
                        _subject = "Site FBTC - Troca de Senha - A sua nova senha de acesso chegou!";

                        _textBody = "<html><body> " +
                                    $"<p>Olá {_userProfile.Nome}!</p>" +
                                    "<p>Esta mensagem foi gerada pelo sistema Troca de Senha do Site FBTC.</p>" +
                                    "<p>Conforme solicitação através do site, a sua senha de acesso a sua conta no site fbtc.org.br foi reiniciada.</br></br>" +
                                        "Para você logar-se, por favor, informe o seu e-mail e a senha abaixo:</br></br></br>" +
                                        $"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>{_newPassword}</b></br></br></br>" +
                                        "Por favor, para seu segurança, troque-a no seu próximo acesso.</br></br></br>" +
                                        "<a href='http://www.fbtc.org.br/Account/Login' target='_blank'>fbtc.org.br - Acessar sua Conta</a></br>" +
                                    "</p>" +
                                    "<p><i>2018 - FBTC Federação Brasileira de Terapias Cognitivas - Direitos reservados.</i></p> " +
                                     "<p>Este é um e-mail automático da FBTC, por favor não o responda.</p> " +
                                    "</body></html> ";

                        _sendSucess = _sendMail.SendMessage(_userProfile.EMail, _subject, _isBodyHtml, _textBody);

                        _msg = _sendSucess == true ? $"A nova senha foi enviada para o e-mail: { _userProfile.EMail }." : "Houve uma falha no envio da sua senha";
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
                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public string Save(UserProfile userProfile)
        {
            bool _resultado = false;
            string _msg = "";
            
            using (SqlConnection connection = new SqlConnection(strConnSql))
            {

                string _passWord = "";
                string _passwordHash = "";

                if (!string.IsNullOrEmpty(userProfile.PasswordHashReturned))
                {
                    _passwordHash = PasswordFunctions.CriptografaSenha(userProfile.PasswordHashReturned);
                    _passWord = ", PassWordHash = @PassWordHash ";
                }

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("AtualizarUserProfile");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Atualizando os dados na tabela PESSOA:
                    string _dtNasc = userProfile.DtNascimento != null ? ", DtNascimento = @DtNascimento " : "";

                    command.CommandText = $@" UPDATE dbo.AD_Pessoa 
                                        SET Nome = @nome, EMail = @EMail, NomeFoto = @NomeFoto, 
                                            Sexo = @Sexo, NrCelular = @NrCelular, 
                                            PerfilId = @PerfilId {_dtNasc} {_passWord}
                                        WHERE PessoaId = @id";

                    command.Parameters.AddWithValue("Nome", userProfile.Nome);
                    command.Parameters.AddWithValue("EMail", userProfile.EMail);
                    command.Parameters.AddWithValue("NomeFoto", userProfile.NomeFoto);
                    command.Parameters.AddWithValue("Sexo", userProfile.Sexo);
                    command.Parameters.AddWithValue("NrCelular", userProfile.NrCelular);
                    command.Parameters.AddWithValue("PerfilId", userProfile.PerfilId);
                    command.Parameters.AddWithValue("id", userProfile.PessoaId);

                    if (_dtNasc != "")
                        command.Parameters.AddWithValue("DtNascimento", userProfile.DtNascimento);

                    if (!string.IsNullOrEmpty(userProfile.PasswordHashReturned))
                        command.Parameters.AddWithValue("PassWordHash", _passwordHash);

                    int i = command.ExecuteNonQuery();
                    _resultado = i > 0;

                    _msg = i > 0 ? "Atualização realizada com sucesso" : "Atualização NÃO realizada com sucesso";

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

        public string ValidaEMail(int pessoaId, string eMail)
        {
            string _msg = "OK";

            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pPessoaId = new SqlParameter() { ParameterName = "@pessoaId", Value = pessoaId };
            _parametros.Add(pPessoaId);

            SqlParameter pEMail = new SqlParameter() { ParameterName = "@eMail", Value = eMail };
            _parametros.Add(pEMail);
            // Fim da definição dos parâmetros

            try
            {
                query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.PerfilId, P.Ativo 
                    FROM dbo.AD_Pessoa P  
                    WHERE P.PessoaId = @pessoaId AND P.EMail = @eMail";

                // Define o banco de dados que será usando:
                CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

                // Obtém os dados do banco de dados:
                IEnumerable<UserProfile> _collection = GetCollection<UserProfile>(cmd)?.ToList();

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

        public string RessetPasswordByEMail(string email)
        {
            bool _sendSucess = false;
            string _msg = "", _newPassword = "", _newPasswordHash = "";
            bool _isBodyHtml = true;

            string _subject, _textBody;

            UserProfile _userProfile = new UserProfile();
            _userProfile = GetByEMail(email);

            SendEMail _sendMail = new SendEMail();

            _newPassword = PasswordFunctions.GetNovaSenhaAcesso("");
            _newPasswordHash = PasswordFunctions.CriptografaSenha(_newPassword);

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("RessetSenha");

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
                    command.Parameters.AddWithValue("id", _userProfile.PessoaId);

                    int i = command.ExecuteNonQuery();

                    transaction.Commit();

                    if (i > 0)
                    {
                        _subject = "Site FBTC - Troca de Senha - A sua nova senha de acesso chegou!";

                        _textBody = "<html><body> " +
                                    $"<p>Olá {_userProfile.Nome}!</p>" +
                                    "<p>Esta mensagem foi gerada pelo sistema Troca de Senha do Site FBTC.</p>" +
                                    "<p>Conforme solicitação através do site, a sua senha de acesso a sua conta no site fbtc.org.br foi reiniciada.</br></br>" +
                                        "Para você logar-se, por favor, informe o seu e-mail e a senha abaixo:</br></br></br>" +
                                        $"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>{_newPassword}</b></br></br></br>" +
                                        "Por favor, para seu segurança, troque-a no seu próximo acesso.</br></br></br>" +
                                        "<a href='http://www.fbtc.org.br/Account/Login' target='_blank'>fbtc.org.br - Acessar sua Conta</a></br>" +
                                    "</p>" +
                                    "<p><i>2018 - FBTC Federação Brasileira de Terapias Cognitivas - Direitos reservados.</i></p> " +
                                     "<p>Este é um e-mail automático da FBTC, por favor não o responda.</p> " +
                                    "</body></html> ";

                        _sendSucess = _sendMail.SendMessage(_userProfile.EMail, _subject, _isBodyHtml, _textBody);

                        _msg = _sendSucess == true ? $"A nova senha foi enviada para o e-mail: { _userProfile.EMail }." : "Houve uma falha no envio da sua senha";
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
                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return _msg;
        }

        public UserProfile Login(string email, string password)
        {
            bool _isSucess = false;

            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pEmail = new SqlParameter() { ParameterName = "@email", Value = email };

            _parametros.Add(pEmail);
            // Fim da definição dos parâmetros

            try
            {
                query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.PerfilId, P.Ativo 
                    FROM dbo.AD_Pessoa P  
                    WHERE P.EMail = @email";

                // Define o banco de dados que será usando:
                CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

                // Obtém os dados do banco de dados:
                UserProfile userProfile = GetCollection<UserProfile>(cmd)?.FirstOrDefault<UserProfile>();

                if (userProfile != null)
                    _isSucess = PasswordFunctions.ValidaSenha(password, userProfile.PasswordHash);

                if (!_isSucess)
                    userProfile = null;

                return userProfile;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public UserProfile GetByEMail(string email)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pEmail = new SqlParameter() { ParameterName = "@email", Value = email };

            _parametros.Add(pEmail);
            // Fim da definição dos parâmetros

            try
            {
                query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.PerfilId, P.Ativo 
                    FROM dbo.AD_Pessoa P  
                    WHERE P.EMail = @email";

                // Define o banco de dados que será usando:
                CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

                // Obtém os dados do banco de dados:
                UserProfile userProfile = GetCollection<UserProfile>(cmd)?.FirstOrDefault<UserProfile>();

                if (userProfile == null)
                {
                    throw new Exception($"Atenção: Não foram encontrados dados para o E-Mail informado:{email}");
                }

                return userProfile;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public UserProfile GetByEmailPassword(string email, string password)
        {
            List<DbParameter> _parametros = new List<DbParameter>();

            // Definição do parâmetros da consulta:
            SqlParameter pEmail = new SqlParameter() { ParameterName = "@email", Value = email };

            _parametros.Add(pEmail);
            // Fim da definição dos parâmetros

            try
            {
                query = @"SELECT P.PessoaId, P.Nome, P.EMail, P.NomeFoto, P.Sexo, 
                        P.DtNascimento , P.NrCelular, P.PasswordHash, P.DtCadastro, P.PerfilId, P.Ativo 
                    FROM dbo.AD_Pessoa P  
                    WHERE P.EMail = @email";

                // Define o banco de dados que será usando:
                CommandSql cmd = new CommandSql(strConnSql, query, EnumDatabaseType.SqlServer, parametros: _parametros);

                // Obtém os dados do banco de dados:
                UserProfile userProfile = GetCollection<UserProfile>(cmd)?.FirstOrDefault<UserProfile>();

                if (userProfile != null)
                {
                    if (!PasswordFunctions.ValidaSenha(password, userProfile.PasswordHash))
                        userProfile = new UserProfile();
                }

                return userProfile;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
