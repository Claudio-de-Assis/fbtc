using System;

namespace Fbtc.Domain.Entities
{
    public class Recebimento
    {
        public int RecebimentoId { get; set; }
        public int? AssinaturaAnuidadeId { get; set; }
        public int? AssinaturaEventoId { get; set; }
        public string Observacao { get; set; }
        public string NotificationCodePS { get; set; }
        public int? TypePS { get; set; }
        public int? StatusPS { get; set; }
        public DateTime? LastEventDatePS { get; set; }
        public int? TypePaymentMethodPS { get; set; }
        public int? CodePaymentMethodPS { get; set; }
        public decimal GrossAmountPS { get; set; }
        public decimal DiscountAmountPS { get; set; }
        public decimal FeeAmountPS { get; set; }
        public decimal NetAmountPS { get; set; }
        public decimal ExtraAmountPS { get; set; }
        public DateTime DtVencimento { get; set; }
        public string StatusFBTC { get; set; }
        public DateTime DtStatusFBTC { get; set; }
        public string OrigemEmissaoTitulo { get; set; }
        public DateTime DtCadastro { get; set; }
        public bool Ativo { get; set; }

//        public Associado Associado { get; set; }
    }

    public class RecebimentoAssinaturaDao : Recebimento
    {
        public int AssociadoId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NomeTP { get; set; }
        public string Titulo { get; set; }
    }

    
    public class RecebimentoAssociadoDao : Recebimento
    {
        public string Titulo { get; set; }
        public int? Anuidade { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NomeTP { get; set; }
        public string EMail { get; set; }
        public string NrCelular { get; set; }
        public bool AtivoAssociado { get; set; }
    }
}
