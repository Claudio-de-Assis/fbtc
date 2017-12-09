import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'objetivoPagamento', pure: true})
export class ObjetivoPagamentoPipe implements PipeTransform {
    transform(id: string): string {
        return this.getTipoObjetivo(id);
    }
    private getTipoObjetivo(id: string): string {
        if (id === '1') {
            return 'Evento';
        } else if (id === '2') {
            return 'Anuidade';
        } else {
            return 'Indefinido';
        }
    }
}
