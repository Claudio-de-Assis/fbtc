using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;


namespace Fbtc.Domain.Services
{
    public class AnuidadeService : IAnuidadeService
    {
        private readonly IAnuidadeRepository _anuidadeRepository;

        public AnuidadeService(IAnuidadeRepository anuidadeRepository)
        {
            _anuidadeRepository = anuidadeRepository;
        }

        public string DeleteById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Anuidade> GetAll()
        {
            return _anuidadeRepository.GetAll();
        }

        public Anuidade GetAnuidadeById(int id)
        {
            return _anuidadeRepository.GetAnuidadeById(id);
        }

        public string Insert(Anuidade anuidade)
        {
            return _anuidadeRepository.Insert(anuidade);
        }

        public string Update(int id, Anuidade anuidade)
        {
            return _anuidadeRepository.Update(id, anuidade);
        }
    }
}
