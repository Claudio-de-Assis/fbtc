export class AssociadoIsentoDao {
    associadoIsentoId: number;
    isencaoId: number;
    associadoId: number;
    nome: string;
    cpf: string;
    crp: string;
    atcId: number;
    tipoPublicoId: number;
    tipoIsencao: string;
    ativo: boolean;
}

export class AssociadoIsento {
    AssociadoIsentoId: number;
    DtCadastro: Date;
    AtaIsencaoId: number;
    AssociadoId: number;
}
