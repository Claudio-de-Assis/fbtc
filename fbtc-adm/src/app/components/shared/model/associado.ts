import { Pessoa } from './pessoa';

export class Associado extends Pessoa {
    associadoId: number;
    atcId: number;
    tipoPublicoId: number;
    nrMatricula: string;
    crp: string;
    crm: string;
    nomeInstFormacao: string;
    certificado: boolean;
    dtCertificacao: Date;
    divulgarContato: boolean;
    tipoFormaContato: string;
    nrTelDivulgacao: string;
    comprovanteAfiliacaoAtc: string;
    tipoProfissao: string;
    tipoTitulacao: string;
}

export class AssociadoDao extends Associado {
    membroDiretoria: boolean;
    anuidadeAtcOk: boolean;
    membroConfi: boolean;
}
