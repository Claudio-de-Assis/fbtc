using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class Perfil
    {
        public int PerfilId { get; set; }
        public string Nome { get; set; }
        public string TipoPerfil { get; set; }
        public bool Ativo { get; set; }
    }
}
