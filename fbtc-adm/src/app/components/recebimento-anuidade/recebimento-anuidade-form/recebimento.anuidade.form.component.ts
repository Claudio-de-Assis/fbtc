import { Endereco } from '../../shared/model/endereco';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { RecebimentoService } from '../../shared/services/recebimento.service';
import { Recebimento, RecebimentoAssociadoDao } from '../../shared/model/recebimento';

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
                    netAmountPS: 0, dtVencimento: null, statusFBTC: null, dtStatusFBTC: null, origemEmissaoTitulo: null,
                    dtCadastro: null, ativo: false};

  title: string;
  private selectedId: any;
  submitted: boolean;

  _msg: string;
  _msgRetorno: string;
  _recebimentoId: number;

  alertClassType: string;

  _msgProgresso: string;

  constructor(
    private service: RecebimentoService,
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

  gotoSave() {

    this.alertClassType = 'alert alert-info';
    this._msg = 'Salvando os dados. Por favor, aguarde...';

    this.service.addRecebimento(this.recebimentoDao)
      .subscribe(
        msg => {
          this._msgRetorno = msg;
          this.avaliaRetorno(this._msgRetorno);
        }
      );
      this.submitted = false;
      this._msgRetorno = '';
      // this._msg = '';
      this._recebimentoId = 0;
  }

  avaliaRetorno(msgRet: string) {

    if (msgRet.substring(0, 1) === '0') {

        this._recebimentoId = parseInt(msgRet.substring(0, 10), 10);

        // this.router.navigate([`admin/Recebimento/${this._recebimentoId}`]);

        this.getRecebimentoAssociadoDaoByRecebimentoId(this._recebimentoId);

        this.alertClassType = 'alert alert-success';
        this._msg = this._msgRetorno.substring(10);

    } else {

      this.alertClassType = 'alert alert-success';
      this._msg = this._msgRetorno;
    }
  }

  onSubmit() {
    this.submitted = true;
    this.gotoSave();
  }

  gotoRecebimentoAnuidade() {

    const recebimentoId = this.recebimentoDao ? this.recebimentoDao.recebimentoId : null;
    this.router.navigate(['/admin/RecebimentoAnuidade', { id: recebimentoId, foo: 'foo' }]);
  }

  ngOnInit() {

    const id = +this.route.snapshot.paramMap.get('id');
      if (id > 0) {
          this.getRecebimentoAssociadoDaoByRecebimentoId(id);
    } else {
      alert('Não foi encontrato recebimento para o Id Informado');
      this.gotoRecebimentoAnuidade();
    }
  }
}
