using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Associado

    {
        public virtual int AssociadoId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual ATC ATC { get; set; }
        public virtual TipoPublico TipoPublicoId { get; set; }
        public virtual string Cpf { get; set; }
        public virtual string Rg { get; set; }
        public virtual string NrMatricula { get; set; }
        public virtual string Crp { get; set; }
        public virtual string Crm { get; set; }
        public virtual string NomeInsFormacao { get; set; }
        public virtual bool Certificado { get; set; }
        public virtual DateTime DtCertificacao { get; set; }
        public virtual bool DivulgarContato { get; set; }
        public virtual string TipoFormaContato { get; set; }
        public virtual bool IntegraDiretoria { get; set; }
        public virtual bool IntegraConfi { get; set; }

    }
}
