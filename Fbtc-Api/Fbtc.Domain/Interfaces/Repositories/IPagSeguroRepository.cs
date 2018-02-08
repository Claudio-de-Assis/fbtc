using Fbtc.Domain.Entities;


namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IPagSeguroRepository
    {
        // string GetNotificationCode(string notificationCode);

        string NotificationTransacao(string notificationCode, string notificationType);

        string UpdateRecebimentoPagSeguro(TransacaoPagSeguro transacaoPagSeguro);

        int UpdateRecebimentosPeriodoPagSeguro(TransactionSearchResult transactionSearchResult);
    }
}
