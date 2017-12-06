import {Pipe, PipeTransform } from '@angular/core';
import { TipoPublico } from '../model/tipo-publico';

@Pipe({
    name: 'findNameInTipoPublico'
})

export class FindNameInTipoPublico implements PipeTransform {
    transform(items: TipoPublico[], id: string): string {

         const tipo =  items.find( it => {
          return it.TipoPublicoId.toString().includes(id);
        });
        return tipo.Nome;
    }
}

