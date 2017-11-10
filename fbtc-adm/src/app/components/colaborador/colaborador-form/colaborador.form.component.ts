import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { ColaboradorService } from '../../shared/services/colaborador.service';

@Component({
    selector: 'colaborador-form',
    templateUrl: './colaborador.form.component.html',
    styleUrls: ['./colaborador.form.component.css']
})
/** ColaboradorFrm component*/
export class ColaboradorFormComponent implements OnInit
{
    /** ColaboradorForm ctor */
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: ColaboradorService
    ) { }
     

    /** Called by Angular after ColaboradorForm component initialized */
    ngOnInit(): void { }
}