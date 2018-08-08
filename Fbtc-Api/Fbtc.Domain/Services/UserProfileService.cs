using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public UserProfile GetByEmailPassword(string email, string password)
        {
            return _userProfileRepository.GetByEmailPassword(email, password);
        }

        public UserProfile GetByPessoaId(int id)
        {
            return _userProfileRepository.GetByPessoaId(id);
        }

        public string GetNomeFotoByPessoaId(int id)
        {
            return _userProfileRepository.GetNomeFotoByPessoaId(id);
        }

        public UserProfile Login(string email, string password)
        {
            return _userProfileRepository.Login(email, password);
        }

        public string RessetPasswordByEMail(string email)
        {
            return _userProfileRepository.RessetPasswordByEMail(email);
        }

        public string RessetPasswordByPessoaId(int id)
        {
            return _userProfileRepository.RessetPasswordByPessoaId(id);
        }

        public string Save(UserProfile userProfile)
        {
            return _userProfileRepository.Save(userProfile);
        }

        public string ValidaEMail(int pessoaId, string eMail)
        {
            return _userProfileRepository.ValidaEMail(pessoaId, eMail);
        }
    }
}
