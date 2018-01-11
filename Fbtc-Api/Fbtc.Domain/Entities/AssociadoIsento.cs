using System;

namespace Fbtc.Domain.Entities
{
    public class AssociadoIsento
    {
        public int AssociadoIsentoId { get; set; }
        public int AssociadoId { get; set; }
        public int IsencaoId { get; set; }
        public DateTime DtCadastro { get; set; }
    }

    public class AssociadoIsentoDao
    {
        public int AssociadoIsentoId { get; set; }
        public int IsencaoId { get; set; }
        public int AssociadoId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Crp { get; set; }
        public int AtcId { get; set; }
        public int TipoPublicoId { get; set; }
        public string TipoIsencao { get; set; }
        public bool Ativo { get; set; }
    }
}
