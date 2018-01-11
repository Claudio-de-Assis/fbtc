using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IIsencaoApplication
    {
        IEnumerable<Isencao> GetAll(string tipoIsencao);

        Isencao GetIsencaoById(int id);

        Isencao SetIsencao(string tipoIsencao);

        string Save(Isencao isencao);

        string DeleteById(int id);

        IEnumerable<Isencao> FindByFilters(string tipoIsencao, string nomeAssociado, string descricao, int ano, int eventoId);

        IEnumerable<IsencaoDao> FindIsencaoByFilters(string tipoIsencao, string nomeAssociado, int ano, string identificacao, string tipoEvento);
    }
}
