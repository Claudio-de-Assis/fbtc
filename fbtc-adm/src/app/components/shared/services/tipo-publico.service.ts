import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs';

import { TipoPublicoRoute } from '../webApi-routes/tipo-publico.route';
import { MessageService } from '../../../message.service';
import { TipoPublico, TipoPublicoValorDao } from '../model/tipo-publico';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class TipoPublicoService {

    tipoPublico: TipoPublico;
    tipoPublico$: Observable<TipoPublico>;

    constructor(
        private http: HttpClient,
        private apiRoute: TipoPublicoRoute,
        private messageService: MessageService) { }

    getTiposPublicos(isAtivo: string): Observable<TipoPublico[]> {
        return this.http.get<TipoPublico[]>(this.apiRoute.getAll(isAtivo))
            .pipe(
                tap(tiposPublicos => this.log('Fetched TipoPublico')),
                catchError(this.handleError('getTiposPublicos()', []))
        );
    }

    getByTipoAssociacao(tipoAssociacao: boolean): Observable<TipoPublico[]> {
        return this.http.get<TipoPublico[]>(this.apiRoute.getByTipoAssociacao(tipoAssociacao))
            .pipe(
                tap(tiposPublicos => this.log('Fetched TipoPublico')),
                catchError(this.handleError('getByTipoAssociacao()', []))
        );
    }

    getById(id: number): Observable<TipoPublico> {
        return this.http.get<TipoPublico>(this.apiRoute.getById(id)).pipe(
            tap(_ => this.log(`fetched tipoPublico id=${id}`)),
            catchError(this.handleError<TipoPublico>(`getTipoPublico id=${id}`))
        );
    }

    getTiposPublicoByEventoId(id: number): Observable<TipoPublicoValorDao[]> {
        return this.http.get<TipoPublicoValorDao[]>(this.apiRoute.getByEventoId(id))
            .pipe(
                tap(tiposPublicos => this.log('Fetched TipoPublicoValorDao')),
                catchError(this.handleError('getTiposPublicoByEventoId()', []))
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
        this.messageService.add('TipoPublicoService: ' + message);
    }

    /** GET Associado by id. Return `undefined` when id not found */
    getTipoPublicoNo404<Data>(id: number): Observable<TipoPublico> {
        // const url = `${this.apiRoute.getById(id)}${id}`;

        return this.http.get<TipoPublico[]>(this.apiRoute.getById(id))
        .pipe(
            map(tiposPublicos => tiposPublicos[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} tipoPublico id=${id}`);
            }),
            catchError(this.handleError<TipoPublico>(`getTipoPublico id=${id}`))
        );
    }
}
