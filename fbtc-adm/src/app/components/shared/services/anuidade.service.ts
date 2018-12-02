import { AnuidadeDao } from './../model/anuidade';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs';

import { MessageService } from '../../../message.service';
import { Anuidade } from '../model/anuidade';

import { AnuidadeRoute } from '../webapi-routes/anuidade.route';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AnuidadeService {

    constructor(
        private http: HttpClient,
        private apiRoute: AnuidadeRoute,
        private messageService: MessageService) { }

    getAnuidades(): Observable<Anuidade[]> {
        return this.http.get<Anuidade[]>(this.apiRoute.getAll())
        .pipe(
            tap(anuidades => this.log('Fetched Anuidade')),
            catchError(this.handleError('getAnuidades()', []))
        );
    }

    getAnuidadesPendentesByPessoaId(pessoaId: number): Observable<Anuidade[]> {
        return this.http.get<Anuidade[]>(this.apiRoute.getAnuidadesPendentesByPessoaId(pessoaId))
        .pipe(
            tap(anuidades => this.log('Fetched Anuidade')),
            catchError(this.handleError('getAnuidadesPendentesByPessoaId()', []))
        );
    }

    getById(id: number): Observable<Anuidade> {
        return this.http.get<Anuidade>(this.apiRoute.getById(id)).pipe(
            tap(_ => this.log(`fetched Anuidade id=${id}`)),
            catchError(this.handleError<Anuidade>(`getAnuidade id=${id}`))
          );
    }



    getAnuidadeDaoById(id: number): Observable<AnuidadeDao> {
        return this.http.get<AnuidadeDao>(this.apiRoute.getAnuidadeDaoById(id)).pipe(
            tap(_ => this.log(`fetched Anuidade id=${id}`)),
            catchError(this.handleError<AnuidadeDao>(`getAnuidade id=${id}`))
          );
    }

    getAnuidadeDaoByIdTipoPublicoId(id: number, tipoPublicoId: number): Observable<AnuidadeDao> {
        return this.http.get<AnuidadeDao>(this.apiRoute.getAnuidadeDaoByIdTipoPublicoId(id, tipoPublicoId)).pipe(
            tap(_ => this.log(`fetched Anuidade id=${id}, tipoPublicoId=${tipoPublicoId}`)),
            catchError(this.handleError<AnuidadeDao>(`getAnuidade id=${id}, tipoPublicoId=${tipoPublicoId}`))
          );
    }

    setAnuidade(): Observable<Anuidade> {
        return this.http.get<Anuidade>(this.apiRoute.setAnuidade()).pipe(
            tap(_ => this.log(`fetched anuidade id=${0}`)),
            catchError(this.handleError<Anuidade>(`getAnuidade id=${0}`))
        );
    }

    getByFilters(exercicio: number, ativo: string): Observable<Anuidade[]> {
        return this.http.get<Anuidade[]>(this.apiRoute.getFindByFilters(exercicio, ativo))
            .pipe(
                tap(anuidades => this.log(`fetched Anuidade Filter Exercicio=${exercicio}, Ativo=${ativo}`)),
                catchError(this.handleError(`getByFilters Exercicio=${exercicio}, Ativo=${ativo}`, []))
        );
    }

    //////// Save methods //////////
    /** POST: add a new Associado to the server */
    addAnuidade (anuidade: Anuidade): Observable<Anuidade> {
        return this.http.post<Anuidade>(this.apiRoute.postAnuidade(), anuidade, httpOptions).pipe(
          tap((_anuidade: Anuidade) => this.log(`added anuidade w/ id=${anuidade.anuidadeId}`)),
          catchError(this.handleError<Anuidade>('addAnuidade'))
        );
    }

    addAnuidadeDao (anuidadeDao: AnuidadeDao): Observable<string> {

        return this.http.post<string>(this.apiRoute.postAnuidadeDao(), anuidadeDao, httpOptions).pipe(
          tap(_ => this.log(`added anuidadeDao w/ id=${anuidadeDao.anuidadeId}`)),
          catchError(this.handleError<string>('addAnuidadeDao'))
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

    /** Log a AnuidadeService message with the MessageService */
    private log(message: string) {
        this.messageService.add('AnuidadeService: ' + message);
    }

    /** GET Anuidade by id. Return `undefined` when id not found */
    getAtcNo404<Data>(id: number): Observable<Anuidade> {

        return this.http.get<Anuidade[]>(this.apiRoute.getById(id))
        .pipe(
            map(anuidade => anuidade[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} atc id=${id}`);
            }),
            catchError(this.handleError<Anuidade>(`getAnuidade id=${id}`))
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
