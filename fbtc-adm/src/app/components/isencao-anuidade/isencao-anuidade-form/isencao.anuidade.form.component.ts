import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { IsencaoService } from '../../shared/services/isencao.service';
import { AnuidadeService } from '../../shared/services/anuidade.service';

import { Isencao } from '../../shared/model/isencao';
import { Anuidade } from '../../shared/model/anuidade';

import { debug } from 'util';
import { Util } from '../../shared/util/util';
import { parse } from 'querystring';

@Component({
  selector: 'app-isencao-anuidade-form',
  templateUrl: './isencao.anuidade.form.component.html',
  styleUrls: ['./isencao.anuidade.form.component.css'],
  providers: [ AnuidadeService ]
})
export class IsencaoAnuidadeFormComponent implements OnInit {

  @Input() isencao: Isencao = { isencaoId: 0, anuidadeId: null, eventoId : null,
                              descricao: '', dtAta: null, anoEvento: 0 , tipoIsencao: '2', ativo: true};

  title: string;
  badge: string;
  isEdicaoIsencao: boolean;

    anuidades: Anuidade[];
   _anuidade: Anuidade;

   _isenAnuidadeId: number;
   _anuiAnuidadeId: number;
   _tipoIsencao: string;

   _isencaoId: number;

   _msg: string;
   _msgRetorno: string;

  private selectedId: any;

  _util = Util;

  submitted: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: IsencaoService,
    private anuidadeService: AnuidadeService
) {
  this.title = 'Conceder Isenção de Anuidade';
  this.badge = '';
  this.isEdicaoIsencao = false;
  this.submitted = false;
  this._msg = '';
  this._msgRetorno = '';

  this._tipoIsencao = '2';
  }


  getAnuidades(): void {

    this.anuidadeService.getAnuidades().subscribe(anuidades => this.anuidades = anuidades);
  }

  gotoIsencaoAnuidades() {

    let eventoId = this.isencao ? this.isencao.isencaoId : null;
    this.router.navigate(['/admin/IsencaoAnuidade', { id: eventoId, foo: 'foo' }]);
  }

  getIsencaoById(id: number): void {

    this.service.getById(id).subscribe(isencao => this.isencao = isencao);
  }

  setIsencao(tipoIsencao: string): void {

    this.service.setIsencao(tipoIsencao).subscribe(isencao => this.isencao = isencao);
  }

  onSubmit() {

    this.submitted = true;
    this.saveIsencao();
  }

  saveIsencao() {

    this._msg = '';

    const y: number = parseInt(this.isencao.anuidadeId.toString());

    this._anuidade = this.anuidades.find(anuidade => anuidade.anuidadeId === y);
    this.isencao.anoEvento = this._anuidade.codigo;

    this.service.addIsencao(this.isencao).subscribe(
      msg => {
          this._msgRetorno = msg;
          this.avaliaRetorno(this._msgRetorno);
      });
  }

  avaliaRetorno(msgRet: string) {

    if (msgRet.substring(0, 1) === '0') {

      this._isencaoId = parseInt(msgRet.substring(0, 10), 10);

      this.router.navigate([`/IsencaoAnuidade/${this._isencaoId}`]);

      this.getIsencaoById(this._isencaoId);

      this._msg = this._msgRetorno.substring(10);

      this.badge = 'Edição';

      this.isEdicaoIsencao = true;

      this.getIsencaoById(this._isencaoId);

    } else {

        this._msg = this._msgRetorno;
    }
  }

  ngOnInit() {

    this.getAnuidades();

    this._isencaoId = +this.route.snapshot.paramMap.get('id');

    if (this._isencaoId > 0) {
        this.isEdicaoIsencao = true;
        this.badge = 'Edição';
        this.getIsencaoById(this._isencaoId);
    } else {
      this.isEdicaoIsencao = false;
      this.badge = 'Novo';
    }
  }
}
