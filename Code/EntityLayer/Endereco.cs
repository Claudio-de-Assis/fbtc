using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Endereco
    {
        public virtual int EnderecoId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        //Endereco
        public virtual string Logradouro { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string SiglaUf { get; set; }
        public virtual string Cep { get; set; }
    }
}
