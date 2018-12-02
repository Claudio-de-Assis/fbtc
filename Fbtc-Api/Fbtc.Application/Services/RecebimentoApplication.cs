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

        public IEnumerable<RecebimentoAssociadoDao> FindAnuidadeByFilters(string nome, string cpf, 
            string crp, string crm, int statusPS, int ano, int mes, bool? ativo, int tipoPublicoId)
        {
            string _nome, _cpf, _crp, _crm;
            int _statusPS;

            _nome = nome == "0" ? "" : nome;
            _cpf = cpf == "0" ? "" : cpf;
            _crp = crp == "0" ? "" : crp;
            _crm = crm == "0" ? "" : crm;
            _statusPS = statusPS;

            return _recebimentoService.FindAnuidadeByFilters(_nome, _cpf, _crp, _crm, _statusPS,
                ano, mes, ativo, tipoPublicoId);
        }

        public IEnumerable<RecebimentoAssociadoDao> FindByAnuidadeIdFilters(int anuidadeId, string nome,
            string cpf, string crp, string crm, int statusPS, int ano, int mes,
            bool? ativo, int tipoPublicoId)
        {
            string _nome, _cpf, _crp, _crm;
            int _statusPS;

            _nome = nome == "0" ? "" : nome;
            _cpf = cpf == "0" ? "" : cpf;
            _crp = crp == "0" ? "" : crp;
            _crm = crm == "0" ? "" : crm;
            _statusPS = statusPS;

            return _recebimentoService.FindByAnuidadeIdFilters(anuidadeId, _nome, _cpf, _crp, _crm, 
                _statusPS, ano, mes, ativo, tipoPublicoId);
        }

        public IEnumerable<RecebimentoAssociadoDao> FindEventoByFilters(string nome, string cpf, 
            string crp, string crm, int statusPS, int ano, int mes, bool? ativo, string tipoEvento,
            int tipoPublicoId)
        {
            string _nome, _cpf, _crp, _crm, _tipoEvento;
            int _statusPS;

            _nome = nome == "0" ? "" : nome;
            _cpf = cpf == "0" ? "" : cpf;
            _crp = crp == "0" ? "" : crp;
            _crm = crm == "0" ? "" : crm;
            _statusPS = statusPS;
            _tipoEvento = tipoEvento == "0" ? "" : tipoEvento;

            return _recebimentoService.FindEventoByFilters(_nome, _cpf, _crp, _crm, _statusPS,
                ano, mes, ativo, _tipoEvento, tipoPublicoId);
        }

        public IEnumerable<RecebimentoAssociadoDao> FindByEventoIdFilters(int eventoId, string nome, 
            string cpf, string crp, string crm, int statusPS, int ano, int mes, bool? ativo,
            string tipoEvento, int tipoPublicoId)
        {
            string _nome, _cpf, _crp, _crm, _tipoEvento;
            int _statusPS;

            _nome = nome == "0" ? "" : nome;
            _cpf = cpf == "0" ? "" : cpf;
            _crp = crp == "0" ? "" : crp;
            _crm = crm == "0" ? "" : crm;
            _statusPS = statusPS;
            _tipoEvento = tipoEvento == "0" ? "" : tipoEvento;

            return _recebimentoService.FindByEventoIdFilters(eventoId, _nome, _cpf, _crp, _crm, _statusPS,
                ano, mes, ativo, _tipoEvento, tipoPublicoId);
        }

        public IEnumerable<RecebimentoAssociadoDao> FindPagamentosByPessoaIdIdFilters(int pessoaId,
            string objetivoPagamento, int ano, int statusPS)
        {
            return _recebimentoService.FindPagamentosByPessoaIdIdFilters(pessoaId,
            objetivoPagamento, ano, statusPS);
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
            
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
               RaiseException.IfEqualsZero(r.RecebimentoId, "Identificação do recebimento inválida")
            );

            Recebimento _r = new Recebimento()
            {
                RecebimentoId = r.RecebimentoId,
                AssinaturaAnuidadeId = r.AssinaturaAnuidadeId,
                AssinaturaEventoId = r.AssinaturaEventoId,
                Observacao = Functions.AjustaTamanhoString(r.Observacao, 500),
                NotificationCodePS = r.NotificationCodePS,
                TypePS = r.TypePS,
                StatusPS = r.StatusPS,
                LastEventDatePS = r.LastEventDatePS,
                TypePaymentMethodPS = r.TypePaymentMethodPS,
                CodePaymentMethodPS = r.CodePaymentMethodPS,
                NetAmountPS = r.NetAmountPS,
                DtVencimento = r.DtVencimento,
                StatusFBTC = r.StatusFBTC,
                DtStatusFBTC = r.DtStatusFBTC,
                OrigemEmissaoTitulo = r.OrigemEmissaoTitulo,
                DtCadastro = r.DtCadastro,
                Ativo = r.Ativo,
            };

            try
            {
                return _recebimentoService.Update(r.RecebimentoId, _r);
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
                CodePaymentMethodPS = null,
                NotificationCodePS = null,
                LastEventDatePS = null,
                NetAmountPS = 0,
                StatusPS = null,
                TypePaymentMethodPS = null,
                TypePS = null,
                Observacao = "",
                Ativo = true
            };
            return r;
        }

        public RecebimentoAssociadoDao GetRecebimentoAssociadoDaoByRecebimentoId(int id)
        {
            return _recebimentoService.GetRecebimentoAssociadoDaoByRecebimentoId(id);
        }
    }
}
