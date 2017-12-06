export class Recebimento {
    recebimentoId: number;
    AssociadoId: number;
    ValorEventoPublicoId: number;
    ObjetivoPagamento: string;
    DtCadastro: Date;
    DtVencimento: Date;
    DtPagamento: Date;
    DtNotificacao: Date;
    StatusPagto: string;
    FormaPagto: string;
    NrDocCobranca: string;
    ValorPago: number;
    Observacao: string;
    TokenPagamento: string;
    Ativo: boolean;
    AssociadoIsentoId: number;
    ValorAnuidadePublicoId: number;
}
