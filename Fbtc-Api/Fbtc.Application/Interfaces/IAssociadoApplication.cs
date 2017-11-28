using Fbtc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Application.Interfaces
{
    public interface IAssociadoApplication
    {
        IEnumerable<Associado> GetAll();

        Associado GetAssociadoById(int id);

        Associado SetAssociado();

        string DeleteById(int id);

        string Save(Associado associado);

        string Insert(Associado associado);

        string Update(int id, Associado associado);

        IEnumerable<Associado> FindByFilters(string nome, string cpf, 
            string sexo, int atcId, string crp, string tipoProfissao);
    }
}
