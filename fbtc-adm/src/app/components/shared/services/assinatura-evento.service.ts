import { AssinaturaEvento, AssinaturaEventoDao } from './../model/assinatura-evento';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
// import 'rxjs';

import { MessageService } from '../../../message.service';
import { AssinaturaEventoRoute } from './../webapi-routes/assinatura-evento.route';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AssinaturaAnuidadeService {

    constructor(
        private http: HttpClient,
        private apiRoute: AssinaturaEventoRoute,
        private messageService: MessageService) { }

    getAssinaturasEventos(): Observable<AssinaturaEvento[]> {
        return this.http.get<AssinaturaEvento[]>(this.apiRoute.getAll())
        .pipe(
            tap(assinaturasEventos => this.log('Fetched AssinaturaEvento')),
            catchError(this.handleError('getAssinaturasEventos()', []))
        );
    }

    getByFilters(eventoId: number, ativo: boolean): Observable<AssinaturaEventoDao[]> {

        return this.http.get<AssinaturaEventoDao[]>(this.apiRoute.getFindByFilters(eventoId, ativo))
            .pipe(
                tap(assinaturaEventoDaos => this.log(`fetched AssinaturaEventoDaos Filter AssinaturaEventoDao=${eventoId},${ativo}`)),
                catchError(this.handleError(`getByFilters AssinaturaEventoDao=${eventoId},${ativo}`, []))
        );
    }

    getById(id: number): Observable<AssinaturaEventoDao> {
        return this.http.get<AssinaturaEventoDao>(this.apiRoute.getById(id)).pipe(
            tap(_ => this.log(`fetched getById id=${id}`)),
            catchError(this.handleError<AssinaturaEventoDao>(`getById id=${id}`))
          );
    }

    //////// Save methods //////////
    /** POST: add a new AssinaturaAnuidadeDao to the server */
    addAssinaturaEventoDao (assinaturaEventoDao: AssinaturaEventoDao): Observable<string> {
        return this.http.post<string>(this.apiRoute.postAssinaturaEventoDao(), assinaturaEventoDao, httpOptions).pipe(
          tap(_ => this.log(`added AssinaturaEvento w/ id=${assinaturaEventoDao.assinaturaEventoId}`)),
          catchError(this.handleError<string>('addAssinaturaEventoDao'))
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
        this.messageService.add('AtcService: ' + message);
    }

    /** GET Associado by id. Return `undefined` when id not found */
    getAtcNo404<Data>(id: number): Observable<AssinaturaEventoDao> {

        return this.http.get<AssinaturaEventoDao[]>(this.apiRoute.getById(id))
        .pipe(
            map(atc => atc[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} atc id=${id}`);
            }),
            catchError(this.handleError<AssinaturaEventoDao>(`getAtc id=${id}`))
        );
    }
}
