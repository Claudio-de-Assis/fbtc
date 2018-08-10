import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { Colaborador } from './../../shared/model/colaborador';
import { ColaboradorService } from '../../shared/services/colaborador.service';

import { Util } from './../../shared/util/util';

// import { CustomAlertsModule } from '../../shared/custom-alerts/custom-alerts.module';

@Component({
  selector: 'app-colaborador-list',
  templateUrl: './colaborador.list.component.html',
  styleUrls: ['./colaborador.list.component.css']
})
export class ColaboradorListComponent implements OnInit {

  title = 'Consulta de integrante da Administração';

  editAtivo: boolean = true;
  editNome: string = '';
  editTipoPerfil: string = '0';
  _nome: string = '0';
  _ativo: string = '2';

  _itensPerPage = 30;

  _util = Util;

  colaboradores: Colaborador[];

  private selectedColaborador: Colaborador;

  submitted = false;

  constructor(
    private service: ColaboradorService,
    private router: Router,
    private route: ActivatedRoute

  ) { }

  getColaboradores(): void {
    this.service.getColaboradores()
      .subscribe(colaboradores => this.colaboradores = colaboradores);
  }

  onSelect(colaborador: Colaborador): void {

    this.selectedColaborador = colaborador;
}

  gotoNovoColaborador() {

      this.router.navigate(['admin/ColaboradorNovo']);
  }

  gotoBuscarColaborador(): void {

    if (this.editNome.trim() !== '') {
      this._nome = this.editNome.trim();
    }

    if (this.editAtivo !== null) {
      if (this.editAtivo) {
        this._ativo = 'true';
      } else {
        this._ativo = 'false';
      }
    }

    this.service.getByFilters(this._nome, this.editTipoPerfil, this._ativo)
      .subscribe(colaboradores => this.colaboradores = colaboradores);

    this.submitted = false;
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

  /*
  excluir() {
    // this.gotoColaboradores();
  }*/
}
