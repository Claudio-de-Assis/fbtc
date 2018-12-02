using System;

namespace Fbtc.Domain.Entities
{
    public class Associado : Pessoa
    {
        public int AssociadoId { get; set; }
        public int? ATCId { get; set; }
        public int TipoPublicoId { get; set; }
        public string NrMatricula { get; set; }
        public string Crp { get; set; }
        public string Crm { get; set; }
        public string NomeInstFormacao { get; set; }
        public bool Certificado { get; set; }
        public DateTime? DtCertificacao { get; set; }
        public bool DivulgarContato { get; set; }
        public string TipoFormaContato { get; set; }
        public string NrTelDivulgacao { get; set; }
        public string ComprovanteAfiliacaoAtc { get; set; }
        public string TipoProfissao { get; set; }
        public string TipoTitulacao { get; set; }
    }

    public class AssociadoDao : Associado
    {
        public bool MembroDiretoria { get; set; }
        public bool AnuidadeAtcOk { get; set; }
        public bool MembroConfi { get; set; }
    }
}
