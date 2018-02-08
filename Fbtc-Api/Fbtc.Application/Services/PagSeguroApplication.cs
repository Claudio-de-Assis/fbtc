using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Fbtc.Application.Services
{
    public class PagSeguroApplication : IPagSeguroApplication
    {
        private readonly IPagSeguroService _pagSeguroService;

        public PagSeguroApplication(IPagSeguroService pagSeguroService)
        {
            _pagSeguroService = pagSeguroService;
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
