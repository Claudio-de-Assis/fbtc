
namespace Fbtc.Domain.Entities
{
    public class Atc
    {
        public int AtcId { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public string NomePres { get; set; }
        public string NomeVPres { get; set; }
        public string NomePSec { get; set; }
        public string NomeSSec { get; set; }
        public string NomePTes { get; set; }
        public string NomeSTes { get; set; }
        public string Site { get; set; }
        public string SiteDiretoria { get; set; }
        public bool Ativo { get; set; }
        public int Codigo { get; set; }
    }
}
