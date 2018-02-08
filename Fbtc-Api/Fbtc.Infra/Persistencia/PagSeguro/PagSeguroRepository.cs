using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

using Fbtc.Infra.Helpers;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;

using prmToolkit.AccessMultipleDatabaseWithAdoNet;
using prmToolkit.AccessMultipleDatabaseWithAdoNet.Enumerators;
using Fbtc.Infra.Persistencia.AdoNet;

namespace Fbtc.Infra.Persistencia.PagSeguro
{
    public class PagSeguroRepository : AbstractRepository, IPagSeguroRepository
    {
        private readonly string strConnSql;

        public PagSeguroRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
        }

        /*
        public string GetNotificationCode(string notificationCode)
        {
            return $"GetNotificationCode: Cheguei na camada de Repository: {notificationCode} ";
        }*/

        public string NotificationTransacao(string notificationCode, string notificationType)
        {

            bool _resultado = false;
            string _msg = "";
            Int32 id = 0;

            using (SqlConnection connection = new SqlConnection(strConnSql))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("IncluirNotificacao");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "" +
                        "INSERT into dbo.AD_Notificacao_PagSeguro (NotificationCode, NotificationType, DtCadastro) " +
                        "VALUES(@NotificationCode, @NotificationType, @DtCadastro) " +
                        "SELECT CAST(scope_identity() AS int) ";

                    command.Parameters.AddWithValue("NotificationCode", notificationCode);
                    command.Parameters.AddWithValue("NotificationType", notificationType);
                    command.Parameters.AddWithValue("DtCadastro", DateTime.Now);

                    id = (Int32)command.ExecuteScalar();
                    _resultado = id > 0;

                    _msg = _resultado ? "Inclusão realiada com sucesso" : "Inclusão Não realiada com sucesso";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception($"Rollback Exception Type:{ex2.GetType()}. Erro:{ex2.Message}");
                    }
                    throw new Exception($"Commit Exception Type:{ex.GetType()}. Erro:{ex.Message}");
                }
                connection.Close();
            }
            //return _msg;
            return $"GetNotificationCodeByType: Cheguei na camada de Repository: {notificationCode} e {notificationType}";
        }

        public string UpdateRecebimentoPagSeguro(TransacaoPagSeguro transacaoPagSeguro)
        {
            string _msg = "";

            if (transacaoPagSeguro == null)
                return "O Objeto TransacaoPagSeguro está nulo!";

            RecebimentoRepository _recRepo = new RecebimentoRepository();

            int _type = (int)transacaoPagSeguro.Type;
            int _status = (int)transacaoPagSeguro.Status;
            int _mtdType = (int)transacaoPagSeguro.PaymentMethod.Type;
            int _mtdcode = transacaoPagSeguro.PaymentMethod.Code ?? 0;
            decimal _netAmount = (decimal)transacaoPagSeguro.NetAmount;

            _msg = _recRepo.UpdateRecebimentoPagSeguro(transacaoPagSeguro.Code, transacaoPagSeguro.Reference, _type, _status,
                transacaoPagSeguro.Lasteventdate, _mtdType, _mtdcode, _netAmount);

            return _msg;
        }

        public int UpdateRecebimentosPeriodoPagSeguro(TransactionSearchResult transactionSearchResult)
        {
            int _qtd = 0;
            string _msg = "";

            if (transactionSearchResult != null)
            {
                foreach (var t in transactionSearchResult.Transacoes)
                {
                    _msg = UpdateRecebimentoPagSeguro(t);
                    if (_msg.Equals("Atualização realizada com sucesso"))
                        _qtd++;
                }
            }

            return _qtd;
        }
    }
}

