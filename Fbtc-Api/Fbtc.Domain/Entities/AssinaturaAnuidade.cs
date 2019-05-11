using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class AssinaturaAnuidade
    {
        public int AssinaturaAnuidadeId { get; set; }
        public int AssociadoId { get; set; }
        public int ValorAnuidadeId { get; set; }
        public int AnoInicio { get; set; }
        public int AnoTermino { get; set; }
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

        public bool PagamentoIsento { get; set; }
        public bool PagamentoIsentoBD { get; set; }
        public DateTime? DtIsencao { get; set; }
        public string ObservacaoIsencao { get; set; }
    }

    public class AssinaturaAnuidadeDao : AssinaturaAnuidade
    {
        public string NomePessoa { get; set; }
        public string CPF { get; set; }
        public string NomeTP { get; set; }
        public int Exercicio { get; set; }
        public int TipoAnuidade { get; set; }
        public decimal ValorTipoAnuidade { get; set; }
        public int AnuidadeId { get; set; }
        public int TipoPublicoId { get; set; }
        public bool? AnuidadeAtcOk { get; set; }
        public bool? MembroDiretoria { get; set; }
        public bool? MembroConfi { get; set; }
        public int ValorAnuidadeIdOriginal { get; set; }
        public int? RecebimentoStatusPS { get; set; }

    }
}
