import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { ColaboradorService } from '../../shared/services/colaborador.service';
import { Colaborador } from './../../shared/model/colaborador';

import { Util } from './../../shared/util/util';

@Component({
    selector: 'app-colaborador-form',
    templateUrl: './colaborador.form.component.html',
    styleUrls: ['./colaborador.form.component.css']
})
/** ColaboradorFrm component*/
export class ColaboradorFormComponent implements OnInit {

    @Input() colaborador: Colaborador;

    title = 'Colaborador';
    badget = '';

    private selectedId: any;

    _util = Util;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: ColaboradorService
    ) { }

    getColaboradorById(id: number): void {

        this.service.getById(id)
            .subscribe(colaborador => this.colaborador = colaborador);
    }

    setColaborador(): void {

        this.service.setColaborador()
            .subscribe(colaborador => this.colaborador = colaborador);
    }

    save() {
        this.service.addColaborador(this.colaborador)
        .subscribe(() =>  this.gotoShowPopUp());
    }

    gotoShowPopUp() {

      // Colocar a chamada para a implementação do PopUp modal de aviso:
      alert('Registro salvo com sucesso!');
    }

    /*excluir() {
        this.gotoColaboradores();
    }*/

    gotoColaboradores() {

        let colaboradorId = this.colaborador ? this.colaborador.colaboradorId : null;
        this.router.navigate(['/Colaborador', { id: colaboradorId, foo: 'foo' }]);
    }

    /** Called by Angular after ColaboradorForm component initialized */
    ngOnInit(): void {

        const id = +this.route.snapshot.paramMap.get('id');
        if (id > 0) {
            this.badget = 'Edição';
            this.getColaboradorById(id);
        } else {
            this.badget = 'Novo';
            this.setColaborador();
        }
    }
}
