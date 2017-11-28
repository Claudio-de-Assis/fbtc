import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { AssociadoRoute } from './../webApi-routes/associado.route';
import { MessageService } from './../../../message.service';
import { Associado } from '../model/associado';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AssociadoService {

    associado: Associado;
    associado$: Observable<Associado>;

    constructor(
        private http: HttpClient,
        private apiRoute: AssociadoRoute,
        private messageService: MessageService) { }

    getAssociados(): Observable<Associado[]> {

        return this.http.get<Associado[]>(this.apiRoute.getAll())
            .pipe(
                tap(associados => this.log('Fetched Associado')),
                catchError(this.handleError('getAssociados()', []))
        );
    }

    getById(id: number): Observable<Associado> {

        const url = `${this.apiRoute.getById()}${id}`;

        return this.http.get<Associado>(url).pipe(
            tap(_ => this.log(`fetched associado id=${id}`)),
            catchError(this.handleError<Associado>(`getAssociado id=${id}`))
          );
    }

    setAssociado(): Observable<Associado> {

        return this.http.get<Associado>(this.apiRoute.setAssociado()).pipe(
            tap(_ => this.log(`fetched associado id=${0}`)),
            catchError(this.handleError<Associado>(`getAssociado id=${0}`))
        );
    }

    //////// Save methods //////////
    /** POST: add a new Associado to the server */
    addAssociado (associado: Associado): Observable<Associado> {
        return this.http.post<Associado>(this.apiRoute.postAssociado(), associado, httpOptions).pipe(
          tap((associado: Associado) => this.log(`added associado w/ id=${associado.associadoId}`)),
          catchError(this.handleError<Associado>('addHero'))
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
        this.messageService.add('AssociadoService: ' + message);
    }

    /** GET Associado by id. Return `undefined` when id not found */
    getAssociadoNo404<Data>(id: number): Observable<Associado> {
        const url = `${this.apiRoute.getById()}${id}`;

        return this.http.get<Associado[]>(url)
        .pipe(
            map(associados => associados[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} associado id=${id}`);
            }),
            catchError(this.handleError<Associado>(`getAssociado id=${id}`))
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
