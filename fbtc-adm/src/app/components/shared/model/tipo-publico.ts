export class TipoPublico {
    tipoPublicoId: number;
    nome: string;
    descricaoValor: string;
    ativo: boolean;
    associado: boolean;
    codigo: string;
}

export class TipoPublicoValorDao extends TipoPublico {
    valorEventoPublicoId: number;
    eventoId: number;
    valor: number;
    valorAtivo: boolean;
}

export class TipoPublicoValorAnuidadeDao extends TipoPublico {
    valorAnuidadePublicoId: number;
    tipoPublicoId: number;
    anuidadeId: number;
    tipoAnuidade: string;
    valor: number;
}
