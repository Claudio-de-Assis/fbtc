﻿
namespace Fbtc.Domain.Entities
{
    public class TipoPublico
    {
        public int TipoPublicoId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int Ordem { get; set; }
        public bool Associado { get; set; }
    }
    
    // Classe DAO usada para apresentar os valores no Cadastro de Eventos
    public class TipoPublicoValorDao : TipoPublico
    {
        public int ValorEventoPublicoId { get; set; }
        public int EventoId { get; set; }
        public decimal Valor { get; set; }
        public bool ValorAtivo { get; set; }
    }

    // Classe DAO usada para apresentar os valores no Cadastro de Anuidades
    public class TipoPublicoValorAnuidadeDao : TipoPublico
    {
        public int ValorAnuidadePublicoId { get; set; }
        public decimal Valor { get; set; }
        public int AnuidadeId { get; set; }
    }
}
