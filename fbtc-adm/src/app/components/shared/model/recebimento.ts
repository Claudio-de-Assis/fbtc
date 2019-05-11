export class Recebimento {
    recebimentoId: number;
    assinaturaAnuidadeId: number;
    assinaturaEventoId: number;
    observacao: string;
    notificationCodePS: string;
    typePS: number;
    statusPS: number;
    lastEventDatePS: Date;
    typePaymentMethodPS: number;
    codePaymentMethodPS: number;
    grossAmountPS: number;
    discountAmountPS: number;
    feeAmountPS: number;
    netAmountPS: number;
    extraAmountPS: number;
    dtVencimento: Date;
    statusFBTC: string;
    dtStatusFBTC: Date;
    origemEmissaoTitulo: string;
    dtCadastro: Date;
    ativo: boolean;
}

export class RecebimentoAssociadoDao extends Recebimento {
    titulo: string;
    anuidade: number;
    nome: string;
    cpf: string;
    nomeTP: string;
    eMail: string;
    nrCelular: string;
    ativoAssociado: boolean;
}
