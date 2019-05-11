import { AppSettings } from './../../../app.settings';
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
import { AssinaturaAnuidadeService } from './../../shared/services/assinatura-anuidade.service';

@Component({
  selector: 'app-assinatura-anuidade-list',
  templateUrl: './assinatura.anuidade.list.component.html',
  styleUrls: ['./assinatura.anuidade.list.component.css']
})
export class AssinaturaAnuidadeListComponent implements OnInit {

  title: string;
  editAnuidadeId: number;
  editAtivo: boolean;
  editNome: string;
  editCpf: string;
  editComAssinatura: boolean;

  submitted: boolean;

  _anuidadeId: number;
  _itensPerPage: number;
  _ativo: string;
  _nome: string;
  _cpf: string;

  _util = Util;

  _msgProgresso: string;
  _msgProgresso2: string;

  private selectedAssinaturaAnuidadeDao: AssinaturaAnuidadeDao;
  private selectedAssinaturaAnuidadePendenteDao: AssinaturaAnuidadeDao;

  assinaturaAnuidades: AssinaturaAnuidade[];
  assinaturaAnuidadesPendentes: AssinaturaAnuidade[];

  anuidades: Anuidade[];

  constructor(
    private service: AssinaturaAnuidadeService,
    private serviceAnuidade: AnuidadeService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.title = 'Consulta de Assinatura de Anuidade';
    this.submitted = false;
    this._itensPerPage = AppSettings.ITENS_PER_PAGE;

    this.editAnuidadeId = null;
    this._anuidadeId = 0;

    this.editAtivo = true;
    this._ativo = '2';

    this.editNome = '';
    this.editCpf = '';
    this._nome = '0';
    this._cpf = '0';

    this._msgProgresso = '';
    this._msgProgresso2 = '';

    this.editComAssinatura = true;
  }

  getAnuidades(): void {

    this.serviceAnuidade.getAnuidades().subscribe(anuidades => this.anuidades = anuidades);
  }

  onSubmit(): void {

    if (this.editAnuidadeId !== 0) {
      this.gotoBuscarAssinaturasAnuidades();
    }
  }

  gotoBuscarAssinaturasAnuidades(): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    if (this.editAnuidadeId !== null) {
        this._anuidadeId = this.editAnuidadeId;
    }

    if (this.editNome.trim() !== '') {
      this.editNome = this._util.StringSanity(this.editNome);
      this._nome = this.editNome !== '' ? this.editNome : '0';
    }

    /*if (this.editCpf !== '') {
        this._cpf = this.editCpf;
    }*/

    if (this.editAtivo !== null) {
      if (this.editAtivo) {
        this._ativo = 'true';
      } else {
        this._ativo = 'false';
      }
    }

    this._msgProgresso = '...Pesquisando...';
    this._msgProgresso2 = '...Pesquisando...';

    this.service.getByFilters(this._anuidadeId, this._nome, this._cpf, this._ativo)
        .subscribe(assinaturaAnuidades => {
          this.assinaturaAnuidades = assinaturaAnuidades;
          this._msgProgresso =  this.assinaturaAnuidades.length === 0 ? ' - Não foram encontrados registros' : '';
          this.submitted = false;
        });

    this.service.getAssinaturaPendenteByFilters(this._anuidadeId, this._nome, this._cpf, this._ativo)
        .subscribe(assinaturaAnuidadesPendentes => {
          this.assinaturaAnuidadesPendentes = assinaturaAnuidadesPendentes;
          this._msgProgresso2 =  this.assinaturaAnuidadesPendentes.length === 0 ? ' - Não foram encontrados registros' : '';
          this.submitted = false;
        });

    this._nome = '0';
    this._cpf = '0';
    this._ativo = '2';
  }

  onSelect(assinaturaAnuidadeDao: AssinaturaAnuidadeDao): void {
    this.selectedAssinaturaAnuidadeDao = assinaturaAnuidadeDao;

     this.router.navigate(['/admin/AssinaturaAnuidadeDetalhe', {
       id: this.selectedAssinaturaAnuidadeDao.assinaturaAnuidadeId,
       anuidadeId: this.editAnuidadeId,
       tipoPublicoId: this.selectedAssinaturaAnuidadeDao.tipoPublicoId,
       foo: 'foo'}]);
  }

  onSelectPendente(assinaturaAnuidadeDao: AssinaturaAnuidadeDao): void {
    this.selectedAssinaturaAnuidadePendenteDao = assinaturaAnuidadeDao;

    this.router.navigate(['/admin/AssinaturaAnuidadeDetalhe', {
      id: 0,
      anuidadeId: this.editAnuidadeId,
      associadoId: this.selectedAssinaturaAnuidadePendenteDao.associadoId,
      tipoPublicoId: this.selectedAssinaturaAnuidadePendenteDao.tipoPublicoId,
      foo: 'foo'}]);
  }

  gotoLimparFiltros(): void {

    this._anuidadeId = 0;
    this.editAtivo = true;
    this._ativo = '2';

    this.editNome = '';
    this._nome = '0';

    this.editCpf = '';
    this._cpf = '0';
  }

  gotoLimparGrids(): void {

    this._msgProgresso = '';
    this._msgProgresso2 = '';

    this.assinaturaAnuidades = null;
    this.assinaturaAnuidadesPendentes = null;
  }

  ngOnInit(): void {

    this.getAnuidades();

    // Mantem a atualização do parametro 'vivo':
    this.route.params.subscribe((params: Params) => {
      const anuidadeId = +params[`anuidadeId`];

      if (anuidadeId > 0) {
        this.editAnuidadeId = +anuidadeId;
        this.gotoBuscarAssinaturasAnuidades();
      }

     });
  }
}
