using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class TipoPublico
    {
        public int TipoPublicoId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int Ordem { get; set; }
    }
}
