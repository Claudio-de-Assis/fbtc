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

  @Input() tiposPublicosValorsAnuidadesDao: TipoPublicoValorAnuidadeDao[];

  @Input() anuidadeDao: AnuidadeDao = {anuidadeId: 0, codigo: null, dtCadastro: null, ativo: false,
    tiposPublicosValorsAnuidadesDao: this.tiposPublicosValorsAnuidadesDao };

  title: string;
  badge: string;
  submitted: boolean;

  anuidadeId: number;
  _msgRetorno: string;
  _msg: string;
  _anuidadeId: number;

  _util = Util;

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
}

  getAnuidadeById(id: number): void {

    this.service.getAnuidadeDaoById(id)
          .subscribe(anuidadeDao => this.anuidadeDao = anuidadeDao);
  }

  gotoSave() {

      this._msg = '';

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

    let anuidadeId = this.anuidadeDao ? this.anuidadeDao.anuidadeId : null;
    this.router.navigate(['/admin/FichaFinanceira', { id: anuidadeId, foo: 'foo' }]);
  }

  getTiposPublicosByAnuidadeId(id: number): void {

    this.serviceTP.getTiposPublicoByAnuidadeId(id).
      subscribe(tiposPublicosValorsAnuidadesDao => this.anuidadeDao.tiposPublicosValorsAnuidadesDao = tiposPublicosValorsAnuidadesDao);
  }

  avaliaRetorno(msgRet: string) {

    if (msgRet.substring(0, 1) === '0') {

        this._anuidadeId = parseInt(msgRet.substring(0, 10), 10);

        this.router.navigate([`admin/FichaFinanceira/${this._anuidadeId}`]);

        this.getAnuidadeById(this._anuidadeId);

        this._msg = this._msgRetorno.substring(10);

        this.badge = 'Edição';

    } else {

        this._msg = this._msgRetorno;
    }
  }


  ngOnInit() {

    this.anuidadeId = +this.route.snapshot.paramMap.get('id');
      if (this.anuidadeId > 0) {
        this.badge = 'Edição';
          this.getAnuidadeById(this.anuidadeId);
    } else {
      this.badge = 'Novo';
      this.getTiposPublicosByAnuidadeId(0);
    }
  }
}
