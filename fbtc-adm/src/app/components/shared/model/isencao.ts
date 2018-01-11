export class Isencao {
    isencaoId: number;
    anuidadeId: number;
    eventoId: number;
    descricao: string;
    dtAta: Date;
    anoEvento: number;
    tipoIsencao: string;
    ativo: boolean;
}

export class IsencaoDao {
    isencaoId: number;
    descricao: string;
    anoIsencao: number;
    quantIsentos: number;
}
