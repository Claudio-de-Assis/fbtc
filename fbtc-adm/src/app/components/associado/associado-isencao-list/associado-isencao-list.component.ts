import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';

import { Associado } from '../../shared/model/associado';
import { AssociadoService } from '../../shared/services/associado.service';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

@Component({
  selector: 'app-associado-isencao-list',
  templateUrl: './associado-isencao-list.component.html',
  styleUrls: ['./associado-isencao-list.component.css']
})
export class AssociadoIsencaoListComponent implements OnInit {

  lstAtc = ['Rio de Janeiro', 'Alagoas', 'São Paulo'];
  lstTipoPublico= ['Profissional - Associado', 'Estudante de Pós - Associado', 'Estudante - Associado'];

  title = 'Pesquisa de Associados';

  associado$: Observable<Associado[]>;

  associados: Associado[];
  private selectedAssociado: Associado;

  private selectedId: number;

  /** AssociadoList ctor */
  constructor(
      private service: AssociadoService,
      private router: Router,
      private route: ActivatedRoute
  ) { }

  getAssociados(): void {
      this.service.getAssociados().then(associados => this.associados = associados);
  }

  /** Called by Angular after AssociadoList component initialized */
  ngOnInit() {
      this.associado$ = this.route.paramMap.switchMap((params: ParamMap) => {
          this.selectedId = +params.get('Id');
          return this.service.getAssociados();
      });
  }

  onSelect(associado: Associado): void {
      this.selectedAssociado = associado;
  }

  gotoBuscarAssociado() { }

}
