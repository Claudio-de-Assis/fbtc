using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class PagamentoFBTC
    {
        public virtual int PagamentoFBTCId { get; set; }
        public virtual Associado Associado { get; set; }
        public virtual AssociadoIsento AssociadoIsento { get; set; }
        public virtual ValorAnuidadePublico ValorAnuidadePublico { get; set; }
        public virtual ValorEventoPublico ValorEventoPublico { get; set; }
        public virtual string ObjetivoPagamento { get; set; }
        public virtual DateTime DtCadastro { get; set; }
        public virtual DateTime DtVencimento { get; set; }
        public virtual DateTime DtPagamento { get; set; }
        public virtual DateTime DtNotificacao { get; set; }
        public virtual string StatusPagto { get; set; }
        public virtual string FormaPagto { get; set; }
        public virtual string NrDocCobranca { get; set; }
        public virtual decimal ValorPago { get; set; }
        public virtual string Observacao { get; set; }
        public virtual string TokenPagamento { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual int MyProperty { get; set; }
    }
}
