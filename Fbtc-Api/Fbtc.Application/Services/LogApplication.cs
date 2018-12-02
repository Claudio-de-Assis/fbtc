using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Application.Interfaces;

using prmToolkit.Validation;
using Fbtc.Application.Helper;

namespace Fbtc.Application.Services
{
    public class LogApplication : ILogApplication
    {
        private readonly ILogService _logService;

        public LogApplication(ILogService logService)
        {
            _logService = logService;
        }

        public IEnumerable<Log> GetAll()
        {
            return _logService.GetAll();
        }

        public string Save(Log log)
        {
            return _logService.Save(log);
        }
    }
}
