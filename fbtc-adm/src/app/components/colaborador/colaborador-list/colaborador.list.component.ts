import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { Colaborador } from './../../shared/model/colaborador';
import { ColaboradorService } from '../../shared/services/colaborador.service';

@Component({
  selector: 'app-colaborador-list',
  templateUrl: './colaborador.list.component.html',
  styleUrls: ['./colaborador.list.component.css']
})
export class ColaboradorListComponent implements OnInit {

  lstPerfil = ['Gestor do Site', 'Secretaria', 'Financeiro'];
  lstStatus = ['Todos', 'Sim', 'Não'];

  title = 'Consulta de Usários';

  colaborador$: Observable<Colaborador[]>;

  colaboradores: Colaborador[];
  private selectedColaborador: Colaborador;

  private selectedId: number;

  constructor(
    private service: ColaboradorService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getColaboradores(): void {
    this.service.getColaboradores().then(colaboradores => this.colaboradores = colaboradores);
}

  ngOnInit() {
      this.colaborador$ = this.route.paramMap.switchMap((params: ParamMap) => {
        this.selectedId = +params.get('Id');
        return this.service.getColaboradores();
    });
  }

  onSelect(colaborador: Colaborador): void {
    this.selectedColaborador = colaborador;
}

  gotoNovoColaborador() {
      this.router.navigate(['/ColaboradorNovo']);
  }

  gotoBuscarColaborador() { }
}
