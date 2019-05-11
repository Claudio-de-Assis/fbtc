import { AnuidadeService } from './../../shared/services/anuidade.service';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { RelatoriosService } from '../../shared/services/relatorios.service';
import { RptRecebimentoStatusDAO } from '../../shared/model/relatorios';
import { Util } from '../../shared/util/util';

import { Anuidade } from './../../shared/model/anuidade';

import { RelatoriosRoute } from '../../shared/webapi-routes/relatorios.route';

@Component({
  selector: 'app-relatorio-recebimento-status',
  templateUrl: './relatorio-recebimento-status.component.html',
  styleUrls: ['./relatorio-recebimento-status.component.css']
})
export class RelatorioRecebimentoStatusComponent implements OnInit {

  rptRecebimentoStatusDAOs: RptRecebimentoStatusDAO[];

  title: string;
  editObjetivoPagamento: number;
  editStatusPS: number;
  editAno: number;
  rptRoute: string;
  submitted: boolean;

  _util = Util;

  _msgProgresso: string;

  anuidades: Anuidade[];

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute,
    private apiRoute: RelatoriosRoute,
    private serviceAnuidade: AnuidadeService,
  ) {

    this.title = 'Relatório de Recebimentos por Tipo';
    this.rptRoute = apiRoute.getRptTotalAssociadosTipoToExcel();
    this.editStatusPS = 99;
    this.submitted = false;

    this._msgProgresso = '';
  }

  getDadosRpt(): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    this.rptRoute = this.apiRoute.getRptRecebimentoStatusToExcel(this.editObjetivoPagamento, this.editAno, this.editStatusPS);

    this._msgProgresso = '...Pesquisando...';

    this.service.getRptRecebimentoStatusDAO(this.editObjetivoPagamento, this.editAno, this.editStatusPS)
    .subscribe(rptRecebimentoStatusDAOs => {
      this.rptRecebimentoStatusDAOs = rptRecebimentoStatusDAOs;
      this._msgProgresso =  this.rptRecebimentoStatusDAOs.length === 0 ? ' - Não foram encontrados registros' : '';
      this.submitted = false;
    });
  }

  onSubmit(): void {

    this.getDadosRpt();
  }

  gotoLimparFiltros(): void {

    this.editStatusPS = 0;
    this.editAno = null;
    this.editObjetivoPagamento = null;
  }

  getAnuidades(): void {
    this.serviceAnuidade.getAnuidades().subscribe(anuidades => this.anuidades = anuidades);
  }

  ngOnInit(): void {

    this.getAnuidades();
  }

}
