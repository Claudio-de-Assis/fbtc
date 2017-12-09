import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { RecebimentoAnuidadeService } from './../../shared/services/recebimento-anuidade.service';
// import { AssociadoService } from './../../shared/services/associado.service';
import { Recebimento } from './../../shared/model/recebimento';
// import { Associado } from '../../shared/model/associado';

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

  editNome: string = '';
  editCPF: string = '';
  editCRP: string = '';
  editCRM: string = '';
  editAno: number = 0;
  editMes: string = '';
  editStatus: string = '';
  editAtivo: string = '';
  editDtVencimento: Date = null;
  editDtPagto: Date = null;

  // cassociados: Associado[];
  recebimentos: Recebimento[];

  lstAno = [2018, 2017, 2016, 2015];
  lstMes = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'];
  lstStatus = ['Adimplente', 'Inadimplente'];

  optBoolean = [
    {name: 'Todos', value: null},
    {name: 'Sim', value: true},
    {name: 'Não', value: false}
  ];

  /** AssociadoList ctor */
  constructor(
      private service: RecebimentoAnuidadeService,
//    private serviceAssoc: AssociadoService,
      private router: Router,
      private route: ActivatedRoute
  ) { }

  /*getAssociados(): void {

    this.serviceAssoc.getAssociados().subscribe(associados => this.associados = associados);
  }*/

  getRecebimentos(objRec): void {

    this.service.getAll(objRec).subscribe(recebimentos => this.recebimentos = recebimentos);
  }

  onSelect(recebimento: Recebimento): void {
    this.selectedRecebimento = recebimento;
  }

  gotoBuscarAssociado() { }

  gotoBuscarRecebimento() { }

  /** Called by Angular after AssociadoList component initialized */
  ngOnInit() {

    // this.getAssociados();

    const objRecebimento = '2'; // 2: Anuidade.
    this.getRecebimentos(objRecebimento);
  }
}
