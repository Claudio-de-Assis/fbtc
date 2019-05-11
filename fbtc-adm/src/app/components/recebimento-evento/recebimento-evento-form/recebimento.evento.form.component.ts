import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { Data } from '@angular/router/src/config';

import { RecebimentoService } from '../../shared/services/recebimento.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { EventoService } from '../../shared/services/evento.service';

import { Recebimento } from '../../shared/model/recebimento';
import { TipoPublico } from '../../shared/model/tipo-publico';
import { Evento } from '../../shared/model/evento';

import { RecebimentoAssociadoDao } from './../../shared/model/recebimento';

@Component({
  selector: 'app-recebimento-evento-form',
  templateUrl: './recebimento.evento.form.component.html',
  styleUrls: ['./recebimento.evento.form.component.css']
})
export class RecebimentoEventoFormComponent implements OnInit {

  @Input() recebimentoDao: RecebimentoAssociadoDao = { titulo: '', anuidade: null, nome: '', cpf: '', nomeTP: '',
                    eMail: '', nrCelular: '', ativoAssociado: false, recebimentoId: 0, assinaturaAnuidadeId: null,
                    assinaturaEventoId: null, observacao: '', notificationCodePS: '', typePS: null,
                    statusPS: null, lastEventDatePS: null, typePaymentMethodPS: null, codePaymentMethodPS: null,
                    grossAmountPS: 0, discountAmountPS: 0, feeAmountPS: 0, netAmountPS: 0, extraAmountPS: 0,
                    dtVencimento: null, statusFBTC: null, dtStatusFBTC: null, origemEmissaoTitulo: null,
                    dtCadastro: null, ativo: false};

  @Input() evento: Evento = new Evento();

  title: string;
  private selectedId: any;
  tiposPublicos: TipoPublico[];
  eventos: Evento[];
  submitted: boolean;

  _msg: string;
  _msgRetorno: string;
  _recebimentoId: number;

  constructor(
    private service: RecebimentoService,
    private serviceTP: TipoPublicoService,
    private serviceEvento: EventoService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.title = 'Dados de pagamento de evento';
    this.submitted = false;
    this._msgRetorno = '';
    this._msg = '';
    this._recebimentoId = 0;
   }

   getRecebimentoAssociadoDaoByRecebimentoId(id: number): void {

    this.service.getRecebimentoAssociadoDaoByRecebimentoId(id)
          .subscribe(recebimentoDao => this.recebimentoDao = recebimentoDao);
  }

  gotoSave() {

    this._msg = '';

    this.service.addRecebimento(this.recebimentoDao)
      .subscribe(
        msg => {
          this._msgRetorno = msg;
          this.avaliaRetorno(this._msgRetorno);
        }
      );
      this.submitted = false;
  }


  avaliaRetorno(msgRet: string) {

    if (msgRet.substring(0, 1) === '0') {

        this._recebimentoId = parseInt(msgRet.substring(0, 10), 10);

        // this.router.navigate([`admin/Recebimento/${this._recebimentoId}`]);

        this.getRecebimentoAssociadoDaoByRecebimentoId(this._recebimentoId);

        this._msg = this._msgRetorno.substring(10);

    } else {

        this._msg = this._msgRetorno;
    }
}

  onSubmit() {
    this.gotoSave();
  }

  gotoRecebimentoAnuidade() {

    const recebimentoId = this.recebimentoDao ? this.recebimentoDao.recebimentoId : null;
    this.router.navigate(['/admin/RecebimentoEvento', { id: recebimentoId, foo: 'foo' }]);
  }

  getTiposPublicos(): void {

    this.serviceTP.getTiposPublicos('true').subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }

  getEventoByRecebimentoId(id: number): void {

    this.serviceEvento.getByRecebimentoId(id)
          .subscribe(evento => this.evento = evento);
  }

  ngOnInit() {

    this.getTiposPublicos();

    const id = +this.route.snapshot.paramMap.get('id');
    if (id > 0) {
        this.getRecebimentoAssociadoDaoByRecebimentoId(id);
        this.getEventoByRecebimentoId(id);
    } else {
      alert('Não foi encontrato recebimento para o Id Informado');
      this.gotoRecebimentoAnuidade();
    }
  }
}
