using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Application.Interfaces;

using prmToolkit.Validation;
using Fbtc.Application.Helper;

namespace Fbtc.Application.Services
{
    public class DescontoAnuidadeAtcApplication : IDescontoAnuidadeAtcApplication
    {
        private readonly IDescontoAnuidadeAtcService _descontoAnuidadeAtcService;

        public DescontoAnuidadeAtcApplication(IDescontoAnuidadeAtcService descontoAnuidadeAtcService)
        {
            _descontoAnuidadeAtcService = descontoAnuidadeAtcService;
        }

        public IEnumerable<DescontoAnuidadeAtcDao> FindByFilters(int anuidadeId, string nomePessoa, bool? ativo, bool? comDesconto)
        {
            string _nome;

            _nome = nomePessoa == "0" ? "" : nomePessoa;
            if (_nome.IndexOf("%20") > 0)
                _nome = _nome.Replace("%20", " ");

            return _descontoAnuidadeAtcService.FindByFilters(anuidadeId, _nome, ativo, comDesconto);
        }

        public IEnumerable<DescontoAnuidadeAtc> GetAll()
        {
            return _descontoAnuidadeAtcService.GetAll();
        }

        public DescontoAnuidadeAtc GetDescontoAnuidadeAtcById(int id)
        {
            return _descontoAnuidadeAtcService.GetDescontoAnuidadeAtcById(id);
        }

        public DescontoAnuidadeAtcDao GetDescontoAnuidadeAtcDaoById(int id)
        {
            return _descontoAnuidadeAtcService.GetDescontoAnuidadeAtcDaoById(id);
        }

        public DescontoAnuidadeAtcDao GetDescontoAnuidadeAtcDaoByPessoaId(int pessoaId)
        {
            return _descontoAnuidadeAtcService.GetDescontoAnuidadeAtcDaoByPessoaId(pessoaId);
        }

        public IEnumerable<DescontoAnuidadeAtcDao> GetDescontoAnuidadeAtcDaoByAnuidadeId(int anuidadeId)
        {
            return _descontoAnuidadeAtcService.GetDescontoAnuidadeAtcDaoByAnuidadeId(anuidadeId);
        }

        public string Save(DescontoAnuidadeAtcDao d)
        {
            DescontoAnuidadeAtc _d = new DescontoAnuidadeAtc() {
                DescontoAnuidadeAtcId = d.DescontoAnuidadeAtcId,
                AssociadoId = d.AssociadoId,
                ColaboradorId = d.ColaboradorId,
                AnuidadeId = d.AnuidadeId,
                AtcId = d.AtcId,
                Observacao = Functions.AjustaTamanhoString(d.Observacao, 500),
                NomeArquivoComprovante = Functions.AjustaTamanhoString(d.NomeArquivoComprovante, 100),
                DtDesconto = d.DtDesconto,
                DtCadastro = d.DtCadastro,
                Ativo = d.Ativo
            };

            try
            {
                if (_d.DescontoAnuidadeAtcId == 0)
                {
                    return _descontoAnuidadeAtcService.Insert(_d);
                }
                else
                {
                    return _descontoAnuidadeAtcService.Update(_d.DescontoAnuidadeAtcId, _d);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DescontoAnuidadeAtcDao GetDadosNovoDescontoAnuidadeAtcDao(int associadoId, int anuidadeId, int colaboradorPessoaId)
        {
            return _descontoAnuidadeAtcService.GetDadosNovoDescontoAnuidadeAtcDao(associadoId, anuidadeId, colaboradorPessoaId);
        }
    }
}
