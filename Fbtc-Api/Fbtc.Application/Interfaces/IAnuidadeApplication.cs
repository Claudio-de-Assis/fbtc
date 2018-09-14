using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IAnuidadeApplication
    {
        IEnumerable<Anuidade> GetAll();

        Anuidade GetAnuidadeById(int id);

        AnuidadeDao GetAnuidadeDaoById(int id);
        
        Anuidade SetAnuidade();

        string DeleteById(int id);

        string Save(Anuidade anuidade);

        string SaveAnuidadeDao(AnuidadeDao anuidadeDao);

        IEnumerable<Anuidade> FindByFilters(int codigo, bool? ativo);
    }
}
