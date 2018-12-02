import { AppSettings } from './../../../app.settings';
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

  title: string;

  _util = Util;

  private selectedId: number;

  private selectedAnuidade: Anuidade;

  anuidades: Anuidade[];

  mensagemSincronizacao: string;

  editExercicio: number;
  editAtivo: boolean;

  _exercicio: number;
  _ativo: string;

  submitted: boolean;

  _itensPerPage: number;

  _msg: string;
  _msgProgresso: string;

  constructor(
      private service: AnuidadeService,
      private servicePS: PagSeguroService,
      private router: Router,
      private route: ActivatedRoute
  ) {
      this.title = 'Consulta da Anuidades';
      this._itensPerPage = AppSettings.ITENS_PER_PAGE;

      this.editExercicio = null;
      this._exercicio = 0;
      this.editAtivo = null;

      this._ativo = '2';
      this.submitted = false;

      this.mensagemSincronizacao = '';
      this._msg = '';
      this._msgProgresso = '';
  }

  onSelect(anuidade: Anuidade): void {

    this.selectedAnuidade = anuidade;
    this.router.navigate(['admin/FichaFinanceira', this.selectedAnuidade.anuidadeId]);
  }

  onSubmit() {
    this.submitted = true;
    this.gotoBuscarAnuidade();
  }

  gotoBuscarAnuidade(): void {

    if (this.editExercicio !== null) {
      this._exercicio = this.editExercicio;
    }

    if (this.editAtivo !== null) {
      if (this.editAtivo) {
        this._ativo = 'true';
      } else {
        this._ativo = 'false';
      }
    }

    this._msgProgresso = '...Pesquisando...';

    this.service.getByFilters(this._exercicio, this._ativo)
        .subscribe(
          anuidades => {
            this.anuidades = anuidades;
            this._msgProgresso =  this.anuidades.length === 0 ? ' - Não foram encontrados registros' : '';
          });

    this.submitted = false;
    this._exercicio = 0;
    this._ativo = '2';
  }

  gotoNovaAnuidade() {

    this.router.navigate(['admin/FichaFinanceiraNova']);
  }

  gotoSicronizarComPagSeguro(): void {
    this.mensagemSincronizacao = 'Processando a sincronização. Por favor, aguarde!....';
    this._msg = '';

    this.servicePS.postSincronizarRecebimentos().subscribe(
      _msg => [
          this._msg = _msg,
          this.mensagemSincronizacao = ''
      ]);
  }

  gotoLimparFiltros() {
    this._exercicio = 0;
    this._ativo = '2';
    this.editExercicio = null;
    this.editAtivo = true;
  }

  ngOnInit() {

    this.gotoBuscarAnuidade();
  }
}
