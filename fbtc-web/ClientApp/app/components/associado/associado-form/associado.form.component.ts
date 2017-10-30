import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { AssociadoService } from '../../shared/services/associado.service';
import { Associado } from '../../shared/model/associado';

@Component({
    selector: 'associado-form',
    templateUrl: './associado.form.component.html',
    styleUrls: ['./associado.form.component.css']
})
/** AssociadoForm component*/
export class AssociadoFormComponent implements OnInit
{
    associado$: Associado;
    
    /** AssociadoFrm ctor */
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: AssociadoService
    ) { }

    /** Called by Angular after AssociadoFrm component initialized */
    ngOnInit(): void {

    }
}