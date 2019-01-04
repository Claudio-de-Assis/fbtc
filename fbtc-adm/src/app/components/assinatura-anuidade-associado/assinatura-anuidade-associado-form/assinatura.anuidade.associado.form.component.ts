import { PagSeguroRoute } from './../../shared/webapi-routes/pagSeguro.route';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { ValorAnuidade } from '../../shared/model/valor-anuidade';
import { AnuidadeTipoPublicoDao } from '../../shared/model/anuidade-tipo-publico';
import { AnuidadeService } from '../../shared/services/anuidade.service';
import { AssociadoService } from '../../shared/services/associado.service';
import { AssinaturaAnuidadeService } from '../../shared/services/assinatura-anuidade.service';
import { AssinaturaAnuidadeDao, AssinaturaAnuidade } from '../../shared/model/assinatura-anuidade';

import { Util } from '../../shared/util/util';
import { Associado, AssociadoDao } from '../../shared/model/associado';
import { AnuidadeDao } from '../../shared/model/anuidade';

@Component({
  selector: 'app-assinatura-anuidade-associado-form',
  templateUrl: './assinatura.anuidade.associado.form.component.html',
  styleUrls: ['./assinatura.anuidade.associado.form.component.css']
})
export class AssinaturaAnuidadeAssociadoFormComponent implements OnInit {

  @Input() assinaturaAnuidadeDao: AssinaturaAnuidadeDao = { nomePessoa: '', cpf: '', nomeTP: '', exercicio: 0,
   tipoAnuidade: 0, assinaturaAnuidadeId: 0, associadoId: 0, valorAnuidadeId: null, valorAnuidadeIdOriginal: null, anoInicio: 0,
   anoTermino: 0, percentualDesconto: 0, tipoDesconto: '0', valor: 0, dtVencimentoPagamento: null,  dtAssinatura: null, dtAtualizacao: null,
   codePS: '', dtCodePS: null, reference: '', emProcessoPagamento: false, dtInicioProcessamento: null,
   ativo: true, valorTipoAnuidade: 0, anuidadeId: 0, tipoPublicoId: 0, anuidadeAtcOk: false, membroDiretoria: false, membroConfi: false
  };

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

  associado = new Associado();
  associadoDao = new AssociadoDao();
  valorAnuidadeSelected = new ValorAnuidade();

  title: string;
  badge: string;
  editAssinaturaAnuidadeId: number;
  editAnuidadeId: number;
  editAssociadoId: number;
  editTipoPublicoId: number;
  _targetPagSeguro: string;
  _tokenPagSeguro: string;

  _botaoPagSeguroOk: boolean;
  _valorAnuidadeIdOriginal: number;
  _emProcessoPagamento: string;

  submitted: boolean;
  _msgRetorno: string;
  _msg: string;
  _assinaturaAnuidadeId: number;
  _anuidadeId: number;
  _associadoId: number;

  _util = Util;

  alertClassType: string;

  _msgProgresso: string;

  constructor(
    private service: AssinaturaAnuidadeService,
    private serviceAnuidade: AnuidadeService,
    private serviceAssociado: AssociadoService,
    private router: Router,
    private route: ActivatedRoute,
    private routePS: PagSeguroRoute,
  ) {
    this.title = 'Minha Assinatura de Anuidade';
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
    this._targetPagSeguro = '';
    this._tokenPagSeguro = '';

    this.anuidadesTPAssoc.valoresAnuidades = [this.ValAnuidade_1, this.ValAnuidade_2, this.ValAnuidade_3];
    this.anuidadeDao.anuidadesTiposPublicosDao = [this.anuidadesTPAssoc];

    this._botaoPagSeguroOk = false;
    this._valorAnuidadeIdOriginal = 0;
    this._emProcessoPagamento = 'false';

    this.alertClassType = 'alert alert-info';

    this._msgProgresso = '';
   }

