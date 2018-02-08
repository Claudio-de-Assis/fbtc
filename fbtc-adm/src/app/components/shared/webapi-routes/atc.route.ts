import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class AtcRoute {

    private url = 'Atc/';

    // [Route("GetAll")]
    getAll(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // [Route("GetAllLst")]
    getAllLst(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAllLst';
    }

    // Deve-se informar "0" quando não houver valor válido para o filtro.
    // [Route("FindByFilters/{atcId}")]
    getFindByFilters(atcId: number): string {
        return AppSettings.API_ENDPOINT + this.url + `FindByFilters/${atcId}`;
    }

    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("Atc")]
    postAtc(): string {
        return AppSettings.API_ENDPOINT + this.url + 'Atc';
    }

    // [Route("SetAtc")]
    setAtc(): string {
        return AppSettings.API_ENDPOINT + this.url + 'SetAtc';
    }
}
