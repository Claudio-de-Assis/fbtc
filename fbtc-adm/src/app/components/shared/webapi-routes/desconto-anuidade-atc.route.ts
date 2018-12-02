import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';

@Injectable()
export class DescontoAnuidadeAtcRoute {

    private url = 'DescontoAnuidadeAtc/';

    // [Route("GetAll")]
    getAll(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll';
    }

    // [Route("FindDaoByAnuidadeId/{anuidadeId:int}")]
    getFindDescontoAnuidadeAtcDaoByAnuidadeId(anuidadeId: number): string {
        return AppSettings.API_ENDPOINT + this.url + `FindDaoByAnuidadeId/${anuidadeId}`;
    }

    // Deve-se informar "0" quando não houver valor válido para o filtro anuidadeId.
    // [Route("FindByFilters/{anuidadeId:int},{nomePessoa},{ativo:bool}")]
    getFindByFilters(anuidadeId: number, nomePessoa: string, ativo: string, comDesconto: string): string {
        return AppSettings.API_ENDPOINT + this.url + `FindByFilters/${anuidadeId},${nomePessoa},${ativo},${comDesconto}`;
    }

    // [Route("{id:int}")]
    getFindById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // [Route("{id:int}")]
    getFindDaoById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + `DescontoAnuidadeAtcDao/${id}`;
    }

    // [Route("FindDaoByPessoaId/{pessoaId:int}")]
    getFindDaoByPessoaId(pessoaId: number): string {
        return AppSettings.API_ENDPOINT + this.url + `FindDaoByPessoaId/${pessoaId}`;
    }

    // [Route("GetDadosNovoDescontoAnuidadeAtcDao/{associadoId:int},{anuidadeId:int},{colaboradorId:int}")]
    getDadosNovoDescontoAnuidadeAtcDao(associadoId: number, anuidadeId: number, colaboradorPessoaId: number): string {
        return AppSettings.API_ENDPOINT + this.url +
            `GetDadosNovoDescontoAnuidadeAtcDao/${associadoId},${anuidadeId},${colaboradorPessoaId}`;
    }

    // [Route("DescontoAnuidadeAtcDao")]
    postAssinaturaAnuidade(): string {
        return AppSettings.API_ENDPOINT + this.url + 'DescontoAnuidadeAtcDao';
    }
}
