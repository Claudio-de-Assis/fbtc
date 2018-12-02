import { ValorAnuidade } from './../../shared/model/valor-anuidade';
import { AnuidadeTipoPublicoDao } from './../../shared/model/anuidade-tipo-publico';
import { TipoPublicoValorAnuidadeDao } from './../../shared/model/tipo-publico';
import { AnuidadeDao } from './../../shared/model/anuidade';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { Util } from './../../shared/util/util';
import { AnuidadeService } from '../../shared/services/anuidade.service';
import { TipoPublicoService } from './../../shared/services/tipo-publico.service';

@Component({
  selector: 'app-ficha-financeira-form',
  templateUrl: './ficha.financeira.form.component.html',
  styleUrls: ['./ficha.financeira.form.component.css']
})
export class FichaFinanceiraFormComponent implements OnInit {

  ValAnuidade_1P: ValorAnuidade = {valorAnuidadeId: 0, valor: 0, tipoAnuidade: 1, AnuidadeTipoPublicoId: 0};
  ValAnuidade_2P: ValorAnuidade = {valorAnuidadeId: 0, valor: 0, tipoAnuidade: 2, AnuidadeTipoPublicoId: 0};
  ValAnuidade_3P: ValorAnuidade = {valorAnuidadeId: 0, valor: 0, tipoAnuidade: 3, AnuidadeTipoPublicoId: 0};
  ValAnuidade_1EP: ValorAnuidade = {valorAnuidadeId: 0, valor: 0, tipoAnuidade: 1, AnuidadeTipoPublicoId: 0};
  ValAnuidade_2EP: ValorAnuidade = {valorAnuidadeId: 0, valor: 0, tipoAnuidade: 2, AnuidadeTipoPublicoId: 0};
  ValAnuidade_3EP: ValorAnuidade = {valorAnuidadeId: 0, valor: 0, tipoAnuidade: 3, AnuidadeTipoPublicoId: 0};
  ValAnuidade_1E: ValorAnuidade = {valorAnuidadeId: 0, valor: 0, tipoAnuidade: 1, AnuidadeTipoPublicoId: 0};
  ValAnuidade_2E: ValorAnuidade = {valorAnuidadeId: 0, valor: 0, tipoAnuidade: 2, AnuidadeTipoPublicoId: 0};
  ValAnuidade_3E: ValorAnuidade = {valorAnuidadeId: 0, valor: 0, tipoAnuidade: 3, AnuidadeTipoPublicoId: 0};

  valoresAnuidade_1: ValorAnuidade[];
  valoresAnuidade_2: ValorAnuidade[];
  valoresAnuidade_3: ValorAnuidade[];

  anuidadesTPProfAsso: AnuidadeTipoPublicoDao = { anuidadeTipoPublicoId: 0, anuidadeId: 0,
    tipoPublicoId: 0, nomeTipoPublico: 'Profissional - Associado', codigo: 'IPA', valoresAnuidades: this.valoresAnuidade_1
   };

   anuidadesTPProfEstPos: AnuidadeTipoPublicoDao = { anuidadeTipoPublicoId: 0, anuidadeId: 0,
    tipoPublicoId: 0, nomeTipoPublico: 'Estudante de Pós - Associado', codigo: 'IEPA', valoresAnuidades: this.valoresAnuidade_2
   };

   anuidadesTPProfEst: AnuidadeTipoPublicoDao = { anuidadeTipoPublicoId: 0, anuidadeId: 0,
    tipoPublicoId: 0, nomeTipoPublico: 'Estudante - Associado', codigo: 'IEA', valoresAnuidades: this.valoresAnuidade_3
   };


  @Input() anuidadesTiposPublicosDao: AnuidadeTipoPublicoDao[];

  @Input() anuidadeDao: AnuidadeDao = {anuidadeId: 0, exercicio: null,
    dtVencimento: null, dtInicioVigencia: null, dtTerminoVigencia: null,
    cobrancaLiberada: false, dtCobrancaLiberada: null, dtCadastro: null, ativo: false,
    anuidadesTiposPublicosDao: this.anuidadesTiposPublicosDao };

