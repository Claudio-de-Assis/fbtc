import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class TipoPublicoRoute {

    private url = 'TipoPublico/';

    // [Route("GetAll")]
    getAll(isAtivo: string): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll/' + isAtivo;
    }

    // [Route("GetAll")]
    getByTipoAssociacao(tipoAssociacao: boolean): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetByTipoAssociacao/' + tipoAssociacao;
    }


    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    //  [Route("Evento/{id:int}")]
    getByEventoId(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + 'Evento/' + id;
    }
}
