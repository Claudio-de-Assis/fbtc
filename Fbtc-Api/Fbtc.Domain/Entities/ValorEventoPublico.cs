using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class ValorEventoPublico
    {
        public int ValorEventoPublicoId { get; set; }
        public int EventoId { get; set; }
        public int TipoPublicoId { get; set; }
        public decimal Valor { get; set; }
    }
}
