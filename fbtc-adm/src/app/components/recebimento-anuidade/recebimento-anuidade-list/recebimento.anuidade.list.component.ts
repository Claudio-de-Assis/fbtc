import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { AssociadoService } from './../../shared/services/associado.service';
import { Associado } from '../../shared/model/associado';
import { Data } from '@angular/router/src/config';

@Component({
  selector: 'app-recebimento-anuidade-list',
  templateUrl: './recebimento.anuidade.list.component.html',
  styleUrls: ['./recebimento.anuidade.list.component.css'],
  providers: [AssociadoService]
})
export class RecebimentoAnuidadeListComponent implements OnInit {

  lstAno = [2018, 2017, 2016, 2015];
  lstMes = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'];
  lstStatus = ['Adimplente', 'Inadimplente'];
  lstAtivo = ['Sim', 'Não'];

  editNome: string;
  editCPF: string;
  editCRP: string;
  editCRM: string;
  editAno: number;
  editMes: string;
  editStatus: string;
  editAtivo: string;

  editDtVencimento: Data;
  editDtPagto: Data;

  title = 'Consulta de Pagamento de Anuidades';

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

  /*
  gotoNovoAssociado() {
    this.router.navigate(['/Associado', 0]);
  }*/

  gotoBuscarAssociado() { }

  /** Called by Angular after AssociadoList component initialized */
  ngOnInit() {
    this.associado$ = this.route.paramMap.switchMap((params: ParamMap) => {
        this.selectedId = +params.get('Id');
        return this.service.getAssociados();
    });

    this.editDtPagto = new Date('01-01-2017');
    this.editDtVencimento = new Date('01-01-2017');
    this.editStatus = 'Adimplente';
    this.editAtivo = 'Sim';
    this.editAno = 2017;
    this.editMes = '11';
  }
}
