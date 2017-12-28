import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

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

    @Input() colaborador: Colaborador = { colaboradorId: 0, tipoPerfil: '',
        pessoaId: 0, nome: '', cpf: '', rg: '', eMail: '', nomeFoto: '',
        sexo: '', dtNascimento: null, nrCelular: '', passwordHash: '',
        dtCadastro: null, ativo: true,
        enderecoPessoa: { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '',
            bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
            cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''}
    };

    title = 'Integrante da Equipe';
    badget = '';

    private selectedId: any;

    _util = Util;

    @Input() editMessagem: string = '';
    @Input() editShowPopup: boolean = false;

    submitted = false;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: ColaboradorService
    ) { }

    getColaboradorById(id: number): void {

        this.service.getById(id)
            .subscribe(colaborador => this.colaborador = colaborador);
    }

    setColaborador(): void {

        this.service.setColaborador()
            .subscribe(colaborador => this.colaborador = colaborador);
    }

    save() {

        this.service.addColaborador(this.colaborador)
            .subscribe(() =>  this.gotoShowPopUp());

        this.submitted = false;
    }

    gotoShowPopUp() {

      this.editMessagem = 'Registro salvo com sucesso!';
      this.editShowPopup = true;
      // Colocar a chamada para a implementação do PopUp modal de aviso:
      alert('Registro salvo com sucesso!');
    }

    /*excluir() {
        this.gotoColaboradores();
    }*/

    reenviarSenha() {

        alert('Senha reenviada com sucesso!');
    }


    gotoColaboradores() {

        let colaboradorId = this.colaborador ? this.colaborador.colaboradorId : null;
        this.router.navigate(['/Colaborador', { id: colaboradorId, foo: 'foo' }]);
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
            this.badget = 'Edição';
            this.getColaboradorById(id);
        } else {
            this.badget = 'Novo';
            // this.setColaborador();
        }
    }
}
