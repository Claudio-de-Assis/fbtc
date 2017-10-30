import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { Associado } from '../../shared/model/associado';
import { AssociadoService } from '../../shared/services/associado.service';
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
    selector: 'associado-list',
    templateUrl: './associado.list.component.html',
    styleUrls: ['./associado.list.component.css']
})
/** AssociadoList component*/
export class AssociadoListComponent implements OnInit
{
    associado$: Observable<Associado[]>;

    private associadoId: any;
    
    /** AssociadoList ctor */
    constructor(
        private service: AssociadoService,
        private route: ActivatedRoute
    ) { }
    
    /** Called by Angular after AssociadoLst component initialized */
    ngOnInit(): void {
       /* this.associado$ = this.route.paramMap
            .switchMap((params: ParamMap) => {
                this.associadoId = params.get('associado.AssociadoId');
                return this.service.getAssociadoById(this.associadoId);
            });*/
    }
}