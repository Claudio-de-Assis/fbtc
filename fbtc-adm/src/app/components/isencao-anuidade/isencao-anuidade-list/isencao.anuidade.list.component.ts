import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { Isencao } from './../../shared/model/isencao';
import { IsencaoAnuidadeService } from '../../shared/services/isencao-anuidade.service';

@Component({
  selector: 'app-isencao-anuidade-list',
  templateUrl: './isencao.anuidade.list.component.html',
  styleUrls: ['./isencao.anuidade.list.component.css']
})
export class IsencaoAnuidadeListComponent implements OnInit {

  lstAno = ['2018', '2017', '2016', '2015'];

  title = 'Consulta de Isenção de Anuidade';

  isencao$: Observable<Isencao[]>;

  isencoes: Isencao[];
  private selectedIsencao: Isencao;

  private selectedId: number;

  constructor(
    private service: IsencaoAnuidadeService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getIsecoes(): void {
    this.service.getIsencoesAnuidade().then(isencoes => this.isencoes = isencoes);
}

  ngOnInit() {
      this.isencao$ = this.route.paramMap.switchMap((params: ParamMap) => {
        this.selectedId = +params.get('Id');
        return this.service.getIsencoesAnuidade();
    });
  }

  onSelect(isencao: Isencao): void {
    this.selectedIsencao = isencao;
}

  gotoNovaIsencao() {
      this.router.navigate(['/IsencaoAnuidadeNova']);
  }

  gotoBuscarIsencao() { }

}
