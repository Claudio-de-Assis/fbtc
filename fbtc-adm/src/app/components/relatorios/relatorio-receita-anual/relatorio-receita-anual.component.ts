import { RptReceitaAnualDAO } from './../../shared/model/relatorios';
import { debug } from 'util';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { RelatoriosService } from '../../shared/services/relatorios.service';

import { RelatoriosRoute } from './../../shared/webapi-routes/relatorios.route';

@Component({
  selector: 'app-relatorio-receita-anual',
  templateUrl: './relatorio-receita-anual.component.html',
  styleUrls: ['./relatorio-receita-anual.component.css']
})
export class RelatorioReceitaAnualComponent implements OnInit {

  rptReceitaAnualDAOs: RptReceitaAnualDAO[];

  title: string;
  submitted: boolean;
  rptRoute: string;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute,
    private apiRoute: RelatoriosRoute,
  ) {

    this.title = 'Relatório de Receita Anual';
    this.rptRoute = apiRoute.getRptReceitaAnualToExcel();
    this.submitted = false;
   }

  getDadosRpt(): void {

    this.service.getRptReceitaAnualDAO().subscribe(rptReceitaAnualDAOs => this.rptReceitaAnualDAOs = rptReceitaAnualDAOs);
  }

  onSubmit() {

    this.submitted = true;
    this.getDadosRpt();
  }

  ngOnInit() {

    this.getDadosRpt();
  }
}
