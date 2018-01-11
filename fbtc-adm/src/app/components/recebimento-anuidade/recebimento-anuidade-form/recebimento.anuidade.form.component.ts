import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { RecebimentoService } from '../../shared/services/recebimento.service';
import { Recebimento } from './../../shared/model/recebimento';

@Component({
  selector: 'app-recebimento-anuidade-form',
  templateUrl: './recebimento.anuidade.form.component.html',
  styleUrls: ['./recebimento.anuidade.form.component.css']
})
export class RecebimentoAnuidadeFormComponent implements OnInit {

  @Input() recebimento: Recebimento = { recebimentoId: 0, associadoId: 0, associadoIsentoId: 0, valorAnuidadePublicoId: 0,
    valorEventoPublicoId: 0, objetivoPagamento: '', dtVencimento: null, dtPagamento: null, dtNotificacao: null,
    statusPagamento: '9', formaPagamento: '', nrDocCobranca: '', valorPago: 0, observacao: '', tokenPagamento: '',
    dtCadastro: null, ativo: true,
      associado: { associadoId: 0, atcId: 0, tipoPublicoId: 0, nrMatricula: '', crp: '',
        crm: '', nomeInstFormacao: '', certificado: false, dtCertificacao: null, divulgarContato: false,
        tipoFormaContato: '', integraDiretoria: false, integraConfi: false, nrTelDivulgacao: '',
        comprovanteAfiliacaoAtc: '', tipoProfissao: '', tipoTitulacao: '',
        pessoaId: 0, nome: '', cpf: '', rg: '', eMail: '', nomeFoto: '',
        sexo: '', dtNascimento: null, nrCelular: '', passwordHash: '',
        dtCadastro: null, ativo: false,
          enderecoPessoa: { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '',
          bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
          cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''}}
  };

  title = 'Dados de pagamento de anuidade do associado';

  private selectedId: any;

  submitted = false;

  constructor(
    private service: RecebimentoService,
    private router: Router,
    private route: ActivatedRoute
) { }

  getRecebimentoById(id: number): void {

    this.service.getById(id)
          .subscribe(recebimento => this.recebimento = recebimento);
  }

  gotoSave() {

      this.service.addRecebimento(this.recebimento)
      .subscribe(() =>  this.gotoShowPopUp());

      this.submitted = false;
  }

  onSubmit() {
    this.submitted = true;
    this.gotoSave();
  }

  gotoShowPopUp() {

    // Colocar a chamada para a implementação do PopUp modal de aviso:
    alert('Registro salvo com sucesso!');
  }


  gotoNotificarAssociado() {

    if (confirm('Deseja notificar o Associado?')) {
      alert('Notificação enviada com sucesso');
    }
  }

  gotoRecebimentoAnuidade() {

    let recebimentoId = this.recebimento ? this.recebimento.recebimentoId : null;
    this.router.navigate(['/RecebimentoAnuidade', { id: recebimentoId, foo: 'foo' }]);
  }

  ngOnInit() {

    const id = +this.route.snapshot.paramMap.get('id');
      if (id > 0) {
          this.getRecebimentoById(id);
    } else {
      // alert('Não foi encontrato recebimento para o Id Informado');
      // this.gotoRecebimentoAnuidade();
    }
  }
}
