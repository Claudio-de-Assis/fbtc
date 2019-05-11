import { AssociadoDao } from './../../shared/model/associado';
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
    selector: 'app-associado-form',
    templateUrl: './associado.form.component.html',
    styleUrls: ['./associado.form.component.css'],
    providers: [AssociadoService, CepCorreiosService, ValueShareService]
})
/** AssociadoForm component*/
export class AssociadoFormComponent implements OnInit {

    enderecoPri: Endereco = { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '', ordemEndereco: '1',
    bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
    cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''};

    enderecoSec: Endereco = { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '', ordemEndereco: '2',
    bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
    cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''};

    enderecos: Endereco[];

    @Input() associado: AssociadoDao = { associadoId: 0, atcId: null, tipoPublicoId: null, nrMatricula: '', crp: '',
            crm: '', nomeInstFormacao: '', certificado: false, dtCertificacao: null, divulgarContato: false,
            tipoFormaContato: '',  nrTelDivulgacao: '',
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
    _assocId: number;
    _pessoaId: number;

    _endId: number;
    _pesId: number;
    _ordEnd: string;
    _isEMailValid: boolean;

    editPessoaId: number;

    private selectedId: any;

    tiposPublicos: TipoPublico[];
    atcs: Atc[];

    submitted: boolean;

    history: string[] = [];

    alertClassType: string;

    _msgProgresso: string;

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
        this.title = 'Usuário';
        this.badge = '';
        this._nomeFotoPadrao = '_no-foto.png';
        this._nomeFoto = '_no-foto.png';
        this.editPessoaId = 0;
        this._msg = '';
        this._msgRetorno = '';
        this._assocId = 0;
        this._pessoaId = 0;
        this._endId = 0;
        this._pesId = 0;
        this._ordEnd = '';
        this.associado.enderecosPessoa = [this.enderecoPri, this.enderecoSec];
        this._isEMailValid = false;
        this.submitted = false;

        this.alertClassType = 'alert alert-info';

        this._msgProgresso = '';

        valueShareService.valueStringInformada$.subscribe(
            nomeFoto => {
                this.history.push(nomeFoto);
            });
    }

    getAssociadoByPessoaId(id: number): void {

        if (this.submitted === false) {
            this.submitted = true;
        } else {
            return;
        }

        this._msgProgresso = '...Carregando os dados. Por favor, aguarde!...';

        this.service.getAssociadoDaoByPessoaId(id)
            .subscribe(associadoDao => {
                this.associado = associadoDao;
                this._msgProgresso = '';
                this.submitted = false;
            });
    }

    gotoAssociados() {

        if (this.submitted === false) {
            this.submitted = true;
        } else {
            return;
        }

        const pessoaId = this.associado ? this.associado.pessoaId : null;
        this.router.navigate(['/admin/Associado/', { id: pessoaId, foo: 'foo' }]);
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

        if (this.submitted === false) {
            this.submitted = true;
        } else {
            return;
        }

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

        this.associado.enderecosPessoa[0].cep = this._util.TelefoneSanity(this.associado.enderecosPessoa[0].cep);
        this.associado.enderecosPessoa[1].cep = this._util.TelefoneSanity(this.associado.enderecosPessoa[1].cep);
        this.associado.nrCelular = this._util.TelefoneSanity(this.associado.nrCelular);

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

    avaliaRetorno(msgRet: string): void {

        if (msgRet.substring(0, 1) === '0') {

            this._pessoaId = parseInt(msgRet.substring(0, 10), 10);

            this.getAssociadoByPessoaId(this._pessoaId);

            this.alertClassType = 'alert alert-success';
            this._msg = this._msgRetorno.substring(10);

            this.badge = 'Edição';

        } else {

            this.alertClassType = 'alert alert-success';
            this._msg = this._msgRetorno;
        }
    }

    avaliaRetornoEMail(msgRet: string): void {

        if (msgRet !== 'OK') {

            alert(msgRet);
            this.associado.eMail = '';
        }
    }

    gotoReenviarSenha(): void {

        this.alertClassType = 'alert alert-info';
        this._msg = 'Enviando a senha para o e-mail do associado. Por favor, aguarde...';

        if (this.editPessoaId !== 0) {

            this.service.ressetPassWordById(this.editPessoaId)
            .subscribe(msg => {
                this._msg = msg;
                this.gotoAvaliaRetornoEMail(this._msg);
            });

        } else {

            alert('Atenção: Você precisa primeiro incluir o registro');
        }
    }

    gotoAvaliaRetornoEMail(msg: string): void {

        if (msg.substring(0, 7) === 'ATENÇÃO') {
            this.alertClassType = 'alert alert-danger';

        } else {
            this.alertClassType = 'alert alert-success';
        }
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

    onSubmit(): void {

        this.save();
    }

    ngOnInit(): void {

        this.getAtcs();

        this.getTiposPublicos();

        this.editPessoaId = +this.route.snapshot.paramMap.get('id');

        if (this.editPessoaId > 0) {
            this.badge = 'Edição';
            this.getAssociadoByPessoaId(this.editPessoaId);

        } else {
            this.badge = 'Novo';
        }
    }

    /*
    refreshImages(status) {
        if (status) {
          console.log( 'Upload realizado com sucesso!');
        }
    }*/
}
