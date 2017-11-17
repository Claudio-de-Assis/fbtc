import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { TipoPublico } from './../../shared/model/tipo.publico';
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
    private serviceTP: TipoPublicoService,
    private service: EventoService,
  ) { }

  gotoEvento() {
    this.router.navigate(['/Evento', this.editEventoId]);
  }

  ngOnInit() {
    this.evento$ = this.route.paramMap
      .switchMap((params: ParamMap) => this.service.getEventoById(params.get('id')));

    this.evento$.subscribe((evento: Evento) => {this.evento = evento});

    this.editEventoId = this.evento ? this.evento.EventoId : 0;
    this.editTitulo = this.evento ? this.evento.Titulo : '';
    this.editDescricao = this.evento ? this.evento.Descricao : '';
    this.editCodigo = this.evento ? this.evento.Codigo : '';
    this.editDtInicio = this.evento ? this.evento.DtInicio : null;
    this.editDtTermino = this.evento ? this.evento.DtTermino : null;
    this.editDtTerminoInscricao = this.evento ? this.evento.DtTerminoInscricao : null;
    this.editTipoEvento = this.evento ? this.evento.TipoEvento : '';
    this.editAceitaIsencaoAta = this.evento ? this.evento.AceitaIsencaoAta : true;
    this.editAtivo = this.evento ? this.evento.Ativo : true;
    this.editNomeFoto = this.evento ? this.evento.NomeFoto : '';

    this.tipoPublico$ = this.route.paramMap
    .switchMap((params: ParamMap) => this.serviceTP.getTiposPublicos());
  }
}
