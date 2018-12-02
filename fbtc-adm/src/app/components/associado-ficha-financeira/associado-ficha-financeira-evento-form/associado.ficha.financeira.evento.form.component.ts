import { RecebimentoAssociadoDao } from './../../shared/model/recebimento';
import { ReactiveFormsModule } from '@angular/forms';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { RecebimentoService } from '../../shared/services/recebimento.service';
import { Recebimento } from '../../shared/model/recebimento';
import { Endereco } from '../../shared/model/endereco';

@Component({
  selector: 'app-associado-ficha-financeira-evento-form',
  templateUrl: './associado.ficha.financeira.evento.form.component.html',
  styleUrls: ['./associado.ficha.financeira.evento.form.component.css']
})
export class AssociadoFichaFinanceiraEventoFormComponent implements OnInit {

   enderecos: Endereco[];

  @Input() recebimentoDao: RecebimentoAssociadoDao = { titulo: '', anuidade: null, nome: '', cpf: '', nomeTP: '',
                    eMail: '', nrCelular: '', ativoAssociado: false, recebimentoId: 0, assinaturaAnuidadeId: null,
                    assinaturaEventoId: null, observacao: '', notificationCodePS: '', typePS: null,
                    statusPS: null, lastEventDatePS: null, typePaymentMethodPS: null, codePaymentMethodPS: null,
                    netAmountPS: 0, dtVencimento: null, statusFBTC: null, dtStatusFBTC: null, origemEmissaoTitulo: null,
                    dtCadastro: null, ativo: false};

  title: string;
  private selectedId: any;
  submitted: boolean;

  constructor(
    private service: RecebimentoService,
    private router: Router,
    private route: ActivatedRoute
) {
  this.title = 'Dados de pagamento do evento';
  this.submitted = false;

}

  getRecebimentoById(id: number): void {

    this.service.getPagamentoAssociadoByRecebimentoId(id)
          .subscribe(recebimentoDao => this.recebimentoDao = recebimentoDao);
   }

  onSubmit() {
    this.submitted = true;
  }

  gotoRecebimentoAnuidade() {

    const recebimentoId = this.recebimentoDao ? this.recebimentoDao.recebimentoId : null;
    this.router.navigate(['admin/AssociadoFichaFinanceira', { id: recebimentoId, foo: 'foo' }]);
  }

  ngOnInit() {

    const id = +this.route.snapshot.paramMap.get('id');
      if (id > 0) {
          this.getRecebimentoById(id);
    }
  }
}
