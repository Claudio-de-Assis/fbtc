import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class PagSeguroRoute {

    private url = 'PagSeguro/';

    // [Route("sincronizar/")]
    postSincronizar(): string {
        return AppSettings.API_ENDPOINT + this.url + 'sincronizar?nrDias=31&nrPage=1&nrMaxPageResults=100';
    }
}
