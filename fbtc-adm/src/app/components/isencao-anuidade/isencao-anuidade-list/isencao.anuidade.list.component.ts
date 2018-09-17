import { Util } from '../../shared/util/util';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { Isencao, IsencaoDao } from '../../shared/model/isencao';
import { IsencaoService } from '../../shared/services/isencao.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { TipoPublico } from '../../shared/model/tipo-publico';

@Component({
  selector: 'app-isencao-anuidade-list',
  templateUrl: './isencao.anuidade.list.component.html',
  styleUrls: ['./isencao.anuidade.list.component.css']
})
export class IsencaoAnuidadeListComponent implements OnInit {

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
  submitted: boolean;

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

    this.title = 'Consulta de Isenção de Anuidade';
    this._itensPerPage = 30;
    this.submitted = false;

    this.editNome = '';
    this.editAnoIsencao = 0;
    this.editIdentificacao = '';
    this.editTipoEvento = '0';
    this._nome = '0';
    this._anoIsencao = 0;
    this._identificacao = '0';
    this._tipoIsencao = '2'; // Anuidade:2 Evento: 1
  }


  onSubmit() {
    this.submitted = true;
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
      this._nome = '0';
      this._identificacao = '0';
      this._anoIsencao = 0;
  }

  gotoNovaIsencao() {

    this.router.navigate(['admin/IsencaoAnuidade', 0]);
  }

  getTiposPublicos(): void {

    this.serviceTP.getTiposPublicos('true').subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }

  gotoLimparFiltros() {
    this.editNome = '';
    this.editAnoIsencao = 0;
    this.editIdentificacao = '';
    this.editTipoEvento = '0';
    this._nome = '0';
    this._anoIsencao = 0;
    this._identificacao = '0';
    this._tipoIsencao = '2'; // Anuidade:2 Evento: 1
  }

  ngOnInit() {

    this.getTiposPublicos();
    this.gotoBuscarIsencao();
  }
}
