using System;

namespace Fbtc.Domain.Entities
{
    public class Recebimento
    {
        public int RecebimentoId { get; set; }
        public int AssociadoId { get; set; }
        public int? AssociadoIsentoId { get; set; }
        public int? ValorAnuidadePublicoId { get; set; }
        public int? ValorEventoPublicoId { get; set; }
        public string ObjetivoPagamento { get; set; }
        public DateTime? DtVencimento { get; set; }
        public DateTime? DtPagamento { get; set; }
        public DateTime? DtNotificacao { get; set; }
        public string StatusPagamento { get; set; }
        public string FormaPagamento { get; set; }
        public string NrDocCobranca { get; set; }
        public decimal ValorPago { get; set; }
        public string Observacao { get; set; }
        public string TokenPagamento { get; set; }
        public DateTime DtCadastro { get; set; }
        public bool Ativo { get; set; }

        public Associado Associado { get; set; }
    }
}
