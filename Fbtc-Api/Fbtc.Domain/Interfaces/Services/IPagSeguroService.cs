using Fbtc.Domain.Entities;
using System.Threading.Tasks;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IPagSeguroService
    {
        // string GetNotificationCode(string notificationCode);

        string NotificationTransacao (string notificationCode, string notificationType);

        string UpdateRecebimentoPagSeguro(TransacaoPagSeguro transacaoPagSeguro);

        int UpdateRecebimentosPeriodoPagSeguro(TransactionSearchResult transactionSearchResult);

    }
}
