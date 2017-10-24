import { Pessoa } from "./pessoa";

export class Associado extends Pessoa {
    AssociadoId: number;
    PessoaId: number;
    AtcId: number;
    TipoPublicoId: number;
    Cpf: string;
    Rg: string;
    NrMatricula: string
    Crp: string;
    Crm: string;
    MomeInsFormacao: string;
    Certificado: boolean;
    DtCertificacao: Date;
    DivulgarContato: boolean;
    TipoFormacao: string;
}