import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Data } from '@angular/router/src/config';

import { AssociadoService } from './../../shared/services/associado.service';
import { Associado } from '../../shared/model/associado';

import { Recebimento } from './../../shared/model/recebimento';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { TipoPublico } from '../../shared/model/tipo-publico';
import { RecebimentoService } from '../../shared/services/recebimento.service';

import { Util } from '../../shared/util/util';

@Component({
  selector: 'app-recebimento-evento-list',
  templateUrl: './recebimento.evento.list.component.html',
  styleUrls: ['./recebimento.evento.list.component.css'],
  providers: [AssociadoService]
})
export class RecebimentoEventoListComponent implements OnInit {

  title = 'Consulta de pagamento de eventos';

  _util = Util;

  private selectedId: number;

  private selectedRecebimento: Recebimento;

  tiposPublicos: TipoPublico[];

  recebimentos: Recebimento[];

  editNome: string;
  editCPF: string;
  editCRP: string;
  editCRM: string;
  editEvento: string;
  editStatus: string;
  editTipoPublico: string;

  editDtVencimento: Data;
  editDtPagto: Data;

  /** AssociadoList ctor */
  constructor(
      private service: RecebimentoService,
      private serviceTP: TipoPublicoService,
      private router: Router,
      private route: ActivatedRoute
  ) { }

  gotoImprimirLista() {}

  getRecebimentos(objRec: string): void {

    this.service.getAll(objRec).subscribe(recebimentos => this.recebimentos = recebimentos);
  }

  onSelect(recebimento: Recebimento): void {
    this.selectedRecebimento = recebimento;
  }

  gotoBuscarRecebimento() { }

  getTiposPublicos(): void {

    this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }

  ngOnInit() {

    this.getTiposPublicos();

    // 1: Eventos.
    const objRecebimento = '1';
    this.getRecebimentos(objRecebimento);
  }
}
