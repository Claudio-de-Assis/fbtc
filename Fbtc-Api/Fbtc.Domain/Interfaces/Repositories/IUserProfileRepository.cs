﻿using Fbtc.Domain.Entities;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByPessoaId(int id);

        string Save(UserProfile userProfile);

        string GetNomeFotoByPessoaId(int id);

        string RessetPasswordByPessoaId(int id);

        string ValidaEMail(int pessoaId, string eMail);

        string RessetPasswordByEMail(string email);

        bool Login(string email, string password);

        UserProfile GetByEmailPassword(string email, string password);
    }
}
