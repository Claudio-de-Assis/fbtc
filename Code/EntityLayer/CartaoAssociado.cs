using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class CartaoAssociado
    {
        public virtual int CartaoAssociadoId { get; set; }
        public virtual Associado Associado { get; set; }
        public virtual DateTime DtEmissao { get; set; }
    }
}
