﻿using Fbtc.Domain.Entities;
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

        IEnumerable<Associado> FindByFilters(string nome, string cpf, 
            string sexo, int atcId, string crp, string tipoProfissao);
    }
}