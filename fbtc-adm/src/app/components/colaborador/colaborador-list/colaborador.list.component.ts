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

  optBoolean = [
    {name: 'Todos', value: null},
    {name: 'Sim', value: true},
    {name: 'Não', value: false}
  ];

  title = 'Consulta de Colaborador';

  editAtivo: string = '';


  colaboradores: Colaborador[];

  private selectedColaborador: Colaborador;

  constructor(
    private service: ColaboradorService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getColaboradores(): void {
    this.service.getColaboradores().subscribe(colaboradores => this.colaboradores = colaboradores);
}

  ngOnInit() {
    this.getColaboradores();
  }

  onSelect(colaborador: Colaborador): void {
    this.selectedColaborador = colaborador;
}

  gotoNovoColaborador() {
      this.router.navigate(['/Colaborador', 0]);
  }

  gotoBuscarColaborador() { }

  excluir() {
    // this.gotoColaboradores();
}
}
