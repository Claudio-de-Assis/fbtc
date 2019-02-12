import { AppSettings } from './../../../app.settings';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { Colaborador } from '../../shared/model/colaborador';
import { ColaboradorService } from '../../shared/services/colaborador.service';

import { Util } from '../../shared/util/util';

// import { CustomAlertsModule } from '../../shared/custom-alerts/custom-alerts.module';

@Component({
  selector: 'app-colaborador-list',
  templateUrl: './colaborador.list.component.html',
  styleUrls: ['./colaborador.list.component.css']
})
export class ColaboradorListComponent implements OnInit {

  title: string;
  editAtivo: boolean;
  editNome: string;
  editTipoPerfil: string;
  _nome: string;
  _ativo: string;
  _itensPerPage: number;

  _msgProgresso: string;

  _util = Util;
  colaboradores: Colaborador[];

  private selectedColaborador: Colaborador;

  submitted = false;

  constructor(
    private service: ColaboradorService,
    private router: Router,
    private route: ActivatedRoute

  ) {
    this.title = 'Consulta de integrante da Administração';
    this._itensPerPage = AppSettings.ITENS_PER_PAGE;

    this.editAtivo = true;
    this.editNome = '';
    this.editTipoPerfil = '0';
    this._nome = '0';
    this._ativo = '2';

    this._msgProgresso = '';
  }

  getColaboradores(): void {

    this._msgProgresso = '...Pesquisando...';

    this.service.getColaboradores()
      .subscribe(
        colaboradores => {
          this.colaboradores = colaboradores;
          this._msgProgresso =  this.colaboradores.length === 0 ? ' - Não foram encontrados registros' : '';
        });
  }

  onSelect(colaborador: Colaborador): void {

    this.selectedColaborador = colaborador;
    this.router.navigate(['admin/Colaborador', this.selectedColaborador.colaboradorId]);
}

  gotoNovoColaborador() {

      this.router.navigate(['admin/ColaboradorNovo']);
  }

  gotoBuscarColaborador(): void {

    if (this.editNome.trim() !== '') {
      this.editNome = this._util.StringSanity(this.editNome);
      this._nome = this.editNome !== '' ? this.editNome : '0';
    }

    if (this.editAtivo !== null) {
      if (this.editAtivo) {
        this._ativo = 'true';
      } else {
        this._ativo = 'false';
      }
    }

    this._msgProgresso = '...Pesquisando...';

    this.service.getByFilters(this._nome, this.editTipoPerfil, this._ativo)
      .subscribe(
        colaboradores => {
          this.colaboradores = colaboradores;
          this._msgProgresso =  this.colaboradores.length === 0 ? ' - Não foram encontrados registros' : '';
        });

    this.submitted = false;
    this._nome = '0';
    this._ativo = '2';
  }

  gotoLimparFiltros() {
    this.editAtivo = true;
    this.editNome = '';
    this.editTipoPerfil = '0';
    this._nome = '0';
    this._ativo = '2';
  }


  onSubmit() {
    this.submitted = true;
    this.gotoBuscarColaborador();
  }

  ngOnInit() {
    this.getColaboradores();
  }
}
