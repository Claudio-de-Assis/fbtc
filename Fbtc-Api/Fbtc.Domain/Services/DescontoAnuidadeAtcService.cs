using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;

namespace Fbtc.Domain.Services
{
    public class DescontoAnuidadeAtcService : IDescontoAnuidadeAtcService
    {
        private readonly IDescontoAnuidadeAtcRepository _descontoAnuidadeAtcRepository;

        public DescontoAnuidadeAtcService(IDescontoAnuidadeAtcRepository descontoAnuidadeAtcRepository)
        {
            _descontoAnuidadeAtcRepository = descontoAnuidadeAtcRepository;
        }

        public IEnumerable<DescontoAnuidadeAtcDao> FindByFilters(int anuidadeId, string nomePessoa, bool? ativo, bool? comDesconto)
        {
            return _descontoAnuidadeAtcRepository.FindByFilters(anuidadeId, nomePessoa, ativo, comDesconto);
        }

        public IEnumerable<DescontoAnuidadeAtc> GetAll()
        {
            return _descontoAnuidadeAtcRepository.GetAll();
        }

        public DescontoAnuidadeAtc GetDescontoAnuidadeAtcById(int id)
        {
            return _descontoAnuidadeAtcRepository.GetDescontoAnuidadeAtcById(id);
        }

        public DescontoAnuidadeAtcDao GetDescontoAnuidadeAtcDaoById(int id)
        {
            return _descontoAnuidadeAtcRepository.GetDescontoAnuidadeAtcDaoById(id);
        }

        public DescontoAnuidadeAtcDao GetDescontoAnuidadeAtcDaoByPessoaId(int pessoaId)
        {
            return _descontoAnuidadeAtcRepository.GetDescontoAnuidadeAtcDaoByPessoaId(pessoaId);
        }

        public IEnumerable<DescontoAnuidadeAtcDao> GetDescontoAnuidadeAtcDaoByAnuidadeId(int anuidadeId)
        {
            return _descontoAnuidadeAtcRepository.GetDescontoAnuidadeAtcDaoByAnuidadeId(anuidadeId);
        }

        public string Insert(DescontoAnuidadeAtc descontoAnuidadeAtc)
        {
            return _descontoAnuidadeAtcRepository.Insert(descontoAnuidadeAtc);
        }

        public string Update(int id, DescontoAnuidadeAtc descontoAnuidadeAtc)
        {
            return _descontoAnuidadeAtcRepository.Update(id, descontoAnuidadeAtc);
        }

        public DescontoAnuidadeAtcDao GetDadosNovoDescontoAnuidadeAtcDao(int associadoId, int anuidadeId, int colaboradorPessoaId)
        {
            return _descontoAnuidadeAtcRepository.GetDadosNovoDescontoAnuidadeAtcDao(associadoId, anuidadeId, colaboradorPessoaId);
        }
    }
}
