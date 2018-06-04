import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { RelatoriosService } from '../../shared/services/relatorios.service';
import { RptTotalAssociadosDAO } from './../../shared/model/relatorios';

import { RelatoriosRoute } from './../../shared/webapi-routes/relatorios.route';
import { Util } from './../../shared/util/util';
import { debug } from 'util';

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

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute,
    private apiRoute: RelatoriosRoute,
  ) {

    this.rptRoute = this.apiRoute.getRptAssociadosAnoToExcel(0);
    this.title = 'Relatório de Usuários por Ano e Tipo de Associação';
    this.submitted = false;
   }

  getDadosRpt(): void {

    this.rptRoute = this.apiRoute.getRptAssociadosAnoToExcel(this.editAno);
    this.service.getRptAssociadosAnoDAO(this.editAno).
    subscribe(rptTotalAssociadosDAOs => this.rptTotalAssociadosDAOs = rptTotalAssociadosDAOs);
  }

  setAnoRpt(): void {
    console.log('aqui');
    this.rptRoute = this.apiRoute.getRptAssociadosAnoToExcel(this.editAno);
  }

  onSubmit() {

    this.submitted = true;
    this.getDadosRpt();
  }

  ngOnInit() {

    // this.getDadosRpt();
  }
}
