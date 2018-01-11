export class TipoPublico {
    tipoPublicoId: number;
    nome: string;
    descricaoValor: string;
    ativo: boolean;
    associado: boolean;
}

export class TipoPublicoValorDao extends TipoPublico {
    valorEventoPublicoId: number;
    eventoId: number;
    valor: number;
    valorAtivo: boolean;
}
