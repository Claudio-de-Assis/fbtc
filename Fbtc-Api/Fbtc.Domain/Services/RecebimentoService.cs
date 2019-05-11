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

        public IEnumerable<RecebimentoAssociadoDao> FindAnuidadeByFilters(string nome, string cpf, 
            string crp, string crm, int statusPS, int ano, int mes, bool? ativo, int tipoPublicoId)
        {
            return _recebimentoRepository.FindAnuidadeByFilters(nome, cpf, crp, crm,
                statusPS, ano, mes, ativo, tipoPublicoId);
        }

        public IEnumerable<RecebimentoAssociadoDao> FindByAnuidadeIdFilters(int anuidadeId, string nome, 
            string cpf, string crp, string crm, int statusPS, int ano, int mes, bool? ativo, int tipoPublicoId)
        {
            return _recebimentoRepository.FindByAnuidadeIdFilters(anuidadeId, nome, cpf, crp, crm,
                statusPS, ano, mes, ativo, tipoPublicoId);
        }

        public IEnumerable<RecebimentoAssociadoDao> FindByEventoIdFilters(int eventoId, string nome, 
            string cpf, string crp, string crm, int statusPS, int ano, int mes, bool? ativo, string tipoEvento, 
            int tipoPublicoId)
        {
            return _recebimentoRepository.FindByEventoIdFilters(eventoId, nome, cpf, crp, crm,
                statusPS, ano, mes, ativo, tipoEvento, tipoPublicoId);
        }

        public IEnumerable<RecebimentoAssociadoDao> FindEventoByFilters(string nome, string cpf, string crp, 
            string crm, int statusPS, int ano, int mes, bool? ativo, string tipoEvento, int tipoPublicoId)
        {
            return _recebimentoRepository.FindEventoByFilters(nome, cpf, crp, crm,
                statusPS, ano, mes, ativo, tipoEvento, tipoPublicoId);
        }

        public IEnumerable<RecebimentoAssociadoDao> FindPagamentosByPessoaIdIdFilters(int pessoaId, string objetivoPagamento, int ano, int statusPS)
        {
            return _recebimentoRepository.FindPagamentosByPessoaIdIdFilters(pessoaId, objetivoPagamento, ano, statusPS);
        }

        public IEnumerable<Recebimento> GetAll(string objetivoPagamento)
        {
            return _recebimentoRepository.GetAll(objetivoPagamento);
        }

        public RecebimentoAssociadoDao GetRecebimentoAssociadoDaoByRecebimentoId(int id)
        {
            return _recebimentoRepository.GetRecebimentoAssociadoDaoByRecebimentoId(id);
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
            return _recebimentoRepository.Insert(recebimento, "");
        }

        public string Update(int id, Recebimento recebimento)
        {
            return _recebimentoRepository.Update(id, recebimento);
        }


        public string InsertRecebimentoIsencao(Recebimento recebimento)
        {
            return _recebimentoRepository.InsertRecebimentoIsencao(recebimento);
        }

        public string UpdateRecebimentoIsencao(int id, Recebimento recebimento)
        {
            return _recebimentoRepository.UpdateRecebimentoIsencao(id, recebimento);
        }

    }
}
