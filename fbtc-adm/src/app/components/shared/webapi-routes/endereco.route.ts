import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class EnderecoRoute {

    private url = 'Endereco/';

    // [Route("GetAllEstados")]
    getAllEstados(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAllEstados';
    }

    // [Route("GetCidade/{estado}")]
    getGetCidadesByEstado(estado: string): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetCidade/' + estado;
    }
}
