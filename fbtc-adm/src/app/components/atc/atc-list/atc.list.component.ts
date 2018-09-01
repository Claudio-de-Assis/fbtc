import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { AtcService } from '../../shared/services/atc.service';

import { Atc, AtcDao } from '../../shared/model/atc';
import { UnidadeFederacaoService } from '../../shared/services/unidade-federacao.service';
import { UnidadeFederacao } from '../../shared/model/unidade-federacao';

@Component({
  selector: 'app-atc-list',
  templateUrl: './atc.list.component.html',
  styleUrls: ['./atc.list.component.css']
})
export class AtcListComponent implements OnInit {

  title = 'Consulta de ATCs';

  editSiglaUF: string = null;
  _siglaUF: string = '0';

  _itensPerPage = 30;

  private selectedAtc: Atc;

  atcs: Atc[];
  unidadesFederacao: UnidadeFederacao[];

  submitted = false;

  constructor(
    private service: AtcService,
    private serviceUF: UnidadeFederacaoService,
    private router: Router,
    private route: ActivatedRoute
  ) { }


  getAtcs(): void {
    this.service.getAtcs().subscribe(atcs => this.atcs = atcs);
  }

  onSubmit() {

    this.submitted = true;
    this.gotoBuscarAtcs();
  }

  gotoBuscarAtcs() {

    if (this.editSiglaUF !== null) {
        this._siglaUF = this.editSiglaUF;
    }

    this.service.getByFilters(this._siglaUF)
        .subscribe(atcs => this.atcs = atcs);

    this._siglaUF = '0';
  }

  getUFsUtilizadas() {

    this.serviceUF.getUnidadesFederacaoUtilizadas().subscribe(unidadesFederacao => this.unidadesFederacao = unidadesFederacao);
  }


  onSelect(atc: Atc): void {
    this.selectedAtc = atc;
  }

  gotoNovaAtc() {
      this.router.navigate(['/admin/Atc', 0]);
  }

  ngOnInit() {

    this.getUFsUtilizadas();
    this.getAtcs();
  }
}
