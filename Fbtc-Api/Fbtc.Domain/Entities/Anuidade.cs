using System;
using System.Collections.Generic;

namespace Fbtc.Domain.Entities
{
    public class Anuidade
    {
        public int AnuidadeId { get; set; }
        public int Codigo { get; set; }
        public DateTime DtCadastro { get; set; }
        public bool Ativo { get; set; }
    }
   
    public class AnuidadeDao : Anuidade
    {
        public IEnumerable<TipoPublicoValorAnuidadeDao> TiposPublicosValorsAnuidadesDao { get; set; }
    }
}
