using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;

namespace Fbtc.Domain.Services
{
    public class PagSeguroService : IPagSeguroService
    {
        private readonly IPagSeguroRepository _pagSeguroRepository;

        public PagSeguroService(IPagSeguroRepository pagSeguroRepository)
        {
            _pagSeguroRepository = pagSeguroRepository;
        }

        public CheckOutPagSeguro getDadosParaCheckOutPagSeguro(int associadoId, string tipoEndereco, int anoInicio, int anoTermino, bool enderecoRequerido, bool isAnuidade)
        {
            return _pagSeguroRepository.getDadosParaCheckOutPagSeguro(associadoId, tipoEndereco, anoInicio, anoTermino, enderecoRequerido, isAnuidade);
        }

        /*
        public string GetNotificationCode(string notificationCode)
        {
            return _pagSeguroRepository.GetNotificationCode(notificationCode);
        }*/

        public string NotificationTransacao(string notificationCode, string notificationType)
        {
            return _pagSeguroRepository.NotificationTransacao(notificationCode, notificationType);
        }

        public string UpdateRecebimentoPagSeguro(TransacaoPagSeguro transacaoPagSeguro)
        {
            return _pagSeguroRepository.UpdateRecebimentoPagSeguro(transacaoPagSeguro);
        }

        public string SaveDadosTransacaoPagSeguro(TransacaoPagSeguro transacaoPagSeguro)
        {
            return _pagSeguroRepository.SaveDadosTransacaoPagSeguro(transacaoPagSeguro);
        }

        public int UpdateRecebimentosPeriodoPagSeguro(TransactionSearchResult transactionSearchResult)
        {
            return _pagSeguroRepository.UpdateRecebimentosPeriodoPagSeguro(transactionSearchResult);
        }
    }
}
