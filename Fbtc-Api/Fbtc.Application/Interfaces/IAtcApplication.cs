using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IAtcApplication
    {
        IEnumerable<Atc> GetAll();

        Atc GetAtcById(int id);

        Atc SetAtc();

        string DeleteById(int id);

        string Save(Atc atc);
    }
}
