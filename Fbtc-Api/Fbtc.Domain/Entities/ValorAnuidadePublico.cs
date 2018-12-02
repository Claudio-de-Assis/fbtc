
namespace Fbtc.Domain.Entities
{
    public class ValorAnuidadePublico
    {
        public int ValorAnuidadePublicoId { get; set; }
        public decimal Valor { get; set; }
        public int AnuidadeId { get; set; }
        public int TipoPublicoId { get; set; }
        public string TipoAnuidade { get; set; }
    }
}
