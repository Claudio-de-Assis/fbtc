import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { AssociadoService } from '../../shared/services/associado.service';
import { Associado } from '../../shared/model/associado';
import { Data } from '@angular/router/src/config';

@Component({
  selector: 'app-recebimento-evento-form',
  templateUrl: './recebimento.evento.form.component.html',
  styleUrls: ['./recebimento.evento.form.component.css']
})
export class RecebimentoEventoFormComponent implements OnInit {

  private selectedId: any;

  associado$: Observable<Associado>;
  associado: Associado;

  editAssociadoId: number;
  editNome: string;
  editCPF: string;
  editCRP: string;
  editCRM: string;
  editAno: number;
  editMes: string;
  editStatus: string;
  editAtivo: string;
  editEMail: string;
  editCelular: string;
  editDtVencimento: Data;
  editDtPagamento: Date;
  editStatusPagto: string;
  editFormaPagto: string;
  editNrDocCobranca: string;
  editValorDevido: number;
  editDtNotificacao: Data;
  editObservacao: string;

  editNomeEvento: string;
  editTipoPublico: string;


  title = 'Dados de pagamento de evento do associado';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: AssociadoService
  ) { }

  gotoRecebimentoAnuidade() {
    let associadoId = this.associado ? this.associado.associadoId : null;
    // Pass along the Associado id if available
    // so that the AssociadoList component can select that Associado.
    // Include a junk 'foo' property for fun.
    this.router.navigate(['/RecebimentoEvento', { id: associadoId, foo: 'foo' }]);
  }

  gotoSave() {
    alert('Registro salvo com sucesso');
    this.gotoRecebimentoAnuidade();
  }

  gotoNotificarAssociado() {
    if (confirm('Deseja notificar o Associado?')) {
      alert('Notificação enviada com sucesso');
    }
  }

  ngOnInit() {
    // this.associado$ = this.route.paramMap
     //   .switchMap((params: ParamMap) => this.service.getAssociadoById(params.get(id)));

        this.associado$.subscribe((associado: Associado) => {this.associado = associado});

        this.editAssociadoId = this.associado ? this.associado.associadoId : 0;
        this.editNome = this.associado ?  this.associado.nome : '';
        this.editEMail = this.associado ?  this.associado.eMail : '';
        this.editCelular = this.associado ?  this.associado.nrCelular : '';
        this.editCPF = this.associado ?  this.associado.cpf : '';
        this.editCRP = this.associado ?  this.associado.crp : '';
        this.editCRM = this.associado ?  this.associado.crm : '';

        this.editStatusPagto = 'Adimplente';
        this.editDtPagamento = new Date('2017-01-01');
        this.editDtVencimento = new Date('2017-01-01');
        this.editAtivo = 'Sim';
        this.editFormaPagto = 'PagSeguro';
        this.editNrDocCobranca = '123456789';
        this.editValorDevido = 180.00;
        this.editObservacao = 'Cliente .....';


        this.editNomeEvento = 'Workshop Internacional....';
        this.editTipoPublico = 'Profissional - Associado';
  }
}
