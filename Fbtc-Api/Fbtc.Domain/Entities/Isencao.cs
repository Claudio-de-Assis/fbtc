﻿using System;

namespace Fbtc.Domain.Entities
{
    public class Isencao
    {
        public int IsencaoId { get; set; }
        public int? AnuidadeId { get; set; }
        public int? EventoId { get; set; }
        public string Descricao { get; set; }
        public DateTime? DtAta { get; set; }
        public int AnoEvento { get; set; }
        public string TipoIsencao { get; set; }
        public bool Ativo { get; set; }
    }

    public class IsencaoDao
    {
        public int IsencaoId { get; set; }
        public string Descricao { get; set; }
        public int AnoIsencao { get; set; }
        public int QuantIsentos { get; set; }
    }
}
