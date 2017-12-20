import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class EventoRoute {

    private url = 'Evento/';

    // [Route("GetAll")]
    getAll(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    getByRecebimentoId(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + 'Recebimento/' + id;
    }

    // [Route("SetEvento")]
    setEvento(): string {
        return AppSettings.API_ENDPOINT + this.url + 'SetEvento';
    }

    // Deve-se informar "0" quando não houver valor válido para o filtro.
    // [Route("FindByFilters/{titulo},{ano},{tipoEvento}")]
    getFindByFilters(titulo: string, ano: number, tipoEvento: string): string {
        return AppSettings.API_ENDPOINT + this.url + `FindByFilters/${titulo},${ano},${tipoEvento}`;
    }

    // [Route("Evento")]
    postEvento(): string {
        return AppSettings.API_ENDPOINT + this.url + 'Evento';
    }
}