  getAssinaturaAnuidadeById(assinaturaAnuidadeId: number, anuidadeId: number, tipoPublicoId): void {

    this._msgProgresso = '...Carregando os dados da assinatura. Por favor, aguarde!...';

    this.service.getById(assinaturaAnuidadeId)
        .subscribe(
            assinaturaAnuidadeDao => {
              this.assinaturaAnuidadeDao = assinaturaAnuidadeDao;
              this.serviceAnuidade.getAnuidadeDaoByIdTipoPublicoId(anuidadeId, tipoPublicoId)
                    .subscribe(anuidadeDao => {
                      this.anuidadeDao = anuidadeDao;
                      this._msgProgresso = '';
                    });
              this._valorAnuidadeIdOriginal = this.assinaturaAnuidadeDao.valorAnuidadeIdOriginal;
              this.gotoAvaliaBotaoPagSeguro();
            });
  }

  getDadosNovaAssinatura(anuidadeId: number, associadoId: number, tipoPublicoId): void {

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
                      });
          });
  }

  setAssinaturaAssociadoDao(_associadoDao: AssociadoDao, _anuidadeDao: AnuidadeDao) {
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
  }


  gotoAvaliaBotaoPagSeguro() {

      if (this.assinaturaAnuidadeDao.assinaturaAnuidadeId > 0
        && this.assinaturaAnuidadeDao.valorAnuidadeId > 0
        && this.assinaturaAnuidadeDao.codePS !== ''
        && this.assinaturaAnuidadeDao.emProcessoPagamento === false
        ) {
            if (this.assinaturaAnuidadeDao.codePS.substring(0, 13) !== 'Dado_Migrado_') {
              this._botaoPagSeguroOk = true;
              this._targetPagSeguro = this.routePS.postGotoChekOut(this.assinaturaAnuidadeDao.codePS);
        }
      }

      if (this.assinaturaAnuidadeDao.emProcessoPagamento === true) {
        this._emProcessoPagamento = 'true';
      } else {
        this._emProcessoPagamento = 'false';
      }
  }

  gotoAssinaturasAnuidades() {

    const anuidadeId = this.assinaturaAnuidadeDao ? this.assinaturaAnuidadeDao.anuidadeId : null;

    this.router.navigate(['/admin/MinhaAssinaturaAnuidade', { anuidadeId: anuidadeId , foo: 'foo' }]);
  }

  setValorAnuidade(id: number) {

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
                this._botaoPagSeguroOk = true;
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

  save() {

    this.alertClassType = 'alert alert-info';
    this._msg = 'Salvando os dados. Por favor, aguarde...';

    this.service.addAssinaturaAnuidadeDao(this.assinaturaAnuidadeDao)
    .subscribe(
      msg => {
          this._msgRetorno = msg;
          this.avaliaRetorno(this._msgRetorno);
      });

    this.submitted = false;
  }

  avaliaRetorno(msgRet: string) {

    if (msgRet.substring(0, 1) === '0') {

        this._assinaturaAnuidadeId = parseInt(msgRet.substring(0, 10), 10);

        this.getAssinaturaAnuidadeById(this._assinaturaAnuidadeId, this.editAnuidadeId, this.editTipoPublicoId);

        this.alertClassType = 'alert alert-success';
        this._msg = this._msgRetorno.substring(10);

        this.badge = 'Edição';

    } else {

      this.getAssinaturaAnuidadeById(this.assinaturaAnuidadeDao.assinaturaAnuidadeId,
        this.assinaturaAnuidadeDao.anuidadeId, this.assinaturaAnuidadeDao.tipoPublicoId);

      this.alertClassType = 'alert alert-success';
      this._msg = this._msgRetorno;
    }
  }

  onSubmit() {

    this._msg = '';
    this._msgRetorno = '';
    this.submitted = true;
    this.save();
  }

  gotoPagSeguro() {

    this.gotoAssinaturasAnuidades();
  }

  ngOnInit() {

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
