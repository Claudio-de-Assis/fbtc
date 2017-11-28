using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class Associado : Pessoa
    {
        public int AssociadoId { get; set; }
        public int ATCId { get; set; }
        public int TipoPublicoId { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string NrMatricula { get; set; }
        public string Crp { get; set; }
        public string Crm { get; set; }
        public string NomeInstFormacao { get; set; }
        public bool Certificado { get; set; }
        public DateTime DtCertificacao { get; set; }
        public bool DivulgarContato { get; set; }
        public string TipoFormaContato { get; set; }
        public bool IntegraDiretoria { get; set; }
        public bool IntegraConfi { get; set; }
        public string NrTelDivulgacao { get; set; }
        public string ComprovanteAfiliacaoAtc { get; set; }
        public string TipoProfissao { get; set; }
        public string TipoTitulacao { get; set; }
    }
}
