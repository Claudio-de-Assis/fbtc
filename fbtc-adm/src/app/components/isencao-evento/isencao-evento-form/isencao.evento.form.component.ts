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

  @Input() isencao: Isencao = { isencaoId: 0, anuidadeId: 0, eventoId : 0,
    descricao: '', dtAta: null, anoEvento: 0 , tipoIsencao: '1', ativo: true};

  title = 'Conceder Isenção de Evento';
  badge = '';

  eventos: Evento[];

  private selectedId: any;

  isencaos: Observable<Isencao>;

  _util = Util;

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

  save() {

    this.service.addIsencao(this.isencao).subscribe(() =>  this.gotoShowPopUp());
  }

  gotoShowPopUp() {
    // Colocar a chamada para a implementação do PopUp modal de aviso:
    alert('Registro salvo com sucesso!');
  }


  getEventos(): void {

    this.serviceEvento.getEventos().subscribe(eventos => this.eventos = eventos);
  }

  ngOnInit() {

    this.getEventos();

    const id = +this.route.snapshot.paramMap.get('id');
    if (id > 0) {
        this.badge = '"Edição';
        this.getIsencaoById(id);
    } else {
        this.badge = 'Novo';
        // this.setIsencao('1');
    }
  }
}
