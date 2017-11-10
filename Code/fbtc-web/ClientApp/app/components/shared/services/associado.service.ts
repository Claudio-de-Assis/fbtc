import { Injectable } from "@angular/core";

import { Http } from "@angular/http";
import 'rxjs/Rx';

import { Associado } from "../model/associado";
import { PessoaEA } from "../model/pessoaEA";
import { Observable } from "rxjs/Observable";
//import { ASSOCIADOS } from '../mock/mock-associados';


@Injectable()
export class AssociadoService {


    constructor(private http: Http) {
    } 

    getListAssociados() {
        return this.http.get('api/associado/GetAll')
            .map(r => r.json() as Associado[])
            .toPromise();
    }


    getAssociadoById(id: string) {
        return this.http.get(`api/associado/Get/${id}`)
            .map(r => r.json() as Associado[])
            .toPromise();
    }
}