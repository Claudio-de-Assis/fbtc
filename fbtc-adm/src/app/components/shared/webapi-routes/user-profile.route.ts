import { UserProfileLogin } from './../model/user-profile';
import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';

@Injectable()
export class UserProfileRoute {

    private url = 'UserProfile/';

    // [Route("{id:int}")]
    getById(id: number): string {

        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("NomeFoto/{id:int}")]
    getNomeFotoById(id: number): string {

        return AppSettings.API_ENDPOINT + this.url + 'NomeFoto/' + id;
    }

    // [Route("UserProfile")]
    postUserProfile(): string {

        return AppSettings.API_ENDPOINT + this.url + `UserProfile`;
    }

    // [Route("RessetPassword/{id:int}")]
    postRessetPassWordById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + `RessetPassword/${id}`;
    }

    // [Route("RessetPasswordByEmail/{email}/")]
    postRessetPassWordByEmail(email: string): string {
        return AppSettings.API_ENDPOINT + this.url + `RessetPasswordByEmail/${email}/`;
    }

    // [Route("ValidaEmail/{pessoaId:int},{eMail}/")]
    getValidaEMail(pessoaId: number, eMail: string): string {

        return AppSettings.API_ENDPOINT + this.url + `ValidaEmail/${pessoaId},${eMail}/`;
    }

    // [Route("LoginUser")]
    loginUser(): string {
        return AppSettings.API_ENDPOINT + this.url + `LoginUser`;
    }

    // [Route("{id:int}")]
    loginUserToken(): string {
        return AppSettings.API_ENDPOINT +  'token';
    }
}
