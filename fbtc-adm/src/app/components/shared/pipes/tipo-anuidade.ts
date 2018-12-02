import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'tipoAnuidade', pure: true})
export class TipoAnuidadePipe implements PipeTransform {
    transform(id: number): string {
        return this.getTipoAnuidade(id);
    }
    private getTipoAnuidade(id: number): string {
        if (id === 1 ) {
            return 'Uma Anuidade';
        } else if  (id === 2 ) {
            return 'Duas Anuidades';
        } else if (id === 3) {
            return 'Três Anuidades';
        } else {
            return '' + id;
        }
    }
}
