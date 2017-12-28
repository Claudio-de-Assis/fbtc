using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IAssociadoService
    {
        IEnumerable<Associado> GetAll();

        Associado GetAssociadoById(int id);

        string DeleteById(int id);

        string Insert(Associado associado);

        string Update(int id, Associado associado);
        
        IEnumerable<Associado> FindByFilters(string nome, string cpf,  
            string sexo, int atcId, string crp, string tipoProfissao, 
            int tipoPublico, string estado, string cidade, bool? ativo);

        string GetNomeFotoByAssociadoId(int id);
    }
}
