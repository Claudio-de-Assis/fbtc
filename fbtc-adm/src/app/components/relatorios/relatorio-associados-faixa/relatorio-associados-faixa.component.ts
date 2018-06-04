import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { RelatoriosService } from '../../shared/services/relatorios.service';
import { RptAssociadoFaixaDAO } from './../../shared/model/relatorios';
import { debug } from 'util';

import { RelatoriosRoute } from './../../shared/webapi-routes/relatorios.route';

@Component({
  selector: 'app-relatorio-associados-faixa',
  templateUrl: './relatorio-associados-faixa.component.html',
  styleUrls: ['./relatorio-associados-faixa.component.css']
})
export class RelatorioAssociadosFaixaComponent implements OnInit {

  rptAssociadoFaixaDAOs: RptAssociadoFaixaDAO[];

  title: string;
  rptRoute: string;
  submitted: boolean;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute,
    private apiRoute: RelatoriosRoute,
  ) {

    this.title = 'Relatório Usuários por Faixa Etária';
    this.rptRoute = apiRoute.getRptAssociadosFaixaToExcel();
    this.submitted = false;
  }

  getDadosRpt(): void {

    this.submitted = true;
    this.service.getRptAssociadosFaixaDAO().subscribe(rptAssociadoFaixaDAOs => this.rptAssociadoFaixaDAOs = rptAssociadoFaixaDAOs);
  }

  onSubmit() {

    this.submitted = true;
    this.getDadosRpt();
  }

  ngOnInit() {

    this.getDadosRpt();
  }
}
