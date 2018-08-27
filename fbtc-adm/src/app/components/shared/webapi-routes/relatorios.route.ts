import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';

@Injectable()
export class RelatoriosRoute {

    private url = 'Relatorios/';

    // [Route("GetRptTotalAssociadosTipo")]
    getRptTotalAssociadosTipo(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptTotalAssociadosTipo';
    }

    // [Route("GetRptTotalAssociadosTipoToExcel")]
    getRptTotalAssociadosTipoToExcel(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptTotalAssociadosTipoToExcel';
    }

    // [Route("GetRptRecebimentoStatus")]
    getRptRecebimentoStatus(objetivoPagamento: number, anoEventoPS: number, statusPS: number): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptRecebimentoStatus/' + objetivoPagamento + ',' + anoEventoPS + ',' + statusPS;
    }

    // [Route("GetRptRecebimentoStatusToExcel")]
    getRptRecebimentoStatusToExcel(objetivoPagamento: number, anoEventoPS: number, statusPS: number): string {
        return AppSettings.API_ENDPOINT
        + this.url + 'GetRptRecebimentoStatusToExcel/' + objetivoPagamento + ',' + anoEventoPS + ',' + statusPS;
    }

    // [Route("GetRptAssociadosFaixa")]
    getRptAssociadosFaixa(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosFaixa';
    }

    // [Route("GetRptAssociadosFaixaToExcel")]
    getRptAssociadosFaixaToExcel(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosFaixaToExcel';
    }

    // [Route("GetRptAssociadosEstados")]
    getRptAssociadosEstados(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosEstados';
    }

    // [Route("GetRptAssociadosEstadosToExcel")]
    getRptAssociadosEstadosToExcel(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosEstadosToExcel';
    }


    // [Route("GetRptAssociadosGenero")]
    getRptAssociadosGenero(genero: string): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosGenero/' + genero;
    }

    // [Route("GetRptAssociadosGeneroToExcel")]
    getRptAssociadosGeneroToExcel(genero: string): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosGeneroToExcel/' + genero;
    }

    // [Route("GetRptAssociadosAno")]
    getRptAssociadosAno(ano: number): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosAno/' + ano;
    }

    // [Route("GetRptAssociadosAnoToExcel")]
    getRptAssociadosAnoToExcel(ano: number): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptAssociadosAnoToExcel/' + ano;
    }

    // [Route("GetRptReceitaAnualDAO")]
    getRptReceitaAnual(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptReceitaAnual';
    }

    // [Route("GetRptReceitaAnualDAOToExcel")]
    getRptReceitaAnualToExcel(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetRptReceitaAnualToExcel';
    }
}
