export class Relatorios {}


export class RptTotalAssociadosDAO {
    nomeTipoAssociado: string;
    ordem: number;
    qtd: number;
    qtdTotal: number;
}

export class RptRecebimentoStatusDAO {
    statusPagamento: string;
    qtd: number;
    valorPorStatus: number;
    valorTotal: number;
}

export class RptAssociadoFaixaDAO {
    faixaAte30: number;
    faixa31a40: number;
    faixa41a50: number;
    faixa51a60: number;
    faixaApos60: number;
}

export class RptAssociadosEstadosDAO {
    nomeUF: string;
    profissionalAssociado: string;
    profissionalNaoAssociado: string;
    estudantePosAssociado: string;
    estudantePosNaoAssociado: string;
    estudanteAssociado: string;
    estudanteNaoAssociado: string;
}

export class RptReceitaAnualDAO {
    ano: number;
    nomeObjetivoPagamento: string;
    valorPrevisto: number;
    valorRealizado: number;
    qtdIsentos: number;
}
