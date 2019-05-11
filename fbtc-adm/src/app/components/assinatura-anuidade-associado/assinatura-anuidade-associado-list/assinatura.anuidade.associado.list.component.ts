import { AppSettings } from './../../../app.settings';
import { Associado } from './../../shared/model/associado';
import { AssociadoService } from './../../shared/services/associado.service';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';
// import { Observable } from 'rxjs/Observable';

// import {FormsModule} from '@angular/forms';
// import {NgModule} from '@angular/core';
// import {BrowserModule} from '@angular/platform-browser';

import { AuthService } from './../../shared/services/auth.service';
import { UserProfile } from './../../shared/model/user-profile';

import { Util } from '../../shared/util/util';
import { AssinaturaAnuidade, AssinaturaAnuidadeDao } from '../../shared/model/assinatura-anuidade';
import { Anuidade } from '../../shared/model/anuidade';
import { AnuidadeService } from '../../shared/services/anuidade.service';
import { AssinaturaAnuidadeService } from '../../shared/services/assinatura-anuidade.service';

@Component({
  selector: 'app-assinatura-anuidade-associado-list',
  templateUrl: './assinatura.anuidade.associado.list.component.html',
  styleUrls: ['./assinatura.anuidade.associado.list.component.css']
})
export class AssinaturaAnuidadeAssociadoListComponent implements OnInit {

  title: string;
  editAnuidadeId: number;

  submitted: boolean;

  _anuidadeId: number;
  _itensPerPage: number;

  _pessoaId: number;
  _util = Util;

  _labelAnuidade: string;
  _habilitaAnuidade: boolean;

  associado = new Associado();

  private selectedAssinaturaAnuidadeDao: AssinaturaAnuidadeDao;

  assinaturaAnuidades: AssinaturaAnuidade[];
  assinaturaAnuidadesPendentes: AssinaturaAnuidade[];

  anuidades: Anuidade[];

  constructor(
    private service: AssinaturaAnuidadeService,
    private serviceAnuidade: AnuidadeService,
    private pessoaService: AssociadoService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService
  ) {
    this.title = 'Consulta da Minha Assinatura de Anuidade';
    this.submitted = false;
    this._itensPerPage = AppSettings.ITENS_PER_PAGE;

    this.editAnuidadeId = null;
    this._anuidadeId = 0;

    this._pessoaId = 0;

    this._labelAnuidade = 'Anuidades disponíveis para renovação';
    this._habilitaAnuidade = false;
  }

  getAnuidades(): void {
    this.serviceAnuidade.getAnuidadesPendentesByPessoaId(this._pessoaId)
      .subscribe(anuidades => {
        this.anuidades = anuidades;
        this.avaliaRetorno();
      });
  }

  avaliaRetorno(): void {
    if (this.anuidades.length  === 0) {
      this._labelAnuidade = 'Não há anuidades disponíveis para renovação';
      this._habilitaAnuidade = true;
    }
  }

  onSubmit(): void {

    if (this.editAnuidadeId !== 0) {
      this.gotoNovaAssinatura();
    }
  }

  gotoBuscarAssinaturasAnuidades(): void {

    this.service.getByPessoaId(this._pessoaId)
        .subscribe(assinaturaAnuidades => this.assinaturaAnuidades = assinaturaAnuidades);
  }

  gotoNovaAssinatura(): void {

    this.router.navigate(['/admin/MinhaAssinaturaAnuidadeDetalhe', {
      id: 0,
      anuidadeId: this.editAnuidadeId,
      associadoId: this.associado.associadoId,
      tipoPublicoId: this.associado.tipoPublicoId,
      foo: 'foo'}]);
  }

  getAssociado(): void {
    this.pessoaService.getPessoaAssociadoById(this._pessoaId)
      .subscribe(associado => this.associado = associado);
  }

  onSelect(assinaturaAnuidadeDao: AssinaturaAnuidadeDao): void {
    this.selectedAssinaturaAnuidadeDao = assinaturaAnuidadeDao;

     this.router.navigate(['/admin/MinhaAssinaturaAnuidadeDetalhe', {
       id: this.selectedAssinaturaAnuidadeDao.assinaturaAnuidadeId,
       anuidadeId: this.selectedAssinaturaAnuidadeDao.anuidadeId,
       tipoPublicoId: this.selectedAssinaturaAnuidadeDao.tipoPublicoId,
       foo: 'foo'}]);
  }

  ngOnInit(): void {

    const userProfile: UserProfile = this.authService.getUserProfile();
    this._pessoaId = userProfile.pessoaId;

    this.getAnuidades();
    this.getAssociado();
    this.gotoBuscarAssinaturasAnuidades();

    // Mantem a atualização do parametro 'vivo':
    this.route.params.subscribe((params: Params) => {
      const anuidadeId = +params[`anuidadeId`];

      if (anuidadeId > 0) {
        this.editAnuidadeId = +anuidadeId;
        this.gotoBuscarAssinaturasAnuidades();
      }
     });
  }
}
