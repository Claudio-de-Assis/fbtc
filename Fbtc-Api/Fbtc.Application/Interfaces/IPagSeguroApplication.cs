using Fbtc.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fbtc.Application.Interfaces
{
    public interface IPagSeguroApplication
    {

        // string GetNotificationCode(string notificationCode);

        /// <summary>
        /// Recebendo uma notificação de transação:
        /// Uma vez configurado o endereço para onde o PagSeguro irá enviar notificações, 
        /// o próximo passo é preparar seu sistema para receber, nesse endereço, um código de notificação.
        /// O PagSeguro envia as notificações para a URL que você configurou usando o protocolo HTTP, 
        /// pelo método POST.
        /// O PagSeguro envia as 
        /// notificações para a URL que você configurou usando o protocolo HTTP, pelo método POST.
        /// </summary>
        /// <param name="notificationCode"></param>
        /// <param name="notificationType"></param>
        /// <returns></returns>
        string NotificationTransacao(string notificationCode, string notificationType);

        
        string UpdateRecebimentoPagSeguro(TransacaoPagSeguro transacaoPagSeguro);

        int UpdateRecebimentosPeriodoPagSeguro(TransactionSearchResult transactionSearchResult);
    }
}
