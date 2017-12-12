
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Data } from '@angular/router/src/config';

// import { EventoService } from './../../shared/services/evento.service';
import { IsencaoService } from '../../shared/services/isencao.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';

// import { Evento } from './../../shared/model/evento';
import { Isencao } from './../../shared/model/isencao';
import { TipoPublico } from '../../shared/model/tipo-publico';

import { Util } from './../../shared/util/util';

@Component({
  selector: 'app-isencao-evento-list',
  templateUrl: './isencao.evento.list.component.html',
  styleUrls: ['./isencao.evento.list.component.css']
})
export class IsencaoEventoListComponent implements OnInit {

  title = 'Consulta de Isenção de Evento';

  _util = Util;

  isencoes: Isencao[];

  private selectedId: number;

  private selectedIsencao: Isencao;

  tiposPublicos: TipoPublico[];

  // eventos: Evento[];

  editNome: string;
  editCPF: string;
  editCRP: string;
  editCRM: string;
  editEvento: string;
  editStatus: string;
  editTipoPublico: string;

  editDtVencimento: Data;
  editDtPagto: Data;

  constructor(
    private service: IsencaoService,
    private serviceTP: TipoPublicoService,
    // private serviceEvento: EventoService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  gotoImprimirLista() {}

  getIsencoes(objIsen): void {

    this.service.getAll(objIsen).subscribe(isencoes => this.isencoes = isencoes);
  }

  onSelect(isencao: Isencao): void {
    this.selectedIsencao = isencao;
  }

  gotoBuscarIsencao() { }

  gotoNovaIsencao() {

    this.router.navigate(['/IsencaoEvento', 0]);
  }

  /*
  getEventos(): void {

    this.serviceEvento.getEventos().subscribe(eventos => this.eventos = eventos);
  }
  */

  getTiposPublicos(): void {

    this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }

  ngOnInit() {

    this.getTiposPublicos();

    // this.getEventos();

    // 1: Eventos.
    const objIsencao = '1';
    this.getIsencoes(objIsencao);
  }
}
