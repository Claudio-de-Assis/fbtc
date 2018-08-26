import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

import { MessageService } from './../../../message.service';
import { UserProfileRoute } from './../webapi-routes/user-profile.route';
import { UserProfile } from '../model/user-profile';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class UserProfileService {

    userProfile: UserProfile;

    constructor(
        private http: HttpClient,
        private apiRoute: UserProfileRoute,
        private messageService: MessageService) { }

    getById(id: number): Observable<UserProfile> {
        return this.http.get<UserProfile>(this.apiRoute.getById(id))
        .pipe(
            tap(_ => this.log(`fetched UserProfile id=${id}`)),
            catchError(this.handleError<UserProfile>(`getUserProfile id=${id}`))
        );
    }

    //////// Save methods //////////
    /** POST: add a new Colaborador to the server */
    addUserProfile (userProfile: UserProfile): Observable<string> {
        return this.http.post<string>(this.apiRoute.postUserProfile(), userProfile, httpOptions).pipe(
            tap(_ => this.log(`added UserProfile w/ id=${userProfile.pessoaId}`)),
            catchError(this.handleError<string>('addUserProfile'))
        );
    }

    ressetPassWordById(id: number): Observable<string> {
        return this.http.get<string>(this.apiRoute.postRessetPassWordById(id)).pipe(
            tap(_ => this.log(`fetched UserProfile id=${id}`)),
            catchError(this.handleError<string>(`ressetPasswordById id=${id}`))
        );
    }

    ressetPassWordByEMail(EMail: string): Observable<string> {
        return this.http.get<string>(this.apiRoute.postRessetPassWordByEmail(EMail)).pipe(
            tap(_ => this.log(`fetched UserProfile id=${EMail}`)),
            catchError(this.handleError<string>(`ressetPasswordById id=${EMail}`))
        );
    }

    getValidaEMail(pessoaId: number, eMail: string): Observable<string> {
        return this.http.get<string>(this.apiRoute.getValidaEMail(pessoaId, eMail)).pipe(
            tap(_ => this.log(`fetched UserProfile pessoaId=${pessoaId}, eMail=${eMail}`)),
            catchError(this.handleError<string>(`getValidaEMail id=${pessoaId}`))
        );
    }

    getNomeImagemByPessoaId(id: number): Observable<string> {
        return this.http.get<string>(this.apiRoute.getNomeFotoById(id)).pipe(
            tap(_ => this.log(`fetched pessoa id=${id}`)),
            catchError(this.handleError<string>(`getNomeImagemByPessoaId id=${id}`))
        );
    }

    getLogin(senha: string, eMail: string): Observable<UserProfile> {
        return this.http.get<UserProfile>(this.apiRoute.loginUser(senha, eMail))
            .do(result => this.userProfile = result)
            // .pipe(tap(_ => this.log(`fetched UserProfile senha=${senha},email=${eMail}`)),
            //       catchError(this.handleError<UserProfile>(`getUserProfile senha=${senha},email=${eMail}`))
        // );
    }

    getByEmailPassword(senha: string, eMail: string): Observable<UserProfile> {
        return this.http.get<UserProfile>(this.apiRoute.getByEmailPassword(senha, eMail)).pipe(
            tap(_ => this.log(`fetched UserProfile senha=${senha},email=${eMail}`)),
            catchError(this.handleError<UserProfile>(`getUserProfile senha=${senha},email=${eMail}`))
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
        this.messageService.add('UserProfileService: ' + message);
    }

    /** GET Colaborador by id. Return `undefined` when id not found */
    getAssociadoNo404<Data>(id: number): Observable<UserProfile> {

        return this.http.get<UserProfile[]>(this.apiRoute.getById(id))
        .pipe(
            map(userProfiles => userProfiles[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} UserProfile id=${id}`);
            }),
            catchError(this.handleError<UserProfile>(`getUserProfile id=${id}`))
        );
    }

    getUserProfile(){
        return this.userProfile
    }
}
