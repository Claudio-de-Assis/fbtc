import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { MessageService } from '../../../message.service';
import { Isencao, IsencaoDao } from '../model/isencao';
import { IsencaoRoute } from '../webapi-routes/isencao.route';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class IsencaoService {

    Isencao: Isencao;
    Isencao$: Observable<Isencao>;

    constructor(
        private http: HttpClient,
        private apiRoute: IsencaoRoute,
        private messageService: MessageService) { }

    getAll(objetivoIsencao: string): Observable<Isencao[]> {

        return this.http.get<Isencao[]>(this.apiRoute.getAll(objetivoIsencao))
            .pipe(
                tap(isencoes => this.log('Fetched Isencoes')),
                catchError(this.handleError('getAll()', []))
        );
    }

    getById(id: number): Observable<Isencao> {

        return this.http.get<Isencao>(this.apiRoute.getById(id)).pipe(
            tap(_ => this.log(`fetched isencao id=${id}`)),
            catchError(this.handleError<Isencao>(`getById id=${id}`))
        );
    }

    getIsencaoByFilters(tipoIsencao: string, nomeAssociado: string, anoIsencao: number, identificacao: string, tipoEvento: string): Observable<IsencaoDao[]> {
            return this.http.get<IsencaoDao[]>(this.apiRoute.getFindIsencaoByFilters(tipoIsencao, nomeAssociado, anoIsencao, identificacao, tipoEvento))
            .pipe(
                tap(Isencoes => this.log(`fetched Isencao Filter tipoIsencao=${tipoIsencao},
                nome=${nomeAssociado}, AnoIsencao=${anoIsencao}, identificacao=${identificacao}, tipoEvento=${tipoEvento}`)),
                catchError(this.handleError(`getByFilters tipoIsencao=${tipoIsencao},
                nome=${nomeAssociado}, AnoIsencao=${anoIsencao}, identificacao=${identificacao}, tipoEvento=${tipoEvento}`, []))
        );
    }

    /*
    getByPessoaId(objetivoIsencao: string, id: number): Observable<Isencao[]> {

        return this.http.get<Isencao[]>(this.apiRoute.getByPessoaId(objetivoIsencao, id))
            .pipe(
                tap(isencoes => this.log('Fetched Isencoes')),
                catchError(this.handleError('getByPessoaId()', []))
        );
    }
    */

    setIsencao(objetivoIsencao: string): Observable<Isencao> {

        return this.http.get<Isencao>(this.apiRoute.setIsencao(objetivoIsencao)).pipe(
            tap(_ => this.log(`fetched isencao id=${0}`)),
            catchError(this.handleError<Isencao>(`setIsencao id=${0}`))
        );
    }

    addIsencao (isencao: Isencao): Observable<Isencao> {

        return this.http.post<Isencao>(this.apiRoute.postIsencao(), isencao, httpOptions).pipe(
            tap((_isencao: Isencao) => this.log(`added isencao w/ id=${isencao.isencaoId}`)),
            catchError(this.handleError<Isencao>('addIsencao'))
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
        this.messageService.add('IsencaoService: ' + message);
    }

    /** GET Associado by id. Return `undefined` when id not found */
    getIsencaoNo404<Data>(id: number): Observable<Isencao> {

        return this.http.get<Isencao[]>(this.apiRoute.getById(id))
        .pipe(
            map(isencoes => isencoes[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} isencao id=${id}`);
            }),
            catchError(this.handleError<Isencao>(`getIsencao id=${id}`))
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
