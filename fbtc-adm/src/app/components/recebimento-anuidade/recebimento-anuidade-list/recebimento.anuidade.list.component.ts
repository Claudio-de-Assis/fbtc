import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Data } from '@angular/router/src/config';

import { RecebimentoService } from '../../shared/services/recebimento.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { PagSeguroService } from '../../shared/services/pagSeguro.service';

import { Recebimento, RecebimentoAssociadoDao } from '../../shared/model/recebimento';
import { TipoPublico } from '../../shared/model/tipo-publico';

import { Util } from '../../shared/util/util';
import { debug } from 'util';

import { Anuidade } from './../../shared/model/anuidade';
import { AnuidadeService } from './../../shared/services/anuidade.service';
import { AppSettings } from './../../../app.settings';

@Component({
  selector: 'app-recebimento-anuidade-list',
  templateUrl: './recebimento.anuidade.list.component.html',
  styleUrls: ['./recebimento.anuidade.list.component.css']
})
export class RecebimentoAnuidadeListComponent implements OnInit {

  title: string;

  _util = Util;

  private selectedId: number;

  private selectedRecebimento: Recebimento;

  recebimentos: RecebimentoAssociadoDao[];
  anuidades: Anuidade[];

  tiposPublicos: TipoPublico[];
  mensagemSincronizacao: string;

  editNome: string;
  editCpf: string;
  editCrp: string;
  editCrm: string;
  editAno: number;
  editMes: number;
  editStatusPS: number;
  editAtivo: boolean;
  editTipoPublicoId: number;

  _objetivoPagamento: string;
  _nome: string;
  _cpf: string;
  _crp: string;
  _crm: string;
  _statusPS: number;
  _ano: number;
  _mes: number;
  _ativo: string;
  _tipoPublicoId: number;
  _isAssociado: boolean;

  submitted: boolean;

  _itensPerPage: number;

  _msg: string;
  _msgProgresso: string;

  _sincronizandoPS: boolean;

  constructor(
      private service: RecebimentoService,
      private serviceTP: TipoPublicoService,
      private servicePS: PagSeguroService,
      private serviceAnuidade: AnuidadeService,
      private router: Router,
      private route: ActivatedRoute
  ) {
      this.title = 'Consulta de pagamento de anuidades';
      this.editNome = '';
      this.editCpf = '';
      this.editCrp = '';
      this.editCrm = '';
      this.editAno = 0;
      this.editMes = 0;
      this.editStatusPS = 99;
      this.editAtivo = true;
      this.editTipoPublicoId = 0;

      this._objetivoPagamento = '2';
      this._nome = '0';
      this._cpf = '0';
      this._crp = '0';
      this._crm = '0';
      this._statusPS = 99;
      this._ano = 0;
      this._mes = 0;
      this._ativo = '2';
      this._tipoPublicoId = 0;
      this._isAssociado = true;
      this.submitted = false;
      this._itensPerPage = AppSettings.ITENS_PER_PAGE;

      this.mensagemSincronizacao = '';
      this._msg = '';
      this._msgProgresso = '';

      this._sincronizandoPS = false;
  }

  getAnuidades(): void {
    this.serviceAnuidade.getAnuidades().subscribe(anuidades => this.anuidades = anuidades);
  }

  onSelect(recebimento: Recebimento): void {

    this.selectedRecebimento = recebimento;
    this.router.navigate(['admin/RecebimentoAnuidade', this.selectedRecebimento.recebimentoId]);
  }

  onSubmit(): void {

    this.mensagemSincronizacao = '';
    this._msg = '';

    this.gotoBuscarRecebimento();
  }

  gotoBuscarRecebimento(): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    if (this.editNome.trim() !== '') {
      this.editNome = this._util.StringSanity(this.editNome);
      this._nome = this.editNome !== '' ? this.editNome : '0';
    }
    if (this.editCpf !== '') {
      this._cpf = this.editCpf;
    }
    if (this.editCrp !== '') {
        this._crp = this.editCrp;
    }
    if (this.editCrm !== '') {
      this._crm = this.editCrm;
    }
    if (this.editCrm !== '') {
      this._crm = this.editCrm;
    }
    if (this.editStatusPS !== 99) {
      this._statusPS = this.editStatusPS;
    }
    /*if (this.editTipoEvento !== '0') {
      this._tipoEvento = this.editTipoEvento;
    }*/
    if (this.editAno !== 0) {
      this._ano = this.editAno;
    }
    if (this.editMes !== 0) {
      this._mes = this.editMes;
    }
    if (this.editAtivo !== null) {
      if (this.editAtivo) {
        this._ativo = 'true';
      } else {
        this._ativo = 'false';
      }
    }

    this._msgProgresso = '...Pesquisando...';

    this.service.getAnuidadeByFilters(this._nome, this._cpf, this._crp,  this._crm,
          this._statusPS, this._ano, this._mes, this._ativo, this.editTipoPublicoId)
        .subscribe(recebimentos => {
          this.recebimentos = recebimentos;
          this._msgProgresso =  this.recebimentos.length === 0 ? ' - Não foram encontrados registros' : '';
          this.submitted = false;
        });

    this._nome = '0';
    this._cpf = '0';
    this._crp = '0';

    this._crm = '0';
    this._statusPS = 99;
    this._ano = 0;
    this._mes = 0;
    this._ativo = '2';
    this._tipoPublicoId = 0;
  }

  getTiposPublicos(): void {

    this.serviceTP.getByTipoAssociacao(this._isAssociado).subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }

  gotoSicronizarComPagSeguro(): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    this.mensagemSincronizacao = 'Processando a sincronização. Por favor, aguarde!....';
    this._msg = '';
    this._sincronizandoPS = true;

    this.servicePS.postSincronizarRecebimentos().subscribe(
      _msg => {
          this._msg = _msg;
          this.mensagemSincronizacao = '';
          this._sincronizandoPS = false;
          this.submitted = false;
          this.gotoBuscarRecebimento();
      });
  }

  gotoLimparFiltros(): void {
    this.editNome = '';
    this.editCpf = '';
    this.editCrp = '';
    this.editCrm = '';
    this.editAno = 0;
    this.editMes = 0;
    this.editStatusPS = 99;
    this.editAtivo = true;
    this.editTipoPublicoId = 0;

    this._objetivoPagamento = '2';
    this._nome = '0';
    this._cpf = '0';
    this._crp = '0';
    this._crm = '0';
    this._statusPS = 99;
    this._ano = 0;
    this._mes = 0;
    this._ativo = '2';
    this._tipoPublicoId = 0;
    this._isAssociado = true;
    this.submitted = false;
    this._itensPerPage = AppSettings.ITENS_PER_PAGE;

    this.mensagemSincronizacao = '';
    this._msg = '';
  }

  ngOnInit(): void {

    this.getAnuidades();
    this.getTiposPublicos();
    this.gotoBuscarRecebimento();
  }
}
