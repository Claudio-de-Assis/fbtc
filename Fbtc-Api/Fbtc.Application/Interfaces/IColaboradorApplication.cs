using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IColaboradorApplication
    {
        IEnumerable<Colaborador> GetAll();

        Colaborador GetColaboradorById(int id);

        Colaborador SetColaborador();

        string Save(Colaborador colaborador);

        string DeleteById(int id);

        IEnumerable<Colaborador> FindByFilters(string nome, int perfilId, bool? ativo);

        string RessetPasswordById(int id);

        string ValidaEMail(int colaboradorId, string eMail);
    }
}
