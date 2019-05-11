import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { RelatoriosService } from '../../shared/services/relatorios.service';
import { RptAssociadosEstadosDAO } from '../../shared/model/relatorios';

import { RelatoriosRoute } from '../../shared/webapi-routes/relatorios.route';

@Component({
  selector: 'app-relatorio-associados-estados',
  templateUrl: './relatorio-associados-estados.component.html',
  styleUrls: ['./relatorio-associados-estados.component.css']
})
export class RelatorioAssociadosEstadosComponent implements OnInit {

  rptAssociadosEstadosDAOs: RptAssociadosEstadosDAO[];

  title: string;
  rptRoute: string;
  submitted: boolean;

  _msgProgresso: string;

  constructor(
    private service: RelatoriosService,
    private router: Router,
    private route: ActivatedRoute,
    private apiRoute: RelatoriosRoute,
  ) {

    this.title = 'Relatório de Usuários para UF e Tipo de Associação';
    this.rptRoute = apiRoute.getRptAssociadosEstadosToExcel();
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

    this.service.getRptAssociadosEstadosDAO()
    .subscribe(rptAssociadosEstadosDAOs => {
      this.rptAssociadosEstadosDAOs = rptAssociadosEstadosDAOs;
      this._msgProgresso =  this.rptAssociadosEstadosDAOs.length === 0 ? ' - Não foram encontrados registros' : '';
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
