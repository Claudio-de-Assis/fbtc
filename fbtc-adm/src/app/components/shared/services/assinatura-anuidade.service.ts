
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
// import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
// import 'rxjs';

import { MessageService } from '../../../message.service';
import { AssinaturaAnuidadeRoute } from './../webapi-routes/assinatura-anuidade.route';
import { AssinaturaAnuidadeDao, AssinaturaAnuidade } from './../model/assinatura-anuidade';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AssinaturaAnuidadeService {

    constructor(
        private http: HttpClient,
        private apiRoute: AssinaturaAnuidadeRoute,
        private messageService: MessageService) { }

    getAssinaturasAnuidades(): Observable<AssinaturaAnuidade[]> {
        return this.http.get<AssinaturaAnuidade[]>(this.apiRoute.getAll())
        .pipe(
            tap(assinaturaAnuidades => this.log('Fetched AssinaturaAnuidade')),
            catchError(this.handleError('getAssinaturasAnuidades()', []))
        );
    }

    getByFilters(anuidadeId: number, nome: string, cpf: string, ativo: string): Observable<AssinaturaAnuidadeDao[]> {

        return this.http.get<AssinaturaAnuidadeDao[]>(this.apiRoute.getFindByFilters(anuidadeId, nome, cpf, ativo))
            .pipe(
                // tslint:disable-next-line:max-line-length
                tap(assinaturaAnuidadeDaos => this.log(`fetched assinaturaAnuidadeDaos Filter AssinaturaAnuidadeDao=${anuidadeId},${nome},${cpf},${ativo}`)),
                catchError(this.handleError(`getByFilters AssinaturaAnuidadeDao=${anuidadeId},${nome},${cpf},${ativo}`, []))
        );
    }

    getByPessoaId(pessoaId: number): Observable<AssinaturaAnuidadeDao[]> {

        return this.http.get<AssinaturaAnuidadeDao[]>(this.apiRoute.getFindByPessoaId(pessoaId))
            .pipe(
                // tslint:disable-next-line:max-line-length
                tap(assinaturaAnuidadeDaos => this.log(`fetched assinaturaAnuidadeDaos Filter AssinaturaAnuidadeDao=${pessoaId}`)),
                catchError(this.handleError(`getByPessoaId AssinaturaAnuidadeDao=${pessoaId}`, []))
        );
    }

    getAssinaturaPendenteByFilters(anuidadeId: number, nome: string, cpf: string, ativo: string): Observable<AssinaturaAnuidadeDao[]> {

        return this.http.get<AssinaturaAnuidadeDao[]>(this.apiRoute.getAssinaturaPendenteByFilters(anuidadeId, nome, cpf, ativo))
            .pipe(
                // tslint:disable-next-line:max-line-length
                tap(assinaturaAnuidadeDaos => this.log(`fetched assinaturaAnuidadeDaos Filter AssinaturaAnuidadeDao=${anuidadeId},${nome},${cpf},${ativo}`)),
                catchError(this.handleError(`getByFilters AssinaturaAnuidadeDao=${anuidadeId},${nome},${cpf},${ativo}`, []))
        );
    }

    getById(id: number): Observable<AssinaturaAnuidadeDao> {
        return this.http.get<AssinaturaAnuidadeDao>(this.apiRoute.getById(id)).pipe(
            tap(_ => this.log(`fetched getById id=${id}`)),
            catchError(this.handleError<AssinaturaAnuidadeDao>(`getById id=${id}`))
          );
    }

    //////// Save methods //////////
    /** POST: add a new AssinaturaAnuidadeDao to the server */
    addAssinaturaAnuidadeDao (assinaturaAnuidadeDao: AssinaturaAnuidadeDao): Observable<string> {
        return this.http.post<string>(this.apiRoute.postAssinaturaAnuidadeDao(), assinaturaAnuidadeDao, httpOptions).pipe(
          tap(_ => this.log(`added AssinaturaAnuidade w/ id=${assinaturaAnuidadeDao.assinaturaAnuidadeId}`)),
          catchError(this.handleError<string>('addAssinaturaAnuidadeDao'))
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
    getAtcNo404<Data>(id: number): Observable<AssinaturaAnuidadeDao> {

        return this.http.get<AssinaturaAnuidadeDao[]>(this.apiRoute.getById(id))
        .pipe(
            map(atc => atc[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} atc id=${id}`);
            }),
            catchError(this.handleError<AssinaturaAnuidadeDao>(`getAtc id=${id}`))
        );
    }
}
