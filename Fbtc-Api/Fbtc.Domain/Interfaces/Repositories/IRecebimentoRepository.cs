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

        string Insert(Recebimento recebimento, string lastEventDate);

        string Update(int id, Recebimento recebimento);
        
        string InsertRecebimentoIsencao(Recebimento recebimento);

        string UpdateRecebimentoIsencao(int id, Recebimento recebimento);

        string DeleteRecebimentoIsencao(int assinaturaAnuidadeId, int statusPS);

        string UpdateRecebimentoPagSeguro(int recebimentoId, Recebimento recebimento, string lastEventDate);

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

        string SaveDadosRecebimentoFromTransacaoPagSeguro(TransacaoPagSeguro transacaoPagSeguro);

        Recebimento GetRecebimentoByReference(string reference);

        string DesativarByAssinaturaAnuidadeId(int assinaturaAnuidadeId);
    }
}
