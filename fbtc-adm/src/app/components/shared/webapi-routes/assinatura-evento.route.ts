import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';

@Injectable()
export class AssinaturaEventoRoute {

    private url = 'AssinaturaEvento/';

    // [Route("GetAll")]
    getAll(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // Deve-se informar "0" quando não houver valor válido para o filtro eventoId.
    // [Route("FindByFilters/{eventoId:int},{ativo:bool}")]
    getFindByFilters(eventoId: number, ativo: boolean): string {
        return AppSettings.API_ENDPOINT + this.url + `FindByFilters/${eventoId},${ativo}`;
    }

    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("AssinaturaEvento")]
    postAssinaturaEventoDao(): string {
        return AppSettings.API_ENDPOINT + this.url + 'AssinaturaEvento';
    }
}
