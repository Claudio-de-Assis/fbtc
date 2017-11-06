using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class ATC
    {
        public virtual int AtcId { get; set; }
        public virtual string Nome { get; set; }
        public virtual string UF { get; set; }
        public virtual string NomePres { get; set; }
        public virtual string NomeVPres { get; set; }
        public virtual string NomeSSec { get; set; }
        public virtual string NomePSec { get; set; }
        public virtual string NomePTes { get; set; }
        public virtual string NomeSTes { get; set; }
        public virtual string Site { get; set; }
        public virtual string SiteDiretoria { get; set; }
        public virtual bool Ativo { get; set; }
    }
}
