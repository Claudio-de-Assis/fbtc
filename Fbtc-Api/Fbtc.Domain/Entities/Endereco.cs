
namespace Fbtc.Domain.Entities
{
    public class Endereco: EnderecoCep
    {
        public int EnderecoId { get; set; }
        public int PessoaId { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string TipoEndereco { get; set; }
        public string OrdemEndereco { get; set; }

    }
}
