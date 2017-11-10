import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { AssociadoService } from '../../shared/services/associado.service';
import { Associado } from '../../shared/model/associado';
import { Http } from '@angular/http';

@Component({
    selector: 'associado-form',
    templateUrl: './associado.form.component.html',
    styleUrls: ['./associado.form.component.css']
})
/** AssociadoForm component*/
export class AssociadoFormComponent implements OnInit
{
    constructor(private http: Http) {
    }

    getListAssociados() {
        return this.http.get('api/associado/GetAll')
            .map(r => r.json() as Associado[])
            .toPromise();
    }


    getAssociadoById(id: string) {
        return this.http.get(`api/associado/Get/${id}`)
            .map(r => r.json() as Associado[])
            .toPromise();
    }

    ngOnInit() {
        return this.getListAssociados();
    }
    
}