using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class CartaoAssociado
    {
        public int CartaoAssociadoId { get; set; }
        public int AssociadoId { get; set; }
        public DateTime DtEmissao { get; set; }
    }
}
