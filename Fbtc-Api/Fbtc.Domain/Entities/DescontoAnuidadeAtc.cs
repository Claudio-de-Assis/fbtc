using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class DescontoAnuidadeAtc
    {
        public int DescontoAnuidadeAtcId { get; set; }
        public int AssociadoId { get; set; }
        public int ColaboradorId { get; set; }
        public int AnuidadeId { get; set; }
        public int AtcId { get; set; }
        public string Observacao { get; set; }
        public string NomeArquivoComprovante { get; set; }
        public DateTime? DtDesconto { get; set; }
        public DateTime DtCadastro { get; set; }
        public bool Ativo { get; set; }
    }

    public class DescontoAnuidadeAtcDao : DescontoAnuidadeAtc
    {
        public string NomePessoa { get; set; }
        public string NomeColaborador { get; set; }
        public string NomeAtc { get; set; }
        public int Exercicio { get; set; }
    }
}
