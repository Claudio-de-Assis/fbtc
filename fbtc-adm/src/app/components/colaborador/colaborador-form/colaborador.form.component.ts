import { Perfil } from '../../shared/model/perfil';
import { Endereco } from '../../shared/model/endereco';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { ColaboradorService } from '../../shared/services/colaborador.service';
import { Colaborador } from '../../shared/model/colaborador';

import { Util } from '../../shared/util/util';

@Component({
    selector: 'app-colaborador-form',
    templateUrl: './colaborador.form.component.html',
    styleUrls: ['./colaborador.form.component.css']
})
/** ColaboradorFrm component*/
export class ColaboradorFormComponent implements OnInit {

    enderecos: Endereco[];
    perfil: Perfil;

    @Input() colaborador: Colaborador = { colaboradorId: 0,
        pessoaId: 0, nome: '', cpf: '', rg: '', eMail: '', nomeFoto: '',
        sexo: '', dtNascimento: null, nrCelular: '', passwordHash: '',
        dtCadastro: null, ativo: true,
        enderecosPessoa: this.enderecos,
        perfilId: 0
    };

    title: string;
    badge: string;
    _msg: string;
    _msgRetorno: string;
    _id: number;

    _colabId: number;
    _isEMailValid: boolean;

    _util = Util;

    @Input() editMessagem: string;
    @Input() editShowPopup: boolean;

    alertClassType: string;

    _msgProgresso: string;

    submitted: boolean;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: ColaboradorService
    ) {
        this.title = 'Integrante da Administração';
        this.badge = '';
        this.editMessagem = '';
        this.editShowPopup = false;
        this._msg = '';
        this._id = 0;
        this.submitted = false;

        this._colabId = 0;
        this._msgRetorno = '';
        this._isEMailValid = false;

        this.alertClassType = 'alert alert-info';

        this._msgProgresso = '';
     }

    getColaboradorById(id: number): void {

        this._msgProgresso = '...Carregando os dados. Por favor, aguarde!...';

        this.service.getById(id)
            .subscribe(colaborador => {
                this.colaborador = colaborador;
                this._msgProgresso = '';
            });
    }

    gotoValidarEMail(): void {

        this.service.getValidaEMail(this.colaborador.pessoaId, this.colaborador.eMail)
        .subscribe(
            msg => {
                this._msgRetorno = msg;
                this.avaliaRetornoEMail(this._msgRetorno);
            });
    }

    save(): void {

        if (this.submitted === false) {
            this.submitted = true;
        } else {
            return;
        }

        this.alertClassType = 'alert alert-info';
        this._msg = 'Salvando os dados. Por favor, aguarde...';

         this.service.addColaborador(this.colaborador)
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

            this._colabId = parseInt(msgRet.substring(0, 10), 10);

            // this.router.navigate([`admin/Colaborador/${this._colabId}`]);

            this.getColaboradorById(this._colabId);

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
            this.colaborador.eMail = '';
        }
    }

    gotoReenviarSenha(): void {

        if (this.submitted === false) {
            this.submitted = true;
        } else {
            return;
        }

        this.alertClassType = 'alert alert-info';
        this._msg = 'Enviando a senha para o e-mail do associado. Por favor, aguarde...';

        if (this._id !== 0) {

            this.service.ressetPassWordById(this._id)
            .subscribe(msg => {
                this._msg = msg;
                this.gotoAvaliaRetornoEMail(this._msg);
                this.submitted = false;
            });

        } else {
            this.submitted = false;
            this._msg = 'Atenção: Você precisa primeiro incluir o registro';
        }
    }

    gotoAvaliaRetornoEMail(msg: string): void {

        if (msg.substring(0, 7) === 'ATENÇÃO') {
            this.alertClassType = 'alert alert-danger';

        } else {
            this.alertClassType = 'alert alert-success';
        }
    }

    gotoColaboradores(): void {

        const colaboradorId = this.colaborador ? this.colaborador.colaboradorId : null;
        this.router.navigate(['admin/Colaborador', { id: colaboradorId, foo: 'foo' }]);
    }

    gotoValidacaoForm(): boolean {

        return true;
    }

    onSubmit(): void {

        this.save();
    }

    /** Called by Angular after ColaboradorForm component initialized */
    ngOnInit(): void {

        const id = +this.route.snapshot.paramMap.get('id');
        if (id > 0) {
            this.badge = 'Edição';
            this.getColaboradorById(id);
            this._id = id;
        } else {
            this.badge = 'Novo';
        }
    }
}
