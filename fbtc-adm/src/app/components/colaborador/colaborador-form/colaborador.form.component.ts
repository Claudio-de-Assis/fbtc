import { Endereco } from './../../shared/model/endereco';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

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

    enderecos: Endereco[];

    @Input() colaborador: Colaborador = { colaboradorId: 0, tipoPerfil: '',
        pessoaId: 0, nome: '', cpf: '', rg: '', eMail: '', nomeFoto: '',
        sexo: '', dtNascimento: null, nrCelular: '', passwordHash: '',
        dtCadastro: null, ativo: true,
        enderecosPessoa: this.enderecos
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
     }

    getColaboradorById(id: number): void {

        this.service.getById(id)
            .subscribe(colaborador => this.colaborador = colaborador);
    }

    setColaborador(): void {

        this.service.setColaborador()
            .subscribe(colaborador => this.colaborador = colaborador);
    }

    gotoValidarEMail() {

        this.service.getValidaEMail(this.colaborador.colaboradorId, this.colaborador.eMail)
        .subscribe(
            msg => {
                this._msgRetorno = msg;
                this.avaliaRetornoEMail(this._msgRetorno);
            });
    }

    save() {

         this.service.addColaborador(this.colaborador)
         .subscribe(
            msg => {
                this._msgRetorno = msg;
                this.avaliaRetorno(this._msgRetorno);
            }
        );

        this.submitted = false;
    }

    gotoShowPopUp() {

      this.editMessagem = 'Registro salvo com sucesso!';
      this.editShowPopup = true;
      // Colocar a chamada para a implementação do PopUp modal de aviso:
      alert('Registro salvo com sucesso!');
    }

    avaliaRetorno(msgRet: string) {

        if (msgRet.substring(0, 1) === '0') {

            this._colabId = parseInt(msgRet.substring(0, 10), 10);

            this.router.navigate([`/Colaborador/${this._colabId}`]);

            this.getColaboradorById(this._colabId);

            this._msg = this._msgRetorno.substring(10);

            this.badge = 'Edição';

        } else {

            this._msg = this._msgRetorno;
        }
    }

    avaliaRetornoEMail(msgRet: string) {

        if (msgRet !== 'OK') {

            alert(msgRet);
            this.colaborador.eMail = '';
        }
    }

    gotoReenviarSenha() {

        this._msg = '';
        if (this._id !== 0) {

            this.service.ressetPassWordById(this._id)
            .subscribe(msg => this._msg = msg);

        } else {

            this._msg = 'Atenção: Você precisa primeiro incluir o registro';
        }
    }

    gotoColaboradores() {

        let colaboradorId = this.colaborador ? this.colaborador.colaboradorId : null;
        this.router.navigate(['admin/Colaborador', { id: colaboradorId, foo: 'foo' }]);
    }

    gotoValidacaoForm(): boolean {

        return true;
    }

    onSubmit() {

        this.submitted = true;
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
