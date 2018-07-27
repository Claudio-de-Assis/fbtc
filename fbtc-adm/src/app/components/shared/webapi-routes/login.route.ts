import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class LoginRoute {

    private url = 'Login/';

    // [Route("GetAll")]
    getAll(): string {

        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // [Route("{id:int}")]
    getById(id: number): string {

        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("{id:int}")]
    getNomeFotoById(id: number): string {

        return AppSettings.API_ENDPOINT + this.url + 'NomeFoto/' + id;
    }
}
