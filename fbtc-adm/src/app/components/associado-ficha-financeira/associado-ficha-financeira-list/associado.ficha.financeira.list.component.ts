import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { RecebimentoService } from '../../shared/services/recebimento.service';

import { Recebimento, RecebimentoAssociadoDao } from '../../shared/model/recebimento';

import { AuthService } from '../../shared/services/auth.service';
import { UserProfile } from '../../shared/model/user-profile';


import { Util } from '../../shared/util/util';
import { debug } from 'util';

@Component({
  selector: 'app-associado-ficha-financeira-list',
  templateUrl: './associado.ficha.financeira.list.component.html',
  styleUrls: ['./associado.ficha.financeira.list.component.css']
})
export class AssociadoFichaFinanceiraListComponent implements OnInit {

  title = 'Consulta da pegamentos';

  _util = Util;

  private selectedId: number;

  private selectedRecebimento: Recebimento;

  recebimentosAnuidade: RecebimentoAssociadoDao[];
  recebimentosEvento: RecebimentoAssociadoDao[];

  editAno: number;
  editStatusPS: number;
  editAtivo: boolean;
  editObjetivoPagamento: string;

  _ano: number;
  _statusPS: number;
  _ativo: string;
  _pessoaId: number;

  submitted: boolean;

  _itensPerPage: number;

  _msg: string;

  constructor(
      private service: RecebimentoService,
      private router: Router,
      private route: ActivatedRoute,
      private authService: AuthService
  ) {
      this.editAno = 0;
      this.editStatusPS = 99;
      this.editAtivo = true;

      this._statusPS = 99;
      this._ano = 0;
      this._ativo = '2';
      this.submitted = false;
      this._itensPerPage = 30;

      this._msg = '';

      this.editObjetivoPagamento = '2';
  }

  onSelect(recebimento: Recebimento): void {
    this.selectedRecebimento = recebimento;
  }

  onSubmit() {
    this.submitted = true;
    this.gotoBuscarRecebimentoAnuidade();
    this.gotoBuscarRecebimentoEvento();
  }


  gotoBuscarRecebimentoAnuidade(): void {

    if (this.editStatusPS !== 99) {
      this._statusPS = this.editStatusPS;
    }
    if (this.editAno !== 0) {
      this._ano = this.editAno;
    }

    this.service.getPagamentosByPessoaIdIdFilters(this._pessoaId, '2', this._ano, this._statusPS)
        .subscribe(recebimentos => this.recebimentosAnuidade = recebimentos);

    this.submitted = false;

    this._statusPS = 99;
    this._ano = 0;
    this._ativo = '2';
  }

  gotoLimparFiltros() {

    this._statusPS = 99;
    this.editStatusPS = 99;
    this._ano = 0;
    this.editAno = 0;
    this._ativo = '2';
  }

  gotoBuscarRecebimentoEvento(): void {

    if (this.editStatusPS !== 99) {
      this._statusPS = this.editStatusPS;
    }
    if (this.editAno !== 0) {
      this._ano = this.editAno;
    }

    this.service.getPagamentosByPessoaIdIdFilters(this._pessoaId, '1', this._ano, this._statusPS)
        .subscribe(recebimentos => this.recebimentosEvento = recebimentos);

    this.submitted = false;

    this._statusPS = 99;
    this._ano = 0;
    this._ativo = '2';
  }

  ngOnInit() {

    const userProfile: UserProfile = this.authService.getUserProfile();
    this._pessoaId = userProfile.pessoaId;

    this.gotoBuscarRecebimentoAnuidade();
    this.gotoBuscarRecebimentoEvento();
  }
}
