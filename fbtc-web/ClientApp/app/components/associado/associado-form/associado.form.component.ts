import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

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
    private selectedId: any;

    associado$: Observable<Associado>;

    associado: Associado;

    editNome: string;
    editDtNascimento: Date;
    editEMail: string;
    editSexo: string;
    editCelular: string;
    editCPF: string;
    editRG: string;
    editMatricula: string;
    editATC: string;
    editTipo: string;
    editCRP: string;
    editCRM: string;
    editInstFormacao: string;
    editTerapeutaAssociado: boolean;
    editDtCertificacao: boolean;
    editDivulgarContato: boolean;
    editTipoContato: string;
    editDiretoria: boolean;
    editConfi: boolean;
    editCEP: string;
    editEndereco: string;
    editNumero: string;
    editComplemento: string;
    editBairro: string;
    editCidade: string;
    editEstado: string;
    editAtivo: boolean;

    /** AssociadoFrm ctor */
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: AssociadoService
    ) { }

    /** Called by Angular after AssociadoForm component initialized */
    ngOnInit() {
        this.associado$ = this.route.paramMap
            .switchMap((params: ParamMap) =>
                this.service.getAssociadoById(params.get('selectedId')));

        /*this.route.data.subscribe((data: { associado: Associado }) => {
            this.editNome = data.associado.Nome;
            this.editDtNascimento = data.associado.DtNascimento;
        });*/
    }

    gotoAssociados() {
        let associadoId = this.associado ? this.associado.AssociadoId : null;
        // Pass along the Associado id if available
        // so that the AssociadoList component can select that Associado.
        // Include a junk 'foo' property for fun.
        this.router.navigate(['/associados', { id: associadoId, foo: 'foo' }]);
    }

    save() {
        this.associado.Nome = this.editNome;
        this.associado.DtNascimento = this.editDtNascimento;
        this.gotoAssociados();

    }

    cancel() {
        this.gotoAssociados();
    }
}