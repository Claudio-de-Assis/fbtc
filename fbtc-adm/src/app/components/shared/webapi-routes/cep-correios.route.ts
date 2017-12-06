import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class CepCorreiosRoute {

    private url = '/';

    // [Route("GetByCep")]
    getByCep(cep: string): string {
        return AppSettings.API_ENDPOINT_CEP + this.url + cep;
    }
}
