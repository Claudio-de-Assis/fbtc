import {Pipe, PipeTransform } from '@angular/core';

import { TipoPublico } from '../model/tipo-publico';

@Pipe({
    name: 'findNameInTipoPublico'
})

export class FindNameInTipoPublicoPipe implements PipeTransform {

    tipo: TipoPublico;

    transform(id: string, items: TipoPublico[]): string {

        this.tipo =  items.find( it => it.tipoPublicoId === +id);
        return this.tipo.nome;
    }
}

