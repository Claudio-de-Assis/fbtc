import { Injectable } from "@angular/core";

import { Http } from "@angular/http";
import 'rxjs/Rx';

import { Associado } from "../model/associado";
import { PessoaEA } from "../model/pessoaEA";
import { Observable } from "rxjs/Observable";
import { ASSOCIADOS } from '../mock/mock-associados';


@Injectable()
export class AssociadoService {

    associado$: Observable<Associado[]>;
    associado: Associado;
    
    constructor() {
    } 

    getListAssociados() { return Observable.of(ASSOCIADOS); }

    getAssociados(): Promise<Associado[]> {
        return Promise.resolve(ASSOCIADOS);
    }

    //getAssociadoById(id: number | string) {

    getAssociadoById(id: number | string) {
        return this.getListAssociados()
            .map(associados => associados.find(associado => associado.AssociadoId === +id));

        //usando a API do EasyAgendamento para teste de conexão do serviço:
        //return this.http.get('http://localhost:54709/api/Pessoa/${id}')
          //  .map(response => response.json() as PessoaEA)
            //.toPromise();
    }
}