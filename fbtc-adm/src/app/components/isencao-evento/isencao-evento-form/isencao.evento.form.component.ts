import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { IsencaoService } from '../../shared/services/isencao.service';
import { EventoService } from '../../shared/services/evento.service';

import { Isencao } from '../../shared/model/isencao';
import { Evento } from '../../shared/model/evento';

import { Util } from '../../shared/util/util';

@Component({
  selector: 'app-isencao-evento-form',
  templateUrl: './isencao.evento.form.component.html',
  styleUrls: ['./isencao.evento.form.component.css']
})
export class IsencaoEventoFormComponent implements OnInit {

  @Input() isencao: Isencao = { isencaoId: 0, anuidadeId: null, eventoId : null,
    descricao: '', dtAta: null, anoEvento: null , tipoIsencao: '1', ativo: true};

  title: string;
  badge: string;
  isEdicaoIsencao: boolean;

  eventos: Evento[];

  _isencaoId: number;
  _tipoIsencao: string;

  _msg: string;
  _msgRetorno: string;

  private selectedId: any;

  _util = Util;

  submitted = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private serviceEvento: EventoService,
    private service: IsencaoService
  ) {

    this.title = 'Conceder Isenção de Evento';
    this.badge = '';
    this.isEdicaoIsencao = false;
    this.submitted = false;

    this._msg = '';
    this._msgRetorno = '';

    this._tipoIsencao = '1';
   }

  gotoIsencaoEventos() {

    let eventoId = this.isencao ? this.isencao.isencaoId : null;
    this.router.navigate(['/admin/IsencaoEvento', { id: eventoId, foo: 'foo' }]);
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

    this.service.addIsencao(this.isencao).subscribe(
      msg => {
          this._msgRetorno = msg;
          this.avaliaRetorno(this._msgRetorno);
      });
  }

  avaliaRetorno(msgRet: string) {

    if (msgRet.substring(0, 1) === '0') {

      this._isencaoId = parseInt(msgRet.substring(0, 10), 10);

        this.router.navigate([`/IsencaoEvento/${this._isencaoId}`]);

        this.getIsencaoById(this._isencaoId);

        this._msg = this._msgRetorno.substring(10);

        this.badge = 'Edição';

        this.isEdicaoIsencao = true;

        this.getIsencaoById(this._isencaoId);

    } else {

        this._msg = this._msgRetorno;
    }
  }

  getEventos(): void {

    this.serviceEvento.getEventos().subscribe(eventos => this.eventos = eventos);
  }

  ngOnInit() {

    this.getEventos();

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
