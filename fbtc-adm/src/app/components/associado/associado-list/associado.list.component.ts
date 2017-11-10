import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';

import { Associado } from '../../shared/model/associado';
import { AssociadoService } from '../../shared/services/associado.service';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

@Component({
    selector: 'app-associado-list',
    templateUrl: './associado.list.component.html',
    styleUrls: ['./associado.list.component.css'],
    providers: [AssociadoService]

})
/** AssociadoList component*/
export class AssociadoListComponent implements OnInit {

    lstSexo = ['Masculino', 'Feminino'];
    lstAtc = ['Rio de Janeiro', 'Alagoas', 'São Paulo'];

    title = 'Consulta de Associados';

    associado$: Observable<Associado[]>;

    associados: Associado[];
    private selectedAssociado: Associado;

    private selectedId: number;

    /** AssociadoList ctor */
    constructor(
        private service: AssociadoService,
        private router: Router,
        private route: ActivatedRoute
    ) { }

    getAssociados(): void {
        this.service.getAssociados().then(associados => this.associados = associados);
    }

    /** Called by Angular after AssociadoList component initialized */
    ngOnInit() {
        this.associado$ = this.route.paramMap.switchMap((params: ParamMap) => {
            this.selectedId = +params.get('Id');
            return this.service.getAssociados();
        });
    }

    onSelect(associado: Associado): void {
        this.selectedAssociado = associado;
    }

    gotoNovoAssociado() {
        this.router.navigate(['/Associado', 0]);
    }

    gotoBuscarAssociado() { }
}
