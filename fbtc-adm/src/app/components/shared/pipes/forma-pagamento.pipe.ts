import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'formaPagamento', pure: true})
export class FormaPagamentoPipe implements PipeTransform {
    transform(id: string): string {
        return this.getForma(id);
    }
    private getForma(id: string): string {
        if (id === '1') {
            return 'PagSeguro';
        } else if (id === '2') {
            return 'Boleto';
        } else if (id === '3') {
            return 'Cartão de Crédito';
        } else {
            return 'Indefinido';
        }
    }
}


