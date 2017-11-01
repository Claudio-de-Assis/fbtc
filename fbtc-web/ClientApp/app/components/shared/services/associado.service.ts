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

    getAssociados(): Promise<Associado[]> {
        return Promise.resolve(ASSOCIADOS);
    }

    getAssociadoById(id: any): Promise<Associado[]>{
        return Promise.resolve(ASSOCIADOS);

        //return this.associado$;
        //usando a API do EasyAgendamento para teste de conexão do serviço:
        //return this.http.get('http://localhost:54709/api/Pessoa/${id}')
          //  .map(response => response.json() as PessoaEA)
            //.toPromise();
    }
}