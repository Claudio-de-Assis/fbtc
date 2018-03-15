using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class Relatorios
    {
    }

    public class RptTotalAssociadosDAO
    {
        public string NomeTipoAssociado { get; set; }
        public int Ordem { get; set; }
        public int Qtd { get; set; }
        public int QtdTotal { get; set; }

    }

    public class RptRecebimentoStatusDAO
    {
        public string StatusPagamento { get; set; }
        public int Qtd { get; set; }
        public decimal ValorPorStatus { get; set; }
        public decimal ValorTotal { get; set; }
    }

    public class RptAssociadoFaixaDAO
    {
        public int FaixaAte30 { get; set; }
        public int Faixa31a40 { get; set; }
        public int Faixa41a50 { get; set; }
        public int Faixa51a60 { get; set; }
        public int FaixaApos60 { get; set; }
    }

    public class RptAssociadosEstadosDAO
    {
        public string NomeUF { get; set; }
        public string ProfissionalAssociado { get; set; }
        public string ProfissionalNaoAssociado { get; set; }
        public string EstudantePosAssociado { get; set; }
        public string EstudantePosNaoAssociado { get; set; }
        public string EstudanteAssociado { get; set; }
        public string EstudanteNaoAssociado { get; set; }
    }

    public class RptReceitaAnualDAO
    {
        public int Ano { get; set; }
        public string NomeObjetivoPagamento { get; set; }
        public decimal ValorPrevisto { get; set; }
        public decimal ValorRealizado { get; set; }
        public int QtdIsentos { get; set; }
    }
}
