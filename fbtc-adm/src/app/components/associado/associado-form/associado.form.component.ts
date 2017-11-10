import { Component, OnInit, Input } from '@angular/core';
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
    lstSexo = ['Masculino','Feminino'];
    lstAtc = ['Rio de Janeiro', 'Alagoas', 'São Paulo'];
    lstTipoContato=['E-Mail','Celular','Endereço','Todos'];
    lstTipoPublico=['Profissional - Associado','Estudante de Pós - Associado','Estudante - Associado'];
    lstSimNao=['Sim','Não'];

    
    private selectedId: any;
    
    title: string = 'Dados do Associado - Edição';

    @Input() associado2 : Associado;

    associado$: Observable<Associado>;

    associado: Associado;
    
    editAssociadoId: number = 0;
    editNome: string = "";
    editDtNascimento: Date;
    editEMail: string;
    editSexo: string = '';
    editCelular: string;
    editCPF: string;
    editRG: string;
    editMatricula: string;
    editATC: number = 0;
    editTipoId: number = 0;
    editCRP: string;
    editCRM: string;
    editNomeInstFormacao: string;
    editDtCadastro: Date;
    editCertificado: boolean = false;
    editDtCertificacao: Date;
    editDivulgarContato: boolean = false;
    editTipoFormaContato: string = '';
    editIntegraDiretoria: boolean = true;
    editIntegraConfi: boolean = true;
    editCEP: string;
    editEndereco: string;
    editNumero: string;
    editComplemento: string;
    editBairro: string;
    editCidade: string;
    editEstado: string;
    editAtivo: boolean = true;

    /** AssociadoFrm ctor */
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: AssociadoService
    ) { }

    /** Called by Angular after AssociadoForm component initialized */
    ngOnInit() {
        this.associado$ = this.route.paramMap
            .switchMap((params: ParamMap) => this.service.getAssociadoById(params.get('id'))); 
           
            this.associado$.subscribe((associado: Associado)=>{this.associado=associado});
            
            this.editAssociadoId = this.associado.AssociadoId;
            this.editNome = this.associado.Nome;
            this.editDtNascimento = this.associado.DtNascimento;
            this.editEMail = this.associado.EMail;
            this.editSexo = this.associado.Sexo;
            this.editCelular = this.associado.NrCelular;
            this.editCPF = this.associado.Cpf;
            this.editRG = this.associado.Rg;
            this.editMatricula = this.associado.NrMatricula;
            this.editATC = this.associado.AtcId;
            this.editTipoId = this.associado.TipoPublicoId;
            this.editCRP = this.associado.Crp;
            this.editCRM = this.associado.Crm;
            this.editNomeInstFormacao = this.associado.NomeInsFormacao
            this.editDtCadastro = this.associado.DtCadastro;
            this.editCertificado = this.associado.Certificado;
            this.editDtCertificacao = this.associado.DtCertificacao;
            this.editDivulgarContato = this.associado.DivulgarContato;
            this.editTipoFormaContato = this.associado.TipoFormaContato;
            this.editIntegraDiretoria = this.associado.IntegraDiretoria
            this.editIntegraConfi = this.associado.IntegraConfi;
            this.editCEP = "";
            this.editEndereco = "";
            this.editNumero = "";
            this.editComplemento = "";
            this.editBairro = "";
            this.editCidade = "";
            this.editEstado = "";
            this.editAtivo = true;
    }

    gotoAssociados() {
        let associadoId = this.associado ? this.associado.AssociadoId : null;
        // Pass along the Associado id if available
        // so that the AssociadoList component can select that Associado.
        // Include a junk 'foo' property for fun.
        this.router.navigate(['/Associado', { id: associadoId, foo: 'foo' }]);
    }

    save() {
        this.gotoAssociados();

    }

    excluir() {
        this.gotoAssociados();
    }
}