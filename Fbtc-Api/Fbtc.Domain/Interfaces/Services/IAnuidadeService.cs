using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IAnuidadeService
    {
        IEnumerable<Anuidade> GetAll();

        Anuidade GetAnuidadeById(int id);

        AnuidadeDao GetAnuidadeDaoById(int id);

        string DeleteById(int id);

        string Insert(Anuidade anuidade);

        string Update(int id, Anuidade anuidade);

        string InsertAnuidadeDao(AnuidadeDao anuidadeDao);

        string UpdateAnuidadeDao(int id, AnuidadeDao anuidadeDao);

        IEnumerable<Anuidade> FindByFilters(int codigo, bool? ativo);
    }
}
