using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class Log
    {
        public int LogId { get; set; }
        public string MetodoOrigem { get; set; }
        public string TipoInstrucao { get; set; }
        public string Entidade { get; set; }
        public int? EntidadeId { get; set; }
        public string InstrucaoSQL { get; set; }
        public string Resultado { get; set; }
        public DateTime DtCadastro { get; set; }
    }
}
