import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class RelatoriosRoute {

    private url = 'Relatorios/';

    // [Route("GetRptTotalAssociadosTipo")]
    getRptTotalAssociadosTipo(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptTotalAssociadosTipo';
    }

    // [Route("GetRptRecebimentoStatus")]
    getRptRecebimentoStatus(objetivoPagamento: number, anoEventoPS: number, statusPS: number): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptRecebimentoStatus/' + objetivoPagamento + ',' + anoEventoPS + ',' + statusPS;
    }

    // [Route("GetRptAssociadosFaixa")]
    getRptAssociadosFaixa(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosFaixa';
    }

    // [Route("GetRptAssociadosEstados")]
    getRptAssociadosEstados(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosEstados';
    }

    // [Route("GetRptAssociadosGenero")]
    getRptAssociadosGenero(genero: string): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosGenero/' + genero;
    }

    // [Route("GetRptAssociadosAno")]
    getRptAssociadosAno(ano: number): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosAno/' + ano;
    }

    // [Route("GetRptReceitaAnualDAO")]
    getRptReceitaAnual(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptReceitaAnual';
    }
}
