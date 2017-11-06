using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class ValorAnuidadePublico
    {
        public virtual int ValorAnuidadePublicoId { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual Anuidade Anuidade { get; set; }
        public virtual TipoPublico TipoPublico { get; set; }
    }
}
