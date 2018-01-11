using System;

namespace Fbtc.Domain.Entities
{
    public class Anuidade
    {
        public int AnuidadeId { get; set; }
        public int Codigo { get; set; }
        public DateTime DtCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
