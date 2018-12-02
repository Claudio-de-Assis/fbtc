using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class AssinaturaEvento
    {
        public int AssinaturaEventoId { get; set; }
        public int AssociadoId { get; set; }
        public int ValorEventoPublicoId { get; set; }

        public decimal PercentualDesconto { get; set; }
        public string TipoDesconto { get; set; }
        public decimal Valor { get; set; }
        public DateTime DtVencimentoPagamento { get; set; }
        public DateTime DtAssinatura { get; set; }
        public string CodePS { get; set; }
        public DateTime? DtCodePS { get; set; }
        public string Reference { get; set; }
        public bool EmProcessoPagamento { get; set; }
        public DateTime? DtInicioProcessamento { get; set; }

        public DateTime DtAtualizacao { get; set; }
        public bool Ativo { get; set; }
    }

    public class AssinaturaEventoDao : AssinaturaEvento
    {
        public string NomePessoa { get; set; }
        public string CPF { get; set; }
        public string NomeTP { get; set; }
        public string Titulo { get; set; }
        public decimal ValorEvento { get; set; }

        public int EventoId { get; set; }
        public int TipoPublicoId { get; set; }
        public bool? AnuidadeAtcOk { get; set; }
        public bool? MembroDiretoria { get; set; }
        public bool? MembroConfi { get; set; }
    }
}
