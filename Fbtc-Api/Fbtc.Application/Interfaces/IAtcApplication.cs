using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IAtcApplication
    {
        IEnumerable<Atc> GetAll();

        IEnumerable<AtcDao> GetAllLst();

        Atc GetAtcById(int id);

        Atc SetAtc();

        string DeleteById(int id);

        string Save(Atc atc);

        IEnumerable<Atc> FindByFilters(string siglaUF);

    }
}
