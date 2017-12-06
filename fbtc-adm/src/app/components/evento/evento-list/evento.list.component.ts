import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { Evento } from '../../shared/model/evento';
import { EventoService } from './../../shared/services/evento.service';


@Component({
  selector: 'app-evento-list',
  templateUrl: './evento.list.component.html',
  styleUrls: ['./evento.list.component.css']
})
export class EventoListComponent implements OnInit {

  lstAno = [2018, 2017, 2016];
  lstTipoEvento = ['Congresso Brasileiro', 'Workshop Internacional'];

  title = 'Consulta de Eventos';

  evento$: Observable<Evento[]>;
  eventos: Evento[];
  private selectedEvento: Evento;

  private selectedId: number;

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
