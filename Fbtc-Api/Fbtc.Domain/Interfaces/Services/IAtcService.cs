using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IAtcService
    {
        IEnumerable<Atc> GetAll();

        Atc GetAtcById(int id);

        string DeleteById(int id);

        string Insert(Atc atc);

        string Update(int id, Atc atc);
    }
}
