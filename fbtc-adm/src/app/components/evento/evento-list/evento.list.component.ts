import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { EventoService } from './../../shared/services/evento.service';

import { Evento } from '../../shared/model/evento';

import { Util } from '../../shared/util/util';

@Component({
  selector: 'app-evento-list',
  templateUrl: './evento.list.component.html',
  styleUrls: ['./evento.list.component.css']
})
export class EventoListComponent implements OnInit {

  title = 'Consulta de Eventos';

  editNome: string = '';
  editAno: number = null;
  editTipoEvento: string = '';

  private selectedEvento: Evento;

  _nome: string = '0';
  _ano: number = 0;
  _tipoEvento: string = '0';

  eventos: Evento[];

  _util = Util;

  submitted = false;

  constructor(
    private service: EventoService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getEventos(): void {
    this.service.getEventos().subscribe(eventos => this.eventos = eventos);
  }

  onSubmit() {

    this.submitted = true;
    this.gotoBuscareventos();
  }

  ngOnInit() {
    this.getEventos();
  }

  onSelect(evento: Evento): void {
    this.selectedEvento = evento;
  }

  gotoNovoEvento() {
      this.router.navigate(['/Evento', 0]);
  }

  gotoBuscareventos() {

    if (this.editNome.trim() !== '') {
      this._nome = this.editNome.trim();
    }
    if (this.editAno !== null) {
        this._ano = this.editAno;
    }
    if (this.editTipoEvento !== '') {
        this._tipoEvento = this.editTipoEvento;
    }

    this.service.getByFilters(this._nome, this._ano, this._tipoEvento)
        .subscribe(eventos => this.eventos = eventos);

    this.submitted = false;
    this._nome = '0';
    this._ano = 0;
    this._tipoEvento = '0';
  }
}
