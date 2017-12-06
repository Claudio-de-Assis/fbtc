import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

// import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { TipoPublico } from './../../shared/model/tipo-publico';
import { Evento } from '../../shared/model/evento';
import { EventoService } from '../../shared/services/evento.service';

@Component({
  selector: 'app-evento-preview',
  templateUrl: './evento-preview.component.html',
  styleUrls: ['./evento-preview.component.css']
})
export class EventoPreviewComponent implements OnInit {

  title: 'Dados do Evento - Preview';

  private selectedId: any;

  evento$: Observable<Evento>;
  evento: Evento;

  tipoPublico$: Observable<TipoPublico[]>;
  tiposPublicos: TipoPublico[];
  tipoPublico: TipoPublico;

  editEventoId: number;
  editTitulo: string;
  editDescricao: string;
  editCodigo: string;
  editDtInicio: Date;
  editDtTermino: Date;
  editDtTerminoInscricao: Date;
  editTipoEvento: string;
  editAceitaIsencaoAta: boolean;
  editAtivo: boolean;
  editNomeFoto: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    // private serviceTP: TipoPublicoService,
    private service: EventoService,
  ) { }

  gotoEvento() {
    this.router.navigate(['/Evento', this.editEventoId]);
  }

  ngOnInit() {

  }
}
