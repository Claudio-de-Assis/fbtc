using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class UserProfile : Pessoa
    {
        public string PasswordHashReturned { get; set; }
    }

    public class UserProfileLogin
    {
        public string EMail { get; set; }
        public string PasswordHash { get; set; }
    }
}
