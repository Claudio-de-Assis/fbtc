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
