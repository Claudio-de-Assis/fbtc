using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Evento
    {
        public virtual int EventoId { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Codigo { get; set; }
        public virtual DateTime DtInicio { get; set; }
        public virtual DateTime DtTermino { get; set; }
        public virtual string TipoEvento { get; set; }
        public virtual bool AceitaIsencaoAta { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual string NomeFoto { get; set; }
    }
}
