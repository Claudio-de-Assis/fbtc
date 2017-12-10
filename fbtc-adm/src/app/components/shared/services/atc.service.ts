import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { AtcRoute } from './../webApi-routes/atc.route';
import { MessageService } from './../../../message.service';
import { Atc } from '../model/atc';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AtcService {

    atc: Atc;
    atc$: Observable<Atc>;

    constructor(
        private http: HttpClient,
        private apiRoute: AtcRoute,
        private messageService: MessageService) { }

    getAtcs(): Observable<Atc[]> {
        return this.http.get<Atc[]>(this.apiRoute.getAll())
        .pipe(
            tap(atcs => this.log('Fetched Atc')),
            catchError(this.handleError('getAtcs()', []))
        );
    }

    getById(id: number): Observable<Atc> {
        return this.http.get<Atc>(this.apiRoute.getById(id)).pipe(
            tap(_ => this.log(`fetched atc id=${id}`)),
            catchError(this.handleError<Atc>(`getAtc id=${id}`))
          );
    }

    setAtc(): Observable<Atc> {
        return this.http.get<Atc>(this.apiRoute.setAtc()).pipe(
            tap(_ => this.log(`fetched atc id=${0}`)),
            catchError(this.handleError<Atc>(`getAtc id=${0}`))
        );
    }

    //////// Save methods //////////
    /** POST: add a new Associado to the server */
    addAtc (atc: Atc): Observable<Atc> {
        return this.http.post<Atc>(this.apiRoute.postAtc(), atc, httpOptions).pipe(
          tap((_atc: Atc) => this.log(`added atc w/ id=${atc.atcId}`)),
          catchError(this.handleError<Atc>('addAtc'))
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
    getAtcNo404<Data>(id: number): Observable<Atc> {

        return this.http.get<Atc[]>(this.apiRoute.getById(id))
        .pipe(
            map(atc => atc[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} atc id=${id}`);
            }),
            catchError(this.handleError<Atc>(`getAtc id=${id}`))
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

    /** PUT: update the Associado on the server */
    /*
    updateAssociado (associado: Associado): Observable<any> {
        return this.http.put(this.webRoute.postAssociado(), associado, httpOptions).pipe(
        tap(_ => this.log(`updated associado id=${associado.associadoId}`)),
        catchError(this.handleError<any>('updateAssociado'))
      );
    }
    */
}
