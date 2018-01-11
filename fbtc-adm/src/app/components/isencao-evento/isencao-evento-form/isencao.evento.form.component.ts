import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { IsencaoService } from '../../shared/services/isencao.service';
import { EventoService } from './../../shared/services/evento.service';

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

  title = 'Conceder Isenção de Evento';
  badge = '';
  isEdicaoIsencao: boolean = false;

  eventos: Evento[];

  _isencaoId: number;
  _tipoIsencao: string;

  private selectedId: any;

  // isencaos: Observable<Isencao>;

  _util = Util;

  submitted = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private serviceEvento: EventoService,
    private service: IsencaoService
  ) { }

  gotoIsencaoEventos() {

    let eventoId = this.isencao ? this.isencao.isencaoId : null;
    this.router.navigate(['/IsencaoEvento', { id: eventoId, foo: 'foo' }]);
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

    this.service.addIsencao(this.isencao).subscribe(() =>  this.savaAssociadosIsentos());
  }

  savaAssociadosIsentos() {

    if (this.isencao.isencaoId !== 0 ) {

      // Colocar aqui a chamada para salvar os associados isentos: Ex:
      // this.service.addValoresEvento(this.tiposPublicosValoresDao)
      // .subscribe(() =>  this.gotoShowPopUp('Registro salvo com sucesso!'));
      this.gotoShowPopUp('Registro salvo com sucesso!');

    } else {
      this.gotoShowPopUp('Registro salvo com sucesso!');
      this.gotoIsencaoEventos();
    }
  }

  gotoShowPopUp(msg: string) {

    // Colocar a chamada para a implementação do PopUp modal de aviso:
    alert(msg);
  }

  getEventos(): void {

    this.serviceEvento.getEventos().subscribe(eventos => this.eventos = eventos);
  }

  ngOnInit() {

    this._tipoIsencao = '1';

    this.getEventos();

    this._isencaoId = +this.route.snapshot.paramMap.get('id');

    if (this._isencaoId > 0) {
      this.isEdicaoIsencao = true;
      this.badge = '"Edição';
        this.getIsencaoById(this._isencaoId);
    } else {
      this.isEdicaoIsencao = false;
      this.badge = 'Novo';
    }
  }
}
