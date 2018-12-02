import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs';

import { MessageService } from '../../../message.service';
import { AssociadoRoute } from '../webApi-routes/associado.route';
import { Associado, AssociadoDao } from '../model/associado';
import { AssociadoIsentoDao } from '../model/associado-isento';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AssociadoService {

    associado: Associado;
    associado$: Observable<Associado>;

    constructor(
        private http: HttpClient,
        private apiRoute: AssociadoRoute,
        private messageService: MessageService) { }

    getAssociados(): Observable<Associado[]> {

        return this.http.get<Associado[]>(this.apiRoute.getAll())
            .pipe(
                tap(associados => this.log('Fetched Associado')),
                catchError(this.handleError('getAssociados()', []))
        );
    }

    getNomeImagemByPessoaId(id: number): Observable<string> {
        return this.http.get<string>(this.apiRoute.getNomeFotoByPessoaId(id)).pipe(
            tap(_ => this.log(`fetched associado pessoaid=${id}`)),
            catchError(this.handleError<string>(`getNomeImagemByPessoaId id=${id}`))
        );
    }

    getById(id: number): Observable<Associado> {
        return this.http.get<Associado>(this.apiRoute.getById(id)).pipe(
            tap(_ => this.log(`fetched associado id=${id}`)),
            catchError(this.handleError<Associado>(`getAssociado id=${id}`))
          );
    }

    getAssociadoDaoById(id: number, anuidadeId: number): Observable<AssociadoDao> {
        return this.http.get<AssociadoDao>(this.apiRoute.getAssociadoDaoById(id, anuidadeId)).pipe(
            tap(_ => this.log(`fetched associadoDao id=${id} anuidadeId=${anuidadeId}`)),
            catchError(this.handleError<AssociadoDao>(`getAssociadoDao id=${id} anuidadeId=${anuidadeId}`))
          );
    }

    getAssociadoDaoByPessoaId(id: number): Observable<AssociadoDao> {
        return this.http.get<AssociadoDao>(this.apiRoute.getAssociadoDaoByPessoaId(id)).pipe(
            tap(_ => this.log(`fetched associadoDao id=${id}`)),
            catchError(this.handleError<AssociadoDao>(`getAssociadoDao id=${id}`))
          );
    }

    getPessoaAssociadoById(id: number): Observable<Associado> {
        return this.http.get<Associado>(this.apiRoute.getPessoaAssociadoById(id)).pipe(
            tap(_ => this.log(`fetched associado id=${id}`)),
            catchError(this.handleError<Associado>(`getAssociado id=${id}`))
          );
    }

    setAssociado(): Observable<Associado> {
        return this.http.get<Associado>(this.apiRoute.setAssociado()).pipe(
            tap(_ => this.log(`fetched associado id=${0}`)),
            catchError(this.handleError<Associado>(`getAssociado id=${0}`))
        );
    }

    //////// Save methods //////////
    /** POST: add a new Associado to the server */
/**
 *     addAssociado (associado: Associado): Observable<Associado> {
        return this.http.post<Associado>(this.apiRoute.postAssociado(), associado, httpOptions).pipe(
          tap((_associado: Associado) => this.log(`added associado w/ id=${associado.associadoId}`)),
          catchError(this.handleError<Associado>('addAssociado'))
        );
    }
  **/
    addAssociado (associado: AssociadoDao): Observable<string> {
        return this.http.post<string>(this.apiRoute.postAssociado(), associado, httpOptions).pipe(
          tap(_ => this.log(`added associado w/ id=${associado.associadoId}`)),
          catchError(this.handleError<string>('addAssociado'))
        );
    }

    /**
     *     ressetPassWordById(id: number): Observable<string> {
        return this.http.get<string>(this.apiRoute.postRessetPassWord(id)).pipe(
            tap(_ => this.log(`fetched associado id=${id}`)),
            catchError(this.handleError<string>(`ressetPasswordById id=${id}`))
        );
    }
     **/

    //////// Save methods //////////
    /** POST: add a new Associado to the server */
    saveAssociadoIsento (associadoIsentoDao: AssociadoIsentoDao): Observable<AssociadoIsentoDao> {
        return this.http.post<AssociadoIsentoDao>(this.apiRoute.postAssociadoIsento(), associadoIsentoDao, httpOptions).pipe(
          tap((_associadoIsentoDao: AssociadoIsentoDao) => this.log(`saved associadoIsentoDao w/ eventoId=${associadoIsentoDao.isencaoId}`)),
          catchError(this.handleError<AssociadoIsentoDao>('saveAssociadoIsentoDao'))
        );
    }

    getByFilters(nome: string, cpf: string, sexo: string, atcId: number,
        crp: string, tipoProfissao: string, tipoPublicoId: number, estado: string, cidade: string, ativo: string): Observable<Associado[]> {
            return this.http.get<Associado[]>(this.apiRoute.getFindByFilters(nome, cpf, sexo, atcId, crp, tipoProfissao, tipoPublicoId, estado, cidade, ativo))
            .pipe(
                tap(associados => this.log(`fetched Associado Filter nome=${nome}, cpf=${cpf}, sexo=${sexo}, atcId=${atcId},
                    crp=${crp}, tipoProfissao=${tipoProfissao}, tipoPublicoId=${tipoPublicoId}, estado=${estado},
                    cidade=${cidade}, ativo=${ativo}`)),
                catchError(this.handleError(`getByFilters nome=${nome}, cpf=${cpf}, sexo=${sexo}, atcId=${atcId},
                    crp=${crp}, tipoProfissao=${tipoProfissao}, tipoPublicoId=${tipoPublicoId}, estado=${estado},
                     cidade=${cidade}, ativo=${ativo}`, []))
        );
    }

    //  nome: string, cpf: string, sexo: string, atcId: number, crp: string, tipoProfissao: string, tipoPublicoId: number
    getIsentoByFilters(isencaoId: number, nome: string, cpf: string, sexo: string, atcId: number,
        crp: string, tipoProfissao: string, tipoPublicoId: number, estado: string, cidade: string, ativo: string): Observable<AssociadoIsentoDao[]> {
            return this.http.get<AssociadoIsentoDao[]>(this.apiRoute.getFindIsentoByFilters(isencaoId, nome, cpf, sexo, atcId, crp, tipoProfissao, tipoPublicoId, estado, cidade, ativo))
            .pipe(
                tap(associadosIsentosDao => this.log(`fetched AssociadoIsentoDao Filter isencaoId=${isencaoId}, nome=${nome}, cpf=${cpf}, sexo=${sexo}, atcId=${atcId},
                    crp=${crp}, tipoProfissao=${tipoProfissao}, tipoPublicoId=${tipoPublicoId}, estado=${estado},
                    cidade=${cidade}, ativo=${ativo}`)),
                catchError(this.handleError(`getByFilters isencaoId=${isencaoId}, nome=${nome}, cpf=${cpf}, sexo=${sexo}, atcId=${atcId},
                    crp=${crp}, tipoProfissao=${tipoProfissao}, tipoPublicoId=${tipoPublicoId}, estado=${estado},
                     cidade=${cidade}, ativo=${ativo}`, []))
        );
    }

    ressetPassWordById(id: number): Observable<string> {
        return this.http.get<string>(this.apiRoute.postRessetPassWord(id)).pipe(
            tap(_ => this.log(`fetched associado id=${id}`)),
            catchError(this.handleError<string>(`ressetPasswordById id=${id}`))
        );
    }

    getValidaEMail(associadoId: number, eMail: string): Observable<string> {
        return this.http.get<string>(this.apiRoute.getValidaEMail(associadoId, eMail)).pipe(
            tap(_ => this.log(`fetched associado associadoId=${associadoId}, eMail=${eMail}`)),
            catchError(this.handleError<string>(`getValidaEMail id=${associadoId}`))
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
        this.messageService.add('AssociadoService: ' + message);
    }

    /** GET Associado by id. Return `undefined` when id not found */
    getAssociadoNo404<Data>(id: number): Observable<Associado> {

        return this.http.get<Associado[]>(this.apiRoute.getById(id))
        .pipe(
            map(associados => associados[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} associado id=${id}`);
            }),
            catchError(this.handleError<Associado>(`getAssociado id=${id}`))
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
