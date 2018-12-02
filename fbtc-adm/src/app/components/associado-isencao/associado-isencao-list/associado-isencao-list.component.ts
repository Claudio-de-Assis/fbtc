import { AppSettings } from './../../../app.settings';
import { AssociadoIsento } from '../../shared/model/associado-isento';
import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { AssociadoService } from '../../shared/services/associado.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { AtcService } from '../../shared/services/atc.service';
import { EnderecoService } from '../../shared/services/endereco.service';

import { Associado } from '../../shared/model/associado';
import { TipoPublico } from '../../shared/model/tipo-publico';
import { Atc } from '../../shared/model/atc';
import { CidadeEnderecoCepDao, EstadoEnderecoCepDao } from '../../shared/model/endereco-cep';
import { AssociadoIsentoDao } from '../../shared/model/associado-isento';

import { Util } from '../../shared/util/util';

@Component({
  selector: 'app-associado-isencao-list',
  templateUrl: './associado-isencao-list.component.html',
  styleUrls: ['./associado-isencao-list.component.css']
})
export class AssociadoIsencaoListComponent implements OnInit {

    title: string;

    @Input() isencaoId: number;
    @Input() tipoIsencao: string;

    _util = Util;

    private selectedAssociado: AssociadoIsento;

    associados: AssociadoIsentoDao[];
    tiposPublicos: TipoPublico[];
    atcs: Atc[];
    estadoEnderecoCepDAO: EstadoEnderecoCepDao[];
    cidadeEnderecoCepDAO: CidadeEnderecoCepDao[];

    _associadoIsentoDao = new AssociadoIsentoDao();

    editNome: string;
    editCpf: string;
    editSexo: string;
    editAtcId: number;
    editCrp: string;
    editTipoProfissao: string;
    editTipoPublicoId: number;
    editEstado: string;
    editCidade: string;
    editAtivo: boolean;

    _nome: string;
    _cpf: string;
    _crp: string;
    _estado: string;
    _cidade: string;
    _ativo: string;

    submitted: boolean;
    isBusy: boolean;

    _itensPerPage: number;

    constructor(
        private service: AssociadoService,
        private serviceTP: TipoPublicoService,
        private router: Router,
        private route: ActivatedRoute,
        private serviceAtc: AtcService,
        private serviceEnd: EnderecoService
    ) {
        this.title = 'Pesquisa de Usários'; // Associados
        this._itensPerPage = AppSettings.ITENS_PER_PAGE;

        this.editNome = '';
        this.editCpf = '';
        this.editSexo = '0';
        this.editAtcId = 0;
        this.editCrp = '';
        this.editTipoProfissao = '0';
        this.editTipoPublicoId = 0;
        this.editEstado = '0';
        this.editCidade = '0';
        this.editAtivo = true;

        this._nome = '0';
        this._cpf = '0';
        this._crp = '0';
        this._estado = '0';
        this._cidade = '0';
        this._ativo = '2';

        this.submitted = false;
        this.isBusy = false;
    }

    getAtcs(): void {

        this.serviceAtc.getAtcs().subscribe(atcs => this.atcs = atcs);
    }

    getTiposPublicos(): void {

        this.serviceTP.getTiposPublicos('true').subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
    }

    onSubmit() {
        this.submitted = true;
        this.gotoBuscarAssociado();
    }

    gotoBuscarAssociado(): void {

        if (this.editNome.trim() !== '') {
            this._nome = this.editNome.trim();
        }
        if (this.editCrp !== '') {
            this._crp = this.editCrp;
        }
        if (this.editAtivo !== null) {
            if (this.editAtivo) {
              this._ativo = 'true';
            } else {
              this._ativo = 'false';
            }
        }

        this.service.getIsentoByFilters(this.isencaoId, this._nome, this._cpf, this.editSexo, this.editAtcId,
                this._crp, this.editTipoProfissao, this.editTipoPublicoId, this.editEstado, this.editCidade, this._ativo)
            .subscribe(associados => this.associados = associados);

          this.submitted = false;
          // this._nome = '0';
          // this._cpf = '0';
          // this._crp = '0';
          this._ativo = '2';
    }

    gotoSavaIsencaoAssociado(associadoId: number, associadoIsentoId: number) {

        if (this.isBusy) {

            this.gotoShowPopUp('Aguarde....');
        } else {

            this.isBusy = true;

            this._associadoIsentoDao.associadoId = associadoId;
            this._associadoIsentoDao.associadoIsentoId =  associadoIsentoId;
            this._associadoIsentoDao.isencaoId = this.isencaoId;
            this._associadoIsentoDao.atcId = 0;
            this._associadoIsentoDao.ativo = false;
            this._associadoIsentoDao.cpf = '';
            this._associadoIsentoDao.crp = '';
            this._associadoIsentoDao.nome = '';
            this._associadoIsentoDao.tipoPublicoId = 0;
            this._associadoIsentoDao.tipoIsencao = this.tipoIsencao;

            this.service.saveAssociadoIsento(this._associadoIsentoDao)
            .subscribe(() =>  this.gotoBuscarAssociado());

            this.isBusy = false;
        }
    }

    gotoShowPopUp(msg: string) {

        // Colocar a chamada para a implementação do PopUp modal de aviso:
        alert(msg);
      }

    ngOnInit() {

        this.getTiposPublicos();

        this.getAtcs();

        this.gotoBuscarAssociado();
    }
}
