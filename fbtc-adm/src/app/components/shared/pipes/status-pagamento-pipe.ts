import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'statusPagamento', pure: true})
export class StatusPagamentoPipe implements PipeTransform {
    transform(id: string): string {
        return this.getTipoStatus(id);
    }
    private getTipoStatus(id: string): string {
        if (id === '0') {
            return 'Inadimplente';
        } else if (id === '1') {
            return 'Adimplente';
        } else if (id === '2') {
            return 'Isento';
        } else {
            return 'Indefinido';
        }
    }
}
