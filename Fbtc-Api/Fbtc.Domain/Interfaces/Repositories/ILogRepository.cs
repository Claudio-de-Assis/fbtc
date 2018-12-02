using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface ILogRepository
    {
        IEnumerable<Log> GetAll();

        string Save(Log log);
    }
}
