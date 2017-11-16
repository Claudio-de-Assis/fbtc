import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

import { TipoPublico } from '../model/tipo.publico';
import { TIPOSPUBLICOS } from './../mock/mock-tipoPublico';

@Injectable()
export class TipoPublicoService {

  tipoPublico$: Observable<TipoPublico[]>;
  tipoPublico: TipoPublico;
  resultado: string;

  constructor() { }

  getListTiposPublicos() { return Observable.of(TIPOSPUBLICOS); }

  getTiposPublicos(): Promise<TipoPublico[]> {
      return Promise.resolve(TIPOSPUBLICOS);
  }

  getTiposPublicosById(id: number | string) {
      return this.getListTiposPublicos()
          .map(tiposPublicos => tiposPublicos.find(tipoPublico => tipoPublico.TipoPublicoId === +id));
  }

  SaveTipoPublico (tipoPublico: TipoPublico) {
      this.resultado = 'Sucesso';
      return this.resultado;
  }
}
