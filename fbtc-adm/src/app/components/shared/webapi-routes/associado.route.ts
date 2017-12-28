import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class AssociadoRoute {

    private url = 'Associado/';

    // [Route("GetAll")]
    getAll(): string {

        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // [Route("{id:int}")]
    getById(id: number): string {

        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("{id:int}")]
    getNomeFotoById(id: number): string {

        return AppSettings.API_ENDPOINT + this.url + 'NomeFoto/' + id;
    }


    // Deve-se informar "0" quando não houver valor válido para o filtro.
    // [Route("FindByFilters/{nome},{cpf},{sexo},{atcId},{crp},{tipoprofissao},{tipoPublicoId}")]
    getFindByFilters(nome: string, cpf: string, sexo: string, atcId: number,
         crp: string, tipoProfissao: string, tipoPublicoId: number, estado: string, cidade: string, ativo: string): string {

        return AppSettings.API_ENDPOINT
        + this.url + `FindByFilters/${nome},${cpf},${sexo},${atcId},${crp},${tipoProfissao},${tipoPublicoId},${estado},${cidade},${ativo}`;
    }

    // [Route("Associado")]
    postAssociado(): string {

        return AppSettings.API_ENDPOINT + this.url + 'Associado';
    }

    // [Route("SetAssociado")]
    setAssociado(): string {
        return AppSettings.API_ENDPOINT + this.url + 'SetAssociado';
    }
}
