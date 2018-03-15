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



@Component({
  selector: 'app-relatorio-receita-anual',
  templateUrl: './relatorio-receita-anual.component.html',
  styleUrls: ['./relatorio-receita-anual.component.css']
})
export class RelatorioReceitaAnualComponent implements OnInit {

  title = 'Relatório de Receita Anual';

  rptReceitaAnualDAOs: RptReceitaAnualDAO[];

  submitted = false;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getDadosRpt(): void {

    this.service.getRptReceitaAnualDAO().subscribe(rptReceitaAnualDAOs => this.rptReceitaAnualDAOs = rptReceitaAnualDAOs);
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
