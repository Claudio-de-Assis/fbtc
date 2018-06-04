import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http, ResponseContentType  } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { MessageService } from './../../../message.service';
import { Relatorios, RptTotalAssociadosDAO, RptRecebimentoStatusDAO, RptAssociadoFaixaDAO,
    RptAssociadosEstadosDAO, RptReceitaAnualDAO } from './../model/relatorios';

import { RelatoriosRoute } from './../webapi-routes/relatorios.route';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class RelatoriosService {

    rptTotalAssociadosDAO: RptTotalAssociadosDAO;
    rptTotalAssociadosDAO$: Observable<RptTotalAssociadosDAO[]>;

    rptRecebimentoStatusDAO: RptRecebimentoStatusDAO;
    rptRecebimentoStatusDAO$: Observable<RptRecebimentoStatusDAO[]>;

    rptAssociadoFaixaDAO: RptAssociadoFaixaDAO;
    rptAssociadoFaixaDAO$: Observable<RptAssociadoFaixaDAO[]>;

    rptAssociadosEstadosDAO: RptAssociadosEstadosDAO;
    rptAssociadosEstadosDAO$: Observable<RptAssociadosEstadosDAO[]>;

    rptReceitaAnualDAO: RptReceitaAnualDAO;
    rptReceitaAnualDAO$: Observable<RptReceitaAnualDAO[]>;

    constructor(
        private http: HttpClient,
        private apiRoute: RelatoriosRoute,
        private messageService: MessageService) { }

    getRptTotalAssociadosTipo(): Observable<RptTotalAssociadosDAO[]> {

        return this.http.get<RptTotalAssociadosDAO[]>(this.apiRoute.getRptTotalAssociadosTipo())
            .pipe(
                tap(getRptTotalAssociadosTipo => this.log('Fetched getRptTotalAssociadosTipo')),
                catchError(this.handleError('getRptTotalAssociadosTipo()', []))
        );
    }

    getRptRecebimentoStatusDAO(objetivoPagamento: number, anoEventoPS: number, statusPS: number): Observable<RptRecebimentoStatusDAO[]> {

        return this.http.get<RptRecebimentoStatusDAO[]>(this.apiRoute.getRptRecebimentoStatus(objetivoPagamento, anoEventoPS, statusPS))
            .pipe(
                tap(getRptRecebimentoStatusDAO => this.log('Fetched getRptRecebimentoStatusDAO')),
                catchError(this.handleError('getRptRecebimentoStatusDAO()', []))
        );
    }

    getRptAssociadosFaixaDAO(): Observable<RptAssociadoFaixaDAO[]> {

        return this.http.get<RptAssociadoFaixaDAO[]>(this.apiRoute.getRptAssociadosFaixa())
            .pipe(
                tap(getRptAssociadoFaixaDAO => this.log('Fetched getRptAssociadoFaixaDAO')),
                catchError(this.handleError('getRptAssociadoFaixaDAO()', []))
        );
    }

    getRptAssociadosEstadosDAO(): Observable<RptAssociadosEstadosDAO[]> {

        return this.http.get<RptAssociadosEstadosDAO[]>(this.apiRoute.getRptAssociadosEstados())
            .pipe(
                tap(getRptAssociadoEstadosDAO => this.log('Fetched getRptAssociadoEstadosDAO')),
                catchError(this.handleError('getRptAssociadoEstadosDAO()', []))
        );
    }

    getRptAssociadosGeneroDAO(genero: string): Observable<RptTotalAssociadosDAO[]> {

        return this.http.get<RptTotalAssociadosDAO[]>(this.apiRoute.getRptAssociadosGenero(genero))
            .pipe(
                tap(getRptAssociadoGeneroDAO => this.log('Fetched getRptAssociadoGeneroDAO')),
                catchError(this.handleError('getRptAssociadoGeneroDAO()', []))
        );
    }

    getRptAssociadosAnoDAO(ano: number): Observable<RptTotalAssociadosDAO[]> {

        return this.http.get<RptTotalAssociadosDAO[]>(this.apiRoute.getRptAssociadosAno(ano))
            .pipe(
                tap(getRptAssociadosAnoDAO => this.log('Fetched getRptAssociadosAnoDAO')),
                catchError(this.handleError('getRptAssociadosAnoDAO()', []))
        );
    }

    getRptReceitaAnualDAO(): Observable<RptReceitaAnualDAO[]> {

        return this.http.get<RptReceitaAnualDAO[]>(this.apiRoute.getRptReceitaAnual())
            .pipe(
                tap(getRptReceitaAnualDAO => this.log('Fetched getRptReceitaAnualDAO')),
                catchError(this.handleError('getRptReceitaAnualDAO()', []))
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

    /** Log a RelatoriosService message with the MessageService */
    private log(message: string) {
        this.messageService.add('RelatoriosService: ' + message);
    }
}
