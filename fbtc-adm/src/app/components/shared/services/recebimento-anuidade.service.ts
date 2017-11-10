import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import 'rxjs/Rx';

import { Associado } from "../model/associado";
import { Colaborador } from "../model/colaborador";

@Injectable()
export class RecebimentoAnuidadeService {
    constructor(private http: Http) {


    }

    getById(id: number) {

        //usando a API do EasyAgendamento para teste de conexão do serviço:
        //        return this.http.get('http://localhost:54709/api/Pessoa/${id}')
        //          .map(response => response.json() as PessoaEA)
        //        .toPromise();

    }
}