import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'statusPagamento', pure: true})
export class StatusPagamentoPipe implements PipeTransform {
    transform(id: number): string {
        return this.getTipoStatus(id);
    }
    private getTipoStatus(id: number): string {
        if (id === 0) {
            return 'Isento';
        } else if (id === 1) {
            return 'Aguardando pagamento';
        } else if (id === 2) {
            return 'Em análise';
        } else if (id === 3) {
            return 'Paga';
        } else if (id === 4) {
            return 'Disponível';
        } else if (id === 5) {
            return 'Em disputa';
        } else if (id === 6) {
            return 'Devolvida';
        } else if (id === 7) {
            return 'Cancelada';
        } else if (id === 8) {
            return 'Debitado';
        } else if (id === 9) {
            return 'Retenção temporária';
        } else {
            return 'Não localizado';
        }
    }
    /*
    0 - Isento
    Códigos de acordo com o PagSeguro:
    Código - Significado
    1 - Aguardando pagamento: o comprador iniciou a transação, mas até o momento o
        PagSeguro não recebeu nenhuma informação sobre o pagamento.
    2 - Em análise: o comprador optou por pagar com um cartão de crédito e o PagSeguro
        está analisando o risco da transação.
    3 - Paga: a transação foi paga pelo comprador e o PagSeguro já recebeu uma confirmação
        da instituição financeira responsável pelo processamento.
    4 - Disponível: a transação foi paga e chegou ao final de seu prazo de liberação sem ter
        sido retornada e sem que haja nenhuma disputa aberta.
    5 - Em disputa: o comprador, dentro do prazo de liberação da transação, abriu uma disputa.
    6 - Devolvida: o valor da transação foi devolvido para o comprador.
    7 - Cancelada: a transação foi cancelada sem ter sido finalizada.
    8 - Debitado: o valor da transação foi devolvido para o comprador.
    9 - Retenção temporária: o comprador contestou o pagamento junto à operadora do cartão de crédito
        ou abriu uma demanda judicial ou administrativa (Procon).
   */
}
