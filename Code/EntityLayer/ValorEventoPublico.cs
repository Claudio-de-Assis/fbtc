using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class ValorEventoPublico
    {
        public virtual int ValorEventoPublicoId { get; set; }
        public virtual Evento Evento { get; set; }
        public virtual TipoPublico TipoPublico { get; set; }
        public virtual decimal Valor { get; set; }
    }
}
