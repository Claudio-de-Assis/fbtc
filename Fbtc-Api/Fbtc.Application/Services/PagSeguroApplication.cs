using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Domain.Services;


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
