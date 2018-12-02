import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';

@Injectable()
export class AnuidadeRoute {

    private url = 'Anuidade/';

    // [Route("GetAll")]
    getAll(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // [Route("AnuidadesPendentes/{pessoaId:int}")]
    getAnuidadesPendentesByPessoaId(pessoaId: number): string {
        return AppSettings.API_ENDPOINT + this.url + `AnuidadesPendentes/${pessoaId}`;
    }

    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("AnuidadeDao/{id:int}")]
    getAnuidadeDaoById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + `AnuidadeDao/${id}`;
    }

    // [Route("AnuidadeDaoTP/{id:int},{tipoPublicoId:int}")]
    getAnuidadeDaoByIdTipoPublicoId(id: number, tipoPublicoId: number): string {
        return AppSettings.API_ENDPOINT + this.url + `AnuidadeDaoTP/${id},${tipoPublicoId}`;
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
    // [Route("FindByFilters/{exercicio},{ativo}")]
    getFindByFilters(exercicio: number, ativo: string): string {
        return AppSettings.API_ENDPOINT + this.url + `FindByFilters/${exercicio},${ativo}`;
    }


}
