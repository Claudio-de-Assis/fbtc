using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class AnuidadeTipoPublico
    {
        public int AnuidadeTipoPublicoId { get; set; }
        public int AnuidadeId { get; set; }
        public int TipoPublicoId { get; set; }

        public IEnumerable<ValorAnuidade> ValoresAnuidades { get; set; }
    }

    public class AnuidadeTipoPublicoDao : AnuidadeTipoPublico
    {
        public string NomeTipoPublico { get; set; }
        public string Codigo { get; set; }
    }
}
