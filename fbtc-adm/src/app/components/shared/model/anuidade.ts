import { TipoPublicoValorAnuidadeDao } from './tipo-publico';
export class Anuidade {

    anuidadeId: number;
    codigo: number;
    dtCadastro: Date;
    ativo: boolean;
}

export class AnuidadeDao extends Anuidade {
    tiposPublicosValorsAnuidadesDao: TipoPublicoValorAnuidadeDao[];
}
