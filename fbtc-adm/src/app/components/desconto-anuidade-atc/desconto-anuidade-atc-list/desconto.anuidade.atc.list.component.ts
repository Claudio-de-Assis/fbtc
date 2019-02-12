import { AppSettings } from './../../../app.settings';
import { DescontoAnuidadeAtc, DescontoAnuidadeAtcDao } from './../../shared/model/desconto-anuidade-atc';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { Util } from './../../shared/util/util';
import { AssinaturaAnuidade, AssinaturaAnuidadeDao } from './../../shared/model/assinatura-anuidade';
import { Anuidade } from './../../shared/model/anuidade';
import { AnuidadeService } from './../../shared/services/anuidade.service';
import { DescontoAnuidadeAtcService } from './../../shared/services/desconto-anuidade-atc.service';
import { UserProfile } from '../../shared/model/user-profile';

@Component({
  selector: 'app-desconto-anuidade-atc-list',
  templateUrl: './desconto.anuidade.atc.list.component.html',
  styleUrls: ['./desconto.anuidade.atc.list.component.css']
})
export class DescontoAnuidadeAtcListComponent implements OnInit {

  title: string;
  editAnuidadeId: number;
  editAtivo: boolean;
  editNome: string;
  editComDesconto: boolean;

  submitted: boolean;

  _anuidadeId: number;
  _itensPerPage: number;
  _ativo: string;
  _nome: string;
  _cpf: string;
  _comDesconto: string;

  _msgProgresso: string;
  _msgProgresso2: string;

  _util = Util;

  private selectedDescontoAnuidadeAtcDao: DescontoAnuidadeAtcDao;
  private selectedSemDescontoAnuidadeAtcDao: DescontoAnuidadeAtcDao;

  descontoAnuidadeAtcDaos: DescontoAnuidadeAtcDao[];
  semDescontoAnuidadeAtcDaos: DescontoAnuidadeAtcDao[];

  anuidades: Anuidade[];

  constructor(
    private service: DescontoAnuidadeAtcService,
    private serviceAnuidade: AnuidadeService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.title = 'Desconto de Anuidade x ATC';
    this.submitted = false;
    this._itensPerPage = AppSettings.ITENS_PER_PAGE;

    this.editAnuidadeId = null;
    this._anuidadeId = 0;

    this.editAtivo = true;
    this._ativo = '2';

    this.editNome = '';
    this._nome = '0';

    this.editComDesconto = true;
    this._comDesconto = 'true';

    this._msgProgresso = '';
    this._msgProgresso2 = '';
  }

  getAnuidades(): void {
    this.serviceAnuidade.getAnuidades().subscribe(anuidades => this.anuidades = anuidades);
  }

  onSubmit() {

    if (this.editAnuidadeId !== 0) {
      this.submitted = true;

      this.gotoDescontoAnuidadeAtc();
    }
  }

  gotoDescontoAnuidadeAtc() {

    if (this.editAnuidadeId !== null) {
      this._anuidadeId = this.editAnuidadeId;
    }

    if (this.editNome.trim() !== '') {
      this.editNome = this._util.StringSanity(this.editNome);
      this._nome = this.editNome !== '' ? this.editNome : '0';
    }

    if (this.editAtivo !== null) {
      if (this.editAtivo) {
        this._ativo = 'true';
      } else {
        this._ativo = 'false';
      }
    }

    if (this.editComDesconto !== null) {
      if (this.editComDesconto) {
        this._comDesconto = 'true';
      } else {
        this._comDesconto = 'false';
      }
    }

    if (this.editComDesconto) {

      this._msgProgresso = '...Pesquisando...';

      this.service.getByFilters(this._anuidadeId, this._nome, this._ativo, this._comDesconto)
      .subscribe(
        descontoAnuidadeAtcDaos => {
          this.descontoAnuidadeAtcDaos = descontoAnuidadeAtcDaos;
          this._msgProgresso =  this.descontoAnuidadeAtcDaos.length === 0 ? ' - Não foram encontrados registros' : '';
        });

    } else {

      this._msgProgresso2 = '...Pesquisando...';

      this.service.getByFilters(this._anuidadeId, this._nome, this._ativo, this._comDesconto)
      .subscribe(
        descontoAnuidadeAtcDaos => {
          this.semDescontoAnuidadeAtcDaos = descontoAnuidadeAtcDaos;
          this._msgProgresso2 =  this.semDescontoAnuidadeAtcDaos.length === 0 ? ' - Não foram encontrados registros' : '';
        });
    }

    this._nome = '0';
    this._cpf = '0';
    this._ativo = '2';

  }


  onSelect(descontoAnuidadeAtcDao: DescontoAnuidadeAtcDao): void {
    this.selectedDescontoAnuidadeAtcDao = descontoAnuidadeAtcDao;

     this.router.navigate(['/admin/DescontoAnuidadeAtcDetalhe', {
       id: this.selectedDescontoAnuidadeAtcDao.descontoAnuidadeAtcId,
       anuidadeId: this.editAnuidadeId,
       foo: 'foo'}]);
  }

  onSelectSemDesconto(descontoAnuidadeAtcDao: DescontoAnuidadeAtcDao): void {
    this.selectedDescontoAnuidadeAtcDao = descontoAnuidadeAtcDao;

     this.router.navigate(['/admin/DescontoAnuidadeAtcDetalhe', {
       associadoId: this.selectedDescontoAnuidadeAtcDao.associadoId,
       anuidadeId: this.editAnuidadeId,
       foo: 'foo'}]);
  }



  gotoLimparFiltros() {

    this._anuidadeId = 0;
    this.editAtivo = true;
    this._ativo = '2';

    this.editNome = '';
    this._nome = '0';

    this._cpf = '0';
  }

  gotoLimparGrids() {

    this._msgProgresso = '';
    this._msgProgresso2 = '';

    this.descontoAnuidadeAtcDaos = null;
  }

  ngOnInit() {

    this.getAnuidades();

    // Mantem a atualização do parametro 'vivo':
    this.route.params.subscribe((params: Params) => {
      const anuidadeId = +params[`anuidadeId`];

      if (anuidadeId > 0) {
        this.editAnuidadeId = +anuidadeId;
        this.gotoDescontoAnuidadeAtc();
      }

     });
  }
}
