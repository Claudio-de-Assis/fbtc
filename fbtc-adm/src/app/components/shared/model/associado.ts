import { Pessoa } from './pessoa';

export class Associado extends Pessoa {

    AssociadoId: number;
    PessoaId: number;
    AtcId: number;
    TipoPublicoId: number;
    Cpf: string;
    Rg: string;
    NrMatricula: string;
    Crp: string;
    Crm: string;
    NomeInsFormacao: string;
    Certificado: boolean;
    DtCertificacao: Date;
    DivulgarContato: boolean;
    TipoFormaContato: string;
    IntegraDiretoria: boolean;
    IntegraConfi: boolean;
    NrTelDivulgacao: string;
    ComprovanteAfiliacaoAtc: string;
    Profissao: string;
    Titulacao: string;

}
