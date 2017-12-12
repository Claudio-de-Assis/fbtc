import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { AssociadoService } from '../../shared/services/associado.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { AtcService } from '../../shared/services/atc.service';

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

    title = 'Pesquisa de Associados';

    _util = Util;

    private selectedAssociado: Associado;

    associados: Associado[];

    tiposPublicos: TipoPublico[];

    atcs: Atc[];

    editAssociadoId: number;
    editNome: string;
    editCPF: string;
    editCRP: string;
    editCRM: string;

    constructor(
        private service: AssociadoService,
        private serviceTP: TipoPublicoService,
        private router: Router,
        private route: ActivatedRoute,
        private serviceAtc: AtcService
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
