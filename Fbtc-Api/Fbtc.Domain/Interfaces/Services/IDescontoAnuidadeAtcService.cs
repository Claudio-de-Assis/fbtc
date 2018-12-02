using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IDescontoAnuidadeAtcService
    {
        IEnumerable<DescontoAnuidadeAtc> GetAll();

        IEnumerable<DescontoAnuidadeAtcDao> GetDescontoAnuidadeAtcDaoByAnuidadeId(int anuidadeId);

        DescontoAnuidadeAtc GetDescontoAnuidadeAtcById(int id);

        DescontoAnuidadeAtcDao GetDescontoAnuidadeAtcDaoById(int id);

        DescontoAnuidadeAtcDao GetDescontoAnuidadeAtcDaoByPessoaId(int pessoaId);

        DescontoAnuidadeAtcDao GetDadosNovoDescontoAnuidadeAtcDao(int associadoId, int anuidadeId, int colaboradorPessoaId);

        string Insert(DescontoAnuidadeAtc descontoAnuidadeAtc);

        string Update(int id, DescontoAnuidadeAtc descontoAnuidadeAtc);

        IEnumerable<DescontoAnuidadeAtcDao> FindByFilters(int anuidadeId, string nomePessoa, bool? ativo, bool? comDesconto);
    }
}
