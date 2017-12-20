
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Data } from '@angular/router/src/config';

import { IsencaoService } from '../../shared/services/isencao.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';

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

  editNome: string;
  editCPF: string;
  editCRP: string;
  editCRM: string;
  editEvento: string;
  editStatusPagamento: string;
  editTipoPublico: string;

  editDtVencimento: Data;
  editDtPagto: Data;

  constructor(
    private service: IsencaoService,
    private serviceTP: TipoPublicoService,
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

  gotoNovaIsencao() {

    this.router.navigate(['/IsencaoEvento', 0]);
  }

  getTiposPublicos(): void {

    this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }

  ngOnInit() {

    this.getTiposPublicos();

    // 1: Eventos.
    const objIsencao = '1';
    this.getIsencoes(objIsencao);
  }
}
