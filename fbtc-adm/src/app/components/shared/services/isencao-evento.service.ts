import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

import { Isencao } from '../model/isencao';
import { EVENTOS } from './../mock/mock-IsencaoEvento';

@Injectable()
export class IsencaoEventoService {

    ataIsencao$: Observable<Isencao[]>;
    ataIsencao: Isencao;
    resultado: string;

//  constructor(private http: Http) {}
    constructor() {
    }

    getListIsecoesEventos() { return Observable.of(EVENTOS); }

    getIsencoesEventos(): Promise<Isencao[]> {
            return Promise.resolve(EVENTOS);
    }

    getIsencaoEventoById(id: number | string) {
        return this.getListIsecoesEventos()
                .map(isencoesEventos => isencoesEventos.find(isencao => isencao.IsencaoId === +id));
    }

    SaveIsencaoEvento (isencao: Isencao) {

        this.resultado = 'Sucesso';
        return this.resultado;
    }
}
