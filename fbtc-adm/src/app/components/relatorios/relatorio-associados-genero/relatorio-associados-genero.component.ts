import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { RelatoriosService } from '../../shared/services/relatorios.service';
import { RptTotalAssociadosDAO } from '../../shared/model/relatorios';

import { Util } from '../../shared/util/util';
import { RelatoriosRoute } from '../../shared/webapi-routes/relatorios.route';
import { debug } from 'util';

@Component({
  selector: 'app-relatorio-associados-genero',
  templateUrl: './relatorio-associados-genero.component.html',
  styleUrls: ['./relatorio-associados-genero.component.css']
})
export class RelatorioAssociadosGeneroComponent implements OnInit {

  rptTotalAssociadosDAOs: RptTotalAssociadosDAO[];

  editSexo: string;
  title: string;
  submitted: boolean;
  rptRoute: string;

  _util = Util;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute,
    private apiRoute: RelatoriosRoute,
  ) {

    this.editSexo = 'M';
    this.title = 'Relatório Total de Usuários por Sexo e Tipo de Associação';
    this.rptRoute = apiRoute.getRptAssociadosGeneroToExcel(this.editSexo);
    this.submitted = false;
  }

  getDadosRpt(): void {

    this.submitted = true;
    this.rptRoute = this.apiRoute.getRptAssociadosGeneroToExcel(this.editSexo);
    this.service.getRptAssociadosGeneroDAO(this.editSexo).
    subscribe(rptTotalAssociadosDAOs => this.rptTotalAssociadosDAOs = rptTotalAssociadosDAOs);
  }

  onSubmit() {

    this.submitted = true;
    this.getDadosRpt();
  }

  ngOnInit() {

    this.getDadosRpt();
  }
}
