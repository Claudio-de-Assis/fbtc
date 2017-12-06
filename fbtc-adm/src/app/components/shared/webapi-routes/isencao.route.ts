import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class IsencaoRoute {

    private url = 'Isencao/';

    // TipoIsenção: 1: Evento; 2: Anuidade
    // [Route("GetAll/{tipoIsencao}")]
    getAll(tipoIsencao: string): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll/' + tipoIsencao;
    }

    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // TipoIsenção: 1: Evento; 2: Anuidade
    // [Route("SetIsencao/{tipoIsencao}")]
    setIsencao(tipoIsencao: string): string {
        return AppSettings.API_ENDPOINT + this.url + `SetIsencao/${tipoIsencao}`;
    }

    // TipoIsenção: 1: Evento; 2: Anuidade
    // Deve-se informar "0" quando não houver valor válido para o filtro.
    // [Route("FindByFilters/{tipoIsencao},{nomeAssociado},{descricao},{ano},{eventoId}")]
    getFindByFilters(tipoIsencao: string, nomeAssociado: string, descricao: string, ano: number,
        eventoId: number): string {
        return AppSettings.API_ENDPOINT + this.url + `FindByFilters/${tipoIsencao},${nomeAssociado},${descricao},${ano},${eventoId}`;
    }

    // TipoIsenção: 1: Evento; 2: Anuidade
    // [Route("Isencao")]
    postIsencao(): string {
        return AppSettings.API_ENDPOINT + this.url + 'Atc';
    }
}
