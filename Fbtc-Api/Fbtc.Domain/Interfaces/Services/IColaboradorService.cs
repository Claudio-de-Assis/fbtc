using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IColaboradorService
    {
        IEnumerable<Colaborador> GetAll();

        Colaborador GetColaboradorById(int id);

        string DeleteById(int id);

        string Insert(Colaborador colaborador);

        string Update(int id, Colaborador colaborador);

        IEnumerable<Colaborador> FindByFilters(string nome, int perfilId, bool? ativo);

        string RessetPasswordById(int id);

        string ValidaEMail(int colaboradorId, string eMail);
    }
}
