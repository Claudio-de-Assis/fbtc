
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { Util } from './../../shared/util/util';
import { FileUploadRoute } from './../../shared/webapi-routes/file-upload.route';

import { ValueShareService } from './../../shared/services/value-share.service';
import { UserProfileService } from './../../shared/services/user-profile.service';

import { UserProfile } from './../../shared/model/user-profile';
import { Endereco } from '../../shared/model/endereco';

@Component({
  selector: 'app-user-profile-form',
  templateUrl: './user-profile-form.component.html',
  styleUrls: ['./user-profile-form.component.css'],
  providers: [UserProfileService, ValueShareService]
})
export class UserProfileFormComponent implements OnInit {

  enderecoPri: Endereco = { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '', ordemEndereco: '1',
  bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
  cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''};

  enderecoSec: Endereco = { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '', ordemEndereco: '2',
  bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
  cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''};

  enderecos: Endereco[];

  @Input() userProfile: UserProfile = {pessoaId: 0, nome: '', cpf: '', rg: '', eMail: '', nomeFoto: '_no-foto.png',
  sexo: '', dtNascimento: null, nrCelular: '', passwordHash: '', dtCadastro: null, ativo: true, passwordHashReturned: '',
  enderecosPessoa: this.enderecos
  };

  title = 'Usuário Profile';
  badge = '';

  _util = Util;
  _nomeFotoPadrao: string;
  _nomeFoto: string;
  _msg: string;
  _msgSenha: string;
  _userProfId: number;

  _endId: number;
  _pesId: number;
  _ordEnd: string;
  _isEMailValid: boolean;
  editNovaSenha: string;
  editNovaSenhaConf: string;

  editUserProfileId: number;

  private selectedId: any;

  submitted: boolean;

  history: string[] = [];


  constructor(
    private service: UserProfileService,
    private router: Router,
    private route: ActivatedRoute,
    private apiRoute: FileUploadRoute,
    private valueShareService: ValueShareService
    ) {
    this._nomeFotoPadrao = '_no-foto.png';
    this._nomeFoto = '_no-foto.png';
    this.editUserProfileId = 0;
    this._msg = '';
    this._msgSenha = '';
    this._userProfId = 0;
    this._endId = 0;
    this._pesId = 0;
    this._ordEnd = '';
    this.userProfile.enderecosPessoa = [this.enderecoPri, this.enderecoSec];
    this._isEMailValid = false;
    this.submitted = false;
    this.editNovaSenha = '';
    this.editNovaSenhaConf = '';

    valueShareService.valueStringInformada$.subscribe(
        nomeFoto => {
            this.history.push(nomeFoto);
        });
  }

  getUserProfileById(id: number): void {

    this.service.getById(id)
        .subscribe(
          userProfile => [
            this.userProfile = userProfile
          ]);
  }

  save() {

    this._msg = '';
    this._msgSenha = '';

    this._nomeFoto = this.history[0];

    if (this._nomeFoto === undefined) {
        this._nomeFoto = this._nomeFotoPadrao;
    }

    if (this.editNovaSenha !== '') {
      this.submitted = false;

      if (this.editNovaSenhaConf === '') {
        this._msgSenha = 'Por favor, confirme a senha!';
        return;
      }

      if (this.editNovaSenha !== this.editNovaSenhaConf ) {
        this._msgSenha = 'A confirmação da senha não Confere!';
        this.submitted = false;

         return;
      }
    }

    this.userProfile.passwordHashReturned = this.editNovaSenha;

    this.userProfile.nomeFoto = this._nomeFoto;
    this.service.addUserProfile(this.userProfile)
    .subscribe(
        msg => {
            this._msg = msg;
            this.submitted = false;
            this.editNovaSenha = '';
            this.editNovaSenhaConf = '';
        }
    );
  }

  onSubmit() {

    if (this.submitted === true) {return; }

    this.submitted = true;
    this.save();
  }

  gotoHome(): void {

    this.router.navigate([`/`]);
  }

  /** Called by Angular after AssociadoForm component initialized */
  ngOnInit(): void {

    this.editUserProfileId = +this.route.snapshot.paramMap.get('id');

    if (this.editUserProfileId > 0) {
        this.badge = 'Edição';
        this.getUserProfileById(this.editUserProfileId);

    } else {
      console.log( 'ID do usuário não informado!');
      this.gotoHome();
    }
  }

  refreshImages(status) {
      if (status) {
        console.log( 'Upload realizado com sucesso!');
      }
  }

}
