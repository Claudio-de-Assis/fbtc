import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

import { Isencao } from '../model/isencao';
import { ISENCOES } from './../mock/mock-IsencaoAnuidade';

@Injectable()
export class IsencaoAnuidadeService {

    Isencao$: Observable<Isencao[]>;
    Isencao: Isencao;
    resultado: string;

//  constructor(private http: Http) {}
    constructor() {
    }

    getListIsecoesAnuidades() { return Observable.of(ISENCOES); }

    getIsencoesAnuidade(): Promise<Isencao[]> {
            return Promise.resolve(ISENCOES);
    }

    getIsencaoAnuidadeById(id: number | string) {
        return this.getListIsecoesAnuidades()
                .map(isencoes => isencoes.find(isencao => isencao.IsencaoId === +id));
    }

    SaveIsencaoAnuidade (isencao: Isencao) {
        this.resultado = 'Sucesso';
        return this.resultado;
    }
}
