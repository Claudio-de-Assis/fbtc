import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { AssociadoService } from '../../shared/services/associado.service';
import { CepCorreiosService } from '../../shared/services/cep-correios.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { AtcService } from '../../shared/services/atc.service';
import { ValueShareService } from '../../shared/services/value-share.service';

import { Associado } from '../../shared/model/associado';
import { TipoPublico } from '../../shared/model/tipo-publico';
import { Atc } from '../../shared/model/atc';

import { Util } from '../../shared/util/util';
import { FileUploadRoute } from '../../shared/webapi-routes/file-upload.route';
import { Endereco } from '../../shared/model/endereco';

@Component({
    selector: 'app-associado-self-form',
    templateUrl: './associado-self.form.component.html',
    styleUrls: ['./associado-self.form.component.css'],
    providers: [AssociadoService, CepCorreiosService, ValueShareService]
})
/** AssociadoSelfForm component*/
export class AssociadoSelfFormComponent implements OnInit {

    /*
    endereco: Endereco = { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '', ordemEndereco: '',
    bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
    cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''};*/

    enderecoPri: Endereco = { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '', ordemEndereco: '1',
    bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
    cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''};

    enderecoSec: Endereco = { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '', ordemEndereco: '2',
    bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
    cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''};


    enderecos: Endereco[];

    @Input() associado: Associado = { associadoId: 0, atcId: null, tipoPublicoId: null, nrMatricula: '', crp: '',
            crm: '', nomeInstFormacao: '', certificado: false, dtCertificacao: null, divulgarContato: false,
            tipoFormaContato: '', integraDiretoria: false, integraConfi: false, nrTelDivulgacao: '',
            comprovanteAfiliacaoAtc: '', tipoProfissao: '', tipoTitulacao: '',
            pessoaId: 0, nome: '', cpf: '', rg: '', eMail: '', nomeFoto: '_no-foto.png',
            sexo: '', dtNascimento: null, nrCelular: '', passwordHash: '',
            dtCadastro: null, ativo: true, perfilId:0,
            enderecosPessoa: this.enderecos
    };

    title = 'Usuário'; // Associado
    badge = '';

    _util = Util;
    _nomeFotoPadrao: string;
    _nomeFoto: string;
    _msg: string;
    _msgRetorno: string;
    _assocId: number;

    _endId: number;
    _pesId: number;
    _ordEnd: string;
    _isEMailValid: boolean;

    editAssociadoId: number;

    private selectedId: any;

    tiposPublicos: TipoPublico[];
    atcs: Atc[];

    submitted: boolean;

    history: string[] = [];

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
        this._msgRetorno = '';
        this._assocId = 0;
        this._endId = 0;
        this._pesId = 0;
        this._ordEnd = '';
        this.associado.enderecosPessoa = [this.enderecoPri, this.enderecoSec];
        this._isEMailValid = false;
        this.submitted = false;

        valueShareService.valueStringInformada$.subscribe(
            nomeFoto => {
                this.history.push(nomeFoto);
            });
    }

    getAssociadoById(id: number): void {

        this.service.getById(id)
            .subscribe(associado => this.associado = associado);
    }

    gotoValidarEMail() {

        this.service.getValidaEMail(this.associado.associadoId, this.associado.eMail)
        .subscribe(
            msg => {
                this._msgRetorno = msg;
                this.avaliaRetornoEMail(this._msgRetorno);
            });
    }


    save() {

        this._msg = '';

        this._nomeFoto = this.history[0];

        if (this._nomeFoto === undefined) {
            this._nomeFoto = this._nomeFotoPadrao;
        }

        if (this.associado.enderecosPessoa[0].ordemEndereco === '') {
            this.associado.enderecosPessoa[0].ordemEndereco = '1';
        }

        if (this.associado.enderecosPessoa[1].ordemEndereco === '') {
            this.associado.enderecosPessoa[1].ordemEndereco = '2';
        }

        this.associado.nomeFoto = this._nomeFoto;
        this.service.addAssociado(this.associado)
        .subscribe(
            msg => {
                this._msgRetorno = msg;
                this.avaliaRetorno(this._msgRetorno);
            }
        );

        this.submitted = false;
    }

    avaliaRetorno(msgRet: string) {

        if (msgRet.substring(0, 1) === '0') {

            this._assocId = parseInt(msgRet.substring(0, 10), 10);

            this.router.navigate([`/Associado/${this._assocId}`]);

            this.getAssociadoById(this._assocId);

            this._msg = this._msgRetorno.substring(10);

            this.badge = 'Edição';

        } else {

            this._msg = this._msgRetorno;
        }
    }

    avaliaRetornoEMail(msgRet: string) {

        if (msgRet !== 'OK') {

            alert(msgRet);
            this.associado.eMail = '';
        }
    }

    gotoReenviarSenha() {

        this._msg = '';
        if (this.editAssociadoId !== 0) {

            this.service.ressetPassWordById(this.editAssociadoId)
            .subscribe(msg => this._msg = msg);

        } else {

            alert('Atenção: Por favor, faça o login');
        }
    }


    gotoShowPopUp(msg: string) {

      // Colocar a chamada para a implementação do PopUp modal de aviso:
      alert(msg);
    }

    getTiposPublicos(): void {

        this.serviceTP.getTiposPublicos('true').subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
    }

    getAtcs(): void {

        this.serviceAtc.getAtcs().subscribe(atcs => this.atcs = atcs);
    }

    getEnderecoByCep(indice: number): void {

        this.associado.enderecosPessoa[indice].logradouro = '';
        this.associado.enderecosPessoa[indice].numero = '';
        this.associado.enderecosPessoa[indice].complemento = '';
        this.associado.enderecosPessoa[indice].bairro = '';
        this.associado.enderecosPessoa[indice].cidade = '';
        this.associado.enderecosPessoa[indice].estado = '';
        this.associado.enderecosPessoa[indice].tipoEndereco = '';

        if (this.associado.enderecosPessoa[indice].cep.length > 7 ) {

            this._endId = this.associado.enderecosPessoa[indice].enderecoId;
            this._pesId = this.associado.enderecosPessoa[indice].pessoaId;
            this._ordEnd = this.associado.enderecosPessoa[indice].ordemEndereco;

            this.serviceCEP.getByCep(this.associado.enderecosPessoa[indice].cep)
                .subscribe(endereco => {
                    this.associado.enderecosPessoa[indice] = endereco;
                    this.associado.enderecosPessoa[indice].enderecoId = this._endId;
                    this.associado.enderecosPessoa[indice].pessoaId = this._pesId;
                    this.associado.enderecosPessoa[indice].ordemEndereco = this._ordEnd;
                    this.associado.enderecosPessoa[indice].numero = '';
                    this.associado.enderecosPessoa[indice].complemento = '';
                    });
        }
    }

    onSubmit() {

        this.submitted = true;
        this.save();
    }

    /** Called by Angular after AssociadoSelfForm component initialized */
    ngOnInit(): void {

        this.getAtcs();

        this.getTiposPublicos();

        this.editAssociadoId = +this.route.snapshot.paramMap.get('id');

        this.badge = 'Edição';
        this.getAssociadoById(this.editAssociadoId);
    }

    refreshImages(status) {
        if (status) {
          console.log( 'Upload realizado com sucesso!');
        }
    }
}
