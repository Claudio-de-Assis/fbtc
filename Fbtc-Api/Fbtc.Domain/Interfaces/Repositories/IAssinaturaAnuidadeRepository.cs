using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IAssinaturaAnuidadeRepository
    {
        IEnumerable<AssinaturaAnuidade> GetAll();

        AssinaturaAnuidadeDao GetAssinaturaAnuidadeById(int id);

        string Insert(AssinaturaAnuidade assinaturaAnuidade);

        string Update(int id, AssinaturaAnuidade assinaturaAnuidade);

        IEnumerable<AssinaturaAnuidadeDao> FindByFilters(int anuidadeId, string nome, string cpf, bool? ativo);

        IEnumerable<AssinaturaAnuidadeDao> FindByPessoaId(int pessoaId);

        IEnumerable<AssinaturaAnuidadeDao> FindAssinaturaPendenteByFilters(int anuidadeId, string nome, string cpf, bool? ativo);

        AssinaturaAnuidade GetAssinaturaAnuidadeByReference(string reference);

        string SetInicioPagamentoPagSeguro(string reference, bool emProcessoPagamento);
    }
}
