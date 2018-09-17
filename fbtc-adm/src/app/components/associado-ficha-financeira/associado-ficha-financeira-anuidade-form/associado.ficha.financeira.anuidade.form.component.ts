import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { RecebimentoService } from '../../shared/services/recebimento.service';
import { Recebimento, RecebimentoAssociadoDao } from '../../shared/model/recebimento';
import { Endereco } from '../../shared/model/endereco';

@Component({
  selector: 'app-associado-ficha-financeira-anuidade-form',
  templateUrl: './associado.ficha.financeira.anuidade.form.component.html',
  styleUrls: ['./associado.ficha.financeira.anuidade.form.component.css']
})
export class AssociadoFichaFinanceiraAnuidadeFormComponent implements OnInit {

  enderecos: Endereco[];

  @Input() recebimento: Recebimento = { recebimentoId: 0, associadoId: 0, associadoIsentoId: 0, valorAnuidadePublicoId: 0,
    valorEventoPublicoId: 0, objetivoPagamento: '', dtNotificacao: null, observacao: '', codePS: '', referencePS: '', typePS: 0,
    statusPS: 99, lastEventDatePS: null, typePaymentMethodPS: 0, codePaymentMethodPS: 0, netAmountPS: 0,
    dtCadastro: null, ativo: true, dtVencimento: null,
      associado: { associadoId: 0, atcId: 0, tipoPublicoId: 0, nrMatricula: '', crp: '',
        crm: '', nomeInstFormacao: '', certificado: false, dtCertificacao: null, divulgarContato: false,
        tipoFormaContato: '', integraDiretoria: false, integraConfi: false, nrTelDivulgacao: '',
        comprovanteAfiliacaoAtc: '', tipoProfissao: '', tipoTitulacao: '',
        pessoaId: 0, nome: '', cpf: '', rg: '', eMail: '', nomeFoto: '',
        sexo: '', dtNascimento: null, nrCelular: '', passwordHash: '',
        dtCadastro: null, ativo: false, perfilId: 0,
          enderecosPessoa: this.enderecos}
  };

  @Input() recebimentoAssociadoDao: RecebimentoAssociadoDao = { associadoId: 0, titulo: '', anuidade: 0, nome: '',
  cpf: '', nomeTP: '', recebimentoId: 0, statusPS: 0, lastEventDatePS: null, ativoRec: false,
  isencaoIdId: 0, dtVencimento: null
};

  title: string;
  private selectedId: any;
  submitted: boolean;

  constructor(
    private service: RecebimentoService,
    private router: Router,
    private route: ActivatedRoute
) {
  this.title = 'Dados de pagamento da anuidade';
  this.submitted = false;

}

  getRecebimentoById(id: number): void {

    this.service.getById(id)
          .subscribe(recebimento => this.recebimento = recebimento);

    this.service.getPagamentoAssociadoByRecebimentoId(id)
          .subscribe(recebimento => this.recebimentoAssociadoDao = recebimento);
  }

  /*
  gotoSave() {

      this.service.addRecebimento(this.recebimento)
      .subscribe(() =>  this.gotoShowPopUp());

      this.submitted = false;
  }*/

  onSubmit() {
    this.submitted = true;
    // this.gotoSave();
  }

  /*
  gotoShowPopUp() {

    // Colocar a chamada para a implementação do PopUp modal de aviso:
    alert('Registro salvo com sucesso!');
  }*/

/*
  gotoNotificarAssociado() {

    if (confirm('Deseja notificar o Associado?')) {
      alert('Notificação enviada com sucesso');
    }
  }
*/
  gotoRecebimentoAnuidade() {

    const recebimentoId = this.recebimento ? this.recebimento.recebimentoId : null;
    this.router.navigate(['admin/AssociadoFichaFinanceira', { id: recebimentoId, foo: 'foo' }]);
  }

  ngOnInit() {

    const id = +this.route.snapshot.paramMap.get('id');
      if (id > 0) {
          this.getRecebimentoById(id);
    }
  }
}
