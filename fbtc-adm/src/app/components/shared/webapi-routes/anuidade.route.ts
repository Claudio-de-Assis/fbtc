import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';

@Injectable()
export class AnuidadeRoute {

    private url = 'Anuidade/';

    // [Route("GetAll")]
    getAll(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("Anuidade")]
    postAnuidade(): string {
        return AppSettings.API_ENDPOINT + this.url + 'Anuidade';
    }

    // [Route("SetAtc")]
    setAnuidade(): string {
        return AppSettings.API_ENDPOINT + this.url + 'SetAnuidade';
    }
}
