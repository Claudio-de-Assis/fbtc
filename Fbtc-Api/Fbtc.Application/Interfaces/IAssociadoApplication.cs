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

        IEnumerable<Associado> FindByFilters(string nome, string cpf, 
            string sexo, int atcId, string crp, string tipoProfissao, int tipoPublico);
    }
}
