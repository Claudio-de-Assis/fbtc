import { RptReceitaAnualDAO } from '../../shared/model/relatorios';
import { debug } from 'util';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { RelatoriosService } from '../../shared/services/relatorios.service';

import { RelatoriosRoute } from '../../shared/webapi-routes/relatorios.route';

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

  _msgProgresso: string;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute,
    private apiRoute: RelatoriosRoute,
  ) {

    this.title = 'Relatório de Receita Anual';
    this.rptRoute = apiRoute.getRptReceitaAnualToExcel();
    this.submitted = false;

    this._msgProgresso = '';
   }

  getDadosRpt(): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    this._msgProgresso = '...Pesquisando...';

    this.service.getRptReceitaAnualDAO().subscribe(
      rptReceitaAnualDAOs => {
        this.rptReceitaAnualDAOs = rptReceitaAnualDAOs;
        this._msgProgresso =  this.rptReceitaAnualDAOs.length === 0 ? ' - Não foram encontrados registros' : '';
        this.submitted = false;

      });
  }

  onSubmit(): void {

    this.getDadosRpt();
  }

  ngOnInit(): void {

    this.getDadosRpt();
  }
}
