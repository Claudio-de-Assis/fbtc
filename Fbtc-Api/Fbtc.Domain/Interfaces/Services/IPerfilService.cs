using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IPerfilService
    {
        IEnumerable<Perfil> GetAll(bool? isAtivo, string dominio);

        Perfil GetPerfilById(int id);

        int GetPerfilIdByNomePerfil(string nomePerfil);
    }
}
