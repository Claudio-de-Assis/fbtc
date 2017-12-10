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
        this.messageService.add('RecebimentoAnuidadeService: ' + message);
    }

    /** GET Associado by id. Return `undefined` when id not found */
    getRecebimentoAnuidadeNo404<Data>(id: number): Observable<Recebimento> {

        return this.http.get<Recebimento[]>(this.apiRoute.getById(id))
        .pipe(
            map(recebimentos => recebimentos[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} recebimento id=${id}`);
            }),
            catchError(this.handleError<Recebimento>(`getRecebimentoAnuidade id=${id}`))
        );
    }

    /* GET heroes whose name contains search term */
    /*
    searchAssociados(term: string): Observable<Associado[]> {
        if (!term.trim()) {
        // if not search term, return empty hero array.
            return of([]);
        }
        return this.http.get<Associado[]>(`api/Associado/?name=${term}`).pipe(
        tap(_ => this.log(`found Associado matching "${term}"`)),
        catchError(this.handleError<Associado[]>('searchAssociados', []))
        );
    }
    */

    /** DELETE: delete the Associado from the server */
    /*
    deleteAssociado (associado: Associado | number): Observable<Associado> {
        const id = typeof associado === 'number' ? associado : associado.associadoId;
        const url = `${this.associadoUrl}/${id}`;

        return this.http.delete<Associado>(url, httpOptions).pipe(
            tap(_ => this.log(`deleted Associado id=${id}`)),
            catchError(this.handleError<Associado>('deleteAssociado'))
        );
    }
    */
}
