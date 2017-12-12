import { Util } from './../../shared/util/util';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { Isencao } from './../../shared/model/isencao';
import { IsencaoService } from '../../shared/services/isencao.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { TipoPublico } from '../../shared/model/tipo-publico';

@Component({
  selector: 'app-isencao-anuidade-list',
  templateUrl: './isencao.anuidade.list.component.html',
  styleUrls: ['./isencao.anuidade.list.component.css']
})
export class IsencaoAnuidadeListComponent implements OnInit {

  title = 'Consulta de Isenção de Anuidade';

  _util = Util;

  isencoes: Isencao[];

  private selectedId: number;

  private selectedIsencao: Isencao;

  tiposPublicos: TipoPublico[];

  constructor(
    private service: IsencaoService,
    private serviceTP: TipoPublicoService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  gotoImprimirLista() {}

  getIsencoes(objIsen: string): void {

    this.service.getAll(objIsen).subscribe(isencoes => this.isencoes = isencoes);
  }

  onSelect(isencao: Isencao): void {
    this.selectedIsencao = isencao;
  }

  gotoBuscarIsencao() { }

  gotoNovaIsencao() {

    this.router.navigate(['/IsencaoEvento', 0]);
  }

  getTiposPublicos(): void {

    this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }

  ngOnInit() {

      this.getTiposPublicos();

      // 2: Anuidade.
      const objIsencao = '2';
      this.getIsencoes(objIsencao);
  }
}
