using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Application.Services
{
    public class RelatoriosApplication : IRelatoriosApplication
    {
        private readonly IRelatoriosService _relatoriosService;

        public RelatoriosApplication(IRelatoriosService relatoriosService)
        {
            _relatoriosService = relatoriosService;
        }

        public IEnumerable<RptTotalAssociadosDAO> GetRptAssociadosAno(int ano)
        {
            return _relatoriosService.GetRptAssociadosAno(ano);
        }

        public IEnumerable<RptAssociadosEstadosDAO> GetRptAssociadosEstados()
        {
            return _relatoriosService.GetRptAssociadosEstados();
        }

        public IEnumerable<RptAssociadoFaixaDAO> GetRptAssociadosFaixa()
        {
            return _relatoriosService.GetRptAssociadosFaixa();
        }

        public IEnumerable<RptTotalAssociadosDAO> GetRptAssociadosGenero(string genero)
        {
            return _relatoriosService.GetRptAssociadosGenero(genero);
        }

        public IEnumerable<RptRecebimentoStatusDAO> GetRptRecebimentoStatus(int objetivoPagamento, int anoEventoPS, int statusPS)
        {
            return _relatoriosService.GetRptRecebimentoStatus(objetivoPagamento, anoEventoPS, statusPS);
        }

        public IEnumerable<RptReceitaAnualDAO> GetRptReceitaAnual()
        {
            return _relatoriosService.GetRptReceitaAnual();
        }

        public IEnumerable<RptTotalAssociadosDAO> GetRptTotalAssociadosTipo()
        {
            return _relatoriosService.GetRptTotalAssociadosTipo();
        }
    }
}
