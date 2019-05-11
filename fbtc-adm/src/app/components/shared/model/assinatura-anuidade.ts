import { Data } from '@angular/router/src/config';
export class AssinaturaAnuidade {
    assinaturaAnuidadeId: number;
    associadoId: number;
    valorAnuidadeId: number;
    anoInicio: number;
    anoTermino: number;
    percentualDesconto: number;
    tipoDesconto: string;
    valor: number;
    dtVencimentoPagamento: Date;
    dtAssinatura: Date;
    codePS: string;
    dtCodePS: Date;
    reference: string;
    emProcessoPagamento: boolean;
    dtInicioProcessamento: Date;
    dtAtualizacao: Date;
    ativo: boolean;

    pagamentoIsento: boolean;
    pagamentoIsentoBD: boolean;
    dtIsencao: Date;
    observacaoIsencao: string;
}

export class AssinaturaAnuidadeDao extends AssinaturaAnuidade {
    nomePessoa: string;
    cpf: string;
    nomeTP: string;
    exercicio: number;
    tipoAnuidade: number;
    valorTipoAnuidade: number;
    anuidadeId: number;
    tipoPublicoId: number;
    anuidadeAtcOk: boolean;
    membroDiretoria: boolean;
    membroConfi: boolean;
    valorAnuidadeIdOriginal: number;
    recebimentoStatusPS: number;
}
