import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
// import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
// import 'rxjs';

import { UnidadeFederacaoRoute } from '../webapi-routes/unidade-federacao.route';
import { MessageService } from '../../../message.service';
import { UnidadeFederacao } from '../model/unidade-federacao';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class UnidadeFederacaoService {

    unidadeFederacao: UnidadeFederacao;
    unidadeFederacao$: Observable<UnidadeFederacao>;

    constructor(
        private http: HttpClient,
        private apiRoute: UnidadeFederacaoRoute,
        private messageService: MessageService) { }

    getUnidadesFederacao(): Observable<UnidadeFederacao[]> {
        return this.http.get<UnidadeFederacao[]>(this.apiRoute.getAll())
            .pipe(
                tap(unidadesFederacao => this.log('Fetched UnidadesFederacao')),
                catchError(this.handleError('getUnidadesFederacao()', []))
        );
    }

    getUnidadesFederacaoDisponiveis(atcId: number): Observable<UnidadeFederacao[]> {
        return this.http.get<UnidadeFederacao[]>(this.apiRoute.getDisponiveis(atcId))
            .pipe(
                tap(unidadesFederacao => this.log('Fetched UnidadesFederacao')),
                catchError(this.handleError('getUnidadesFederacao()', []))
        );
    }


    getUnidadesFederacaoUtilizadas(): Observable<UnidadeFederacao[]> {
        return this.http.get<UnidadeFederacao[]>(this.apiRoute.getUtilizadas())
            .pipe(
                tap(unidadesFederacao => this.log('Fetched UnidadesFederacao')),
                catchError(this.handleError('getUnidadesFederacao()', []))
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
        this.messageService.add('UnidadeFederacaoService: ' + message);
    }
}
