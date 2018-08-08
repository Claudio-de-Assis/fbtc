using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IColaboradorRepository
    {
        IEnumerable<Colaborador> GetAll();

        Colaborador GetColaboradorById(int id);

        string DeleteById(int id);

        string Insert(Colaborador colaborador);

        string Update(int id, Colaborador colaborador);

        IEnumerable<Colaborador> FindByFilters(string nome, int perfilId, bool? ativo);

        string RessetPasswordById(int id);

        string ValidaEMail(int associadoId, string eMail);
    }
}
