import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';

@Injectable()
export class UnidadeFederacaoRoute {

    private url = 'UnidadeFederacao/';

    // [Route("GetAll")]
    getAll(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // [Route("GetDisponiveis")]
    getDisponiveis(atcId: number): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetDisponiveis/' + atcId;
    }

    // [Route("GetUtilizados")]
    getUtilizadas(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetUtilizadas';
    }
}
