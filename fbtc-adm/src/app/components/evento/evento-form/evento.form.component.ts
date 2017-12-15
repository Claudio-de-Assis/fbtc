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
import { TipoPublico } from './../../shared/model/tipo-publico';

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
            ativo: true, nomeFoto: ''
  };

  title = 'Evento';
  badge = '';

  _util = Util;

  private selectedId: any;

  tiposPublicos: TipoPublico[];

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

  getTiposPublicos(): void {

    this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }

  gotoEventos() {

    let eventoId = this.evento ? this.evento.eventoId : null;
    this.router.navigate(['/Evento', { id: eventoId, foo: 'foo' }]);
  }

  gotoSaveEvento() {

    this.service.addEvento(this.evento)
    .subscribe(() =>  this.gotoShowPopUp());
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

    this.getTiposPublicos();

    const id = +this.route.snapshot.paramMap.get('id');
    if (id > 0) {
      this.badge = 'Edição';
      this.getEventoById(id);
    } else {
      this.badge = 'Novo';
      // this.setEvento();
    }
  }
}
