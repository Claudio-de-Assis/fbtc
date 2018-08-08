using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Application.Services
{
    public class PerfilApplication : IPerfilApplication
    {
        private readonly IPerfilService _perfilService;


        public PerfilApplication(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        public IEnumerable<Perfil> GetAll(bool? isAtivo, string dominio)
        {
            return _perfilService.GetAll(isAtivo, dominio);
        }

        public Perfil GetPerfilById(int id)
        {
            return _perfilService.GetPerfilById(id);
        }
    }
}
