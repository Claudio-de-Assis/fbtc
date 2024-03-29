import { AppSettings } from './../../../app.settings';
import { Component, OnInit } from '@angular/core';
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
import { EstadoEnderecoCepDao, CidadeEnderecoCepDao } from '../../shared/model/endereco-cep';

import { Util } from '../../shared/util/util';

@Component({
    selector: 'app-associado-list',
    templateUrl: './associado.list.component.html',
    styleUrls: ['./associado.list.component.css'],
    providers: [AssociadoService]
})

/** AssociadoList component*/
export class AssociadoListComponent implements OnInit {

    title: string;

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

    _util = Util;
    submitted: boolean;
    _itensPerPage: number;

    _msgProgresso: string;

    associados: Associado[];
    tiposPublicos: TipoPublico[];
    atcs: Atc[];
    estados: EstadoEnderecoCepDao[];
    cidades: CidadeEnderecoCepDao[];

    private selectedAssociado: Associado;

    constructor(
        private service: AssociadoService,
        private serviceTP: TipoPublicoService,
        private router: Router,
        private route: ActivatedRoute,
        private serviceAtc: AtcService,
        private serviceEnd: EnderecoService
    ) {
        this.title = 'Consulta de Usuário'; // Associado
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

        this._msgProgresso = '';

        this.submitted = false;
        this._itensPerPage = AppSettings.ITENS_PER_PAGE;
    }

    getEstados(): void {

        this.serviceEnd.getAllEstados().subscribe(estados => this.estados = estados);
    }

    gotoGetCidades(): void {

        if (this.editEstado !== '') {
            this.serviceEnd.getGetCidadesByEstado(this.editEstado).subscribe(cidades => this.cidades = cidades);
        }
    }

    getAssociados(): void {

        if (this.submitted === false) {
            this.submitted = true;
        } else {
            return;
        }

        this._msgProgresso = '...Pesquisando...';

        this.service.getAssociados().subscribe(
            associados => {
                this.submitted = false;
                this.associados = associados;
                this._msgProgresso =  this.associados.length === 0 ? ' - Não foram encontrados registros' : '';
            });
    }

    onSubmit(): void {

        this.gotoBuscarAssociado();
    }

    getAtcs(): void {

        this.serviceAtc.getAtcs().subscribe(atcs => this.atcs = atcs);
    }

    getTiposPublicos(): void {

        this.serviceTP.getTiposPublicos('true').subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
    }

    onSelect(associado: Associado): void {

        if (this.submitted === false) {
            this.submitted = true;
        } else {
            return;
        }

        this.selectedAssociado = associado;
        this.router.navigate(['admin/Associado', this.selectedAssociado.pessoaId]);
    }

    gotoNovoAssociado(): void {

        if (this.submitted === false) {
            this.submitted = true;
        } else {
            return;
        }

        this.router.navigate(['admin/AssociadoNovo']);
    }

    gotoBuscarAssociado(): void {

        if (this.submitted === false) {
            this.submitted = true;
        } else {
            return;
        }

        if (this.editNome.trim() !== '') {
            this.editNome = this._util.StringSanity(this.editNome);
            this._nome = this.editNome !== '' ? this.editNome : '0';
        }
        if (this.editCrp !== '') {
            this.editCrp = this._util.StringSanity(this.editCrp);
            this._crp = this.editCrp !== '' ? this.editCrp : '0';
        }
        if (this.editAtivo !== null) {
            if (this.editAtivo) {
              this._ativo = 'true';
            } else {
              this._ativo = 'false';
            }
        }

        this._msgProgresso = '...Pesquisando...';

        this.service.getByFilters(this._nome, this._cpf, this.editSexo, this.editAtcId,
                this._crp, this.editTipoProfissao, this.editTipoPublicoId, this.editEstado, this.editCidade, this._ativo)
            .subscribe(
                associados => {
                    this.submitted = false;
                    this.associados = associados;
                    this._msgProgresso =  this.associados.length === 0 ? ' - Não foram encontrados registros' : '';
                });

          this._nome = '0';
          this._cpf = '0';
          this._crp = '0';
          this._ativo = '2';
    }

    gotoLimparFiltros(): void {

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
    }

    ngOnInit(): void {

        this.getTiposPublicos();

        this.getAtcs();

        this.getEstados();

        this.getAssociados();
    }
}
