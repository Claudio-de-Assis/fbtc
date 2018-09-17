using Fbtc.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IRecebimentoRepository
    {
        IEnumerable<Recebimento> GetAll(string objetivoPagamento);

        Recebimento GetRecebimentoById(int id);

        string DeleteById(int id);

        string Insert(Recebimento recebimento);

        string Update(int id, Recebimento recebimento);

        string InsertIsento(int associadoId, int associadoIsentoId, string ojetivoPagamento, string tipoIsencao);

        string DeleteByAssociadoIsentoId(int id);

        string UpdateRecebimentoPagSeguro(string code, string reference, int type,
            int status, string lasteventdate, int TypePaymentoMethod, int CodePaymentoMethod, decimal NetAmountPS);
        

        IEnumerable<RecebimentoAssociadoDao> FindAnuidadeByFilters(string nome, string cpf,
           string crp, string crm, int statusPS, int ano, int mes, bool? ativo, int tipoPublicoId);

        IEnumerable<RecebimentoAssociadoDao> FindEventoByFilters(string nome, string cpf,
            string crp, string crm, int statusPS, int ano, int mes, bool? ativo,
            string tipoEvento, int tipoPublicoId);

        IEnumerable<RecebimentoAssociadoDao> FindByAnuidadeIdFilters(int anuidadeId, string nome, string cpf,
            string crp, string crm, int statusPS, int ano, int mes, bool? ativo, int tipoPublicoId);

        IEnumerable<RecebimentoAssociadoDao> FindByEventoIdFilters(int eventoId, string nome, string cpf,
            string crp, string crm, int statusPS, int ano, int mes, bool? ativo,
            string tipoEvento, int tipoPublicoId);

        IEnumerable<RecebimentoAssociadoDao> FindPagamentosByPessoaIdIdFilters(int pessoaId,
            string objetivoPagamento, int ano, int statusPS);

        // Tipo 1: Anuidade; Tipo 2: Evento
        IEnumerable<Recebimento> GetRecebimentoByPessoaId(string objetivoPagamento, int id);

        IEnumerable<Recebimento> GetRecebimentoByEventoId(int id);

        IEnumerable<Recebimento> GetRecebimentoByAnuidadeId(int id);

        RecebimentoAssociadoDao GetRecebimentoAssociadoDaoByRecebimentoId(int id);
    }
}
