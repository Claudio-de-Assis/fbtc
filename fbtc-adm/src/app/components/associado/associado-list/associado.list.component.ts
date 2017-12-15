import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { AssociadoService } from '../../shared/services/associado.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { AtcService } from '../../shared/services/atc.service';
 import { EnderecoService } from './../../shared/services/endereco.service';

import { Associado } from '../../shared/model/associado';
import { TipoPublico } from './../../shared/model/tipo-publico';
import { Atc } from '../../shared/model/atc';
import { EstadoEnderecoCepDAO, CidadeEnderecoCepDAO } from '../../shared/model/endereco-cep';

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

    editNome: string = '';
    editCpf: string = '';
    editSexo: string = '0';
    editAtcId: number = 0;
    editCrp: string = '';
    editTipoProfissao: string = '0';
    editTipoPublicoId: number = 0;
    editEstado: string = '';
    editCidade: string = '';
    editAtivo: boolean = true;

    _nome: string = '0';
    _cpf: string = '0';
    _crp: string = '0';
    _estado: string = '0';
    _cidade: string = '0';
    _ativo: string = '2';

    _util = Util;

    associados: Associado[];
    tiposPublicos: TipoPublico[];
    atcs: Atc[];
    estados: EstadoEnderecoCepDAO[];
    cidades: CidadeEnderecoCepDAO[];

    private selectedAssociado: Associado;

    submitted = false;

    /** AssociadoList ctor */
    constructor(
        private service: AssociadoService,
        private serviceTP: TipoPublicoService,
        private router: Router,
        private route: ActivatedRoute,
        private serviceAtc: AtcService,
        // private serviceEnd: EnderecoService
    ) { }

    getEstados(): void {

        // this.serviceEnd.getAllEstados().subscribe(estados => this.estados = estados);
    }

    getCidadeByEstados(): void {

        // this.serviceEnd.getGetCidadesByEstado(this.editEstado).subscribe(cidades => this.cidades = cidades);
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

        this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
    }

    onSelect(associado: Associado): void {

        this.selectedAssociado = associado;
    }

    gotoNovoAssociado() {

        this.router.navigate(['/Associado', 0]);
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

        this.service.getByFilters(this._nome, this._cpf, this.editSexo, this.editAtcId,
                this._crp, this.editTipoProfissao, this.editTipoPublicoId)
            .subscribe(associados => this.associados = associados);

          this.submitted = false;
          this._nome = '0';
          this._cpf = '0';
          this._crp = '0';
    }

    ngOnInit(): void {

        this.getTiposPublicos();

        this.getAtcs();

        this.getEstados();

        this.getAssociados();
    }
}
