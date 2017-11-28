import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { AssociadoService } from './../../shared/services/associado.service';
import { Associado } from '../../shared/model/associado';
import { Data } from '@angular/router/src/config';

@Component({
  selector: 'app-recebimento-evento-list',
  templateUrl: './recebimento.evento.list.component.html',
  styleUrls: ['./recebimento.evento.list.component.css'],
  providers: [AssociadoService]
})
export class RecebimentoEventoListComponent implements OnInit {

  lstStatus = ['Adimplente', 'Inadimplente'];
  lstEventos = ['Workshop internacinal....', 'Congresso BRasileiro....'];
  lstTipoPublico = ['Profissional - Associado', 'Profissional - Não Associado', 'Estudante de Pós - Associado',
    'Estudante de Pós - Não Associado', 'Estudante - Associado', 'Estudante - Não Associado'];

  editNome: string;
  editCPF: string;
  editCRP: string;
  editCRM: string;
  editEvento: string;
  editStatus: string;
  editTipoPublico: string;

  editDtVencimento: Data;
  editDtPagto: Data;

  title = 'Consulta de Pagamento de eventos';

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
    this.service.getAssociados().subscribe(associados => this.associados = associados);
  }

  onSelect(associado: Associado): void {
    this.selectedAssociado = associado;
  }

  gotoImprimirLista() {}

  /*
  gotoNovoAssociado() {
    this.router.navigate(['/Associado', 0]);
  }*/

  gotoBuscarAssociado() { }

  ngOnInit() {
    this.associado$ = this.route.paramMap.switchMap((params: ParamMap) => {
        this.selectedId = +params.get('Id');
        return this.service.getAssociados();
    });

    this.editDtPagto = new Date('01-01-2017');
    this.editDtVencimento = new Date('01-01-2017');
    this.editStatus = 'Adimplente';
    this.editEvento = 'Workshop Internacional...';
    this.editTipoPublico = 'Profissional - Associado';
  }
}
