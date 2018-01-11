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


    public class RecebimentoAssociadoDao
    {
        public int AssociadoId { get; set; }
        public string Titulo { get; set; }
        public int? Anuidade { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NomeTP { get; set; }
        public int RecebimentoId { get; set; }
        public string StatusPagamento { get; set; }
        public DateTime? DtVencimento { get; set; }
        public DateTime? DtPagamento { get; set; }
        public bool AtivoRec { get; set; }
        public int IsencaoIdId { get; set; }
    }
}
