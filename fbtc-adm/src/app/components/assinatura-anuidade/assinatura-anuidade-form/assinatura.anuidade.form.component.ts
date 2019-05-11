import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';
// import { Observable } from 'rxjs/Observable';

import { ValorAnuidade } from './../../shared/model/valor-anuidade';
import { AnuidadeTipoPublicoDao } from './../../shared/model/anuidade-tipo-publico';
import { AnuidadeService } from './../../shared/services/anuidade.service';
import { AssociadoService } from './../../shared/services/associado.service';
import { AssinaturaAnuidadeService } from './../../shared/services/assinatura-anuidade.service';
import { AssinaturaAnuidadeDao, AssinaturaAnuidade } from './../../shared/model/assinatura-anuidade';

import { AuthService } from './../../shared/services/auth.service';

import { Util } from '../../shared/util/util';
import { Associado, AssociadoDao } from '../../shared/model/associado';
import { AnuidadeDao } from '../../shared/model/anuidade';
import { PagSeguroRoute } from './../../shared/webapi-routes/pagSeguro.route';
import { Recebimento } from '../../shared/model/recebimento';
// import { RecebimentoService } from '../../shared/services/recebimento.service';
import { UserProfile } from '../../shared/model/user-profile';

// import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-assinatura-anuidade-form',
  templateUrl: './assinatura.anuidade.form.component.html',
  styleUrls: ['./assinatura.anuidade.form.component.css']
})
export class AssinaturaAnuidadeFormComponent implements OnInit {

  @Input() assinaturaAnuidadeDao: AssinaturaAnuidadeDao = { nomePessoa: '', cpf: '', nomeTP: '', exercicio: 0,
   tipoAnuidade: 0, assinaturaAnuidadeId: 0, associadoId: 0, valorAnuidadeId: null, valorAnuidadeIdOriginal: null,
   recebimentoStatusPS: null, anoInicio: 0,
   anoTermino: 0, percentualDesconto: 0, tipoDesconto: '0', valor: 0, dtVencimentoPagamento: null, dtAssinatura: null, dtAtualizacao: null,
   codePS: '', dtCodePS: null, reference: '', emProcessoPagamento: false, dtInicioProcessamento: null,
   ativo: true, valorTipoAnuidade: 0, anuidadeId: 0, tipoPublicoId: 0, anuidadeAtcOk: false, membroDiretoria: false,
   membroConfi: false, pagamentoIsento: false, pagamentoIsentoBD: false, dtIsencao: null, observacaoIsencao: '' };

  anuidadesTiposPublicosDao: AnuidadeTipoPublicoDao[];

  anuidadeDao: AnuidadeDao = {anuidadeId: 0, exercicio: null,
    dtVencimento: null, dtInicioVigencia: null, dtTerminoVigencia: null,
    cobrancaLiberada: false, dtCobrancaLiberada: null, dtCadastro: null, ativo: false,
    anuidadesTiposPublicosDao: this.anuidadesTiposPublicosDao };

  ValAnuidade_1: ValorAnuidade = {valorAnuidadeId: null, valor: 0, tipoAnuidade: 1, AnuidadeTipoPublicoId: 0};
  ValAnuidade_2: ValorAnuidade = {valorAnuidadeId: null, valor: 0, tipoAnuidade: 2, AnuidadeTipoPublicoId: 0};
  ValAnuidade_3: ValorAnuidade = {valorAnuidadeId: null, valor: 0, tipoAnuidade: 3, AnuidadeTipoPublicoId: 0};

  valoresAnuidade_1: ValorAnuidade[];

  anuidadesTPAssoc: AnuidadeTipoPublicoDao = { anuidadeTipoPublicoId: 0, anuidadeId: 0,
    tipoPublicoId: 0, nomeTipoPublico: '', codigo: '', valoresAnuidades: this.valoresAnuidade_1
   };

   recebimento = new Recebimento();

  associado = new Associado();
  associadoDao = new AssociadoDao();
  valorAnuidadeSelected = new ValorAnuidade();

  title: string;
  badge: string;
  editAssinaturaAnuidadeId: number;
  editAnuidadeId: number;
  editAssociadoId: number;
  editTipoPublicoId: number;

  submitted: boolean;
  _msgRetorno: string;
  _msg: string;
  _assinaturaAnuidadeId: number;
  _anuidadeId: number;
  _associadoId: number;

