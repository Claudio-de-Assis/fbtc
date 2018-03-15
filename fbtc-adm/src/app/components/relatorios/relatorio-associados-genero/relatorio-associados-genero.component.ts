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

import { Util } from './../../shared/util/util';


@Component({
  selector: 'app-relatorio-associados-genero',
  templateUrl: './relatorio-associados-genero.component.html',
  styleUrls: ['./relatorio-associados-genero.component.css']
})
export class RelatorioAssociadosGeneroComponent implements OnInit {

  title = 'Relatório Total de Usuários por Sexo e Tipo de Associação';

  rptTotalAssociadosDAOs: RptTotalAssociadosDAO[];

  editSexo: string = 'M';

  _util = Util;

  submitted = false;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getDadosRpt(): void {

    this.service.getRptAssociadosGeneroDAO(this.editSexo).
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

    this.getDadosRpt();
  }
}
