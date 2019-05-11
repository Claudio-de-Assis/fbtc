
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Data } from '@angular/router/src/config';

import { IsencaoService } from '../../shared/services/isencao.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';

import { Isencao, IsencaoDao } from '../../shared/model/isencao';
import { TipoPublico } from '../../shared/model/tipo-publico';

import { Util } from '../../shared/util/util';

@Component({
  selector: 'app-isencao-evento-list',
  templateUrl: './isencao.evento.list.component.html',
  styleUrls: ['./isencao.evento.list.component.css']
})
export class IsencaoEventoListComponent implements OnInit {

  _util = Util;

  isencoes: IsencaoDao[];

  private selectedId: number;

  private selectedIsencao: Isencao;

  tiposPublicos: TipoPublico[];

  title: string;
  editNome: string;
  editAnoIsencao: number;
  editIdentificacao: string;
  editTipoEvento: string;

  _nome: string;
  _anoIsencao: number;
  _identificacao: string;
  _tipoIsencao: string;

  _itensPerPage: number;

  constructor(
    private service: IsencaoService,
    private serviceTP: TipoPublicoService,
    private router: Router,
    private route: ActivatedRoute
  ) {

    this.title = 'Consulta de Isenção de Evento';

    this.editNome = '';
    this.editAnoIsencao = 0;
    this.editIdentificacao = '';
    this.editTipoEvento = '0';
    this._nome = '0';
    this._anoIsencao = 0;
    this._identificacao = '0';
    this._tipoIsencao = '1'; // Anuidade:2 Evento: 1
  }

  submitted = false;

  onSubmit() {
    this.gotoBuscarIsencao();
  }

  gotoImprimirLista() {}

  onSelect(isencao: Isencao): void {
    this.selectedIsencao = isencao;
  }

  gotoBuscarIsencao() {

    if (this.editNome.trim() !== '') {
      this._nome = this.editNome.trim();
    }
    if (this.editIdentificacao.trim() !== '') {
      this._identificacao = this.editIdentificacao.trim();
    }

    this.service.getIsencaoByFilters(this._tipoIsencao, this._nome, this.editAnoIsencao, this._identificacao, this.editTipoEvento)
      .subscribe(isencoes => this.isencoes = isencoes);

      this.editNome = '';
      this.editAnoIsencao = 0;
      this.editIdentificacao = '';
      this.editTipoEvento = '0';
      this._nome = '0';
      this._identificacao = '0';
      this._anoIsencao = 0;
  }

  getIsencoes(objIsen): void {

    // this.service.getAll(objIsen).subscribe(isencoes => this.isencoes = isencoes);
  }

  gotoNovaIsencao() {

    this.router.navigate(['admin/IsencaoEvento', 0]);
  }

  getTiposPublicos(): void {

    this.serviceTP.getTiposPublicos('true').subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }

  ngOnInit() {

    this.getTiposPublicos();
    this.gotoBuscarIsencao();
  }

  gotoLimparFiltros() {
    this.editNome = '';
    this.editAnoIsencao = 0;
    this.editIdentificacao = '';
    this.editTipoEvento = '0';
    this._nome = '0';
    this._anoIsencao = 0;
    this._identificacao = '0';
    this._tipoIsencao = '1'; // Anuidade:2 Evento: 1
  }
}
