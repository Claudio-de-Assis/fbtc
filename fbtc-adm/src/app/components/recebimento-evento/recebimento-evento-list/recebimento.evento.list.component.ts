import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Data } from '@angular/router/src/config';

import { RecebimentoService } from '../../shared/services/recebimento.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';

import { TipoPublico } from '../../shared/model/tipo-publico';
import { Recebimento } from './../../shared/model/recebimento';

import { Util } from '../../shared/util/util';

@Component({
  selector: 'app-recebimento-evento-list',
  templateUrl: './recebimento.evento.list.component.html',
  styleUrls: ['./recebimento.evento.list.component.css'],
})
export class RecebimentoEventoListComponent implements OnInit {

  title = 'Consulta de pagamento de eventos';

  _util = Util;

  private selectedId: number;

  private selectedRecebimento: Recebimento;

  tiposPublicos: TipoPublico[];

  recebimentos: Recebimento[];

  editNome: string = '';
  editCpf: string = '';
  editCrp: string = '';
  editCrm: string = '';
  editStatusPagamento: string = '0';
  editAno: number = 0;
  editAtivo: boolean = true;
  editTipoEvento: string = '0';
  editTipoPublicoId: number = 0;

  _objetivoPagamento: string = '1';
  _nome: string = '0';
  _cpf: string = '0';
  _crp: string = '0';
  _crm: string = '0';
  _statusPagamento: string = '0';
  _ano: number = 0;
  _mes: number = 0;
  _ativo: string = '2';
  _tipoEvento: string = '0';
  _tipoPublicoId: number = 0;

  submitted = false;

  constructor(
      private service: RecebimentoService,
      private serviceTP: TipoPublicoService,
      private router: Router,
      private route: ActivatedRoute
  ) { }

  gotoImprimirLista() {}

  getRecebimentos(objRec: string): void {

    this.service.getAll(objRec).subscribe(recebimentos => this.recebimentos = recebimentos);
  }

  onSelect(recebimento: Recebimento): void {
    this.selectedRecebimento = recebimento;
  }


  onSubmit() {
    this.submitted = true;
    this.gotoBuscarRecebimento();
  }

  gotoBuscarRecebimento(): void {

    if (this.editNome.trim() !== '') {
      this._nome = this.editNome.trim();
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
    if (this.editStatusPagamento !== '0') {
      this._statusPagamento = this.editStatusPagamento;
    }
    if (this.editTipoEvento !== '0') {
      this._tipoEvento = this.editTipoEvento;
    }
    if (this.editAno !== 0) {
      this._ano = this.editAno;
    }
    if (this.editAtivo !== null) {
      if (this.editAtivo) {
        this._ativo = 'true';
      } else {
        this._ativo = 'false';
      }
    }

    this.service.getByFilters(this._objetivoPagamento, this._nome, this._cpf, this._crp,  this._crm,
          this._statusPagamento, this._ano, this._mes, this._ativo, this._tipoEvento, this.editTipoPublicoId)
        .subscribe(recebimentos => this.recebimentos = recebimentos);

    this.submitted = false;
    this._nome = '0';
    this._cpf = '0';
    this._crp = '0';

    this._crm = '0';
    this._statusPagamento = '0';
    this._ano = 0;
    this._mes = 0;
    this._ativo = '2';
    this._tipoEvento = '0';
    this._tipoPublicoId = 0;
  }

  getTiposPublicos(): void {

    this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }

  ngOnInit() {

    this.getTiposPublicos();

    // 1: Eventos.
    const objRecebimento = '1';
    this.getRecebimentos(objRecebimento);
  }
}
