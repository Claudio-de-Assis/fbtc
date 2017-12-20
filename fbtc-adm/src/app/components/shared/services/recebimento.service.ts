import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { MessageService } from '../../../message.service';
import { RecebimentoRoute } from './../webapi-routes/recebimento.route';
import { Recebimento } from './../model/recebimento';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class RecebimentoService {

    recebimento: Recebimento;
    recebimento$: Observable<Recebimento>;

    constructor(
        private http: HttpClient,
        private apiRoute: RecebimentoRoute,
        private messageService: MessageService) { }

    getAll(objetivoPagamento: string): Observable<Recebimento[]> {

        return this.http.get<Recebimento[]>(this.apiRoute.getAll(objetivoPagamento))
            .pipe(
                tap(recebimentos => this.log('Fetched Recebimentos')),
                catchError(this.handleError('getAll()', []))
        );
    }

    getById(id: number): Observable<Recebimento> {

        return this.http.get<Recebimento>(this.apiRoute.getById(id)).pipe(
            tap(_ => this.log(`fetched recebimento id=${id}`)),
            catchError(this.handleError<Recebimento>(`getById id=${id}`))
        );
    }

    getByPessoaId(objetivoPagamento: string, id: number): Observable<Recebimento[]> {

        return this.http.get<Recebimento[]>(this.apiRoute.getByPessoaId(objetivoPagamento, id))
            .pipe(
                tap(recebimentos => this.log('Fetched Recebimentos')),
                catchError(this.handleError('getByPessoaId()', []))
        );
    }

    setRecebimento(objetivoPagamento: string): Observable<Recebimento> {

        return this.http.get<Recebimento>(this.apiRoute.setRecebimento(objetivoPagamento)).pipe(
            tap(_ => this.log(`fetched recebimento id=${0}`)),
            catchError(this.handleError<Recebimento>(`setRecebimento id=${0}`))
        );
    }

    addRecebimento (recebimento: Recebimento): Observable<Recebimento> {

        return this.http.post<Recebimento>(this.apiRoute.postRecebimento(), recebimento, httpOptions).pipe(
            tap((_recebimento: Recebimento) => this.log(`added recebimento w/ id=${recebimento.recebimentoId}`)),
            catchError(this.handleError<Recebimento>('addRecebimento'))
        );
    }

    getByFilters(objetivoPagamento: string, nome: string, cpf: string, crp: string, crm: string, statusPagamento: string,
        ano: number, mes: number, ativo: string, tipoEvento: string, tipoPublicoId: number): Observable<Recebimento[]> {
            return this.http.get<Recebimento[]>(this.apiRoute
                .getFindByFilters(objetivoPagamento, nome, cpf, crp, crm, statusPagamento, ano, mes, ativo, tipoEvento,
                    tipoPublicoId))
                .pipe(tap(recebimentos => this.log(`fetched recebimento Filter objetivoPagamento=${objetivoPagamento},
                    nome=${nome}, cpf=${cpf}, crp=${crp},crp=${crm}, statusPagamento=${statusPagamento},
                    ano=${ano},mes=${mes},ativo=${ativo}, tipoEvento=${tipoEvento}, tipoPublicoId=${tipoPublicoId}`)),
                catchError(this.handleError(`getByFilters objetivoPagamento=${objetivoPagamento},
                    nome=${nome}, cpf=${cpf}, crp=${crp},crp=${crm}, status=${status},
                    ano=${ano},mes=${mes},ativo=${ativo}, tipoEvento=${tipoEvento}, tipoPublicoId=${tipoPublicoId}`, []))
        );
    }



    /**
    * Handle Http operation that failed.
    * Let the app continue.
    * @param operation - name of the operation that failed
    * @param result - optional value to return as the observable result
    */
    private handleError<T> (operation = 'operation', result?: T) {

        return (error: any): Observable<T> => {

        // TODO: send the error to remote logging infrastructure
        console.log(error); // log to console instead

        // TODO: better job of transforming error for user consumption
        this.log(`${operation} failed: ${error.message}`);

        // Let the app keep running by returning an empty result.
        // return of (result as T);
        return error (result as T);
        };
    }

    /** Log a AssociadoService message with the MessageService */
    private log(message: string) {
        this.messageService.add('RecebimentoService: ' + message);
    }
}
