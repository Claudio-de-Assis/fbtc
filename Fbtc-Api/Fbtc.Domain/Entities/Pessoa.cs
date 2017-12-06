﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fbtc.Domain.Entities
{
    public class Pessoa
    {
        public int PessoaId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string EMail { get; set; }
        public string NomeFoto { get; set; }
        public string Sexo { get; set; }
        public DateTime? DtNascimento { get; set; }
        public string NrCelular { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? DtCadastro { get; set; }
        public bool Ativo { get; set; }

        public Endereco EnderecoPessoa { get; set; }
    }
}
