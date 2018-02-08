import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { AtcService } from '../../shared/services/atc.service';

import { Atc, AtcDao } from './../../shared/model/atc';

@Component({
  selector: 'app-atc-list',
  templateUrl: './atc.list.component.html',
  styleUrls: ['./atc.list.component.css']
})
export class AtcListComponent implements OnInit {

  title = 'Consulta de ATCs';

  editAtcId: number = null;
  _atcId: number = 0;

  private selectedAtc: Atc;

  atcDaos: AtcDao[];
  atcs: Atc[];



  submitted = false;

  constructor(
    private service: AtcService,
    private router: Router,
    private route: ActivatedRoute
  ) { }


  getAtcs(): void {
    this.service.getAtcs().subscribe(atcs => this.atcs = atcs);
  }

  getAtcsLst(): void {
    this.service.getAtcsLst().subscribe(atcDaos => this.atcDaos = atcDaos);
  }

  onSubmit() {

    this.submitted = true;
    this.gotoBuscarAtcs();
  }

  gotoBuscarAtcs() {

    if (this.editAtcId !== null) {
        this._atcId = this.editAtcId;
    }

    this.service.getByFilters(this._atcId)
        .subscribe(atcs => this.atcs = atcs);

    this._atcId = 0;
  }


  onSelect(atc: Atc): void {
    this.selectedAtc = atc;
  }

  gotoNovaAtc() {
      this.router.navigate(['/Atc', 0]);
  }

  ngOnInit() {

    this.getAtcsLst();
    this.getAtcs();
  }
}
