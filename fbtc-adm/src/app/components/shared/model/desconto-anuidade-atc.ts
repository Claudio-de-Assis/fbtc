export class DescontoAnuidadeAtc {
    descontoAnuidadeAtcId: number;
    associadoId: number;
    colaboradorId: number;
    anuidadeId: number;
    atcId: number;
    observacao: string;
    nomeArquivoComprovante: string;
    dtDesconto: Date;
    dtCadastro: Date;
    ativo: boolean;
}

export class DescontoAnuidadeAtcDao extends DescontoAnuidadeAtc {
    nomePessoa: string;
    nomeColaborador: string;
    nomeAtc: string;
    exercicio: number;
}
