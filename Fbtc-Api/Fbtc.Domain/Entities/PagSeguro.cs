using System;
using System.Collections.Generic;

namespace Fbtc.Domain.Entities
{
    //Informações a respeito da interface API de Notificações:
    //https://dev.pagseguro.uol.com.br/documentacao/pagamento-online/notificacoes/api-de-notificacoes#status-da-transacao


    /// <summary>
    /// Classe que contém as classes necessárias para o PagSeguro
    /// </summary>
    public class PagSeguro { }


    /// <summary>
    /// Classe com a estrutura para o recebimento das notificações das transações oriundas do PagSeguro
    /// </summary>
    public class NotificacaoPagSeguro
    {
        public int NotificacaoPagSeguroId { get; set; }
        public string NotificationCode { get; set; }
        public string NotificationType { get; set; }
        public DateTime DtCadastro { get; set; }
    }
    
    /// <summary>
    /// Classe com a estrutura de uma Transação do PagSeguro.
    /// </summary>
    public class TransacaoPagSeguro
    {
        public string Date { get; set; }
        public string NotificationCode { get; set; }
        public string Reference { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
        public string Lasteventdate { get; set; }
        public PaymentMethodPagSeguro PaymentMethod { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? FeeAmount { get; set; }
        public CreditorFeesPagSeguro CreditorFees { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? ExtraAmount { get; set; }
        public string InstallmentCount { get; set; }
        public int? ItemCount { get; set; }
        public IEnumerable<ItemTransacaoPagSeguro> Items { get; set; }
        public SenderTransacaoPagSeguro Sender { get; set; }
        public ShippingTransacaoPagSeguro Shipping { get; set; }
    }

    /// <summary>
    /// Dados do frete
    /// Atribs: AddressShipping, Type, Cost
    /// </summary>
    public class ShippingTransacaoPagSeguro
    {
        public AddressShipping Address { get; set; }
        public int Type { get; set; }
        public decimal Cost { get; set; }
    }

    /// <summary>
    /// Endereço de entrega;
    /// Atribs: Street, Number, Complement, District, PostalCode, City, State, Country
    /// </summary>
    public class AddressShipping
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    /// <summary>
    /// Dados do comprador
    /// Atribs: Nome, Email, PhoneSender;
    /// </summary>
    public class SenderTransacaoPagSeguro
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public PhoneSender Phone { get; set; }
    }

    /// <summary>
    /// Dados do telefone do SenderTransacaoPagSeguro;
    /// Atribs: AreaCode, Number;
    /// </summary>
    public class PhoneSender
    {
        public int AreaCode { get; set; }
        public int Number { get; set; }
    }

    /// <summary>
    /// Itens de uma TransacaoPagSeguro;
    /// Atribs: Id; Description, Quantity, Amount
    /// </summary>
    public class ItemTransacaoPagSeguro
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }

    /// <summary>
    /// Método de pgamento de uma TransacaoPagSeguro;
    /// Atribs: Type, Code;
    /// </summary>
    public class PaymentMethodPagSeguro
    {
        public int? Type { get; set; }
        public int? Code { get; set; }
    }

    /// <summary>
    /// Taxas aplicadas a uma TransacaoPagSeguro;
    /// Atribs: IntermediationRateAmount, IntermediationFeeAmount
    /// </summary>
    public class CreditorFeesPagSeguro
    {
        public decimal IntermediationRateAmount { get; set; }
        public decimal IntermediationFeeAmount { get; set; }
    }


    public class TransactionSearchResult
    {
        public DateTime? Date { get; set; }
        public List<TransacaoPagSeguro> Transacoes { get; set; }
        public int? ResultsInThisPage { get; set; }
        public int? CurrentPage { get; set; }
        public int? TotalPages { get; set; }
    }

    /// <summary>
    /// Estrutura para obter o código do checkout do PagSeguro
    /// </summary>
    public class CheckOutPagSeguro
    {
        public string Currency { get; set; }
        public string ItemId1 { get; set; }
        public string ItemDescription1 { get; set; }
        public string ItemAmount1 { get; set; }
        public string ItemQuantity1 { get; set; }
        public string ItemWeight1 { get; set; }
        public string Reference { get; set; }
        public string SenderName { get; set; }
        public string SenderAreaCode { get; set; }
        public string SenderPhone { get; set; }
        public string SenderEmail { get; set; }
        public string ShippingType { get; set; }
        public string ShippingAddressRequired { get; set; }
        public string ShippingAddressStreet { get; set; }
        public string ShippingAddressNumber { get; set; }
        public string ShippingAddressComplement { get; set; }
        public string ShippingAddressDistrict { get; set; }
        public string ShippingAddressPostalCode { get; set; }
        public string ShippingAddressCity { get; set; }
        public string ShippingAddressState { get; set; }
        public string ShippingAddressCountry { get; set; }
    }

    public class TokenCheckOutPagSeguro
    {
        public string Code { get; set; }
        public DateTime? Date { get; set; }
        public string Reference { get; set; }
    }

    public class ErrorsPagSeguro
    {
        public string NotificationConde { get; set; }
        public DateTime? DtNotificacaoErro { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
