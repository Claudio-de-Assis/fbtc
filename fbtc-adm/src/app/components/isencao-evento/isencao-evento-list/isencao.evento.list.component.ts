import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { Isencao } from './../../shared/model/isencao';
import { IsencaoEventoService } from '../../shared/services/isencao-evento.service';

@Component({
  selector: 'app-isencao-evento-list',
  templateUrl: './isencao.evento.list.component.html',
  styleUrls: ['./isencao.evento.list.component.css']
})
export class IsencaoEventoListComponent implements OnInit {

  lstAno = ['2018', '2017', '2016', '2015'];

  lstEvento = ['Tratamento Cognitivo Comportamental para Transtorno...',
    'Evento XPTO Anual para Debate...', 'Evento Anual para Elicitação...'];

    title = 'Consulta de Isenção de Evento';

    isencao$: Observable<Isencao[]>;

    isencoes: Isencao[];
    private selectedIsencao: Isencao;

    private selectedId: number;

    constructor(
      private service: IsencaoEventoService,
      private router: Router,
      private route: ActivatedRoute
    ) { }

    getIsecoes(): void {
      this.service.getIsencoesEventos().then(isencoes => this.isencoes = isencoes);
  }

    ngOnInit() {
        this.isencao$ = this.route.paramMap.switchMap((params: ParamMap) => {
          this.selectedId = +params.get('Id');
          return this.service.getIsencoesEventos();
      });
    }

    onSelect(isencao: Isencao): void {
      this.selectedIsencao = isencao;
  }

    gotoNovaIsencao() {
        this.router.navigate(['/IsencaoEventoNova']);
    }

    gotoBuscarIsencao() { }

}
