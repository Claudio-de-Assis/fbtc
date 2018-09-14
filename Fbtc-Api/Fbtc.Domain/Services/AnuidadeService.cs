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

        public IEnumerable<Anuidade> FindByFilters(int codigo, bool? ativo)
        {
            return _anuidadeRepository.FindByFilters(codigo, ativo);
        }

        public IEnumerable<Anuidade> GetAll()
        {
            return _anuidadeRepository.GetAll();
        }

        public Anuidade GetAnuidadeById(int id)
        {
            return _anuidadeRepository.GetAnuidadeById(id);
        }

        public AnuidadeDao GetAnuidadeDaoById(int id)
        {
            return _anuidadeRepository.GetAnuidadeDaoById(id);
        }

        public string Insert(Anuidade anuidade)
        {
            return _anuidadeRepository.Insert(anuidade);
        }

        public string InsertAnuidadeDao(AnuidadeDao anuidadeDao)
        {
            return _anuidadeRepository.InsertAnuidadeDao(anuidadeDao);
        }

        public string Update(int id, Anuidade anuidade)
        {
            return _anuidadeRepository.Update(id, anuidade);
        }

        public string UpdateAnuidadeDao(int id, AnuidadeDao anuidadeDao)
        {
            return _anuidadeRepository.UpdateAnuidadeDao(id, anuidadeDao);
        }
    }
}
