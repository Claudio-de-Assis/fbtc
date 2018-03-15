import { Util } from './../../shared/util/util';
import { debug } from 'util';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { RelatoriosService } from '../../shared/services/relatorios.service';
import { RptTotalAssociadosDAO } from './../../shared/model/relatorios';

@Component({
  selector: 'app-relatorio-associados-ano',
  templateUrl: './relatorio-associados-ano.component.html',
  styleUrls: ['./relatorio-associados-ano.component.css']
})
export class RelatorioAssociadosAnoComponent implements OnInit {

  title = 'Relatório de Usuários por Ano e Tipo de Associação';

  rptTotalAssociadosDAOs: RptTotalAssociadosDAO[];

  _util = Util;
  editAno: number;

  submitted = false;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getDadosRpt(): void {

    this.service.getRptAssociadosAnoDAO(this.editAno).
    subscribe(rptTotalAssociadosDAOs => this.rptTotalAssociadosDAOs = rptTotalAssociadosDAOs);
  }

  gotoImprimir() {

    console.log('imprimir');
  }

  onSubmit() {

    this.submitted = true;
    this.getDadosRpt();
  }

  ngOnInit() {

    // this.getDadosRpt();
  }
}
