import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { RecebimentoService } from '../../shared/services/recebimento.service';
import { RecebimentoAssociadoDao } from '../../shared/model/recebimento';
import { Endereco } from '../../shared/model/endereco';
import { PdfService } from '../../shared/services/pdf.service';

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
                    grossAmountPS: 0, discountAmountPS: 0, feeAmountPS: 0, netAmountPS: 0, extraAmountPS: 0,
                    dtVencimento: null, statusFBTC: null, dtStatusFBTC: null, origemEmissaoTitulo: null,
                    dtCadastro: null, ativo: false};

  title: string;
  private selectedId: any;
  submitted: boolean;

  alertClassType: string;

  _msgProgresso: string;

  constructor(
    private service: RecebimentoService,
    private pdfService: PdfService,
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

  gotoRecebimentoAnuidade(): void {

    const recebimentoId = this.recebimentoDao ? this.recebimentoDao.recebimentoId : null;
    this.router.navigate(['admin/AssociadoFichaFinanceira', { id: recebimentoId, foo: 'foo' }]);
  }

  gotoGerarCertificadoAdimplencia(): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    this.pdfService.getDeclaracaoAdimplenciaAssociado(this.recebimentoDao.nome, this.recebimentoDao.anuidade);

    this.submitted = false;
  }

  ngOnInit(): void {

    const id = +this.route.snapshot.paramMap.get('id');
      if (id > 0) {
          this.getRecebimentoById(id);
    }
  }
}
