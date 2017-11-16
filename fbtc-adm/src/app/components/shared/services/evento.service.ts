import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

import { Evento } from './../model/evento';
import { EVENTOS } from './../mock/mock-evento';

@Injectable()
export class EventoService {

    eventos$: Observable<Evento[]>;
    evento: Evento;
    resultado: string;

/*  constructor(private http: Http) {*/
    constructor() {
    }

    getListEventos() { return Observable.of(EVENTOS); }

    getEventos(): Promise<Evento[]> {
        return Promise.resolve(EVENTOS);
    }

    getEventoById(id: number | string) {
        return this.getListEventos()
            .map(eventos => eventos.find(evento => evento.EventoId === +id));
    }

    SaveEvento (evento: Evento) {
        this.resultado = 'Sucesso';
        return this.resultado;
    }
}
