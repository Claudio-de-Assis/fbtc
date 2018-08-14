using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Fbtc.Domain.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;

        public PerfilService(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        public IEnumerable<Perfil> GetAll(bool? isAtivo, string dominio)
        {
            return _perfilRepository.GetAll(isAtivo, dominio);

        }

        public Perfil GetPerfilById(int id)
        {
            return _perfilRepository.GetPerfilById(id);
        }
    }
}
