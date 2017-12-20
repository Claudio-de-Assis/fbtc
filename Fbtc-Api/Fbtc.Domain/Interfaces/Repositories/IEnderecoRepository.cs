using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        IEnumerable<Endereco> GetByPessoaId(int id);

        Endereco GetEnderecoById(int id);

        string DeleteById(int id);

        string DeleteByPessoaId(int id);

        string Insert(Endereco endereco);

        string Update(int id, Endereco endereco);

        IEnumerable<EstadoEnderecoCepDao> GetAllNomesEstados();

        IEnumerable<CidadeEnderecoCepDao> GetNomesCidadesByEstado(string nomeEstado);
    }
}
