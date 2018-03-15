import { debug } from 'util';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { RelatoriosService } from '../../shared/services/relatorios.service';
import { RptAssociadoFaixaDAO } from './../../shared/model/relatorios';

@Component({
  selector: 'app-relatorio-associados-faixa',
  templateUrl: './relatorio-associados-faixa.component.html',
  styleUrls: ['./relatorio-associados-faixa.component.css']
})
export class RelatorioAssociadosFaixaComponent implements OnInit {

  title = 'Relatório Usuários por Faixa Etária';

  rptAssociadoFaixaDAOs: RptAssociadoFaixaDAO[];

  submitted = false;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getDadosRpt(): void {

    this.service.getRptAssociadosFaixaDAO().subscribe(rptAssociadoFaixaDAOs => this.rptAssociadoFaixaDAOs = rptAssociadoFaixaDAOs);
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
