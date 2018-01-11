using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IRecebimentoApplication
    {
        IEnumerable<Recebimento> GetAll(string objetivoPagamento);

        Recebimento GetRecebimentoById(int id);

        Recebimento SetRecebimento(string objetivoPagamento);

        string DeleteById(int id);

        string Save(Recebimento recebimento);

        string InsertIsento(int associadoId, int associadoIsentoId, string ojetivoPagamento, string tipoIsencao);

        string DeleteByAssociadoIsentoId(int id);

        IEnumerable<RecebimentoAssociadoDao> FindAnuidadeByFilters(string nome, string cpf,
            string crp, string crm, string status, int ano, int mes, bool? ativo, int tipoPublicoId);

        IEnumerable<RecebimentoAssociadoDao> FindEventoByFilters(string nome, string cpf,
            string crp, string crm, string status, int ano, int mes, bool? ativo,
            string tipoEvento, int tipoPublicoId);
        
        IEnumerable<RecebimentoAssociadoDao> FindByAnuidadeIdFilters(int anuidadeId, string nome, string cpf,
            string crp, string crm, string status, int ano, int mes, bool? ativo, int tipoPublicoId);

        IEnumerable<RecebimentoAssociadoDao> FindByEventoIdFilters(int eventoId, string nome, string cpf,
            string crp, string crm, string status, int ano, int mes, bool? ativo,
            string tipoEvento, int tipoPublicoId);

        // Tipo 1: Anuidade; Tipo 2: Evento
        IEnumerable<Recebimento> GetRecebimentoByPessoaId(string objetivoPagamento, int id);

        IEnumerable<Recebimento> GetRecebimentoByEventoId(int id);

        IEnumerable<Recebimento> GetRecebimentoByAnuidadeId(int id);
    }
}
