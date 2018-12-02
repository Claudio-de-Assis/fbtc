using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface ILogService
    {
        IEnumerable<Log> GetAll();

        string Save(Log log);
    }
}
