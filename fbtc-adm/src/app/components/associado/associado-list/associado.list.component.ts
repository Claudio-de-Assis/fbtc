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

    title = 'Consulta de Usuário'; // Associado

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

    associados: Associado[];
    tiposPublicos: TipoPublico[];
    atcs: Atc[];
    estados: EstadoEnderecoCepDao[];
    cidades: CidadeEnderecoCepDao[];

    private selectedAssociado: Associado;

    submitted = false;

    _itensPerPage = 30;

    /** AssociadoList ctor */
    constructor(
        private service: AssociadoService,
        private serviceTP: TipoPublicoService,
        private router: Router,
        private route: ActivatedRoute,
        private serviceAtc: AtcService,
        private serviceEnd: EnderecoService
    ) {

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

    getEstados(): void {

        this.serviceEnd.getAllEstados().subscribe(estados => this.estados = estados);
    }

    gotoGetCidades() {

        if (this.editEstado !== '') {
            this.serviceEnd.getGetCidadesByEstado(this.editEstado).subscribe(cidades => this.cidades = cidades);
        }
    }

    getAssociados(): void {

        this.service.getAssociados().subscribe(associados => this.associados = associados);
    }

    onSubmit() {
        this.submitted = true;
        this.gotoBuscarAssociado();
    }

    getAtcs(): void {

        this.serviceAtc.getAtcs().subscribe(atcs => this.atcs = atcs);
    }

    getTiposPublicos(): void {

        this.serviceTP.getTiposPublicos('true').subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
    }

    onSelect(associado: Associado): void {

        this.selectedAssociado = associado;
    }

    gotoNovoAssociado() {

        this.router.navigate(['admin/AssociadoNovo']);
    }

    gotoBuscarAssociado(): void {

        if (this.editNome.trim() !== '') {
            this._nome = this.editNome.trim();
        }
        if (this.editCpf !== '') {
            this._cpf = this.editCpf;
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

        this.service.getByFilters(this._nome, this._cpf, this.editSexo, this.editAtcId,
                this._crp, this.editTipoProfissao, this.editTipoPublicoId, this.editEstado, this.editCidade, this._ativo)
            .subscribe(associados => this.associados = associados);

          this.submitted = false;
          this._nome = '0';
          this._cpf = '0';
          this._crp = '0';
          this._ativo = '2';
    }

    gotoLimparFiltros() {

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
