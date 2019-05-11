import { AppSettings } from './../../../app.settings';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { AtcService } from '../../shared/services/atc.service';

import { Atc, AtcDao } from '../../shared/model/atc';
import { UnidadeFederacaoService } from '../../shared/services/unidade-federacao.service';
import { UnidadeFederacao } from '../../shared/model/unidade-federacao';

@Component({
  selector: 'app-atc-list',
  templateUrl: './atc.list.component.html',
  styleUrls: ['./atc.list.component.css']
})
export class AtcListComponent implements OnInit {

  title: string;

  editSiglaUF: string;
  _siglaUF: string;
  submitted: boolean;

  _itensPerPage: number;

  _msgProgresso: string;

  private selectedAtc: Atc;

  atcs: Atc[];
  unidadesFederacao: UnidadeFederacao[];

  constructor(
    private service: AtcService,
    private serviceUF: UnidadeFederacaoService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.title = 'Consulta de ATCs';
    this.submitted = false;
    this._itensPerPage = AppSettings.ITENS_PER_PAGE;

    this.editSiglaUF = null;
    this._siglaUF = '0';

    this._msgProgresso = '';
  }


  getAtcs(): void {

    this._msgProgresso = '...Pesquisando...';

    this.service.getAtcs().subscribe(
      atcs => {
        this.atcs = atcs;
        this._msgProgresso =  this.atcs.length === 0 ? ' - Não foram encontrados registros' : '';
      });
  }

  onSubmit(): void {

    this.gotoBuscarAtcs();
  }

  gotoBuscarAtcs(): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    if (this.editSiglaUF !== null) {
        this._siglaUF = this.editSiglaUF;
    }

    this._msgProgresso = '...Pesquisando...';

    this.service.getByFilters(this._siglaUF)
        .subscribe(
          atcs => {
            this.atcs = atcs;
            this._msgProgresso =  this.atcs.length === 0 ? ' - Não foram encontrados registros' : '';
            this.submitted = false;
          });

    this._siglaUF = '0';
  }

  getUFsUtilizadas(): void {

    this.serviceUF.getUnidadesFederacaoUtilizadas().subscribe(unidadesFederacao => this.unidadesFederacao = unidadesFederacao);
  }

  onSelect(atc: Atc): void {

    this.selectedAtc = atc;
    this.router.navigate(['admin/Atc', this.selectedAtc.atcId]);
  }

  gotoNovaAtc(): void {
      this.router.navigate(['/admin/Atc', 0]);
  }

  gotoLimparFiltros(): void {
    this.editSiglaUF = null;
    this._siglaUF = '0';
  }

  ngOnInit(): void {

    this.getUFsUtilizadas();
    this.getAtcs();
  }
}
