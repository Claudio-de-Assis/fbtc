using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class AtaIsencao
    {
        public virtual int AtaIsencaoId { get; set; }
        public virtual Anuidade Anuidade { get; set; }
        public virtual Evento Evento { get; set; }
        public virtual string Descricao { get; set; }
        public virtual DateTime DtAta { get; set; }
        public virtual int AnoEvento { get; set; }
        public virtual string TipoIsencao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}
