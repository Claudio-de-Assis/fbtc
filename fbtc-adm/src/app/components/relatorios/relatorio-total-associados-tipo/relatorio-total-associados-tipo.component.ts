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
  selector: 'app-relatorio-total-associados-tipo',
  templateUrl: './relatorio-total-associados-tipo.component.html',
  styleUrls: ['./relatorio-total-associados-tipo.component.css']
})
export class RelatorioTotalAssociadosTipoComponent implements OnInit {

  title = 'Relatório Total de Usuários por Tipo de Associação';

  rptTotalAssociadosDAOs: RptTotalAssociadosDAO[];

  submitted = false;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getDadosRpt(): void {

    this.service.getRptTotalAssociadosTipo().subscribe(rptTotalAssociadosDAOs => this.rptTotalAssociadosDAOs = rptTotalAssociadosDAOs);
  }

  gotoImprimir() {

    console.log('imprimir');
  }

  onSubmit() {

    this.submitted = true;
    this.getDadosRpt();
  }

  ngOnInit() {

    this.getDadosRpt();
  }
}
