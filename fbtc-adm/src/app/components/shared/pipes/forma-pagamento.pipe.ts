import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'formaPagamento', pure: true})
export class FormaPagamentoPipe implements PipeTransform {
    transform(id: number): string {
        return this.getForma(id);
    }
    private getForma(id: number): string {
        if (id === 1) {
            return 'Cartão de crédito';
        } else if (id === 2) {
            return 'Boleto';
        } else if (id === 3) {
            return 'Débito online (TEF)';
        } else if (id === 4) {
            return 'Saldo PagSeguro';
        } else if (id === 5) {
            return 'Oi Paggo';
        } else if (id === 7) {
            return 'Depósito em conta';
        } else {
            return 'Não informado';
        }
    }
    /*
    Código de acordo com o PagSeguro:
    Significado
    1 - Cartão de crédito: O comprador pagou pela transação com um cartão de crédito.
    Neste caso, o pagamento é processado imediatamente ou no máximo em algumas horas, dependendo da sua classificação de risco.
    2 - Boleto: O comprador optou por pagar com um boleto bancário. Ele terá que imprimir
    o boleto e pagá-lo na rede bancária. Este tipo de pagamento é confirmado em geral de um a dois
    dias após o pagamento do boleto. O prazo de vencimento do boleto é de 3 dias.
    3 - Débito online (TEF): O comprador optou por pagar com débito online de algum dos bancos
    com os quais o PagSeguro está integrado. O PagSeguro irá abrir uma nova janela com o Internet
    Banking do banco escolhido, onde o comprador irá efetuar o pagamento. Este tipo de pagamento
    é confirmado normalmente em algumas horas.
    4 - Saldo PagSeguro: O comprador possuía saldo suficiente na sua conta PagSeguro e pagou integralmente
    pela transação usando seu saldo.
    5 - Oi Paggo *: o comprador paga a transação através de seu celular Oi. A confirmação do
    pagamento acontece em até duas horas.
    7 - Depósito em conta: o comprador optou por fazer um depósito na conta corrente do PagSeguro.
    Ele precisará ir até uma agência bancária, fazer o depósito, guardar o comprovante e retornar
    ao PagSeguro para informar os dados do pagamento. A transação será confirmada somente após a
    finalização deste processo, que pode levar de 2 a 13 dias úteis.
    * Os tipos marcados não estão disponíveis para utilização.
    */
}


