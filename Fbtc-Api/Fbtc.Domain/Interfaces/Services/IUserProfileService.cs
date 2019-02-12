using Fbtc.Domain.Entities;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IUserProfileService
    {
        UserProfile GetByPessoaId(int id);

        string Save(UserProfile userProfile);

        string GetNomeFotoByPessoaId(int id);

        string RessetPasswordByPessoaId(int id);

        string ValidaEMail(int pessoaId, string eMail);

        string RessetPasswordByEMail(string email);

        // bool Login(string email, string password);

        UserProfile Login(string email, string password);

        UserProfile LoginUser(UserProfileLogin userProfileLogin);

        UserProfile GetByEmailPassword(string email, string password);
    }
}
