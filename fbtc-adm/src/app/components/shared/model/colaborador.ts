import { Pessoa } from "./pessoa";

export class Colaborador extends Pessoa {
    ColaboradorId: number;
    PessoaId: number;
    TipoPerfil: string;
}