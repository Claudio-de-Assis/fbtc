using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;


namespace Fbtc.Domain.Services
{
    public class LogService : ILogService
    {

        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public IEnumerable<Log> GetAll()
        {
            return _logRepository.GetAll();
        }

        public string Save(Log log)
        {
            return _logRepository.Save(log);
        }
    }
}
