import { Pessoa } from './../../shared/model/pessoa';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { AssociadoService } from '../../shared/services/associado.service';
import { CepCorreiosService } from './../../shared/services/cep-correios.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { AtcService } from './../../shared/services/atc.service';
import { ValueShareService } from './../../shared/services/value-share.service';

import { Associado } from '../../shared/model/associado';
import { TipoPublico } from '../../shared/model/tipo-publico';
import { Atc } from './../../shared/model/atc';

import { Util } from './../../shared/util/util';
import { FileUploadRoute } from './../../shared/webapi-routes/file-upload.route';

@Component({
    selector: 'app-associado-form',
    templateUrl: './associado.form.component.html',
    styleUrls: ['./associado.form.component.css'],
    providers: [AssociadoService, CepCorreiosService, ValueShareService]
})
/** AssociadoForm component*/
export class AssociadoFormComponent implements OnInit {

    @Input() associado: Associado = { associadoId: 0, atcId: null, tipoPublicoId: null, nrMatricula: '', crp: '',
            crm: '', nomeInstFormacao: '', certificado: false, dtCertificacao: null, divulgarContato: false,
            tipoFormaContato: '', integraDiretoria: false, integraConfi: false, nrTelDivulgacao: '',
            comprovanteAfiliacaoAtc: '', tipoProfissao: '', tipoTitulacao: '',
            pessoaId: 0, nome: '', cpf: '', rg: '', eMail: '', nomeFoto: '_no-foto.png',
            sexo: '', dtNascimento: null, nrCelular: '', passwordHash: '',
            dtCadastro: null, ativo: true,
            enderecoPessoa: { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '',
                bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
                cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''}
    };

    title = 'Usuário'; // Associado
    badge = '';

    _util = Util;
    _nomeFotoPadrao: string;
    _nomeFoto: string;
    _msg: string;

    editAssociadoId: number;

    private selectedId: any;

    tiposPublicos: TipoPublico[];
    atcs: Atc[];

    submitted = false;

    history: string[] = [];

    /** AssociadoFrm ctor */
    constructor(
        private service: AssociadoService,
        private serviceTP: TipoPublicoService,
        private router: Router,
        private route: ActivatedRoute,
        private serviceCEP: CepCorreiosService,
        private serviceAtc: AtcService,
        private apiRoute: FileUploadRoute,
        private valueShareService: ValueShareService
    ) {
        this._nomeFotoPadrao = '_no-foto.png';
        this._nomeFoto = '_no-foto.png';
        this.editAssociadoId = 0;
        this._msg = '';

        valueShareService.valueStringInformada$.subscribe(
            nomeFoto => {
                this.history.push(nomeFoto);
            });
    }

    getAssociadoById(id: number): void {

        this.service.getById(id)
            .subscribe(associado => this.associado = associado);
/*
            valueShareService.valueStringInformada$.subscribe(
                nomeFoto => {
                    this.history.push(nomeFoto);
                });*/
    }

    setAssociado(): void {

        this.service.setAssociado()
            .subscribe(associado => this.associado = associado);
    }

    gotoAssociados() {

        let associadoId = this.associado ? this.associado.associadoId : null;
        this.router.navigate(['/Associado', { id: associadoId, foo: 'foo' }]);
    }

    save() {

        this._nomeFoto = this.history[0];

        if (this._nomeFoto === undefined) {
            this._nomeFoto = this._nomeFotoPadrao;
        }

        this.associado.nomeFoto = this._nomeFoto;
        this.service.addAssociado(this.associado)
        .subscribe(() =>  this.gotoShowPopUp('Registro salvo com sucesso!'));

        this.submitted = false;
        this._msg = '';
    }

    gotoReenviarSenha() {

        this._msg = '';
        if (this.editAssociadoId !== 0) {

            this.service.ressetPassWordById(this.editAssociadoId)
            .subscribe(msg => this._msg = msg);

        } else {

            alert('Atenção: Você precisa primeiro incluir o registro');
        }
    }


    gotoShowPopUp(msg: string) {

      // Colocar a chamada para a implementação do PopUp modal de aviso:
      alert(msg);
    }

    excluir() {

        this.gotoAssociados();
    }

    getTiposPublicos(): void {

        this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
    }

    getAtcs(): void {

        this.serviceAtc.getAtcs().subscribe(atcs => this.atcs = atcs);
    }

    getEnderecoByCep(): void {

        this.associado.enderecoPessoa.logradouro = '';
        this.associado.enderecoPessoa.numero = '';
        this.associado.enderecoPessoa.complemento = '';
        this.associado.enderecoPessoa.bairro = '';
        this.associado.enderecoPessoa.cidade = '';
        this.associado.enderecoPessoa.estado = '';

        this.serviceCEP.getByCep(this.associado.enderecoPessoa.cep)
            .subscribe(endereco => this.associado.enderecoPessoa = endereco);
    }

    onSubmit() {

        this.submitted = true;
        this.save();
    }

    /** Called by Angular after AssociadoForm component initialized */
    ngOnInit(): void {

        this.getAtcs();

        this.getTiposPublicos();

        this.editAssociadoId = +this.route.snapshot.paramMap.get('id');

        if (this.editAssociadoId > 0) {
            this.badge = 'Edição';
            this.getAssociadoById(this.editAssociadoId);

        } else {
            this.badge = 'Novo';
        }
    }

    refreshImages(status) {
        if (status) {
          console.log( 'Upload realizado com sucesso!');
        }
    }
}
