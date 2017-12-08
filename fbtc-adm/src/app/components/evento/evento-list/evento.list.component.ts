import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { EventoService } from './../../shared/services/evento.service';

import { Evento } from '../../shared/model/evento';

@Component({
  selector: 'app-evento-list',
  templateUrl: './evento.list.component.html',
  styleUrls: ['./evento.list.component.css']
})
export class EventoListComponent implements OnInit {

  title = 'Consulta de Eventos';

  private selectedEvento: Evento;

  editTipoEvento: string = '0';
  editAno: string = '';
  editNome: string = '';

  eventos: Evento[];

  lstAno = [2018, 2017, 2016];

  optTiposEventos = [
    {name: 'Workshop Internacional', value: '2'},
    {name: 'Congresso Brasileiro', value: '4'},
  ];

  constructor(
    private service: EventoService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getEventos(): void {
    this.service.getEventos().subscribe(eventos => this.eventos = eventos);
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

  gotoBuscareventos() { }
}
