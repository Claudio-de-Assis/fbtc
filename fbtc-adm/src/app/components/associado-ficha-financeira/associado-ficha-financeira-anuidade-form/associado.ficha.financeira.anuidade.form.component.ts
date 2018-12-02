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

  @Input() recebimentoDao: RecebimentoAssociadoDao = { titulo: '', anuidade: null, nome: '', cpf: '', nomeTP: '',
                    eMail: '', nrCelular: '', ativoAssociado: false, recebimentoId: 0, assinaturaAnuidadeId: null,
                    assinaturaEventoId: null, observacao: '', notificationCodePS: '', typePS: null,
                    statusPS: null, lastEventDatePS: null, typePaymentMethodPS: null, codePaymentMethodPS: null,
                    netAmountPS: 0, dtVencimento: null, statusFBTC: null, dtStatusFBTC: null, origemEmissaoTitulo: null,
                    dtCadastro: null, ativo: false};

  title: string;
  private selectedId: any;
  submitted: boolean;

  alertClassType: string;

  _msgProgresso: string;

  constructor(
    private service: RecebimentoService,
    private router: Router,
    private route: ActivatedRoute
) {
  this.title = 'Dados de pagamento da anuidade';
  this.submitted = false;

  this.alertClassType = 'alert alert-info';

  this._msgProgresso = '';
}

  getRecebimentoById(id: number): void {

    this._msgProgresso = '...Carregando os dados. Por favor, aguarde!...';

    this.service.getPagamentoAssociadoByRecebimentoId(id)
          .subscribe(recebimentoDao => {
            this.recebimentoDao = recebimentoDao;
            this._msgProgresso = '';
          });
  }

  onSubmit() {
    this.submitted = true;
    // this.gotoSave();
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
