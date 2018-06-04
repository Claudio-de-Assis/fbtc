import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { RelatoriosService } from '../../shared/services/relatorios.service';
import { RptAssociadosEstadosDAO } from './../../shared/model/relatorios';

import { RelatoriosRoute } from './../../shared/webapi-routes/relatorios.route';
import { debug } from 'util';

@Component({
  selector: 'app-relatorio-associados-estados',
  templateUrl: './relatorio-associados-estados.component.html',
  styleUrls: ['./relatorio-associados-estados.component.css']
})
export class RelatorioAssociadosEstadosComponent implements OnInit {

  rptAssociadosEstadosDAOs: RptAssociadosEstadosDAO[];

  title: string;
  rptRoute: string;
  submitted: boolean;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute,
    private apiRoute: RelatoriosRoute,
  ) {

    this.title = 'Relatório de Usuários para UF e Tipo de Associação';
    this.rptRoute = apiRoute.getRptAssociadosEstadosToExcel();
    this.submitted = false;
  }

  getDadosRpt(): void {

    this.service.getRptAssociadosEstadosDAO()
    .subscribe(rptAssociadosEstadosDAOs => this.rptAssociadosEstadosDAOs = rptAssociadosEstadosDAOs);
  }

  onSubmit() {

    this.submitted = true;
    this.getDadosRpt();
  }

  ngOnInit() {

    this.getDadosRpt();
  }
}
