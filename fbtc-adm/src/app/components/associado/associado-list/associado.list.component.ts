import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { AssociadoService } from '../../shared/services/associado.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { AtcService } from '../../shared/services/atc.service';

import { Associado } from '../../shared/model/associado';
import { TipoPublico } from './../../shared/model/tipo-publico';
import { Atc } from '../../shared/model/atc';

@Component({
    selector: 'app-associado-list',
    templateUrl: './associado.list.component.html',
    styleUrls: ['./associado.list.component.css'],
    providers: [AssociadoService]
})
/** AssociadoList component*/
export class AssociadoListComponent implements OnInit {

    title = 'Consulta de Associados';

    private selectedAssociado: Associado;

    associados: Associado[];
    tiposPublicos: TipoPublico[];
    atcs: Atc[];

    optSexo = [
        {name: 'Masculino', value: 'M'},
        {name: 'Feminino', value: 'F'}
    ];

    optTipoProfissao = [
        {name: 'Psicólogo', value: '7'},
        {name: 'Médico', value: '8'}
    ];

    /** AssociadoList ctor */
    constructor(
        private service: AssociadoService,
        private serviceTP: TipoPublicoService,
        private router: Router,
        private route: ActivatedRoute,
        // private serviceAtc: AtcService
    ) { }

    getAssociados(): void {
        this.service.getAssociados().subscribe(associados => this.associados = associados);
    }

    getAtcs(): void {
    //    this.serviceAtc.getAtcs.subscribe(atcs => this.atcs = atcs);
    }

    /** Called by Angular after AssociadoList component initialized */
    ngOnInit(): void {
        this.getTiposPublicos();

        this.getAssociados();

        this.getAtcs();
    }

    onSelect(associado: Associado): void {
        this.selectedAssociado = associado;
    }

    gotoNovoAssociado() {
        this.router.navigate(['/Associado', 0]);
    }

    gotoBuscarAssociado() { }

    getTiposPublicos(): void {
        this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
    }
}