  title: string;
  badge: string;
  submitted: boolean;

  anuidadeId: number;
  _msgRetorno: string;
  _msg: string;
  _anuidadeId: number;

  editTipoAnuidade_1: number;
  editTipoAnuidade_2: number;
  editTipoAnuidade_3: number;

  _util = Util;

  alertClassType: string;

  _msgProgresso: string;

  constructor(
    private service: AnuidadeService,
    private serviceTP: TipoPublicoService,
    private router: Router,
    private route: ActivatedRoute
) {
  this.title = 'Dados da anuidade';
  this.submitted = false;
  this._msg = '';
  this._msgRetorno = '';
  this.anuidadeId = 0;
  this.badge = '';
  this._anuidadeId = 0;

  this.editTipoAnuidade_1 = 0;
  this.editTipoAnuidade_2 = 0;
  this.editTipoAnuidade_3 = 0;

  this.anuidadesTPProfAsso.valoresAnuidades = [this.ValAnuidade_1P, this.ValAnuidade_2P, this.ValAnuidade_3P];
  this.anuidadesTPProfEstPos.valoresAnuidades = [this.ValAnuidade_1EP, this.ValAnuidade_2EP, this.ValAnuidade_3EP];
  this.anuidadesTPProfEst.valoresAnuidades = [this.ValAnuidade_1E, this.ValAnuidade_2E, this.ValAnuidade_3E];

  this.anuidadeDao.anuidadesTiposPublicosDao = [this.anuidadesTPProfAsso, this.anuidadesTPProfEstPos, this.anuidadesTPProfEst];

  this.alertClassType = 'alert alert-info';

  this._msgProgresso = '';
}

  getAnuidadeDaoById(id: number): void {

    this._msgProgresso = '...Carregando os dados. Por favor, aguarde!...';

    this.service.getAnuidadeDaoById(id)
          .subscribe(anuidadeDao => {
            this.anuidadeDao = anuidadeDao;
            this._msgProgresso = '';
          });
  }

  gotoSave() {

    this.alertClassType = 'alert alert-info';
    this._msg = 'Salvando os dados. Por favor, aguarde...';

      this.service.addAnuidadeDao(this.anuidadeDao)
       .subscribe(
        msg => {
            this._msgRetorno = msg;
            this.avaliaRetorno(this._msgRetorno);
        });
  }

  onSubmit() {
    this.submitted = true;
    this.gotoSave();
  }

  gotoFichaFinanceira() {

    const anuidadeId = this.anuidadeDao ? this.anuidadeDao.anuidadeId : null;
    this.router.navigate(['/admin/FichaFinanceira', { id: anuidadeId, foo: 'foo' }]);
  }
/*
  getTiposPublicosByAnuidadeId(id: number): void {

    this.service.getAnuidadeDaoById(id).
      subscribe(anuidadesDao => this.anuidadeDao = anuidadesDao);
  }*/

  avaliaRetorno(msgRet: string) {

    if (msgRet.substring(0, 1) === '0') {

console.log('msgRet...' + msgRet);

        this._anuidadeId = parseInt(msgRet.substring(0, 10), 10);

console.log('_anuidadeId...' + this._anuidadeId);
        // this.router.navigate([`admin/FichaFinanceira/${this._anuidadeId}`]);

        this.getAnuidadeDaoById(this._anuidadeId);

        this.alertClassType = 'alert alert-success';
        this._msg = this._msgRetorno.substring(10);

        this.badge = 'Edição';

    } else {

        this.alertClassType = 'alert alert-success';
        this._msg = this._msgRetorno;
    }
  }

  ngOnInit() {

    this.anuidadeId = +this.route.snapshot.paramMap.get('id');

      if (this.anuidadeId > 0) {
        this.badge = 'Edição';
          this.getAnuidadeDaoById(this.anuidadeId);
    } else {
      this.badge = 'Novo';
    }
  }
}
