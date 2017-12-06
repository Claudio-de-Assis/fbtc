import { TipoPublico } from './../../shared/model/tipo-publico';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { Associado } from '../../shared/model/associado';
import { AssociadoService } from '../../shared/services/associado.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';

@Component({
    selector: 'app-associado-list',
    templateUrl: './associado.list.component.html',
    styleUrls: ['./associado.list.component.css'],
    providers: [AssociadoService]
})
/** AssociadoList component*/
export class AssociadoListComponent implements OnInit {

 /* Tipos Aceitos: Psicólogo: 7, Médico: 8 */
    lstSexo = ['Masculino', 'Feminino'];
    lstAtc = ['Rio de Janeiro', 'Alagoas', 'São Paulo'];
    lstProfissao= ['Médico', 'Psicólogo'];

    title = 'Consulta de Associados';

    associados: Associado[];
    tiposPublicos: TipoPublico[];

    private selectedAssociado: Associado;

    /** AssociadoList ctor */
    constructor(
        private service: AssociadoService,
        private serviceTP: TipoPublicoService,
        private router: Router,
        private route: ActivatedRoute
    ) { }

    getAssociados(): void {
        this.service.getAssociados().subscribe(associados => this.associados = associados);
    }

    /** Called by Angular after AssociadoList component initialized */
    ngOnInit(): void {
        this.getTiposPublicos();

        this.getAssociados();
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
