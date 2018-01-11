import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { MessageService } from './../../../message.service';
import { Evento } from './../model/evento';
import { TipoPublicoValorDao } from '../model/tipo-publico';

import { EventoRoute } from './../webapi-routes/evento.route';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class EventoService {

    evento: Evento;
    eventos$: Observable<Evento[]>;

    constructor(
        private http: HttpClient,
        private apiRoute: EventoRoute,
        private messageService: MessageService) { }

    getEventos(): Observable<Evento[]> {

        return this.http.get<Evento[]>(this.apiRoute.getAll())
            .pipe(
                tap(eventos => this.log('Fetched evento')),
                catchError(this.handleError('getEventos()', []))
        );
    }

    getById(id: number): Observable<Evento> {

        return this.http.get<Evento>(this.apiRoute.getById(id)).pipe(
            tap(_ => this.log(`fetched Evento id=${id}`)),
            catchError(this.handleError<Evento>(`getById id=${id}`))
        );
    }

    getNomeImagemById(id: number): Observable<string> {
        return this.http.get<string>(this.apiRoute.getNomeFotoById(id)).pipe(
            tap(_ => this.log(`fetched evento id=${id}`)),
            catchError(this.handleError<string>(`getNomeImagemById id=${id}`))
        );
    }

    getByRecebimentoId(id: number): Observable<Evento> {

        return this.http.get<Evento>(this.apiRoute.getByRecebimentoId(id)).pipe(
            tap(_ => this.log(`fetched Evento id=${id}`)),
            catchError(this.handleError<Evento>(`getByRecebimentoId id=${id}`))
        );
    }

    setEvento(): Observable<Evento> {

        return this.http.get<Evento>(this.apiRoute.setEvento()).pipe(
            tap(_ => this.log(`fetched Evento id=${0}`)),
            catchError(this.handleError<Evento>(`getEvento id=${0}`))
        );
    }

    getByFilters(nome: string, ano: number, tipoEvento: string): Observable<Evento[]> {

        return this.http.get<Evento[]>(this.apiRoute.getFindByFilters(nome, ano, tipoEvento))
            .pipe(
                tap(eventos => this.log(`fetched Eventos Filter nome=${nome}, ano=${ano}, tipoPerfil=${tipoEvento}`)),
                catchError(this.handleError(`getByFilters nome=${nome}, ano=${ano}, Ativo=${tipoEvento}`, []))
        );
    }

    //////// Save methods //////////
    addEvento (evento: Evento): Observable<Evento> {

        return this.http.post<Evento>(this.apiRoute.postEvento(), evento, httpOptions).pipe(
            tap((_evento: Evento) => this.log(`added Evento w/ id=${evento.eventoId}`)),
            catchError(this.handleError<Evento>('addEvento'))
        );
    }

        //////// Save methods //////////
    addValoresEvento (tiposPublicosValoresDao: TipoPublicoValorDao[]): Observable<TipoPublicoValorDao[]> {

        return this.http.post<TipoPublicoValorDao[]>(this.apiRoute.postValoresEvento(), tiposPublicosValoresDao, httpOptions).pipe(
            tap((_valorEvento: TipoPublicoValorDao[]) => this.log(`added ValoresEventos w/`)),
            catchError(this.handleError<TipoPublicoValorDao[]>('addValoresEvento'))
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
        this.messageService.add('EventoService: ' + message);
    }

    /** GET Colaborador by id. Return `undefined` when id not found */
    getEventoNo404<Data>(id: number): Observable<Evento> {

        return this.http.get<Evento[]>(this.apiRoute.getById(id))
        .pipe(
            map(eventos => eventos[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} Evento id=${id}`);
            }),
            catchError(this.handleError<Evento>(`getEvento id=${id}`))
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
