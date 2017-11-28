using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class ValorAnuidadePublico
    {
        public int ValorAnuidadePublicoId { get; set; }
        public decimal Valor { get; set; }
        public int AnuidadeId { get; set; }
        public int TipoPublicoId { get; set; }
    }
}
