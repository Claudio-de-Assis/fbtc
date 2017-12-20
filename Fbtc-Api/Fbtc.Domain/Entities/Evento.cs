using System;

namespace Fbtc.Domain.Entities
{
    public class Evento
    {
        public int EventoId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public DateTime? DtInicio { get; set; }
        public DateTime? DtTermino { get; set; }
        public DateTime? DtTerminoInscricao { get; set; }
        public string TipoEvento { get; set; }
        public bool AceitaIsencaoAta { get; set; }
        public bool Ativo { get; set; }
        public string NomeFoto { get; set; }
    }
}
