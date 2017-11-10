import { Endereco } from "./endereco";

export class Pessoa {
    PessoaId: number;
    Nome: string;
    EMail: string;
    NomeFoto: string;
    Sexo: string;
    DtNascimento: Date;
    NrCelular: string;
    PasswordHash: string;
    DtCadastro: Date;
    Ativo: Boolean;

    //Enderecos: Endereco[];
}