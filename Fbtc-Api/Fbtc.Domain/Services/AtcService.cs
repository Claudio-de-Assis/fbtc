using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;

namespace Fbtc.Domain.Services
{
    public class AtcService : IAtcService
    {
        private readonly IAtcRepository _atcRepository;

        public AtcService(IAtcRepository atcRepository)
        {
            _atcRepository = atcRepository;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Atc> FindByFilters(int atcId)
        {
            return _atcRepository.FindByFilters(atcId);
        }

        public IEnumerable<Atc> GetAll()
        {
            return _atcRepository.GetAll();
        }

        public IEnumerable<AtcDao> GetAllLst()
        {
            return _atcRepository.GetAllLst();
        }

        public Atc GetAtcById(int id)
        {
            return _atcRepository.GetAtcById(id);
        }

        public string Insert(Atc atc)
        {
            return _atcRepository.Insert(atc);
        }

        public string Update(int id, Atc atc)
        {
            return _atcRepository.Update(id, atc);
        }
    }
}
