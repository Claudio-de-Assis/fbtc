import { AppSettings } from './../../../app.settings';
import { AnuidadeService } from '../../shared/services/anuidade.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Data } from '@angular/router/src/config';

import { Util } from '../../shared/util/util';

import { Anuidade } from '../../shared/model/anuidade';

@Component({
  selector: 'app-anuidade.list',
  templateUrl: './anuidade.list.component.html',
  styleUrls: ['./anuidade.list.component.css']
})
export class AnuidadeListComponent implements OnInit {

  title: string;
  _util = Util;
  _itensPerPage: number;

  anuidades: Anuidade[];

  private selectedId: number;

  private selectedAnuidade: Anuidade;

  constructor(
    private service: AnuidadeService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.title = 'Consulta de Anuidade';
    this._itensPerPage = AppSettings.ITENS_PER_PAGE;
  }

  submitted = false;

  onSubmit() {
    this.submitted = true;
    this.gotoBuscaAnuidade();
  }

  onSelect(anuidade: Anuidade): void {
    this.selectedAnuidade = anuidade;
  }

  gotoBuscaAnuidade() {}

  gotoNovaAnuidade() {

    this.router.navigate(['/Anuidade', 0]);
  }

  ngOnInit() {

    this.gotoBuscaAnuidade();
  }
}
