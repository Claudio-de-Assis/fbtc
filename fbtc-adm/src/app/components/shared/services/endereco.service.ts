import { EnderecoRoute } from './../webapi-routes/endereco.route';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { MessageService } from './../../../message.service';
import { EstadoEnderecoCepDao, CidadeEnderecoCepDao, EnderecoCep } from './../model/endereco-cep';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class EnderecoService {

    constructor(
        private http: HttpClient,
        private apiRoute: EnderecoRoute,
        private messageService: MessageService) { }

    getAllEstados(): Observable<EstadoEnderecoCepDao[]> {
        return this.http.get<EstadoEnderecoCepDao[]>(this.apiRoute.getAllEstados())
        .pipe(
            tap(estados => this.log('Fetched Endereço')),
            catchError(this.handleError('getAllEstados()', []))
        );
    }

    getGetCidadesByEstado(estado: string): Observable<CidadeEnderecoCepDao[]> {
        return this.http.get<CidadeEnderecoCepDao[]>(this.apiRoute.getGetCidadesByEstado(estado))
        .pipe(
            tap(estados => this.log('Fetched Endereço')),
            catchError(this.handleError('getGetCidadesByEstado()', []))
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
        this.messageService.add('EnderecoService: ' + message);
    }
}
