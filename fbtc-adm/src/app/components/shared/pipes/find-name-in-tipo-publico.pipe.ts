import {Pipe, PipeTransform } from '@angular/core';

import { TipoPublico } from '../model/tipo-publico';
import { isDefined } from '@angular/compiler/src/util';

@Pipe({
    name: 'findNameInTipoPublico'
})

export class FindNameInTipoPublicoPipe implements PipeTransform {

    tipo: TipoPublico;

    transform(id: string, items: TipoPublico[]): string {

        if (items !== undefined) {
            if (items.length > 0) {
                this.tipo =  items.find( it => it.tipoPublicoId === +id);
                if (this.tipo !== undefined) {
                    return this.tipo.nome;
                } else {
                    return '';
                }
            }
        }
    }
}

