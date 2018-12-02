using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IDescontoAnuidadeAtcApplication
    {
        IEnumerable<DescontoAnuidadeAtc> GetAll();

        IEnumerable<DescontoAnuidadeAtcDao> GetDescontoAnuidadeAtcDaoByAnuidadeId(int anuidadeId);

        DescontoAnuidadeAtc GetDescontoAnuidadeAtcById(int id);

        DescontoAnuidadeAtcDao GetDescontoAnuidadeAtcDaoById(int id);

        DescontoAnuidadeAtcDao GetDadosNovoDescontoAnuidadeAtcDao(int associadoId, int anuidadeId, int colaboradorPessoaId);

        DescontoAnuidadeAtcDao GetDescontoAnuidadeAtcDaoByPessoaId(int pessoaId);

        string Save(DescontoAnuidadeAtcDao descontoAnuidadeAtcDao);

        IEnumerable<DescontoAnuidadeAtcDao> FindByFilters(int anuidadeId, string nomePessoa, bool? ativo, bool? comDesconto);
    }
}
