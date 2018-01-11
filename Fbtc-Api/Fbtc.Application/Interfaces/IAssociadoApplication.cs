using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IAssociadoApplication
    {
        IEnumerable<Associado> GetAll();

        Associado GetAssociadoById(int id);

        Associado SetAssociado();

        string DeleteById(int id);

        string Save(Associado associado);

        string SaveIsento(AssociadoIsentoDao associadoIsentoDao);

        IEnumerable<Associado> FindByFilters(string nome, string cpf, 
            string sexo, int atcId, string crp, string tipoProfissao, 
            int tipoPublico, string estado, string cidade, bool? ativo);

        IEnumerable<AssociadoIsentoDao> FindIsentoByFilters(int isencaoId, string nome, string cpf,
            string sexo, int atcId, string crp, string tipoProfissao,
            int tipoPublico, string estado, string cidade, bool? ativo);

        string GetNomeFotoByAssociadoId(int id);
    }
}
