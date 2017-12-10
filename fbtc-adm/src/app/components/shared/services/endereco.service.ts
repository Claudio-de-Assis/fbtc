import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/Rx';

import { Endereco } from '../model/endereco';

@Injectable()
export class EnderecoService {
    constructor(private http: Http) {

    }

    getById(id: number) {

    }
}
