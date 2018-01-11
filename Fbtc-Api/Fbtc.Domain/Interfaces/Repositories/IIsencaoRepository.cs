using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IIsencaoRepository
    {
        IEnumerable<Isencao> GetAll(string tipoIsencao);

        Isencao GetIsencaoById(int id);

        string Insert(Isencao isencao);

        string Update(int id, Isencao isencao);

        string DeleteById(int id);

        IEnumerable<Isencao> FindByFilters(string tipoIsencao, string nomeAssociado, string descricao, int ano, int eventoId);

        IEnumerable<IsencaoDao> FindIsencaoByFilters(string tipoIsencao, string nomeAssociado, int ano, string identificacao, string tipoEvento);
    }
}
