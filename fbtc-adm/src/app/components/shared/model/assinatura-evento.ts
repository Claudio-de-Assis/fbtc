export class AssinaturaEvento {
    assinaturaEventoId: number;
    associadoId: number;
    valorEventoPublicoId: number;
    percentualDesconto: number;
    tipoDesconto: string;
    dtAssinatura: Date;
    dtAtualizacao: Date;
    ativo: boolean;
}

export class AssinaturaEventoDao extends AssinaturaEvento {
    nomePessoa: string;
    cPF: string;
    nomeTP: string;
    titulo: string;
    valor: number;
}
