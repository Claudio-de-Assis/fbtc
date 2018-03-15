import { debug } from 'util';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { RelatoriosService } from '../../shared/services/relatorios.service';
import { RptRecebimentoStatusDAO } from './../../shared/model/relatorios';
import { Util } from '../../shared/util/util';

@Component({
  selector: 'app-relatorio-recebimento-status',
  templateUrl: './relatorio-recebimento-status.component.html',
  styleUrls: ['./relatorio-recebimento-status.component.css']
})
export class RelatorioRecebimentoStatusComponent implements OnInit {

  title = 'Relatório de Recebimentos por Tipo';

  rptRecebimentoStatusDAOs: RptRecebimentoStatusDAO[];

  editObjetivoPagamento: number;
  editStatusPS: number = 0;
  editAno: number;

  _util = Util;

  submitted = false;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getDadosRpt(): void {

    this.service.getRptRecebimentoStatusDAO(this.editObjetivoPagamento, this.editAno, this.editStatusPS)
    .subscribe(rptRecebimentoStatusDAOs => this.rptRecebimentoStatusDAOs = rptRecebimentoStatusDAOs);
  }

  gotoImprimir() {

    console.log('imprimir');
  }

  onSubmit() {

    this.submitted = true;
    this.getDadosRpt();
  }

  ngOnInit() {

    // this.getDadosRpt();
  }

}
