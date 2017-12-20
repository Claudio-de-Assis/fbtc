using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IRecebimentoService
    {
        IEnumerable<Recebimento> GetAll(string objetivoPagamento);

        Recebimento GetRecebimentoById(int id);

        string DeleteById(int id);

        string Insert(Recebimento recebimento);

        string Update(int id, Recebimento recebimento);

        IEnumerable<Recebimento> FindByFilters(string objetivoPagamento, string nome, string cpf,
            string crp, string crm, string status, int ano, int mes, bool? ativo,
            string tipoEvento, int tipoPublicoId);

        // Tipo 1: Anuidade; Tipo 2: Evento
        IEnumerable<Recebimento> GetRecebimentoByPessoaId(string objetivoPagamento, int id);

        IEnumerable<Recebimento> GetRecebimentoByEventoId(int id);

        IEnumerable<Recebimento> GetRecebimentoByAnuidadeId(int id);
    }
}
