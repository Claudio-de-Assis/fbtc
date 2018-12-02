import { AuthService } from './../../shared/services/auth.service';
import { DescontoAnuidadeAtcService } from './../../shared/services/desconto-anuidade-atc.service';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { ValorAnuidade } from './../../shared/model/valor-anuidade';
import { AnuidadeTipoPublicoDao } from './../../shared/model/anuidade-tipo-publico';
import { AnuidadeService } from './../../shared/services/anuidade.service';
import { AssociadoService } from './../../shared/services/associado.service';
import { AssinaturaAnuidadeService } from './../../shared/services/assinatura-anuidade.service';
import { AssinaturaAnuidadeDao, AssinaturaAnuidade } from './../../shared/model/assinatura-anuidade';

import { Util } from '../../shared/util/util';
import { Associado, AssociadoDao } from '../../shared/model/associado';
import { AnuidadeDao } from '../../shared/model/anuidade';
import { DescontoAnuidadeAtcDao } from '../../shared/model/desconto-anuidade-atc';
import { Atc } from '../../shared/model/atc';
import { AtcService } from '../../shared/services/atc.service';
import { UserProfile } from '../../shared/model/user-profile';

@Component({
  selector: 'app-desconto-anuidade-atc-form',
  templateUrl: './desconto.anuidade.atc.form.component.html',
  styleUrls: ['./desconto.anuidade.atc.form.component.css']
})
export class DescontoAnuidadeAtcFormComponent implements OnInit {

  @Input() descontoAnuidadeAtcDao: DescontoAnuidadeAtcDao = {descontoAnuidadeAtcId: 0,  associadoId: 0, colaboradorId: 0,
    anuidadeId: 0, atcId: 0, observacao: '', nomeArquivoComprovante: '', dtDesconto: null, dtCadastro: null,
    ativo: false, nomePessoa: '', nomeColaborador: '', nomeAtc: '', exercicio: 0
  };

  title: string;
  badge: string;
  editDescontoAnuidadeAtcId: number;
  editAnuidadeId: number;
  editAssociadoId: number;
  editAtcId: number;

  submitted: boolean;
  _msgRetorno: string;
  _msg: string;
  _descontoAnuidadeAtcId: number;
  _anuidadeId: number;
  _associadoId: number;
  _atcId: number;
  _colaboradorPessoaId: number;

  _util = Util;

  alertClassType: string;

  _msgProgresso: string;

  constructor(
    private service: DescontoAnuidadeAtcService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService
  ) {
    this.title = 'Desconto de Anuidade x ATC';
    this.badge = '';
    this.editDescontoAnuidadeAtcId = 0;
    this.submitted = false;
    this._msgRetorno = '';
    this._msg = '';
    this._descontoAnuidadeAtcId = 0;

    this.editAnuidadeId = 0;
    this._anuidadeId = 0;

    this.editAssociadoId = 0;
    this._associadoId = 0;
    this._colaboradorPessoaId = 0;

    this.alertClassType = 'alert alert-info';

    this._msgProgresso = '';
   }

  getDescontoAssinaturaAtcById(descontoAssinaturaAtcId: number): void {

    this._msgProgresso = '...Carregando os dados. Por favor, aguarde!...';

    this.service.getDaoById(descontoAssinaturaAtcId)
        .subscribe(descontoAnuidadeAtcDao => {
          this.descontoAnuidadeAtcDao = descontoAnuidadeAtcDao;
          this._msgProgresso = '';
        });
  }

  getDadosNovoDescontoAnuidadeAtcDao(associadoId: number, anuidadeId: number, colaboradorPessoaId: number) {

    this._msgProgresso = '...Carregando os dados. Por favor, aguarde!...';

    this.service.getDadosNovoDescontoAnuidadeAtcDao(associadoId, anuidadeId, colaboradorPessoaId)
        .subscribe(descontoAnuidadeAtcDao => {
          this.descontoAnuidadeAtcDao = descontoAnuidadeAtcDao;
          this._msgProgresso = '';
        });
  }

  gotoDescontoAssinaturasAtcs() {

    const anuidadeId = this.descontoAnuidadeAtcDao ? this.descontoAnuidadeAtcDao.anuidadeId : null;

    this.router.navigate(['/admin/DescontoAnuidadeAtc', { anuidadeId: anuidadeId , foo: 'foo' }]);
  }

  save() {

    this.alertClassType = 'alert alert-info';
    this._msg = 'Salvando os dados. Por favor, aguarde...';

    this.service.addDescontoAnuidadeAtc(this.descontoAnuidadeAtcDao)
    .subscribe(
      msg => {
          this._msgRetorno = msg;
          this.avaliaRetorno(this._msgRetorno);
      });

    this.submitted = false;
  }

  avaliaRetorno(msgRet: string) {

    if (msgRet.substring(0, 1) === '0') {

        this._descontoAnuidadeAtcId = parseInt(msgRet.substring(0, 10), 10);

        this.getDescontoAssinaturaAtcById(this._descontoAnuidadeAtcId);

        this.alertClassType = 'alert alert-success';
        this._msg = this._msgRetorno.substring(10);

        this.badge = 'Edição';

    } else {

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

  ngOnInit() {

    // Usuário logado que está criando o novo desconto:
    const userProfile: UserProfile = this.authService.getUserProfile();
    this._colaboradorPessoaId = userProfile.pessoaId;

    // Mantem a atualização do parametro 'vivo'
    this.route.params.subscribe((params: Params) => {

      const id = +params[`id`];
      const anuidadeId = params[`anuidadeId`];
      const associadoId = params[`associadoId`];

      this.editDescontoAnuidadeAtcId = id;
      this.editAnuidadeId = +anuidadeId;
      this.editAssociadoId = +associadoId;

      this.descontoAnuidadeAtcDao.descontoAnuidadeAtcId = id;
      this.descontoAnuidadeAtcDao.anuidadeId = anuidadeId;
    });

    if (this.editDescontoAnuidadeAtcId > 0) {
        this.badge = 'Edição';
        this.getDescontoAssinaturaAtcById(this.editDescontoAnuidadeAtcId);

    } else {
        this.badge = 'Novo';
        this.getDadosNovoDescontoAnuidadeAtcDao(this.editAssociadoId, this.editAnuidadeId, this._colaboradorPessoaId);
    }
  }
}
