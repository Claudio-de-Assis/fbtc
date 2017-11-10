using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Anuidade
    {
        public virtual int AnuidadeId { get; set; }
        public virtual DateTime DtCadastro { get; set; }
        public virtual int Codigo { get; set; }
    }
}
