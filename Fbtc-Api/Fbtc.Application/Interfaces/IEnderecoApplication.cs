using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IEnderecoApplication
    {
        IEnumerable<Endereco> GetByPessoaId(int id);

        Endereco GetEnderecoById(int id);

        Endereco SetEndereco();

        string DeleteById(int id);

        string DeleteByPessoaId(int id);

        string Save(Endereco endereco);
    }
}
