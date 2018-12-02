using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface ILogApplication
    {
        IEnumerable<Log> GetAll();

        string Save(Log log);
    }
}
