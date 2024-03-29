﻿
namespace Fbtc.Domain.Entities
{
    public class EnderecoCep
    {
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public EstadoInfo Estado_Info { get; set; }
        public string Cep { get; set; }
        public CidadeInfo Cidade_Info { get; set; }
        public string Estado { get; set; }
    }

    public class EstadoInfo
    {
        public string Area_Km2 { get; set; }
        public string Codigo_Ibge { get; set; }
        public string Nome { get; set; }
    }

    public class CidadeInfo
    {
        public string Area_Km2 { get; set; }
        public string Codigo_Ibge { get; set; }
    }

    
    // *********************************************
    // Classes DAO realativas ao objeto EnderecoCEP:
    // *********************************************
    
    public class EstadoEnderecoCepDao
    {
        public string Estado { get; set; }
    }
    
    
    public class CidadeEnderecoCepDao
    {
        public string Cidade { get; set; }
    }
}
