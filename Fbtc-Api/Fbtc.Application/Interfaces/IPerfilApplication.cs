using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IPerfilApplication
    {
        IEnumerable<Perfil> GetAll(bool? isAtivo, string dominio);

        Perfil GetPerfilById(int id);
    }
}
