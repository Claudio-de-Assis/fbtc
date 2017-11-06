using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class AssociadoIsento
    {
        public virtual int AssociadoIsentoId { get; set; }
        public virtual Associado Associado { get; set; }
        public virtual AtaIsencao AtaIsencao { get; set; }
        public virtual DateTime DtCadastro { get; set; }
    }
}
