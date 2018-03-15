import { debug } from 'util';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { RelatoriosService } from '../../shared/services/relatorios.service';
import { RptAssociadosEstadosDAO } from './../../shared/model/relatorios';


@Component({
  selector: 'app-relatorio-associados-estados',
  templateUrl: './relatorio-associados-estados.component.html',
  styleUrls: ['./relatorio-associados-estados.component.css']
})
export class RelatorioAssociadosEstadosComponent implements OnInit {

  title = 'Relatório de Usuários para UF e Tipo de Associação';

  rptAssociadosEstadosDAOs: RptAssociadosEstadosDAO[];


  submitted = false;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getDadosRpt(): void {

    this.service.getRptAssociadosEstadosDAO()
    .subscribe(rptAssociadosEstadosDAOs => this.rptAssociadosEstadosDAOs = rptAssociadosEstadosDAOs);
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
