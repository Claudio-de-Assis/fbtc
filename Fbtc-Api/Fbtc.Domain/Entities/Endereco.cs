
namespace Fbtc.Domain.Entities
{
    public class Endereco: EnderecoCep
    {
        public int EnderecoId { get; set; }
        public int PessoaId { get; set; }
        // public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        // public string Bairro { get; set; }
        // public string Cidade { get; set; }
        // public string SiglaUf { get; set; }
        // public string Cep { get; set; }
        public string TipoEndereco { get; set; }

    }
}
