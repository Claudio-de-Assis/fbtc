using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;

namespace Fbtc.Domain.Services
{
    public class RecebimentoService : IRecebimentoService
    {
        private readonly IRecebimentoRepository _recebimentoRepository;

        public RecebimentoService(IRecebimentoRepository recebimentoRepository)
        {
            _recebimentoRepository = recebimentoRepository;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recebimento> FindByFilters(string objetivoPagamento, string nome, string cpf, 
            string crp, string crm, string status, int ano, int mes, bool? ativo, 
            string tipoEvento, int tipoPublicoId)
        {
            return _recebimentoRepository.FindByFilters(objetivoPagamento, nome, cpf, crp, crm,
                status, ano, mes, ativo, tipoEvento, tipoPublicoId);
        }

        public IEnumerable<Recebimento> GetAll(string objetivoPagamento)
        {
            return _recebimentoRepository.GetAll(objetivoPagamento);
        }

        public IEnumerable<Recebimento> GetRecebimentoByAnuidadeId(int id)
        {
            return _recebimentoRepository.GetRecebimentoByAnuidadeId(id);
        }

        public IEnumerable<Recebimento> GetRecebimentoByEventoId(int id)
        {
            return _recebimentoRepository.GetRecebimentoByEventoId(id);
        }

        public Recebimento GetRecebimentoById(int id)
        {
            return _recebimentoRepository.GetRecebimentoById(id);
        }

        public IEnumerable<Recebimento> GetRecebimentoByPessoaId(string objetivoPagamento, int id)
        {
            return _recebimentoRepository.GetRecebimentoByPessoaId(objetivoPagamento, id);
        }

        public string Insert(Recebimento recebimento)
        {
            return _recebimentoRepository.Insert(recebimento);
        }

        public string Update(int id, Recebimento recebimento)
        {
            return _recebimentoRepository.Update(id, recebimento);
        }
    }
}
