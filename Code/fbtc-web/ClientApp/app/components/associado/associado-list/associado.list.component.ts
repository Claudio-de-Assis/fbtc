import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';

import { Associado } from '../../shared/model/associado';
import { AssociadoService } from '../../shared/services/associado.service';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

@Component({
    selector: 'associado-list',
    templateUrl: './associado.list.component.html',
    styleUrls: ['./associado.list.component.css'],
    providers: [AssociadoService]

})
/** AssociadoList component*/
export class AssociadoListComponent implements OnInit {
    title: string = 'Listagem de associados';
    subTitle: string = 'Selecione um associado:';

    associado$: Observable<Associado[]>;

    associados: Associado[];
    selectedAssociado: Associado;

    private selectedId: any;

    constructor(
        private service: AssociadoService,
        private router: Router,
        private route: ActivatedRoute
    ) { }

    getAssociados(): void {
        this.service.getListAssociados().then(associados => this.associados = associados);
    }

    /** Called by Angular after AssociadoList component initialized */
    ngOnInit() {
        //return this.getAssociados();
    }

    onSelect(associado: Associado): void {
        this.selectedAssociado = associado;
    }
}