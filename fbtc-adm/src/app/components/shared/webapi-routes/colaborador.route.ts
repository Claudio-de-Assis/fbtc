import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';


@Injectable()
export class ColaboradorRoute {

    private url = 'Colaborador/';

    // [Route("GetAll")]
    getAll(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("SetColaborador")]
    setColaborador(): string {
        return AppSettings.API_ENDPOINT + this.url + 'SetColaborador';
    }

    // Deve-se informar "0" quando não houver valor válido para o filtro.
    // [Route("FindByFilters/{nome},{tipoPerfil},{ativo}")]
    getFindByFilters(nome: string, tipoPerfil: string, ativo: string): string {
        return AppSettings.API_ENDPOINT + this.url + `FindByFilters/${nome},${tipoPerfil},${ativo}`;
    }

    // [Route("Colaborador")]
    postColaborador(): string {
        return AppSettings.API_ENDPOINT + this.url + `Colaborador`;
    }

    // [Route("RessetPassword/{id:int}")]
    ressetPassWord(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + `RessetPassword/${id}`;
    }

    // [Route("ValidaEmail/{colaboradorId:int},{eMail}")]
    getValidaEMail(colaboradorId: number, eMail: string): string {

        return AppSettings.API_ENDPOINT + this.url + `ValidaEmail/${colaboradorId},${eMail}/`;
    }
}


