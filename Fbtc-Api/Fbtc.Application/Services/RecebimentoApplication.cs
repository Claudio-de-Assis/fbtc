using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Application.Interfaces;

using prmToolkit.Validation;
using Fbtc.Application.Helper;

namespace Fbtc.Application.Services
{
    public class RecebimentoApplication : IRecebimentoApplication
    {
        private readonly IRecebimentoService _recebimentoService;

        public RecebimentoApplication(IRecebimentoService recebimentoService)
        {
            _recebimentoService = recebimentoService;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recebimento> FindByFilters(string objetivoPagamento, string nome, string cpf,
            string crp, string crm, string status, int ano, int mes, bool ativo, int eventoId, int tipoPublicoId)
        {
            string _nome, _cpf, _crp, _crm, _status;

            _nome = nome == "0" ? "" : nome;
            _cpf = cpf == "0" ? "" : cpf;
            _crp = crp == "0" ? "" : crp;
            _crm = crm == "0" ? "" : crm;
            _status = status == "0" ? "" : status;

            return _recebimentoService.FindByFilters(objetivoPagamento, _nome, _cpf, _crp, _crm, _status,
                ano, mes , ativo, eventoId, tipoPublicoId);
        }

        public IEnumerable<Recebimento> GetAll(string objetivoPagamento)
        {
            return _recebimentoService.GetAll(objetivoPagamento);
        }

        public IEnumerable<Recebimento> GetRecebimentoByAnuidadeId(int id)
        {
            return _recebimentoService.GetRecebimentoByAnuidadeId(id);
        }

        public IEnumerable<Recebimento> GetRecebimentoByEventoId(int id)
        {
            return _recebimentoService.GetRecebimentoByEventoId(id);
        }

        public Recebimento GetRecebimentoById(int id)
        {
            return _recebimentoService.GetRecebimentoById(id);
        }

        public IEnumerable<Recebimento> GetRecebimentoByPessoaId(string objetivoPagamento, int id)
        {
            return _recebimentoService.GetRecebimentoByPessoaId(objetivoPagamento, id);
        }

        public string Save(Recebimento r)
        {
            /*
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
               RaiseException.IfNullOrEmpty(r.Observacao, "Observação não informada")
            );*/

            Recebimento _r = new Recebimento()
            {
                RecebimentoId = r.RecebimentoId,
                AssociadoId = r.AssociadoId,
                ValorEventoPublicoId = r.ValorEventoPublicoId,
                ValorAnuidadePublicoId = r.ValorAnuidadePublicoId,
                AssociadoIsentoId = r.AssociadoIsentoId,
                ObjetivoPagamento = r.ObjetivoPagamento,
                DtVencimento = r.DtVencimento,
                DtPagamento = r.DtPagamento,
                DtNotificacao = r.DtNotificacao,
                StatusPagamento = r.StatusPagamento,
                NrDocCobranca = Functions.AjustaTamanhoString(r.NrDocCobranca, 100),
                ValorPago = r.ValorPago,
                Observacao = Functions.AjustaTamanhoString(r.Observacao, 500),
                TokenPagamento = r.TokenPagamento,
                Ativo = r.Ativo,
                FormaPagamento = r.FormaPagamento
            };

            try
            {
                if (_r.RecebimentoId == 0)
                {
                    return _recebimentoService.Insert(_r);
                }
                else
                {
                    return _recebimentoService.Update(r.RecebimentoId, _r);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Recebimento SetRecebimento(string objetivoPagamento)
        {
            Recebimento r = new Recebimento() {
                RecebimentoId = 0,
                AssociadoId = null,
                ValorEventoPublicoId = null,
                ValorAnuidadePublicoId = null,
                AssociadoIsentoId = null,
                ObjetivoPagamento = objetivoPagamento,
                DtVencimento = null,
                DtPagamento = null,
                DtNotificacao = null,
                StatusPagamento = "",
                NrDocCobranca = "",
                ValorPago = 0,
                Observacao = "",
                TokenPagamento = "",
                Ativo = true,
                FormaPagamento = ""
            };
            return r;
        }
    }
}
