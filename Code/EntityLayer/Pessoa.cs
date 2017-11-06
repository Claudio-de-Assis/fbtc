using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Pessoa
    {
        public Pessoa()
        {
            Enderecos = new List<Endereco>();
        }

        public virtual int PessoaId { get; set; }
        public virtual string Nome { get; set; }
        public virtual string EMail { get; set; }
        public virtual string NomeFoto { get; set; }
        public virtual string Sexo { get; set; }
        public virtual DateTime DtNascimento { get; set; }
        public virtual string NrCelular { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual DateTime DtCadastro { get; set; }
        public virtual bool Ativo { get; set; }

        //Lista Endereços
        List<Endereco> Enderecos { get; set; }

    }
}
