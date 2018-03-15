using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IRelatoriosService
    {
        // -- Relatório Total de Associados + Relatório de Associados por Tipo:
        IEnumerable<RptTotalAssociadosDAO> GetRptTotalAssociadosTipo();

        // -- Relatório de Recebimento por Status PagSeguro E Relatório de Receita Anual:: Parametros: ObjetivoPagamento e AnoEventoPS
        IEnumerable<RptRecebimentoStatusDAO> GetRptRecebimentoStatus(int objetivoPagamento, int anoEventoPS, int statusPS);

        // -- Relatório de Associados Por Faixa Etária:
        IEnumerable<RptAssociadoFaixaDAO> GetRptAssociadosFaixa();

        // -- Relatório de Associados Por Estado ==> Filtro: Estados:
        IEnumerable<RptAssociadosEstadosDAO> GetRptAssociadosEstados();

        // -- Relatório de Associados por Gênero
        IEnumerable<RptTotalAssociadosDAO> GetRptAssociadosGenero(string genero);

        // -- Relatório da Quantidade de Associados Por Ano:
        IEnumerable<RptTotalAssociadosDAO> GetRptAssociadosAno(int ano);

        // - RElatório de Receita Anual:
        IEnumerable<RptReceitaAnualDAO> GetRptReceitaAnual();
    }
}
