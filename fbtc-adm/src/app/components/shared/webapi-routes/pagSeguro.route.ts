import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';

@Injectable()
export class PagSeguroRoute {

    private url = 'PagSeguro/';

    private _nrDiasPS = AppSettings.NR_DIAS_CONSULTA_PS;
    private _nrMaxPageResults = AppSettings.NR_MAX_PAGE_RESULTS_PS;

    // [Route("sincronizar/")]
    postSincronizar(): string {
        // tslint:disable-next-line:max-line-length
        return AppSettings.API_ENDPOINT + this.url + `sincronizar?nrDias=${this._nrDiasPS}&nrPage=1&nrMaxPageResults=${this._nrMaxPageResults}`;
    }

    // [Route("https://pagseguro.uol.com.br/checkout/v2/payment.html?code={code}")]
    postGotoChekOut(code: string) {

        if (AppSettings.API_ENDPOINT_PS_TARGET_PRD) {
            return AppSettings.API_ENDPOINT_PRODUCAO_PS + `checkout/v2/payment.html?code=${code}`;

        } else {
            return AppSettings.API_ENDPOINT_SANDBOX_PS + `checkout/v2/payment.html?code=${code}`;
        }
    }
}
