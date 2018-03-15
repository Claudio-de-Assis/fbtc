using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;


namespace Fbtc.Domain.Services
{
    public class RelatoriosService : IRelatoriosService
    {
        private readonly IRelatoriosRepository _relatoriosRepository;

        public RelatoriosService(IRelatoriosRepository relatoriosRepository)
        {
            _relatoriosRepository = relatoriosRepository;
        }

        public IEnumerable<RptTotalAssociadosDAO> GetRptAssociadosAno(int ano)
        {
            return _relatoriosRepository.GetRptAssociadosAno(ano);
        }

        public IEnumerable<RptAssociadosEstadosDAO> GetRptAssociadosEstados()
        {
            return _relatoriosRepository.GetRptAssociadosEstados();
        }

        public IEnumerable<RptAssociadoFaixaDAO> GetRptAssociadosFaixa()
        {
            return _relatoriosRepository.GetRptAssociadosFaixa();
        }

        public IEnumerable<RptTotalAssociadosDAO> GetRptAssociadosGenero(string genero)
        {
            return _relatoriosRepository.GetRptAssociadosGenero(genero);
        }

        public IEnumerable<RptRecebimentoStatusDAO> GetRptRecebimentoStatus(int objetivoPagamento, int anoEventoPS, int statusPS)
        {
            return _relatoriosRepository.GetRptRecebimentoStatus(objetivoPagamento, anoEventoPS, statusPS);
        }

        public IEnumerable<RptReceitaAnualDAO> GetRptReceitaAnual()
        {
            return _relatoriosRepository.GetRptReceitaAnual();
        }

        public IEnumerable<RptTotalAssociadosDAO> GetRptTotalAssociadosTipo()
        {
            return _relatoriosRepository.GetRptTotalAssociadosTipo();
        }
    }
}
