import { Associado } from './associado';

export class Recebimento {
    recebimentoId: number;
    associadoId: number;
    associadoIsentoId: number;
    valorAnuidadePublicoId: number;
    valorEventoPublicoId: number;
    objetivoPagamento: string;
    dtNotificacao: Date;
    observacao: string;
    codePS: string;
    referencePS: string;
    typePS: number;
    statusPS: number;
    lastEventDatePS: Date;
    typePaymentMethodPS: number;
    codePaymentMethodPS: number;
    netAmountPS: number;
    dtCadastro: Date;
    ativo: boolean;
    dtVencimento: Date;

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
    statusPS: number;
    lastEventDatePS: Date;
    ativoRec: boolean;
    isencaoIdId: number;
    dtVencimento: Date;

}
