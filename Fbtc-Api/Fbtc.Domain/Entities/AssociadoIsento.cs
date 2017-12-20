using System;

namespace Fbtc.Domain.Entities
{
    public class AssociadoIsento
    {
        public int AssociadoIsentoId { get; set; }
        public int AssociadoId { get; set; }
        public int IsencaoId { get; set; }
        public DateTime DtCadastro { get; set; }
    }
}
