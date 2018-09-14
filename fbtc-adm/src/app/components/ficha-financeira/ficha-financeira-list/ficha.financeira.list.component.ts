import { AnuidadeService } from './../../shared/services/anuidade.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { PagSeguroService } from '../../shared/services/pagSeguro.service';

import { Util } from '../../shared/util/util';
import { Anuidade } from '../../shared/model/anuidade';

@Component({
  selector: 'app-ficha-financeira-list',
  templateUrl: './ficha.financeira.list.component.html',
  styleUrls: ['./ficha.financeira.list.component.css']
})
export class FichaFinanceiraListComponent implements OnInit {

  title = 'Consulta da Anuidades';

  _util = Util;

  private selectedId: number;

  private selectedAnuidade: Anuidade;

  anuidades: Anuidade[];

  mensagemSincronizacao: string;

  editCodigo: number;
  editAtivo: boolean;

  _codigo: number;
  _ativo: string;

  submitted: boolean;

  _itensPerPage: number;

  _msg: string;

  constructor(
      private service: AnuidadeService,
      private serviceTP: TipoPublicoService,
      private servicePS: PagSeguroService,
      private router: Router,
      private route: ActivatedRoute
  ) {
      this.editCodigo = null;
      this._codigo = 0;
      this.editAtivo = true;

      this._ativo = '2';
      this.submitted = false;
      this._itensPerPage = 30;

      this.mensagemSincronizacao = '';
      this._msg = '';
  }

  onSelect(anuidade: Anuidade): void {
    this.selectedAnuidade = anuidade;
  }

  onSubmit() {
    this.submitted = true;
    this.gotoBuscarAnuidade();
  }

  gotoBuscarAnuidade(): void {

    if (this.editCodigo !== null) {
      this._codigo = this.editCodigo;
    }

    if (this.editAtivo !== null) {
      if (this.editAtivo) {
        this._ativo = 'true';
      } else {
        this._ativo = 'false';
      }
    }

    this.service.getByFilters(this._codigo, this._ativo)
        .subscribe(anuidades => this.anuidades = anuidades);

    this.submitted = false;
    this._codigo = 0;
    this._ativo = '2';
    this.editCodigo = null;
  }


  gotoNovaAnuidade() {

    this.router.navigate(['admin/FichaFinanceiraNova']);
  }

  /*
  getTiposPublicos(): void {

    this.serviceTP.getByTipoAssociacao(this._isAssociado).subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }*/

  gotoSicronizarComPagSeguro(): void {
    this.mensagemSincronizacao = 'Processando a sincronização. Por favor, aguarde!....';
    this._msg = '';

    this.servicePS.postSincronizarRecebimentos().subscribe(
      _msg => [
          this._msg = _msg,
          this.mensagemSincronizacao = ''
      ]);
  }

  ngOnInit() {

    // this.getTiposPublicos();
    this.gotoBuscarAnuidade();
  }
}
