using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IAssociadoRepository
    {
        IEnumerable<Associado> GetAll();

        Associado GetAssociadoById(int id);

        string DeleteById(int id);

        string Insert(Associado associado);

        string Update(int id, Associado associado);

        string InsertIsento(AssociadoIsentoDao associadoIsentoDao);

        string DeleteIsentoByAssociadoIsentoId(int AssociadoIsentoId);

        IEnumerable<Associado> FindByFilters(string nome, string cpf, 
            string sexo, int atcId, string crp, string tipoProfissao, 
            int tipoPublico, string estado, string cidade, bool? ativo);

        IEnumerable<AssociadoIsentoDao> FindIsentoByFilters(int isencaoId, string nome, string cpf,
            string sexo, int atcId, string crp, string tipoProfissao,
            int tipoPublico, string estado, string cidade, bool? ativo);

        string GetNomeFotoByAssociadoId(int id);
    }
}
