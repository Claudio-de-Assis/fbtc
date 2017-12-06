using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IEnderecoService
    {
        IEnumerable<Endereco> GetByPessoaId(int id);

        Endereco GetEnderecoById(int id);

        string DeleteById(int id);

        string DeleteByPessoaId(int id);

        string Insert(Endereco endereco);

        string Update(int id, Endereco endereco);
    }
}
