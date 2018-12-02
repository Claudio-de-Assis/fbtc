using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IAssinaturaAnuidadeApplication
    {
        IEnumerable<AssinaturaAnuidade> GetAll();

        AssinaturaAnuidadeDao GetAssinaturaAnuidadeById(int id);

        string Save(AssinaturaAnuidadeDao assinaturaAnuidadeDao);

        IEnumerable<AssinaturaAnuidadeDao> FindByFilters(int anuidadeId, string nome, string cpf,  bool? ativo);

        IEnumerable<AssinaturaAnuidadeDao> FindByPessoaId(int pessoaId);

        IEnumerable<AssinaturaAnuidadeDao> FindAssinaturaPendenteByFilters(int anuidadeId, string nome, string cpf, bool? ativo);
    }
}
