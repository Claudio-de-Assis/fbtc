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
        public DateTime? DtNotificacao { get; set; }
        public string Observacao { get; set; }
        public string CodePS { get; set; }
        public string ReferencePS { get; set; }
        public int? TypePS { get; set; }
        public int? StatusPS { get; set; }
        public DateTime? LastEventDatePS { get; set; }
        public int? TypePaymentMethodPS { get; set; }
        public int? CodePaymentMethodPS { get; set; }
        public decimal NetAmountPS { get; set; }
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
        public int? StatusPS { get; set; }
        public DateTime? LastEventDatePS { get; set; }
        public bool AtivoRec { get; set; }
        public int IsencaoIdId { get; set; }
    }
}
