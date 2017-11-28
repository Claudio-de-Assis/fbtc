import { Pessoa } from './pessoa';

export class Associado extends Pessoa {
    associadoId: number;
    atcId: number;
    tipoPublicoId: number;
    cpf: string;
    rg: string;
    nrMatricula: string;
    crp: string;
    crm: string;
    nomeInstFormacao: string;
    certificado: boolean;
    dtCertificacao: Date;
    divulgarContato: boolean;
    tipoFormaContato: string;
    integraDiretoria: boolean;
    integraConfi: boolean;
    nrTelDivulgacao: string;
    comprovanteAfiliacaoAtc: string;
    tipoProfissao: string;
    tipoTitulacao: string;
}
