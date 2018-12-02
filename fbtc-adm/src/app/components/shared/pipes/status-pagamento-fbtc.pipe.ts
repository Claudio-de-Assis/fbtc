import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'statusPagamentoFBTC', pure: true})
export class StatusPagamentoFBTCPipe implements PipeTransform {
    transform(id: string): string {
        return this.getTipoStatus(id);
    }
    private getTipoStatus(id: string): string {
        if (id === '1') {
            return 'Vigente';
        } else if (id === '2') {
            return 'Cancelado';
        } else if (id === '3') {
            return 'Baixa Manual';
        } else {
            return 'Não localizado';
        }
    }
    /*
    Informa se há algum status FBTC

    Valores aceitos:
    Vigente: 1 (Nesse caso, o status do título é o que é reportado pelo PagSeguro)
    Cancelado: 2
    Baixa Manual 3
   */
}
