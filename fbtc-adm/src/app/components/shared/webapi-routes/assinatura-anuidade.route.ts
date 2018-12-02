import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';

@Injectable()
export class AssinaturaAnuidadeRoute {

    private url = 'AssinaturaAnuidade/';

    // [Route("GetAll")]
    getAll(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // Deve-se informar "0" quando não houver valor válido para o filtro anuidadeId.
    // [Route("FindByFilters/{anuidadeId:int},{ativo:bool}")]
    getFindByFilters(anuidadeId: number, nome: string, cpf: string, ativo: string): string {
        return AppSettings.API_ENDPOINT + this.url + `FindByFilters/${anuidadeId},${nome},${cpf},${ativo}`;
    }

    // [Route("FindByFilters/{anuidadeId:int},{ativo:bool}")]
    getFindByPessoaId(pessoaId: number): string {
        return AppSettings.API_ENDPOINT + this.url + `FindByPessoaId/${pessoaId}`;
    }

    // Deve-se informar "0" quando não houver valor válido para o filtro anuidadeId.
    // [Route("FindAssinaturaPendenteByFilters/{anuidadeId:int},{ativo:bool}")]
    getAssinaturaPendenteByFilters(anuidadeId: number, nome: string, cpf: string, ativo: string): string {
        return AppSettings.API_ENDPOINT + this.url + `FindAssinaturaPendenteByFilters/${anuidadeId},${nome},${cpf},${ativo}`;
    }

    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("AssinaturaAnuidade")]
    postAssinaturaAnuidadeDao(): string {
        return AppSettings.API_ENDPOINT + this.url + 'AssinaturaAnuidade';
    }
}
