import { TipoPublicoValorDao } from './tipo-publico';

export class Evento {
    eventoId: number;
    titulo: string;
    descricao: string;
    codigo: string;
    dtInicio: Date;
    dtTermino: Date;
    dtTerminoInscricao: Date;
    tipoEvento: string;
    aceitaIsencaoAta: boolean;
    ativo: boolean;
    nomeFoto: string;
}

export class EventoDao extends Evento {
    tiposPublicosValoresDao: TipoPublicoValorDao[];
}
