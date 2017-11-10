using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class ParametroAdimplencia
    {
        public virtual int ParametroAdimplenciaId { get; set; }
        public virtual bool NotificarVencAnuidade { get; set; }
        public virtual bool NotificarVencEventos { get; set; }
        public virtual bool EnviarBoletoEMail { get; set; }
        public virtual int QtdDias { get; set; }
    }
}
