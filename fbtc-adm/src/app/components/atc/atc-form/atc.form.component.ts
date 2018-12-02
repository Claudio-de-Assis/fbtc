import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { Atc } from '../../shared/model/atc';

import { AtcService } from '../../shared/services/atc.service';
import { UnidadeFederacaoService } from '../../shared/services/unidade-federacao.service';

import { Util } from '../../shared/util/util';
import { UnidadeFederacao } from '../../shared/model/unidade-federacao';

@Component({
  selector: 'app-atc-form',
  templateUrl: './atc.form.component.html',
  styleUrls: ['./atc.form.component.css']
})
export class AtcFormComponent implements OnInit {

  @Input() atc: Atc = { atcId: 0, nome: '', uf: '', nomePres: '', nomeVPres: '', nomePSec: '', nomeSSec: '', nomePTes: '',
                        nomeSTes: '', site: '', siteDiretoria: '', ativo: true};

  title: string;
  badge: string;
  editAtcId: number;
  // private selectedId: any;
  submitted: boolean;
  _msgRetorno: string;
  _msg: string;
  _atcId: number;

  unidadesFederacao: UnidadeFederacao[];
  _util = Util;

  alertClassType: string;

  _msgProgresso: string;

  constructor(
    private service: AtcService,
    private serviceUF: UnidadeFederacaoService,
    private router: Router,
    private route: ActivatedRoute,
  ) {
    this.title = 'Atc';
    this.badge = '';
    this.editAtcId = 0;
    this.submitted = false;
    this._msgRetorno = '';
    this._msg = '';
    this._atcId = 0;

    this.alertClassType = 'alert alert-info';

    this._msgProgresso = '';
   }

  getAtcById(id: number): void {

    this._msgProgresso = '...Carregando os dados. Por favor, aguarde!...';

    this.service.getById(id)
        .subscribe(atc => {
          this.atc = atc;
          this._msgProgresso = '';
        });
  }

  gotoAtcs() {

    const atcId = this.atc ? this.atc.atcId : null;
    this.router.navigate(['/admin/Atc', { id: atcId, foo: 'foo' }]);
  }

  save() {

    this.alertClassType = 'alert alert-info';
    this._msg = 'Salvando os dados. Por favor, aguarde...';

    this.service.addAtc(this.atc)
    .subscribe(
      msg => {
          this._msgRetorno = msg;
          this.avaliaRetorno(this._msgRetorno);
      });

    this.submitted = false;
  }

  avaliaRetorno(msgRet: string) {

    if (msgRet.substring(0, 1) === '0') {

        this._atcId = parseInt(msgRet.substring(0, 10), 10);

        this.router.navigate([`/admin/Atc/${this._atcId}`]);

        this.getAtcById(this._atcId);

        this.alertClassType = 'alert alert-success';
        this._msg = this._msgRetorno.substring(10);

        this.badge = 'Edição';

    } else {

      this.alertClassType = 'alert alert-success';
      this._msg = this._msgRetorno;
    }
  }

  gotoShowPopUp(msg: string) {

    // Colocar a chamada para a implementação do PopUp modal de aviso:
    alert(msg);
  }

  excluir() {

    this.gotoAtcs();
  }

  getUFsDisponiveis(atcId: number) {

    this.serviceUF.getUnidadesFederacaoDisponiveis(atcId).subscribe(unidadesFederacao => this.unidadesFederacao = unidadesFederacao);
  }

  onSubmit() {

    this.submitted = true;
    this.save();
  }

  ngOnInit() {

    this.editAtcId = +this.route.snapshot.paramMap.get('id');

    this.getUFsDisponiveis(this.editAtcId);

    if (this.editAtcId > 0) {
        this.badge = 'Edição';
        this.getAtcById(this.editAtcId);

    } else {
        this.badge = 'Novo';
    }
  }
}
