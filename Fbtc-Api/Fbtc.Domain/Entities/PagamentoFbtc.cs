using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class PagamentoFbtc
    {
        public int PagamentoFBTCId { get; set; }
        public int AssociadoId { get; set; }
        public int AssociadoIsentoId { get; set; }
        public int ValorAnuidadePublicoId { get; set; }
        public int ValorEventoPublicoId { get; set; }
        public string ObjetivoPagamento { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime DtVencimento { get; set; }
        public DateTime DtPagamento { get; set; }
        public DateTime DtNotificacao { get; set; }
        public string StatusPagto { get; set; }
        public string FormaPagto { get; set; }
        public string NrDocCobranca { get; set; }
        public decimal ValorPago { get; set; }
        public string Observacao { get; set; }
        public string TokenPagamento { get; set; }
        public bool Ativo { get; set; }
        public int MyProperty { get; set; }
    }
}
