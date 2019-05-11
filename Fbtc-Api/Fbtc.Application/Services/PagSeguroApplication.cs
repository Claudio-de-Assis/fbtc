using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;

using Fbtc.Application.Helper;


namespace Fbtc.Application.Services
{
    public class PagSeguroApplication : IPagSeguroApplication
    {
        private readonly IPagSeguroService _pagSeguroService;

        public PagSeguroApplication(IPagSeguroService pagSeguroService)
        {
            _pagSeguroService = pagSeguroService;
        }

        public CheckOutPagSeguro getDadosParaCheckOutPagSeguro(int associadoId, string valor, string tipoEndereco, int anoInicio, int anoTermino, bool enderecoRequerido, bool isAnuidade)
        {
            CheckOutPagSeguro _check = new CheckOutPagSeguro();

            if (!valor.Equals("0.00"))
            {
                _check = _pagSeguroService.getDadosParaCheckOutPagSeguro(associadoId, tipoEndereco, anoInicio, anoTermino, enderecoRequerido, isAnuidade);

                DateTime _date = DateTime.Now;

                _check.Currency = "BRL";
                _check.ItemId1 = "01";
                _check.ItemDescription1 = isAnuidade ? $"Assinatura Anuidade {anoInicio}" : "Assinatura de Evento";
                _check.ItemAmount1 = valor;
                _check.ItemQuantity1 = "1";
                _check.ItemWeight1 = "0";
                _check.Reference = isAnuidade ? $"A{anoInicio}{_date.GetHashCode()}".Replace("-","") : $"E{_date.Year}{_date.GetHashCode()}".Replace("-", "");

                _check.SenderName = Functions.AjustaTamanhoString(_check.SenderName,50);
                //_check.SenderAreaCode = Functions.AjustaTamanhoString(_check.SenderAreaCode, 2);
                _check.SenderPhone = Functions.AjustaTamanhoString(_check.SenderPhone, 9);
                _check.SenderEmail = Functions.AjustaTamanhoString(_check.SenderEmail, 60);
                // _check.ShippingType = Functions.AjustaTamanhoString(_check.ShippingType, 3);
                //_check.ShippingAddressRequired = Functions.AjustaTamanhoString(_check.ShippingAddressRequired, 0);
                _check.ShippingAddressStreet = Functions.AjustaTamanhoString(_check.ShippingAddressStreet, 80);
                _check.ShippingAddressNumber = Functions.AjustaTamanhoString(_check.ShippingAddressNumber, 20);
                _check.ShippingAddressComplement = Functions.AjustaTamanhoString(_check.ShippingAddressComplement, 40);
                _check.ShippingAddressDistrict = Functions.AjustaTamanhoString(_check.ShippingAddressDistrict, 60);
                //_check.ShippingAddressPostalCode = Functions.AjustaTamanhoString(_check.ShippingAddressPostalCode, 0);
                _check.ShippingAddressCity = Functions.AjustaTamanhoString(_check.ShippingAddressCity, 60);
                _check.ShippingAddressState = Functions.AjustaTamanhoString(_check.ShippingAddressState, 2);
                //_check.ShippingAddressCountry = Functions.AjustaTamanhoString(_check.ShippingAddressCountry, 0);
            }
            return _check;
        }

        /*
        public string GetNotificationCode(string notificationCode)
        {
            return _pagSeguroService.GetNotificationCode(notificationCode);
        }*/

        public string NotificationTransacao(string notificationCode, string notificationType)
        {
            return _pagSeguroService.NotificationTransacao(notificationCode, notificationType);
        }

        public string SaveDadosTransacaoPagSeguro(TransacaoPagSeguro transacaoPagSeguro)
        {
            return _pagSeguroService.SaveDadosTransacaoPagSeguro(transacaoPagSeguro);
        }

        public string UpdateRecebimentoPagSeguro(TransacaoPagSeguro transacaoPagSeguro)
        {
            return _pagSeguroService.UpdateRecebimentoPagSeguro(transacaoPagSeguro);
        }

        public int UpdateRecebimentosPeriodoPagSeguro(TransactionSearchResult transactionSearchResult)
        {
            return _pagSeguroService.UpdateRecebimentosPeriodoPagSeguro(transactionSearchResult);
        }
    }
}
