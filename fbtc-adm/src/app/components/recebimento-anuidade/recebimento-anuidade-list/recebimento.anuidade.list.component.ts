import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { RecebimentoService } from './../../shared/services/recebimento.service';
import { Recebimento } from './../../shared/model/recebimento';

import { Util } from './../../shared/util/util';

@Component({
  selector: 'app-recebimento-anuidade-list',
  templateUrl: './recebimento.anuidade.list.component.html',
  styleUrls: ['./recebimento.anuidade.list.component.css']
})
export class RecebimentoAnuidadeListComponent implements OnInit {

  title = 'Consulta de Pagamento de Anuidades';

  private selectedId: number;

  // private selectedAssociado: Associado;
  private selectedRecebimento: Recebimento;

  editNome: string;
  editCPF: string;
  editCRP: string;
  editCRM: string;
  editAno: number;
  editMes: string;
  editStatus: string;
  editAtivo: string;
  editDtVencimento: Date;
  editDtPagto: Date;

  recebimentos: Recebimento[];

  _util = Util;

  constructor(
      private service: RecebimentoService,
      private router: Router,
      private route: ActivatedRoute
  ) { }

  getRecebimentos(objRec): void {

    this.service.getAll(objRec).subscribe(recebimentos => this.recebimentos = recebimentos);
  }

  onSelect(recebimento: Recebimento): void {
    this.selectedRecebimento = recebimento;
  }

  gotoBuscarRecebimento() { }

  /** Called by Angular after AssociadoList component initialized */
  ngOnInit() {

    // 2: Anuidade.
    const objRecebimento = '2';
    this.getRecebimentos(objRecebimento);
  }
}
