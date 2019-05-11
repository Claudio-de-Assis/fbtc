import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'statusPagamentoAssociado', pure: true})
export class StatusPagamentoAssociadoPipe implements PipeTransform {
    transform(id: number): string {
        return this.getTipoStatus(id);
    }
    private getTipoStatus(id: number): string {
        if (id === 0) {
            return 'Isento';                        /*ISENTO*/
        } else if (id === 1) {
            return 'Pagamento em processamento';    /*AGUARDANDO PAGAMENTO*/
        } else if (id === 2) {
            return 'Pagamento em processamento';    /*EM ANÁLISE*/
        } else if (id === 3) {
            return 'Adimplente';                    /*PAGA*/
        } else if (id === 4) {
            return 'Adimplente';                    /*DISPONÍVEL*/
        } else if (id === 5) {
            return 'Pagamento em processamento';    /*EM DISPUTA*/
        } else if (id === 6) {
            return 'Inadimplente - Verifique no PagSeguro';    /*DEVOLVIDA*/
        } else if (id === 7) {
            return 'Inadimplente - Verifique no PagSeguro';    /*CANCELADA*/
        } else if (id === 8) {
            return 'Inadimplente - Verifique no PagSeguro';    /*DEBITADO*/
        } else if (id === 9) {
            return 'Pagamento em processamento';    /*RETENÇÃO TEMPORÁRIA*/
        } else {
            return 'Inadimplente';
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
