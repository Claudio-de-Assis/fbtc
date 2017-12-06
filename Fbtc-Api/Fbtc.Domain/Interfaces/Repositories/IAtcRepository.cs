using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IAtcRepository
    {
        IEnumerable<Atc> GetAll();

        Atc GetAtcById(int id);

        string DeleteById(int id);

        string Insert(Atc atc);

        string Update(int id, Atc atc);
    }
}
