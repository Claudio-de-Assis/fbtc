import { Endereco } from '../../shared/model/endereco';
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

@Component({
  selector: 'app-recebimento-evento-form',
  templateUrl: './recebimento.evento.form.component.html',
  styleUrls: ['./recebimento.evento.form.component.css']
})
export class RecebimentoEventoFormComponent implements OnInit {

  enderecos: Endereco[];

  @Input() recebimento: Recebimento = { recebimentoId: 0, associadoId: 0, associadoIsentoId: 0, valorAnuidadePublicoId: 0,
        valorEventoPublicoId: 0, objetivoPagamento: '', dtNotificacao: null, observacao: '',
        codePS: '', referencePS: '', typePS: 0, statusPS: 99, lastEventDatePS: null, typePaymentMethodPS: 0,
        codePaymentMethodPS: 0, netAmountPS: 0,
        dtCadastro: null, ativo: true,
          associado: { associadoId: 0, atcId: 0, tipoPublicoId: 0, nrMatricula: '', crp: '',
            crm: '', nomeInstFormacao: '', certificado: false, dtCertificacao: null, divulgarContato: false,
            tipoFormaContato: '', integraDiretoria: false, integraConfi: false, nrTelDivulgacao: '',
            comprovanteAfiliacaoAtc: '', tipoProfissao: '', tipoTitulacao: '',
            pessoaId: 0, nome: '', cpf: '', rg: '', eMail: '', nomeFoto: '',
            sexo: '', dtNascimento: null, nrCelular: '', passwordHash: '',
            dtCadastro: null, ativo: true, perfilId:0,
              enderecosPessoa: this.enderecos}
  };

  /*
   new endereco { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '', ordemEndereco: '',
              bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
              cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''}
  */
  @Input() evento: Evento = new Evento();

  title: string;
  private selectedId: any;
  tiposPublicos: TipoPublico[];
  eventos: Evento[];
  submitted: boolean;

  constructor(
    private service: RecebimentoService,
    private serviceTP: TipoPublicoService,
    private serviceEvento: EventoService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.title = 'Dados de pagamento de evento';
    this.submitted = false;
   }

  getRecebimentoById(id: number): void {

    this.service.getById(id)
          .subscribe(recebimento => this.recebimento = recebimento);

    this.submitted = false;
  }

  gotoSave() {

    this.service.addRecebimento(this.recebimento)
    .subscribe(() =>  this.gotoShowPopUp());
  }

  onSubmit() {
    this.submitted = true;
    this.gotoSave();
  }

  gotoShowPopUp() {

    // Colocar a chamada para a implementação do PopUp modal de aviso:
    alert('Registro salvo com sucesso!');
  }

  /*
  gotoNotificarAssociado() {

    if (confirm('Deseja notificar o Associado?')) {
      alert('Notificação enviada com sucesso');
    }
  }
  */

  gotoRecebimentoAnuidade() {

    let recebimentoId = this.recebimento ? this.recebimento.recebimentoId : null;
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
        this.getRecebimentoById(id);
        this.getEventoByRecebimentoId(id);
    } else {
      alert('Não foi encontrato recebimento para o Id Informado');
      this.gotoRecebimentoAnuidade();
    }
  }
}
