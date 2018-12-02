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
        private readonly string pS_IsDebug;
        private readonly string pS_CompradorTeste;

        public PagSeguroRepository()
        {
            strConnSql = ConfigHelper.GetConnectionString("FBTC_ConnectionString");
            pS_IsDebug = ConfigHelper.GetKeyAppSetting("PS_IsDebug");
            pS_CompradorTeste = ConfigHelper.GetKeyAppSetting("PSSbox_CompradorTeste");
        }

        public CheckOutPagSeguro getDadosParaCheckOutPagSeguro(int associadoId, string tipoEndereco, int anoInicio, int anoTermino, bool enderecoRequerido, bool isAnuidade)
        {
            CheckOutPagSeguro _check = new CheckOutPagSeguro();

            try
            {
                AssociadoRepository associadoRepository = new AssociadoRepository();

                Associado a = associadoRepository.GetAssociadoById(associadoId);

                if (a != null)
                {
                    string _codeArea = "";
                    string _telefone = "";
                    if (!string.IsNullOrWhiteSpace(a.NrCelular))
                    {
                        _codeArea = a.NrCelular.Substring(0, 2);
                        _telefone = a.NrCelular.Substring(2);
                    }

                    string _eMail = "";
                    _eMail = pS_IsDebug == "true" ? pS_CompradorTeste : a.EMail;

                    Endereco e = new Endereco();

                    if (a.EnderecosPessoa != null)
                    {
                        List<Endereco> lst = new List<Endereco>();

                        foreach (var end in a.EnderecosPessoa)
                        {
                            if (end.TipoEndereco == tipoEndereco)
                            {
                                e = end;
                            }
                        }
                    }

                    _check = new CheckOutPagSeguro()
                    {
                        Currency = "",
                        ItemId1 = "",
                        ItemDescription1 = "",
                        ItemAmount1 = "0.00",
                        ItemQuantity1 = "",
                        ItemWeight1 = "",
                        Reference = "",
                        SenderName = a.Nome,
                        SenderAreaCode = _codeArea,
                        SenderPhone = _telefone,
                        SenderEmail = _eMail,
                        ShippingType = "1",
                        ShippingAddressRequired = "true",
                        ShippingAddressStreet = e.Logradouro,
                        ShippingAddressNumber = e.Numero,
                        ShippingAddressComplement = e.Complemento,
                        ShippingAddressDistrict = e.Bairro,
                        ShippingAddressPostalCode = e.Cep,
                        ShippingAddressCity = e.Cidade,
                        ShippingAddressState = e.Estado,
                        ShippingAddressCountry = "BRA"
                    };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _check;
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

                    _msg = _resultado ? "Inclusão Realizada com sucesso" : "Inclusão Não Realizada com sucesso";

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

        public string SaveDadosTransacaoPagSeguro(TransacaoPagSeguro transacaoPagSeguro)
        {
            string _msg = "";
            if (transacaoPagSeguro != null)
            {
                AssinaturaAnuidadeRepository assinaturaAnuidadeRepository = new AssinaturaAnuidadeRepository();
                RecebimentoRepository recebimentoRepository = new RecebimentoRepository();

                var assinatura = assinaturaAnuidadeRepository.GetAssinaturaAnuidadeByReference(transacaoPagSeguro.Reference);
                var recebimento = recebimentoRepository.GetRecebimentoByReference(transacaoPagSeguro.Reference);

                if (assinatura != null)
                {
                    if (recebimento == null)
                    {
                        //Ainda não há regitro 
                        Recebimento rec = new Recebimento()
                        {
                            RecebimentoId = 0,
                            AssinaturaAnuidadeId = assinatura.AssinaturaAnuidadeId,
                            NotificationCodePS = transacaoPagSeguro.NotificationCode,

                            TypePS = transacaoPagSeguro.Type,
                            StatusPS = transacaoPagSeguro.Status,
                            TypePaymentMethodPS = transacaoPagSeguro.PaymentMethod.Type ?? 0,
                            CodePaymentMethodPS = transacaoPagSeguro.PaymentMethod.Code ?? 0,
                            NetAmountPS = (decimal)transacaoPagSeguro.NetAmount,
                            GrossAmountPS = (decimal)transacaoPagSeguro.GrossAmount,
                            DiscountAmountPS = (decimal)transacaoPagSeguro.DiscountAmount,
                            FeeAmountPS = (decimal)transacaoPagSeguro.FeeAmount,
                            ExtraAmountPS = (decimal)transacaoPagSeguro.ExtraAmount,
                            DtVencimento = assinatura.DtVencimentoPagamento,
                            StatusFBTC = "1",
                            OrigemEmissaoTitulo = "1"
                        };

                        _msg = recebimentoRepository.Insert(rec, transacaoPagSeguro.Lasteventdate);
                        _msg = assinaturaAnuidadeRepository.SetInicioPagamentoPagSeguro(transacaoPagSeguro.Reference, true);
                    }
                    else
                    {
                        recebimento.StatusPS = transacaoPagSeguro.Status;
                        recebimento.TypePaymentMethodPS = transacaoPagSeguro.PaymentMethod.Type ?? 0;
                        recebimento.CodePaymentMethodPS = transacaoPagSeguro.PaymentMethod.Code ?? 0;
                        recebimento.NetAmountPS = (decimal)transacaoPagSeguro.NetAmount;
                        recebimento.GrossAmountPS = (decimal)transacaoPagSeguro.GrossAmount;
                        recebimento.DiscountAmountPS = (decimal)transacaoPagSeguro.DiscountAmount;
                        recebimento.FeeAmountPS = (decimal)transacaoPagSeguro.FeeAmount;
                        recebimento.ExtraAmountPS = (decimal)transacaoPagSeguro.ExtraAmount;
                        recebimento.DtVencimento = assinatura.DtVencimentoPagamento;

                        _msg = recebimentoRepository.UpdateRecebimentoPagSeguro(recebimento.RecebimentoId, recebimento, transacaoPagSeguro.Lasteventdate);
                    }
                }
                else
                {
                    _msg = $"ATENÇÃO: Não foi encontrado registro para a referência {transacaoPagSeguro.Reference} e NotificationCode {transacaoPagSeguro.NotificationCode} informados pelo PagSeguro";
                }
            }
            else
            {
                _msg = $"ATENÇÃO: Objeto transacaoPagSeguro está nulo";
            }

            return _msg;
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

            //_msg = _recRepo.UpdateRecebimentoPagSeguro(transacaoPagSeguro.NotificationCode, transacaoPagSeguro.Reference, _type, _status,
             //   transacaoPagSeguro.Lasteventdate, _mtdType, _mtdcode, _netAmount);

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
//                    _msg = UpdateRecebimentoPagSeguro(t);
                    _msg = SaveDadosTransacaoPagSeguro(t);
                    if (_msg.Equals("Atualização realizada com sucesso"))
                        _qtd++;
                }
            }
            return _qtd;
        }
    }
}

