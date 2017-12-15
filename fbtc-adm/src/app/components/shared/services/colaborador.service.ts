import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { MessageService } from './../../../message.service';
import { Colaborador } from '../model/colaborador';
import { ColaboradorRoute } from '../webApi-routes/colaborador.route';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class ColaboradorService {

    colaborador: Colaborador;
    colaborador$: Observable<Colaborador>;

    constructor(
        private http: HttpClient,
        private apiRoute: ColaboradorRoute,
        private messageService: MessageService) { }

    getColaboradores(): Observable<Colaborador[]> {
        return this.http.get<Colaborador[]>(this.apiRoute.getAll())
            .pipe(
                tap(colaboradores => this.log('Fetched Colaborador')),
                catchError(this.handleError('getColaborador()', []))
        );
    }

    getById(id: number): Observable<Colaborador> {
        return this.http.get<Colaborador>(this.apiRoute.getById(id)).pipe(
            tap(_ => this.log(`fetched Colaborador id=${id}`)),
            catchError(this.handleError<Colaborador>(`getColaborador id=${id}`))
        );
    }

    getByFilters(nome: string, tipoPerfil: string, ativo: string): Observable<Colaborador[]> {
        return this.http.get<Colaborador[]>(this.apiRoute.getFindByFilters(nome, tipoPerfil, ativo))
            .pipe(
                tap(colaboradores => this.log(`fetched Colaborador Filter nome=${nome}, tipoPerfil=${tipoPerfil}, Ativo=${ativo}`)),
                catchError(this.handleError(`getByFilters nome=${nome}, tipoPerfil=${tipoPerfil}, Ativo=${ativo}`, []))
        );
    }

    setColaborador(): Observable<Colaborador> {
        return this.http.get<Colaborador>(this.apiRoute.setColaborador()).pipe(
            tap(_ => this.log(`fetched Colaborador id=${0}`)),
            catchError(this.handleError<Colaborador>(`getColaborador id=${0}`))
        );
    }

    //////// Save methods //////////
    /** POST: add a new Colaborador to the server */
    addColaborador (colaborador: Colaborador): Observable<Colaborador> {
        return this.http.post<Colaborador>(this.apiRoute.postColaborador(), colaborador, httpOptions).pipe(
            tap((_colaborador: Colaborador) => this.log(`added Colaborador w/ id=${colaborador.colaboradorId}`)),
            catchError(this.handleError<Colaborador>('addColaborador'))
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
        this.messageService.add('ColaboradorService: ' + message);
    }

    /** GET Colaborador by id. Return `undefined` when id not found */
    getAssociadoNo404<Data>(id: number): Observable<Colaborador> {

        return this.http.get<Colaborador[]>(this.apiRoute.getById(id))
        .pipe(
            map(colaboradores => colaboradores[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} Colaborador id=${id}`);
            }),
            catchError(this.handleError<Colaborador>(`getColaborador id=${id}`))
        );
    }

    /* GET heroes whose name contains search term */
    /*
    searchAssociados(term: string): Observable<Colaborador[]> {
        if (!term.trim()) {
        // if not search term, return empty hero array.
            return of([]);
        }
        return this.http.get<Colaborador[]>(`api/Colaborador/?name=${term}`).pipe(
        tap(_ => this.log(`found Colaborador matching "${term}"`)),
        catchError(this.handleError<Colaborador[]>('searchAssociados', []))
        );
    }
    */

    /** DELETE: delete the Colaborador from the server */
    /*
    deleteAssociado (Colaborador: Colaborador | number): Observable<Colaborador> {
        const id = typeof Colaborador === 'number' ? Colaborador : Colaborador.associadoId;
        const url = `${this.associadoUrl}/${id}`;

        return this.http.delete<Colaborador>(url, httpOptions).pipe(
            tap(_ => this.log(`deleted Colaborador id=${id}`)),
            catchError(this.handleError<Colaborador>('deleteAssociado'))
        );
    }
    */

    /** PUT: update the Colaborador on the server */
    /*
    updateAssociado (Colaborador: Colaborador): Observable<any> {
        return this.http.put(this.webRoute.postAssociado(), Colaborador, httpOptions).pipe(
        tap(_ => this.log(`updated Colaborador id=${Colaborador.associadoId}`)),
        catchError(this.handleError<any>('updateAssociado'))
        );
    }
    */
}
