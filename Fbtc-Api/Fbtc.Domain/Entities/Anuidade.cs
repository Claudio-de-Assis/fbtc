using System;
using System.Collections.Generic;

namespace Fbtc.Domain.Entities
{
    public class Anuidade
    {
        public int AnuidadeId { get; set; }
        public int Exercicio { get; set; }
        public DateTime DtVencimento { get; set; }
        public DateTime DtInicioVigencia { get; set; }
        public DateTime DtTerminoVigencia { get; set; }
        public bool CobrancaLiberada { get; set; }
        public DateTime? DtCobrancaLiberada { get; set; }
        public DateTime DtCadastro { get; set; }
        public bool Ativo { get; set; }
    }
   
    public class AnuidadeDao : Anuidade
    {
        public IEnumerable<AnuidadeTipoPublicoDao> AnuidadesTiposPublicosDao { get; set; }
    }
}
