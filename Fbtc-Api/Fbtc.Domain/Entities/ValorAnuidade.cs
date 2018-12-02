using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class ValorAnuidade
    {
        public int ValorAnuidadeId { get; set; }
        public decimal Valor { get; set; }
        public int TipoAnuidade { get; set; }
        public int AnuidadeTipoPublicoId { get; set; }
    }
}
