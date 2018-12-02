import { AnuidadeTipoPublicoDao } from './anuidade-tipo-publico';
export class Anuidade {

    anuidadeId: number;
    exercicio: number;
    dtVencimento: Date;
    dtInicioVigencia: Date;
    dtTerminoVigencia: Date;
    cobrancaLiberada: boolean;
    dtCobrancaLiberada: Date;
    dtCadastro: Date;
    ativo: boolean;
}

export class AnuidadeDao extends Anuidade {
    anuidadesTiposPublicosDao: AnuidadeTipoPublicoDao[];
}
