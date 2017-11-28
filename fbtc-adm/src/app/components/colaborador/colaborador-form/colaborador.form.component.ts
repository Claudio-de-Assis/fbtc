import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { ColaboradorService } from '../../shared/services/colaborador.service';
import { Colaborador } from './../../shared/model/colaborador';

@Component({
    selector: 'app-colaborador-form',
    templateUrl: './colaborador.form.component.html',
    styleUrls: ['./colaborador.form.component.css']
})
/** ColaboradorFrm component*/
export class ColaboradorFormComponent implements OnInit {

    lstPerfil = ['Gestor do Site', 'Secretaria', 'Financeiro'];
    lstSimNao= ['Sim', 'Não'];

    private selectedId: any;

    title = 'Dados do Usuário - Edição';

    colaborador$: Observable<Colaborador>;

    colaborador: Colaborador;

    editColaboradorId: number;
    editNome: string;
    editTipoPerfil: string;
    editEMail: string;
    editCelular: string;
    editAtivo: boolean;

    /** ColaboradorForm ctor */
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: ColaboradorService
    ) { }

    /** Called by Angular after ColaboradorForm component initialized */
    ngOnInit() {
        this.colaborador$ = this.route.paramMap
        .switchMap((params: ParamMap) => this.service.getColaboradorById(params.get('id')));

        this.colaborador$.subscribe((colaborador: Colaborador) => {this.colaborador = colaborador});

        this.editColaboradorId = this.colaborador ? this.colaborador.ColaboradorId : 0;
        this.editNome = this.colaborador ? this.colaborador.nome : '';
        this.editTipoPerfil = this.colaborador ? this.colaborador.TipoPerfil : null;
        this.editEMail = this.colaborador ? this.colaborador.eMail : '';
        this.editCelular = this.colaborador ? this.colaborador.nrCelular : '';
        this.editAtivo = this.colaborador ? true : false;
     }

     gotoColaboradores() {
        let colaboradorId = this.colaborador ? this.colaborador.ColaboradorId : null;
        this.router.navigate(['/Colaborador', { id: colaboradorId, foo: 'foo' }]);
    }

    save() {
        this.gotoColaboradores();
    }

    excluir() {
        this.gotoColaboradores();
    }
}