  _util = Util;

  _targetPagSeguro: string;
  _tokenPagSeguro: string;

  _botaoPagSeguroOk: boolean;
  _valorAnuidadeIdOriginal: number;
  _emProcessoPagamento: string;

  _botaoIsencaoPagamentoOk: boolean;

  alertClassType: string;

  _msgProgresso: string;

  _colaboradorPessoaId: number;
  _colaboradorNome: string;

  _valorTemp: number;
  _idTemp: number;
  _targetTemp: string;
  _botaoPagTemp: boolean;
  _codePSTemp: string;
  _tipoAnuidadeTemp: number;

  constructor(
    private service: AssinaturaAnuidadeService,
    private serviceAnuidade: AnuidadeService,
    private serviceAssociado: AssociadoService,
//    private recebimentoService: RecebimentoService,
    private router: Router,
    private route: ActivatedRoute,
    private routePS: PagSeguroRoute,
    private authService: AuthService,
  ) {
    this.title = 'Assinatura Anuidade';
    this.badge = '';
    this.editAssinaturaAnuidadeId = 0;
    this.submitted = false;
    this._msgRetorno = '';
    this._msg = '';
    this._assinaturaAnuidadeId = 0;
    this.editTipoPublicoId = 0;

    this.editAnuidadeId = 0;
    this._anuidadeId = 0;

    this.editAssociadoId = 0;
    this._associadoId = 0;

    this.anuidadesTPAssoc.valoresAnuidades = [this.ValAnuidade_1, this.ValAnuidade_2, this.ValAnuidade_3];
    this.anuidadeDao.anuidadesTiposPublicosDao = [this.anuidadesTPAssoc];

    this._targetPagSeguro = '';
    this._tokenPagSeguro = '';

    this._botaoPagSeguroOk = false;
    this._valorAnuidadeIdOriginal = 0;
    this._emProcessoPagamento = 'false';

    this._botaoIsencaoPagamentoOk = false;

    this.alertClassType = 'alert alert-info';

    this._msgProgresso = '';

    this._colaboradorPessoaId = 0;
    this._colaboradorNome = '';

    this._valorTemp = 0;
    this._idTemp = null;
    this._targetTemp = null;
    this._botaoPagTemp = null;
    this._codePSTemp = null;
    this._tipoAnuidadeTemp = 0;
   }

  getAssinaturaAnuidadeById(assinaturaAnuidadeId: number, anuidadeId: number, tipoPublicoId: number): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    this._msgProgresso = '...Carregando os dados da assinatura. Por favor, aguarde!...';

