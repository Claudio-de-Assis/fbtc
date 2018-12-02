import { AssociadoDao } from '../../shared/model/associado';
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
import { AuthService } from '../../shared/services/auth.service';
import { UserProfile } from '../../shared/model/user-profile';
import { stringify } from '@angular/core/src/util';

@Component({
    selector: 'app-associado-captacao-form',
    templateUrl: './associado-captacao.form.component.html',
    styleUrls: ['./associado-captacao.form.component.css'],
    providers: [AssociadoService, CepCorreiosService, ValueShareService]
})
/** AssociadoCaptacaoForm component*/
export class AssociadoCaptacaoFormComponent implements OnInit {

    enderecoPri: Endereco = { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '', ordemEndereco: '1',
    bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
    cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''};

    enderecoSec: Endereco = { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '', ordemEndereco: '2',
    bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
    cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''};

    enderecos: Endereco[];

    @Input() associado: AssociadoDao = { associadoId: 0, atcId: null, tipoPublicoId: null, nrMatricula: '', crp: '',
            crm: '', nomeInstFormacao: '', certificado: false, dtCertificacao: null, divulgarContato: false,
            tipoFormaContato: '', nrTelDivulgacao: '',
            comprovanteAfiliacaoAtc: '', tipoProfissao: '', tipoTitulacao: '',
            pessoaId: 0, nome: '', cpf: '', rg: '', eMail: '', nomeFoto: '_no-foto.png',
            sexo: '', dtNascimento: null, nrCelular: '', passwordHash: '',
            dtCadastro: null, ativo: true, perfilId: 0, membroConfi: false, membroDiretoria: false, anuidadeAtcOk: false,
            enderecosPessoa: this.enderecos
    };

    title: string;
    badge: string;

    _util = Util;
    _nomeFotoPadrao: string;
    _nomeFoto: string;
    _msg: string;
    _msgRetorno: string;
    _msgRetEmail: string;
    _assocId: number;

    _endId: number;
    _pesId: number;
    _ordEnd: string;
    _isEMailValid: boolean;
    _pessoaId: number;

    _associadoCaptacaoSalvo: boolean;

    editPessoaId: number;

    alertClassType: string;

    _msgDeOrientacao: string;

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
        private valueShareService: ValueShareService,
        // private authService: AuthService
    ) {
        this.title = 'Captação de Usuário';
        this.badge = 'Novo';
        this._nomeFotoPadrao = '_no-foto.png';
        this._nomeFoto = '_no-foto.png';
        this.editPessoaId = 0;
        this._msg = '';
        this._msgRetorno = '';
        this._assocId = 0;
        this._endId = 0;
        this._pesId = 0;
        this._ordEnd = '';
        this.associado.enderecosPessoa = [this.enderecoPri, this.enderecoSec];
        this._isEMailValid = false;
        this.submitted = false;

        this._pessoaId = 0;
        this._associadoCaptacaoSalvo = false;
        this._msgRetEmail = '';

        this.alertClassType = 'alert alert-info';
        this._msgDeOrientacao = '...está quase pronto...';

        valueShareService.valueStringInformada$.subscribe(
            nomeFoto => {
                this.history.push(nomeFoto);
            });
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

        this.alertClassType = 'alert alert-info';
        this._msg = 'Salvando os dados. Por favor, aguarde...';

        this._nomeFoto = this.history[0];

        if (this._nomeFoto === undefined) {
            if (this.associado.associadoId === 0) {
                this._nomeFoto = this._nomeFotoPadrao;
            } else {
                this._nomeFoto = this.associado.nomeFoto;
            }
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
                this.submitted = false;
            }
        );
    }

    avaliaRetorno(msgRet: string) {

        if (msgRet.substring(0, 1) === '0') {

            this._pessoaId = parseInt(msgRet.substring(0, 10), 10);

            this.alertClassType = 'alert alert-success';
            this._msg = msgRet.substring(10);

            if (this._pessoaId > 0) {

                this.gotoEnviarSenha(this._pessoaId);

                this._associadoCaptacaoSalvo = true;
            }

        } else {

            this._associadoCaptacaoSalvo = false;

            this.alertClassType = 'alert alert-warning';
            this._msg = msgRet;
        }
    }

    gotoLogin() {

        this.router.navigate(['']);
    }

    avaliaRetornoEMail(msgRet: string) {

        if (msgRet !== 'OK') {

            alert(msgRet);
            this.associado.eMail = '';
        }
    }

    gotoEnviarSenha(_pessoaId: number) {

        this.alertClassType = 'alert alert-info';
        this._msg = 'Enviando a senha para o seu e-mail. Por favor, aguarde...';

        if (_pessoaId !== 0) {

            this.service.ressetPassWordById(_pessoaId)
            .subscribe(
                msg => {
                    this.alertClassType = 'alert alert-success';
                    this._msg = msg;
                    // tslint:disable-next-line:max-line-length
                    this._msgDeOrientacao = 'Por favor, com a senha enviada para o e-mail informado, faça o seu login na área restrita e realize o pagamento da sua Assinatura de Anuidade';
                }
            );
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

    ngOnInit(): void {

        this.getAtcs();

        this.getTiposPublicos();
    }

    refreshImages(status) {
        if (status) {
          console.log( 'Upload realizado com sucesso!');
        }
    }
}
