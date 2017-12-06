import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class AtcRoute {

    private url = 'Atc/';

    // [Route("GetAll")]
    getAll(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("Atc")]
    postAtc(): string {
        return AppSettings.API_ENDPOINT + this.url + 'Atc';
    }

    // [Route("SetAtc")]
    setAtc(): string {
        return AppSettings.API_ENDPOINT + this.url + 'SetAtc';
    }
}
