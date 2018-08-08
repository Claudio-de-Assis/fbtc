using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IPerfilRepository
    {
        IEnumerable<Perfil> GetAll(bool? isAtivo, string dominio);

        Perfil GetPerfilById(int id);
    }
}
