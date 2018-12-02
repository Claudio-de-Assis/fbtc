import { ValorAnuidade } from './valor-anuidade';
// import { TipoPublicoValorAnuidadeDao } from './tipo-publico';
export class AnuidadeTipoPublico {

    anuidadeTipoPublicoId: number;
    anuidadeId: number;
    tipoPublicoId: number;
    valoresAnuidades: ValorAnuidade[];
}

export class AnuidadeTipoPublicoDao extends AnuidadeTipoPublico {
    nomeTipoPublico: string;
    codigo: string;
}
