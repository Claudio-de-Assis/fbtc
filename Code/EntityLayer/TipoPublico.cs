using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class TipoPublico
    {
        public virtual int TipoPessoaId { get; set; }
        public virtual string Nome { get; set; }
        public virtual bool Ativo { get; set; }
    }
}
