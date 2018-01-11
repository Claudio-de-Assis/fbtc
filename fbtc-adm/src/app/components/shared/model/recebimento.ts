import { Associado } from './associado';

export class Recebimento {
    recebimentoId: number;
    associadoId: number;
    associadoIsentoId: number;
    valorAnuidadePublicoId: number;
    valorEventoPublicoId: number;
    objetivoPagamento: string;
    dtVencimento: Date;
    dtPagamento: Date;
    dtNotificacao: Date;
    statusPagamento: string;
    formaPagamento: string;
    nrDocCobranca: string;
    valorPago: number;
    observacao: string;
    tokenPagamento: string;
    dtCadastro: Date;
    ativo: boolean;

    associado: Associado;
}

export class RecebimentoAssociadoDao {

    associadoId: number;
    titulo: string;
    anuidade: number;
    nome: string;
    cpf: string;
    nomeTP: string;
    recebimentoId: number;
    statusPagamento: string;
    dtVencimento: Date;
    dtPagamento: Date;
    ativoRec: boolean;
    isencaoIdId: number;
}

