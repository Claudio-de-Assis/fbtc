
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
// import 'rxjs';

import { MessageService } from '../../../message.service';
import { DescontoAnuidadeAtcRoute } from '../webapi-routes/desconto-anuidade-atc.route';
import { DescontoAnuidadeAtc, DescontoAnuidadeAtcDao } from './../model/desconto-anuidade-atc';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class DescontoAnuidadeAtcService {

    constructor(
        private http: HttpClient,
        private apiRoute: DescontoAnuidadeAtcRoute,
        private messageService: MessageService) { }

    getDescontosAnuidadesAtcs(): Observable<DescontoAnuidadeAtc[]> {
        return this.http.get<DescontoAnuidadeAtc[]>(this.apiRoute.getAll())
        .pipe(
            tap(descontosAnuidadesAtcs => this.log('Fetched getDescontosAnuidadesAtcs')),
            catchError(this.handleError('getDescontosAnuidadesAtcs()', []))
        );
    }

    getDescontoAnuidadeAtcDaoByAnuidadeId(anuidadeId: number): Observable<DescontoAnuidadeAtc> {
        return this.http.get<DescontoAnuidadeAtc>(this.apiRoute.getFindDescontoAnuidadeAtcDaoByAnuidadeId(anuidadeId)).pipe(
            tap(_ => this.log(`fetched getDescontoAnuidadeAtcDaoByAnuidadeId id=${anuidadeId}`)),
            catchError(this.handleError<DescontoAnuidadeAtc>(`getDescontoAnuidadeAtcDaoByAnuidadeId id=${anuidadeId}`))
          );
    }

    getByFilters(anuidadeId: number, nomePessoa: string, ativo: string, comDesconto: string): Observable<DescontoAnuidadeAtcDao[]> {
        return this.http.get<DescontoAnuidadeAtcDao[]>(this.apiRoute.getFindByFilters(anuidadeId, nomePessoa, ativo, comDesconto))
            .pipe(
                // tslint:disable-next-line:max-line-length
                tap(descontosAnuidadesAtcsDaos => this.log(`fetched getByFilters Filter anuidadeId=${anuidadeId},nomePessoa=${nomePessoa},ativo=${ativo}`)),
                catchError(this.handleError(`getByFilters anuidadeId=${anuidadeId},nomePessoa=${nomePessoa},ativo=${ativo}`, []))
        );
    }

    getById(id: number): Observable<DescontoAnuidadeAtc> {
        return this.http.get<DescontoAnuidadeAtc>(this.apiRoute.getFindById(id)).pipe(
            tap(_ => this.log(`fetched getById id=${id}`)),
            catchError(this.handleError<DescontoAnuidadeAtc>(`getById id=${id}`))
          );
    }

    getDaoById(id: number): Observable<DescontoAnuidadeAtcDao> {
        return this.http.get<DescontoAnuidadeAtcDao>(this.apiRoute.getFindDaoById(id)).pipe(
            tap(_ => this.log(`fetched getDaoById id=${id}`)),
            catchError(this.handleError<DescontoAnuidadeAtcDao>(`getDaoById id=${id}`))
          );
    }

    getDaoByPessoaId(pessoaId: number): Observable<DescontoAnuidadeAtcDao[]> {
        return this.http.get<DescontoAnuidadeAtcDao[]>(this.apiRoute.getFindDaoByPessoaId(pessoaId))
            .pipe(
                // tslint:disable-next-line:max-line-length
                tap(descontoAnuidadeAtcDaos => this.log(`fetched getDaoByPessoaId Filter pessoaId=${pessoaId}`)),
                catchError(this.handleError(`getDaoByPessoaId pessoaId=${pessoaId}`, []))
        );
    }

    // tslint:disable-next-line:max-line-length
    getDadosNovoDescontoAnuidadeAtcDao(associadoId: number, anuidadeId: number, colaboradorPessoaId: number): Observable<DescontoAnuidadeAtcDao> {
        // tslint:disable-next-line:max-line-length
        return this.http.get<DescontoAnuidadeAtcDao>(this.apiRoute.getDadosNovoDescontoAnuidadeAtcDao(associadoId, anuidadeId, colaboradorPessoaId)).pipe(
            tap(_ => this.log(`fetched getDadosNovoDescontoAnuidadeAtcDao id=${associadoId}`)),
            catchError(this.handleError<DescontoAnuidadeAtcDao>(`getDadosNovoDescontoAnuidadeAtcDao id=${associadoId}`))
          );
    }

    //////// Save methods //////////
    /** POST: add a new DescontoAnuidadeAtcDao to the server */
    addDescontoAnuidadeAtc (descontoAnuidadeAtcDao: DescontoAnuidadeAtcDao): Observable<string> {
        return this.http.post<string>(this.apiRoute.postAssinaturaAnuidade(), descontoAnuidadeAtcDao, httpOptions).pipe(
          tap(_ => this.log(`added DescontoAnuidadeAtc w/ id=${descontoAnuidadeAtcDao.descontoAnuidadeAtcId}`)),
          catchError(this.handleError<string>('addDescontoAnuidadeAtc'))
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
    getAtcNo404<Data>(id: number): Observable<DescontoAnuidadeAtc> {

        return this.http.get<DescontoAnuidadeAtc[]>(this.apiRoute.getFindById(id))
        .pipe(
            map(atc => atc[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} atc id=${id}`);
            }),
            catchError(this.handleError<DescontoAnuidadeAtc>(`getAtc id=${id}`))
        );
    }
}
