import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { AssociadoService } from '../../shared/services/associado.service';
import { Associado } from '../../shared/model/associado';

@Component({
    selector: 'app-associado-form',
    templateUrl: './associado.form.component.html',
    styleUrls: ['./associado.form.component.css']
})
/** AssociadoForm component*/
export class AssociadoFormComponent implements OnInit {

    lstSexo = ['Masculino', 'Feminino'];
    lstAtc = ['Rio de Janeiro', 'Alagoas', 'São Paulo'];
    lstTipoContato= ['E-Mail', 'Celular', 'Endereço', 'Todos'];
    lstTipoPublico= ['Profissional - Associado', 'Estudante de Pós - Associado', 'Estudante - Associado'];
    lstSimNao= ['Sim', 'Não'];

    title: "Dados do Associado - Edição";

    /* Tipos Aceitos: Psicólogo: 7, Médico: 8*/
    lstProfissao= ['Médico', 'Psicólogo'];

    /*Graduado: 1, Especialista: 2,Mestre: 3,Doutor: 4,Pós-Doutor: 5*/
    lstTitulacao= ['Graduado', 'Especialista', 'Mestre', 'Doutor', 'Pós-Doutor'];

    private selectedId: any;

    // @Input() associado2: Associado;

    associado$: Observable<Associado>;

    associado: Associado;

    editAssociadoId: number;
    editNome: string;
    editDtNascimento: Date;
    editEMail: string;
    editSexo: string;
    editCelular: string;
    editCPF: string;
    editRG: string;
    editMatricula: string;
    editATC: number;
    editTipoId: number;
    editCRP: string;
    editCRM: string;
    editNomeInstFormacao: string;
    editDtCadastro: Date;
    editCertificado: boolean;
    editDtCertificacao: Date;
    editDivulgarContato: boolean;
    editTipoFormaContato: string;
    editIntegraDiretoria: boolean;
    editIntegraConfi: boolean;
    editNrTelDivilgacao: string;
    editComprovanteAfiliacaoAtc: string;
    editProfissao: string;
    editTitulacao: string;
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
            .switchMap((params: ParamMap) => this.service.getAssociadoById(params.get('id')));

            this.associado$.subscribe((associado: Associado) => {this.associado = associado});

            this.editAssociadoId = this.associado ? this.associado.AssociadoId : 0;
            this.editNome = this.associado ?  this.associado.Nome : '';
            this.editDtNascimento = this.associado ?  this.associado.DtNascimento : null;
            this.editEMail = this.associado ?  this.associado.EMail : '';
            this.editSexo = this.associado ?  this.associado.Sexo : '';
            this.editCelular = this.associado ?  this.associado.NrCelular : '';
            this.editCPF = this.associado ?  this.associado.Cpf : '';
            this.editRG = this.associado ?  this.associado.Rg : '';
            this.editMatricula = this.associado ?  this.associado.NrMatricula : '';
            this.editATC = this.associado ?  this.associado.AtcId : 0;
            this.editTipoId = this.associado ?  this.associado.TipoPublicoId  : 0;
            this.editCRP = this.associado ?  this.associado.Crp : '';
            this.editCRM = this.associado ?  this.associado.Crm : '';
            this.editNomeInstFormacao = this.associado ?  this.associado.NomeInsFormacao : '';
            this.editDtCadastro = this.associado ?  this.associado.DtCadastro : null;
            this.editCertificado = this.associado ?  this.associado.Certificado : true;
            this.editDtCertificacao = this.associado ?  this.associado.DtCertificacao : null;
            this.editDivulgarContato = this.associado ?  this.associado.DivulgarContato : false;
            this.editTipoFormaContato = this.associado ?  this.associado.TipoFormaContato  : '';
            this.editIntegraDiretoria = this.associado ?  this.associado.IntegraDiretoria : false;
            this.editIntegraConfi = this.associado ?  this.associado.IntegraConfi : false ;
            this.editNrTelDivilgacao = this.associado ? this.associado.NrTelDivulgacao : '';
            this.editComprovanteAfiliacaoAtc = this.associado ? this.associado.ComprovanteAfiliacaoAtc : '';
            this.editProfissao = this.associado ? this.associado.Profissao : '';
            this.editTitulacao = this.associado ? this.associado.Titulacao : '';
            this.editCEP = '';
            this.editEndereco = '';
            this.editNumero = '';
            this.editComplemento = '';
            this.editBairro = '';
            this.editCidade = '';
            this.editEstado = '';
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
