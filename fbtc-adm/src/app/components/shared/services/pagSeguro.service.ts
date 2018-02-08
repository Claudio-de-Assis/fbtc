import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { MessageService } from './../../../message.service';
import { PagSeguroRoute } from '../webapi-routes/pagSeguro.route';
// import { Atc } from '../model/atc';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class PagSeguroService {

    constructor(
        private http: HttpClient,
        private apiRoute: PagSeguroRoute,
        private messageService: MessageService) { }

    //////// Save methods //////////
    /** POST: add a new Associado to the server */
    postSincronizarRecebimentos (): Observable<string> {
        return this.http.post<string>(this.apiRoute.postSincronizar(), httpOptions).pipe(
          tap((_msg: string) => this.log(`Sincronizar com PagSeguro: 30 dias`)),
          catchError(this.handleError<string>('Sincronizar Atc'))
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
        this.messageService.add('PagSeguroService: ' + message);
    }
}
