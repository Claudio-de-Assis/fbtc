using Fbtc.Application.Helper;
using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using prmToolkit.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Application.Services
{
    public class UserProfileApplication : IUserProfileApplication
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileApplication(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public UserProfile GetByEmailPassword(string email, string password)
        {
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
               RaiseException.IfNotEmail(email, "Atenção: E-Mail inválido"),
               RaiseException.IfNullOrEmpty(password, "Atenção: Senha inválida")
           );

            return _userProfileService.GetByEmailPassword(email, password);
        }

        public UserProfile GetByPessoaId(int id)
        {
            return _userProfileService.GetByPessoaId(id);
        }

        public string GetNomeFotoByPessoaId(int id)
        {
            return _userProfileService.GetNomeFotoByPessoaId(id);
        }

        public bool Login(string email, string password)
        {
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                RaiseException.IfNotEmail(email, "Atenção: E-Mail inválido"),
                RaiseException.IfNullOrEmpty(password, "Atenção: Senha inválida")
            );

            return _userProfileService.Login(email, password);
        }

        public string RessetPasswordByEMail(string email)
        {

            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                RaiseException.IfNotEmail(email, "Atenção: E-Mail inválido")
            );

            return _userProfileService.RessetPasswordByEMail(email);
        }

        public string RessetPasswordByPessoaId(int id)
        {
            return _userProfileService.RessetPasswordByPessoaId(id);
        }

        public string Save(UserProfile p)
        {
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                RaiseException.IfNullOrEmpty(p.Nome, "Nome não informado"),
                RaiseException.IfNotEmail(p.EMail, "E-Mail inválido"),
                RaiseException.IfNullOrEmpty(p.NrCelular, "NrCelular não informado")
            );

            if (string.IsNullOrEmpty(p.PasswordHashReturned))
            {
                string _passwordHashReturned = "";
                _passwordHashReturned = Functions.AjustaTamanhoString(p.PasswordHashReturned, 100);
                p.PasswordHash = Functions.CriptografaSenhaNova(_passwordHashReturned);
            }

            UserProfile _p = new UserProfile() {
                PessoaId = p.PessoaId,
                Nome = Functions.AjustaTamanhoString(p.Nome, 100),
                EMail = Functions.AjustaTamanhoString(p.EMail, 100),
                NomeFoto = Functions.AjustaTamanhoString(p.NomeFoto, 32),
                Sexo = p.Sexo,
                NrCelular = Functions.AjustaTamanhoString(p.NrCelular, 15),
                PasswordHash = p.PasswordHash,
            };

            return _userProfileService.Save(p);
        }

        public string ValidaEMail(int pessoaId, string eMail)
        {
            return _userProfileService.ValidaEMail(pessoaId, eMail);
        }
    }
}
