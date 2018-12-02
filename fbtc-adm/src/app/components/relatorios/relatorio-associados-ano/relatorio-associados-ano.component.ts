import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { RelatoriosService } from '../../shared/services/relatorios.service';
import { RptTotalAssociadosDAO } from '../../shared/model/relatorios';

import { RelatoriosRoute } from '../../shared/webapi-routes/relatorios.route';
import { Util } from '../../shared/util/util';
import { debug } from 'util';

import { Anuidade } from './../../shared/model/anuidade';
import { AnuidadeService } from './../../shared/services/anuidade.service';

@Component({
  selector: 'app-relatorio-associados-ano',
  templateUrl: './relatorio-associados-ano.component.html',
  styleUrls: ['./relatorio-associados-ano.component.css']
})
export class RelatorioAssociadosAnoComponent implements OnInit {

  rptTotalAssociadosDAOs: RptTotalAssociadosDAO[];

  title: string;
  _util = Util;
  editAno: number;
  submitted: boolean;
  rptRoute: string;

  _msgProgresso: string;

  anuidades: Anuidade[];

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute,
    private apiRoute: RelatoriosRoute,
    private serviceAnuidade: AnuidadeService,
  ) {

    this.rptRoute = this.apiRoute.getRptAssociadosAnoToExcel(0);
    this.title = 'Relatório de Usuários por Ano e Tipo de Associação';
    this.submitted = false;

    this._msgProgresso = '';
   }

  getDadosRpt(): void {

    this._msgProgresso = '...Pesquisando...';

    this.rptRoute = this.apiRoute.getRptAssociadosAnoToExcel(this.editAno);

    this.service.getRptAssociadosAnoDAO(this.editAno).
    subscribe(
      rptTotalAssociadosDAOs => {
        this.rptTotalAssociadosDAOs = rptTotalAssociadosDAOs;
        this._msgProgresso =  this.rptTotalAssociadosDAOs.length === 0 ? ' - Não foram encontrados registros' : '';
      });
  }

  setAnoRpt(): void {

    this.rptRoute = this.apiRoute.getRptAssociadosAnoToExcel(this.editAno);
  }

  onSubmit() {

    this.submitted = true;
    this.getDadosRpt();
  }

  gotoLimparFiltros() {

    this.editAno = null;
  }

  getAnuidades(): void {
    this.serviceAnuidade.getAnuidades().subscribe(anuidades => this.anuidades = anuidades);
  }
  ngOnInit() {
    this.getAnuidades();

    // this.getDadosRpt();
  }
}
