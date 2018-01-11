import { Injectable } from '@angular/core';
import { AppSettings } from './../../../app.settings';

@Injectable()
export class RecebimentoRoute {

    private url = 'Recebimento/';

    // objetivoPagamento: 1: Evento; 2: Anuidade
    // [Route("GetAll/{objetivoPagamento}")]
    getAll(objetivoPagamento: string): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetAll/' + objetivoPagamento;
    }

    // [Route("{id:int}")]
    getById(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + id;
    }

    // objetivoPagamento: 1: Evento; 2: Anuidade
    // [Route("SetRecebimento/{objetivoPagamento}")]
    setRecebimento(objetivoPagamento: string): string {
        return AppSettings.API_ENDPOINT + this.url + 'SetRecebimento/' + objetivoPagamento;
    }

    // objetivoPagamento: 1: Evento; 2: Anuidade
    // Deve-se informar "0" quando não houver valor válido para o filtro. Os atributos
    // 'objetivoPagamento', 'Ativo' são obrigatórios.
    // [Route("FindByFilters/{objetivoPagamento},{nome},{cpf},{crp},{crm},{status},{ano},{mes},{ativo},{tipoEvento},{tipoPublicoId}")]
    // getFindByFilters(objetivoPagamento: string, nome: string, cpf: string, crp: string, crm: string,
    //    statusPagamento: string, ano: number, mes: number, ativo: string, tipoEvento: string, tipoPublicoId: number): string {
    //
    //    return AppSettings.API_ENDPOINT + this.url +
    //    `FindByFilters/${objetivoPagamento},${nome},${cpf},${crp},${crm},
    // ${statusPagamento},${ano},${mes},${ativo},${tipoEvento},${tipoPublicoId}`;
    // }

    getFindAnuidadeByFilters(nome: string, cpf: string, crp: string, crm: string,
        statusPagamento: string, ano: number, mes: number, ativo: string, tipoPublicoId: number): string {

        return AppSettings.API_ENDPOINT + this.url +
        `FindAnuidadeByFilters/${nome},${cpf},${crp},${crm},${statusPagamento},${ano},${mes},${ativo},${tipoPublicoId}`;
    }

    FindByAnuidadeIdFilters(anuidadeId: number, nome: string, cpf: string, crp: string, crm: string,
        statusPagamento: string, ano: number, mes: number, ativo: string, tipoPublicoId: number): string {

        return AppSettings.API_ENDPOINT + this.url +
        `FindByAnuidadeIdFilters/${anuidadeId},${nome},${cpf},${crp},${crm},${statusPagamento},${ano},${mes},${ativo},${tipoPublicoId}`;
    }

    getFindEventoByFilters(nome: string, cpf: string, crp: string, crm: string,
        statusPagamento: string, ano: number, mes: number, ativo: string, tipoEvento: string, tipoPublicoId: number): string {

        return AppSettings.API_ENDPOINT + this.url +
        `FindEventoByFilters/${nome},${cpf},${crp},${crm},${statusPagamento},${ano},${mes},${ativo},${tipoEvento},${tipoPublicoId}`;
    }

    getFindByEventoIdFilters(eventoId: number, nome: string, cpf: string, crp: string, crm: string,
        statusPagamento: string, ano: number, mes: number, ativo: string, tipoEvento: string, tipoPublicoId: number): string {

        return AppSettings.API_ENDPOINT + this.url +
        `FindByEventoIdFilters/${eventoId},${nome},${cpf},${crp},${crm},${statusPagamento},${ano},${mes},${ativo},${tipoEvento},${tipoPublicoId}`;
    }

    // [Route("Recebimento")]
    postRecebimento(): string {
        return AppSettings.API_ENDPOINT + this.url + 'Recebimento';
    }

    // objetivoPagamento: 1: Evento; 2: Anuidade
    // [Route("GetByPessoaId/{objetivoPagamento},{id:int}")]
    getByPessoaId(objetivoPagamento: string, id: number): string {
        return AppSettings.API_ENDPOINT + this.url + `GetByPessoaId/${objetivoPagamento},${id}`;
    }

    // [Route("GetByEventoId/{id:int}")]
    getByEventoId(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetByEventoId/' + id;
    }

    // [Route("GetByAnuidadeId/{id:int}")]
    getByAnuidadeId(id: number): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetByAnuidadeId/' + id;
    }
}