    this.service.getById(assinaturaAnuidadeId)
        .subscribe(
            assinaturaAnuidadeDao => {
              this.assinaturaAnuidadeDao = assinaturaAnuidadeDao;
              this.serviceAnuidade.getAnuidadeDaoByIdTipoPublicoId(anuidadeId, tipoPublicoId)
                    .subscribe(anuidadeDao => {
                      this.anuidadeDao = anuidadeDao;
                      this._msgProgresso = '';
                      this.submitted = false;
                    });
              this.submitted = false;
              this._valorAnuidadeIdOriginal = this.assinaturaAnuidadeDao.valorAnuidadeIdOriginal;
              this.assinaturaAnuidadeDao.pagamentoIsentoBD = this.assinaturaAnuidadeDao.pagamentoIsento;
              this.gotoAvaliaBotaoPagSeguro();
            });
  }

  getDadosNovaAssinatura(anuidadeId: number, associadoId: number, tipoPublicoId: number): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    this._msgProgresso = '...Carregando os dados para a nova assinatura. Por favor, aguarde!...';

    this.serviceAssociado.getAssociadoDaoById(associadoId, anuidadeId)
          .subscribe(
            associadoDao => {
              this.associadoDao = associadoDao;
              this.serviceAnuidade.getAnuidadeDaoByIdTipoPublicoId(anuidadeId, tipoPublicoId)
                    .subscribe(
                      anuidadeDao => {
                        this.anuidadeDao = anuidadeDao;
                        this._msgProgresso = '';
                        this.setAssinaturaAssociadoDao(this.associadoDao, this.anuidadeDao);
                        this.submitted = false;
                      });
          });
  }

  setAssinaturaAssociadoDao(_associadoDao: AssociadoDao, _anuidadeDao: AnuidadeDao): void {

    this.assinaturaAnuidadeDao.nomePessoa = _associadoDao.nome;
    this.assinaturaAnuidadeDao.cpf = _associadoDao.cpf;
    this.assinaturaAnuidadeDao.nomeTP = _anuidadeDao.anuidadesTiposPublicosDao[0].nomeTipoPublico;
    this.assinaturaAnuidadeDao.exercicio = _anuidadeDao.exercicio;
    this.assinaturaAnuidadeDao.tipoAnuidade = 0;
    this.assinaturaAnuidadeDao.assinaturaAnuidadeId = 0;
    this.assinaturaAnuidadeDao.associadoId = _associadoDao.associadoId;
    this.assinaturaAnuidadeDao.valorAnuidadeId = null;
    this.assinaturaAnuidadeDao.anoInicio = 0;
    this.assinaturaAnuidadeDao.anoTermino = 0;
    this.assinaturaAnuidadeDao.percentualDesconto = 0;
    this.assinaturaAnuidadeDao.tipoDesconto = '';
    this.assinaturaAnuidadeDao.dtVencimentoPagamento = _anuidadeDao.dtVencimento;
    this.assinaturaAnuidadeDao.dtAssinatura = null;
    this.assinaturaAnuidadeDao.dtAtualizacao = null;
    this.assinaturaAnuidadeDao.codePS = '';
    this.assinaturaAnuidadeDao.dtCodePS = null;
    this.assinaturaAnuidadeDao.reference = '';
    this.assinaturaAnuidadeDao.emProcessoPagamento = false;
    this.assinaturaAnuidadeDao.dtInicioProcessamento = null;
    this.assinaturaAnuidadeDao.ativo = true;
    this.assinaturaAnuidadeDao.valor = 0;
    this.assinaturaAnuidadeDao.anuidadeId = _anuidadeDao.anuidadeId;
    this.assinaturaAnuidadeDao.tipoPublicoId = _associadoDao.tipoPublicoId;
    this.assinaturaAnuidadeDao.membroDiretoria = _associadoDao.membroDiretoria;
    this.assinaturaAnuidadeDao.anuidadeAtcOk = _associadoDao.anuidadeAtcOk;
    this.assinaturaAnuidadeDao.membroConfi = _associadoDao.membroConfi;
    this.assinaturaAnuidadeDao.valorAnuidadeIdOriginal = null;

    this.assinaturaAnuidadeDao.pagamentoIsento = false;
    this.assinaturaAnuidadeDao.dtIsencao = null;
    this.assinaturaAnuidadeDao.observacaoIsencao = '';
  }

  gotoAvaliaBotaoPagSeguro(): void {

   if (this.assinaturaAnuidadeDao.assinaturaAnuidadeId > 0
        && this.assinaturaAnuidadeDao.valorAnuidadeId > 0
        && this.assinaturaAnuidadeDao.codePS !== ''
        && this.assinaturaAnuidadeDao.emProcessoPagamento === false) {

        if (this.assinaturaAnuidadeDao.pagamentoIsento === false) {
            if (this.assinaturaAnuidadeDao.codePS.substring(0, 13) !== 'Dado_Migrado_' ||
            this.assinaturaAnuidadeDao.codePS.substring(0, 25) !== 'Isento Pagamento Anuidade') {

              this._botaoPagSeguroOk = true;
              this._targetPagSeguro = this.routePS.postGotoChekOut(this.assinaturaAnuidadeDao.codePS);
            }
        } else {

          this._botaoPagSeguroOk = false;
          this._emProcessoPagamento = 'false';
        }
    }

    if (this.assinaturaAnuidadeDao.emProcessoPagamento === true) {

      this._emProcessoPagamento = 'true';

    } else {

      this._emProcessoPagamento = 'false';
    }
  }

  gotoAssinaturasAnuidades(): void {

    const anuidadeId = this.assinaturaAnuidadeDao ? this.assinaturaAnuidadeDao.anuidadeId : null;

    this.router.navigate(['/admin/AssinaturaAnuidade', { anuidadeId: anuidadeId , foo: 'foo' }]);
  }

  setValorAnuidade(id: number): void {

    this._msg = '';
    this._msgRetorno = '';

    if (this.anuidadeDao.anuidadesTiposPublicosDao[0].valoresAnuidades.length > 0) {

        this.valorAnuidadeSelected =
        this.anuidadeDao.anuidadesTiposPublicosDao[0].valoresAnuidades.find(a => a.valorAnuidadeId === +id);

          if (this.valorAnuidadeSelected !== undefined) {

            this.assinaturaAnuidadeDao.valor = this.valorAnuidadeSelected.valor;
            this.assinaturaAnuidadeDao.tipoAnuidade = this.valorAnuidadeSelected.tipoAnuidade;

            if (this._valorAnuidadeIdOriginal > 0) {

              if ( this._valorAnuidadeIdOriginal !== this.valorAnuidadeSelected.valorAnuidadeId) {

                this._botaoPagSeguroOk = false;
              } else {

               if (this.assinaturaAnuidadeDao.codePS.substring(0, 13) !== 'Dado_Migrado_' &&
                this.assinaturaAnuidadeDao.codePS.substring(0, 25) !== 'Isento Pagamento Anuidade') {

                  this._botaoPagSeguroOk = true;
                }
              }
            }
          } else {
            this.assinaturaAnuidadeDao.valor = 0;
            this._botaoPagSeguroOk = false;
            this._valorAnuidadeIdOriginal = 0;
            this._emProcessoPagamento = 'false';
          }
    }
  }

  setDadosParaIsencao(id: number): void {

    if (this.assinaturaAnuidadeDao.emProcessoPagamento === false) {

      this._msg = '';
      this._msgRetorno = '';

      if (this.assinaturaAnuidadeDao.pagamentoIsento === false) {

          // Guardando os valores:
          this._valorTemp = this.assinaturaAnuidadeDao.valor;
          this._idTemp = this.assinaturaAnuidadeDao.valorAnuidadeId;
          this._tipoAnuidadeTemp = this.assinaturaAnuidadeDao.tipoAnuidade;
          this._targetTemp = this._targetPagSeguro;
          this._botaoPagTemp = this._botaoPagSeguroOk;
          this._codePSTemp = this.assinaturaAnuidadeDao.codePS;

          // Setando os valores:
          // this.assinaturaAnuidadeDao.valor = null;
          this._tipoAnuidadeTemp = null;
          this._targetPagSeguro = null;
          this._botaoPagSeguroOk = null;

          // tslint:disable-next-line:max-line-length
          this.assinaturaAnuidadeDao.codePS = ''; // `Isento Pagamento Anuidade ${this.assinaturaAnuidadeDao.exercicio}_${this.assinaturaAnuidadeDao. .assinaturaAnuidadeId}`;


          this.valorAnuidadeSelected =
          this.anuidadeDao.anuidadesTiposPublicosDao[0].valoresAnuidades.find(a => a.tipoAnuidade === +1);

          if (this.valorAnuidadeSelected !== undefined) {
            this.assinaturaAnuidadeDao.valor = this.valorAnuidadeSelected.valor;
            this.assinaturaAnuidadeDao.tipoAnuidade = this.valorAnuidadeSelected.tipoAnuidade;
            this.assinaturaAnuidadeDao.valorAnuidadeId = this.valorAnuidadeSelected.valorAnuidadeId;

          }

        } else {

        // Voltando os valores originais, se houverem:
        this.assinaturaAnuidadeDao.valor = this._valorTemp; // !== 0 ? this._valorTemp : this.assinaturaAnuidadeDao.valor;
        this.assinaturaAnuidadeDao.tipoAnuidade = this._tipoAnuidadeTemp;
        this.assinaturaAnuidadeDao.valorAnuidadeId = this._idTemp; // !== null ? this._idTemp : this.assinaturaAnuidadeDao.valorAnuidadeId;
        this._targetPagSeguro = this._targetTemp !== null ? this._targetTemp : this._targetPagSeguro ;
        this._botaoPagSeguroOk = this._botaoPagTemp !== null ? this._botaoPagTemp : this._botaoPagSeguroOk;
        this.assinaturaAnuidadeDao.codePS = this._codePSTemp !== null ? this._codePSTemp : this.assinaturaAnuidadeDao.codePS;
      }

    } else {

      this.alertClassType = 'alert alert-danger';
      this._msg = 'Esta anuidade já está em processo de pagamento junto ao PagSeguro';
    }
  }

  save(): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    this.alertClassType = 'alert alert-info';
    this._msg = 'Salvando os dados. Por favor, aguarde...';

    // Avaliando se houve alterção na isenção:

    if (this.assinaturaAnuidadeDao.pagamentoIsento === true && this.assinaturaAnuidadeDao.pagamentoIsentoBD === false) {
      // Passou a ser isento:
      this.assinaturaAnuidadeDao.observacaoIsencao = `Isenção de anuidade concedida por ${this._colaboradorNome}`;
      this.assinaturaAnuidadeDao.codePS = '';

    } else if (this.assinaturaAnuidadeDao.pagamentoIsento === false && this.assinaturaAnuidadeDao.pagamentoIsentoBD === true) {
      // Deixou de ser isento:
      this.assinaturaAnuidadeDao.observacaoIsencao = '';
      this.assinaturaAnuidadeDao.codePS = '';
    }

    if (this.assinaturaAnuidadeDao.pagamentoIsento === true) {

      if (this.assinaturaAnuidadeDao.valorAnuidadeId !== 1) {

        this.valorAnuidadeSelected =
        this.anuidadeDao.anuidadesTiposPublicosDao[0].valoresAnuidades.find(a => a.tipoAnuidade === +1);

        if (this.valorAnuidadeSelected !== undefined) {

          this.assinaturaAnuidadeDao.valor = this.valorAnuidadeSelected.valor;
          this.assinaturaAnuidadeDao.tipoAnuidade = this.valorAnuidadeSelected.tipoAnuidade;
          this.assinaturaAnuidadeDao.valorAnuidadeId = this.valorAnuidadeSelected.valorAnuidadeId;

        }
      }
    }

    if (this.assinaturaAnuidadeDao.emProcessoPagamento === true) {
        this.assinaturaAnuidadeDao.pagamentoIsento = false;
    }

    this.service.addAssinaturaAnuidadeDao(this.assinaturaAnuidadeDao)
    .subscribe(
      msg => {
          this.submitted = false;
          this._msgRetorno = msg;
          this.avaliaRetorno(this._msgRetorno);
      });

      this._valorTemp = null;
      this._idTemp = null;
      this._targetTemp = null;
      this._botaoPagTemp = null;
      this._codePSTemp = null;
  }

  avaliaRetorno(msgRet: string): void {

    if (msgRet.substring(0, 1) === '0') {

        this._assinaturaAnuidadeId = parseInt(msgRet.substring(0, 10), 10);

        this.alertClassType = 'alert alert-success';
        this.getAssinaturaAnuidadeById(this._assinaturaAnuidadeId, this.editAnuidadeId, this.editTipoPublicoId);

        this._msg = this._msgRetorno.substring(10);

        this.badge = 'Edição';

    } else {

        this.getAssinaturaAnuidadeById(this.assinaturaAnuidadeDao.assinaturaAnuidadeId,
            this.assinaturaAnuidadeDao.anuidadeId, this.assinaturaAnuidadeDao.tipoPublicoId);

        this.alertClassType = 'alert alert-success';
        this._msg = this._msgRetorno;
    }
  }

  onSubmit(): void {

    this._msg = '';
    this._msgRetorno = '';
    this.save();
  }

  gotoPagSeguro(): void {

    this.gotoAssinaturasAnuidades();
  }

  ngOnInit(): void {

    // Usuário logado que está criando o novo desconto:
    const userProfile: UserProfile = this.authService.getUserProfile();
    this._colaboradorPessoaId = userProfile.pessoaId;
    this._colaboradorNome = userProfile.nome;

    // Mantem a atualização do parametro 'vivo'
    this.route.params.subscribe((params: Params) => {

      const id = +params[`id`];
      const anuidadeId = params[`anuidadeId`];
      const associadoId = params[`associadoId`];
      const tipoPublicoId = params[`tipoPublicoId`];

      this.editAssinaturaAnuidadeId = id;
      this.editAnuidadeId = +anuidadeId;
      this.editAssociadoId = +associadoId;
      this.editTipoPublicoId = +tipoPublicoId;

      this.assinaturaAnuidadeDao.anuidadeId = this.editAnuidadeId;
    });

    if (this.editAssinaturaAnuidadeId > 0) {
        this.badge = 'Edição';
        this.getAssinaturaAnuidadeById(this.editAssinaturaAnuidadeId, this.editAnuidadeId, this.editTipoPublicoId);

    } else {
        this.badge = 'Novo';
        this.getDadosNovaAssinatura(this.editAnuidadeId, this.editAssociadoId, this.editTipoPublicoId);
    }
  }
}
