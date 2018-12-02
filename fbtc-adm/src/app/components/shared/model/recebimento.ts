import { Associado } from './associado';

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
    netAmountPS: number;
    dtVencimento: Date;
    statusFBTC: string;
    dtStatusFBTC: Date;
    origemEmissaoTitulo: string;
    dtCadastro: Date;
    ativo: boolean;

//    associado: Associado;
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
