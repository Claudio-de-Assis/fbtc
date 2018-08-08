import { Endereco } from './endereco';

export class Pessoa {
    pessoaId: number;
    perfilId: number;
    nome: string;
    cpf: string;
    rg: string;
    eMail: string;
    nomeFoto: string;
    sexo: string;
    dtNascimento: Date;
    nrCelular: string;
    passwordHash: string;
    dtCadastro: Date;
    ativo: Boolean;

    enderecosPessoa: Endereco[];
}
