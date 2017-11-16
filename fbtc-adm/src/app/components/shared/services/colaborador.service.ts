import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

import { Colaborador } from '../model/colaborador';
import { COLABORADORES } from '../mock/mock-colaborador';

@Injectable()
export class ColaboradorService {

    colaborador$: Observable<Colaborador[]>;
    colaborador: Colaborador;
    resultado: string;

    /* constructor(private http: Http) {}*/
    constructor() {
    }

    getListColaboradores() { return Observable.of(COLABORADORES); }

    getColaboradores(): Promise<Colaborador[]> {
        return Promise.resolve(COLABORADORES);
    }

    getColaboradorById(id: number | string) {
        return this.getListColaboradores()
            .map(colaboradores => colaboradores.find(colaborador => colaborador.ColaboradorId === +id));
    }

    SaveColaborador (colaborador: Colaborador) {

        this.resultado = 'Sucesso';
        return this.resultado;
    }
}
