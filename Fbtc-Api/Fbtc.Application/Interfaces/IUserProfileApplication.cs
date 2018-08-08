using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IUserProfileApplication
    {
        UserProfile GetByPessoaId(int id);

        string Save(UserProfile userProfile);

        string GetNomeFotoByPessoaId(int id);

        string RessetPasswordByPessoaId(int id);

        string ValidaEMail(int pessoaId, string eMail);

        string RessetPasswordByEMail(string email);

        // bool Login(string email, string password);

        UserProfile Login(string email, string password);

        UserProfile GetByEmailPassword(string email, string password);
    }
}
