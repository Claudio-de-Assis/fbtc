import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';

@Injectable()
export class AnuidadeRoute {

    private url = 'Anuidade/';

    // [Route("GetAll")]
    getAll(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("{id:int}")]
    getAnuidadeDaoById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + `AnuidadeDao/${id}`;
    }

    // [Route("Anuidade")]
    postAnuidade(): string {
        return AppSettings.API_ENDPOINT + this.url + 'Anuidade';
    }

    // [Route("AnuidadeDao")]
    postAnuidadeDao(): string {
        return AppSettings.API_ENDPOINT + this.url + 'AnuidadeDao';
    }

    // [Route("SetAtc")]
    setAnuidade(): string {
        return AppSettings.API_ENDPOINT + this.url + 'SetAnuidade';
    }

    // Deve-se informar "0" quando não houver valor válido para o filtro.
    // [Route("FindByFilters/{codigo},{ativo}")]
    getFindByFilters(codigo: number, ativo: string): string {
        return AppSettings.API_ENDPOINT + this.url + `FindByFilters/${codigo},${ativo}`;
    }


}
