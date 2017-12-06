import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { CepCorreiosRoute } from './../webApi-routes/cep-correios.route';
import { MessageService } from './../../../message.service';
// import { CepCorreios } from './../model/cep-correios';
import { Endereco } from '../model/endereco';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class CepCorreiosService {

   // cepCorreios$: Observable<CepCorreios>;
    endereco: Endereco;

   /* cepCorreios: CepCorreios = {
        bairro: '',
        cidade: '',
        logradouro: '',
        estado_info: {area_km2: '', codigo_ibge: '', nome: '' },
        cep: '',
        cidade_info: {area_km2: '', codigo_ibge: ''},
        estado: '' };*/

    constructor(
        private http: HttpClient,
        private apiRoute: CepCorreiosRoute,
        private messageService: MessageService) { }

    getByCep(cep: string): Observable<Endereco> {
        return this.http.get<Endereco>(this.apiRoute.getByCep(cep)).pipe(
            tap(_ => this.log(`fetched CepCorreios CEP=${cep}`)),
            catchError(this.handleError<Endereco>(`getByCep id=${cep}`))
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
        this.messageService.add('cepCorreiosService: ' + message);
    }

    /** GET Associado by id. Return `undefined` when id not found */
    getAtcNo404<Data>(cep: string): Observable<Endereco> {

        return this.http.get<Endereco[]>(this.apiRoute.getByCep(cep))
        .pipe(
            map(cepCorreios => cepCorreios[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} CepCorreios cep=${cep}`);
            }),
            catchError(this.handleError<Endereco>(`getByCep cep=${cep}`))
        );
    }
}
