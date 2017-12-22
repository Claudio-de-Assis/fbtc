﻿
namespace Fbtc.Domain.Entities
{
    public class TipoPublico
    {
        public int TipoPublicoId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int Ordem { get; set; }
    }
    
    // Classe DAO usada para apresentar os valores no Cadastro de Eventos
    public class TipoPublicoValorDao : TipoPublico
    {
        public int ValorEventoPublicoId { get; set; }
        public int EventoId { get; set; }
        public decimal Valor { get; set; }
        public bool ValorAtivo { get; set; }
    }
}
