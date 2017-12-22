import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';


import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { EventoService } from '../../shared/services/evento.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';

import { Evento } from '../../shared/model/evento';
import { TipoPublicoValorDao } from './../../shared/model/tipo-publico';

import { Util } from './../../shared/util/util';

@Component({
  selector: 'app-evento-form',
  templateUrl: './evento.form.component.html',
  styleUrls: ['./evento.form.component.css'],
  providers: [TipoPublicoService]
})
export class EventoFormComponent implements OnInit {

  @Input() evento: Evento = { eventoId: 0, titulo: '', descricao: '', codigo: '', dtInicio: null,
            dtTermino: null, dtTerminoInscricao: null, tipoEvento: '', aceitaIsencaoAta: false,
            ativo: false, nomeFoto: ''
  };

  title = 'Evento';
  badge = '';

  _util = Util;

  private selectedId: any;

  @Input() tiposPublicosValoresDao: TipoPublicoValorDao[];

  submitted = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private serviceTP: TipoPublicoService,
    private service: EventoService,
  ) { }

  getEventoById(id: number): void {

      this.service.getById(id)
          .subscribe(evento => this.evento = evento);
  }

  setEvento(): void {

      this.service.setEvento()
          .subscribe(evento => this.evento = evento);
  }

  getTiposPublicos(id: number): void {

    this.serviceTP.getTiposPublicoByEventoId(id).
      subscribe(tiposPublicosValoresDao => this.tiposPublicosValoresDao = tiposPublicosValoresDao);
  }

  gotoEventos() {

    let eventoId = this.evento ? this.evento.eventoId : null;
    this.router.navigate(['/Evento', { id: eventoId, foo: 'foo' }]);
  }

  SaveEvento() {

    this.service.addEvento(this.evento)
    .subscribe(() =>  this.SaveValoresEvento());
  }

  SaveValoresEvento() {

    if (this.evento.eventoId !== 0 ) {
      this.service.addValoresEvento(this.tiposPublicosValoresDao)
      .subscribe(() =>  this.gotoShowPopUp());
    } else {
      this.gotoShowPopUp();
      this.gotoEventos();
    }
    this.submitted = false;
  }

  onSubmit() {

      this.submitted = true;
      this.SaveEvento();
  }


  gotoShowPopUp() {

    // Colocar a chamada para a implementação do PopUp modal de aviso:
    alert('Registro salvo com sucesso!');
  }

  /*gotoDeleteEvento() {
    if (confirm('Confirma a exclusão do registro?')) {
      alert(this.service.DeleteEvento(this.editEventoId));
      this.gotoEventos();
    }
  }*/

  gotoPreviewAnuncio() {

    this.router.navigate(['/EventoPreview', +this.route.snapshot.paramMap.get('id') ]);
  }

  ngOnInit() {

    const id = +this.route.snapshot.paramMap.get('id');


    if (id > 0) {
      this.badge = 'Edição';
      this.getEventoById(id);
      this.getTiposPublicos(id);
    } else {
      this.badge = 'Novo';
    }
  }
}
