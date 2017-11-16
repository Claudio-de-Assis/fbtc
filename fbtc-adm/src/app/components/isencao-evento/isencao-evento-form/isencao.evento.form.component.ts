import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { Isencao } from '../../shared/model/isencao';
import { IsencaoEventoService } from '../../shared/services/isencao-evento.service';

@Component({
  selector: 'app-isencao-evento-form',
  templateUrl: './isencao.evento.form.component.html',
  styleUrls: ['./isencao.evento.form.component.css']
})
export class IsencaoEventoFormComponent implements OnInit {

  lstAno = ['2018', '2017', '2016', '2015'];

  private selectedId: any;

  title = 'Conceder Isenção de Evento';

  isencao$: Observable<Isencao>;

  isencao: Isencao;

  editIsecaoId: number;
  editAnuidadeId: number;
  editEventoId: number;
  editDescricao: string;
  editDtAta: Date;
  editAnoEvento: number;
  editTipoIsencao: string;
  editAtivo: boolean;

  editTitulo: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: IsencaoEventoService
  ) { }

  ngOnInit() {
    this.isencao$ = this.route.paramMap
    .switchMap((params: ParamMap) => this.service.getIsencaoEventoById(params.get('id')));

    this.isencao$.subscribe((isencao: Isencao) => {this.isencao = isencao});

    this.editIsecaoId = this.isencao ? this.isencao.IsencaoId : 0;
    this.editAnuidadeId = this.isencao ? this.isencao.AnuidadeId : 0;
    this.editEventoId = this.isencao ? this.isencao.EventoId : 0;
    this.editDescricao = this.isencao ? this.isencao.Descricao : '';
    this.editDtAta = this.isencao ? this.isencao.DtAta : null;
    this.editAnoEvento = this.isencao ? this.isencao.AnoEvento : 0;
    this.editTipoIsencao = this.isencao ? this.isencao.TipoIsencao : '';
    this.editAtivo = this.isencao ? this.isencao.Ativo : false;

    this.editTitulo = 'Evento .....';
  }

  gotoEventos() {
    let eventoId = this.isencao ? this.isencao.IsencaoId : null;
    this.router.navigate(['/IsencaoEvento', { id: eventoId, foo: 'foo' }]);
  }
  save() {
      this.gotoEventos();
  }
  excluir() {
      this.gotoEventos();
  }
}
