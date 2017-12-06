import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'tipoEvento', pure: true})
export class TipoEventoPipe implements PipeTransform {
    transform(id: string): string {
        return this.getTipoEvento(id);
    }
    private getTipoEvento(id: string): string {
        if (id === '1') {
            return 'Workshop internacional e Congresso';
        } else if (id === '2') {
            return 'Workshop Internacional';
        } else if (id === '3') {
            return 'Workshop Nacional';
        } else if (id === '4') {
            return 'Congresso Brasileiro';
        } else if (id === '5') {
            return 'Certificação';
        } else {
            return '';
        }
    }
}
