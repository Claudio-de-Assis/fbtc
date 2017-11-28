using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class Colaborador : Pessoa
    {
        public int ColaboradorId { get; set; }
        public string TipoPerfil { get; set; }
    }
}
