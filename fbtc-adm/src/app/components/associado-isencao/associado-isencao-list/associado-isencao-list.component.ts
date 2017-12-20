import { CidadeEnderecoCepDao, EstadoEnderecoCepDao } from './../../shared/model/endereco-cep';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { AssociadoService } from '../../shared/services/associado.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { AtcService } from '../../shared/services/atc.service';
import { EnderecoService } from './../../shared/services/endereco.service';

import { Associado } from '../../shared/model/associado';
import { TipoPublico } from '../../shared/model/tipo-publico';
import { Atc } from '../../shared/model/atc';

import { Util } from './../../shared/util/util';

@Component({
  selector: 'app-associado-isencao-list',
  templateUrl: './associado-isencao-list.component.html',
  styleUrls: ['./associado-isencao-list.component.css']
})
export class AssociadoIsencaoListComponent implements OnInit {

    title = 'Pesquisa de Usários'; // Associados

    _util = Util;

    private selectedAssociado: Associado;

    associados: Associado[];
    tiposPublicos: TipoPublico[];
    atcs: Atc[];
    estadoEnderecoCepDAO: EstadoEnderecoCepDao[];
    cidadeEnderecoCepDAO: CidadeEnderecoCepDao[];

    editNome: string;
    editCPF: string;
    editCRP: string;
    editCRM: string;
    editEstado: string;
    editCidade: string;
    editAtivo:boolean = true;

    _nome: string = '0';
    _cpf: string = '0';
    _crp: string = '0';
    _estado: string = '0';
    _cidade: string = '0';
    _ativo: string = '2';

    submitted = false;

    constructor(
        private service: AssociadoService,
        private serviceTP: TipoPublicoService,
        private router: Router,
        private route: ActivatedRoute,
        private serviceAtc: AtcService,
        private serviceEnd: EnderecoService
    ) { }

    getAssociados(): void {

        this.service.getAssociados().subscribe(associados => this.associados = associados);
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

    gotoBuscarAssociado() { }

    ngOnInit() {

        this.getTiposPublicos();

        this.getAtcs();

        this.getAssociados();
    }
}
