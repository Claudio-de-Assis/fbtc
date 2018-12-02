using Fbtc.Application.Helper;
using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;


namespace Fbtc.Application.Services
{
    public class AssinaturaAnuidadeApplication : IAssinaturaAnuidadeApplication
    {

        private readonly IAssinaturaAnuidadeService _assinaturaAnuidadeService;

        public AssinaturaAnuidadeApplication(IAssinaturaAnuidadeService assinaturaAnuidadeService)
        {
            _assinaturaAnuidadeService = assinaturaAnuidadeService;
        }

        public IEnumerable<AssinaturaAnuidadeDao> FindAssinaturaPendenteByFilters(int anuidadeId, string nome, string cpf, bool? ativo)
        {
            string _nome, _cpf;

            _nome = nome == "0" ? "" : nome;
            _cpf = cpf == "0" ? "" : cpf;

            if (_nome.IndexOf("%20") > 0)
                _nome = _nome.Replace("%20", " ");

            return _assinaturaAnuidadeService.FindAssinaturaPendenteByFilters(anuidadeId, _nome, _cpf, ativo);
        }

        public IEnumerable<AssinaturaAnuidadeDao> FindByFilters(int anuidadeId, string nome, string cpf, bool? ativo)
        {
            string _nome, _cpf;

            _nome = nome == "0" ? "" : nome;
            _cpf = cpf == "0" ? "" : cpf;

            if (_nome.IndexOf("%20") > 0)
                _nome = _nome.Replace("%20", " ");

            return _assinaturaAnuidadeService.FindByFilters(anuidadeId, _nome, _cpf, ativo);
        }

        public IEnumerable<AssinaturaAnuidadeDao> FindByPessoaId(int pessoaId)
        {
            return _assinaturaAnuidadeService.FindByPessoaId(pessoaId);
        }

        public IEnumerable<AssinaturaAnuidade> GetAll()
        {
            return _assinaturaAnuidadeService.GetAll();
        }

        public AssinaturaAnuidadeDao GetAssinaturaAnuidadeById(int id)
        {
            return _assinaturaAnuidadeService.GetAssinaturaAnuidadeById(id);
        }

        public string Save(AssinaturaAnuidadeDao a)
        {
            int _percentualDesconto = 0;
            string _tipoDesconto = "0";
            decimal _valor = a.Valor;

            _valor = Functions.CalcularDescontoAnuidade(a);
   
            // Desconto aplicado para Membros CONFI:
            if (a.MembroConfi == true)
            {
                _percentualDesconto = 100;
                _tipoDesconto = "4";
                //_valor = 0;
            }
            else
            {
                // O Desconto somente é aplicado para a Assinatura de Um Ano:
                if (a.TipoAnuidade == 1)
                {
                    if (a.MembroDiretoria == true)
                    {
                        _percentualDesconto = 100;
                        _tipoDesconto = "1";
                        //_valor = 0;
                    }

                    if (_percentualDesconto == 0)
                    {
                        if (a.AnuidadeAtcOk == true)
                        {
                            _percentualDesconto = 50;
                            _tipoDesconto = "2";
                            //_valor = a.Valor > 0 ? a.Valor / 2 : 0;
                        }
                    }
                }
            }
        
            AssinaturaAnuidade assinaturaAnuidade = new AssinaturaAnuidade
            {
                AssinaturaAnuidadeId = a.AssinaturaAnuidadeId,
                AssociadoId = a.AssociadoId,
                ValorAnuidadeId = a.ValorAnuidadeId,
                AnoInicio = a.AnoInicio == 0 ? a.Exercicio : a.AnoInicio,
                AnoTermino = a.AnoTermino == 0 ? a.Exercicio + a.TipoAnuidade : a.AnoTermino,
                PercentualDesconto = _percentualDesconto,
                TipoDesconto = _tipoDesconto,
                Valor = _valor,
                DtVencimentoPagamento = a.DtVencimentoPagamento,
                DtAssinatura = a.DtAssinatura,
                CodePS = a.CodePS,
                DtCodePS = a.DtCodePS,
                Reference = a.Reference,
                EmProcessoPagamento = a.EmProcessoPagamento,
                DtInicioProcessamento = a.DtInicioProcessamento,
                DtAtualizacao = a.DtAtualizacao,
                Ativo = a.Ativo
            };

            try
            {
                if (assinaturaAnuidade.AssinaturaAnuidadeId == 0)
                {
                    // assinaturaAnuidade.Reference = "A" + a.Exercicio + DateTime.Now.GetHashCode();
                    return _assinaturaAnuidadeService.Insert(assinaturaAnuidade);
                }
                else
                {
                    return _assinaturaAnuidadeService.Update(assinaturaAnuidade.AssinaturaAnuidadeId, assinaturaAnuidade);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
