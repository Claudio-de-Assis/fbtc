import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { Util } from './../../shared/util/util';

import { RecebimentoService } from '../../shared/services/recebimento.service';
import { RecebimentoAssociadoDao } from '../../shared/model/recebimento';
import { Endereco } from '../../shared/model/endereco';

import { PdfService } from '../../shared/services/pdf.service';

@Component({
  selector: 'app-recebimento-anuidade-form',
  templateUrl: './recebimento.anuidade.form.component.html',
  styleUrls: ['./recebimento.anuidade.form.component.css']
})
export class RecebimentoAnuidadeFormComponent implements OnInit {

  enderecos: Endereco[];

  @Input() recebimentoDao: RecebimentoAssociadoDao = { titulo: '', anuidade: null, nome: '', cpf: '', nomeTP: '',
                    eMail: '', nrCelular: '', ativoAssociado: false, recebimentoId: 0, assinaturaAnuidadeId: null,
                    assinaturaEventoId: null, observacao: '', notificationCodePS: '', typePS: null,
                    statusPS: null, lastEventDatePS: null, typePaymentMethodPS: null, codePaymentMethodPS: null,
                    grossAmountPS: 0, discountAmountPS: 0, feeAmountPS: 0, netAmountPS: 0, extraAmountPS: 0,
                    dtVencimento: null, statusFBTC: null, dtStatusFBTC: null, origemEmissaoTitulo: null,
                    dtCadastro: null, ativo: false};

  title: string;
  submitted: boolean;

  _msg: string;
  _msgRetorno: string;
  _recebimentoId: number;

  _util = Util;

  alertClassType: string;

  _msgProgresso: string;

  constructor(
    private service: RecebimentoService,
    private pdfService: PdfService,
    private router: Router,
    private route: ActivatedRoute
) {
  this.title = 'Dados de pagamento de anuidade do associado';
  this.submitted = false;

  this._msgRetorno = '';
  this._msg = '';
  this._recebimentoId = 0;

  this.alertClassType = 'alert alert-info';

  this._msgProgresso = '';

}

  getRecebimentoAssociadoDaoByRecebimentoId(id: number): void {

    this._msgProgresso = '...Carregando os dados. Por favor, aguarde!...';

    this.service.getRecebimentoAssociadoDaoByRecebimentoId(id)
          .subscribe(recebimentoDao => {
            this.recebimentoDao = recebimentoDao;
            this._msgProgresso = '';
          });
  }

  gotoSave(): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    this.alertClassType = 'alert alert-info';
    this._msg = 'Salvando os dados. Por favor, aguarde...';

    this.service.addRecebimento(this.recebimentoDao)
      .subscribe(
        msg => {
          this._msgRetorno = msg;
          this.avaliaRetorno(this._msgRetorno);
          this.submitted = false;
        }
      );
      this._msgRetorno = '';
      this._recebimentoId = 0;
  }

  avaliaRetorno(msgRet: string): void {

    if (msgRet.substring(0, 1) === '0') {

        this._recebimentoId = parseInt(msgRet.substring(0, 10), 10);

        this.getRecebimentoAssociadoDaoByRecebimentoId(this._recebimentoId);

        this.alertClassType = 'alert alert-success';
        this._msg = this._msgRetorno.substring(10);

    } else {

      this.alertClassType = 'alert alert-success';
      this._msg = this._msgRetorno;
    }
  }

  onSubmit(): void {

    this.gotoSave();
  }

  gotoRecebimentoAnuidade(): void {

    const recebimentoId = this.recebimentoDao ? this.recebimentoDao.recebimentoId : null;
    this.router.navigate(['/admin/RecebimentoAnuidade', { id: recebimentoId, foo: 'foo' }]);
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
          this.getRecebimentoAssociadoDaoByRecebimentoId(id);
    } else {
      alert('Não foi encontrato recebimento para o Id Informado');
      this.gotoRecebimentoAnuidade();
    }
  }
}
