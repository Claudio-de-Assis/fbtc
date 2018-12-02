using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class TipoAnuidade
    {
        public int TipoAnuidadeId { get; set; }
        public string Descricao { get; set; }
        public int QtdAnuidade { get; set; }
    }
}
