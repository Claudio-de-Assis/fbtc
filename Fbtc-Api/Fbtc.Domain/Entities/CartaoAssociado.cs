using System;

namespace Fbtc.Domain.Entities
{
    public class CartaoAssociado
    {
        public int CartaoAssociadoId { get; set; }
        public int AssociadoId { get; set; }
        public DateTime DtEmissao { get; set; }
    }
}
