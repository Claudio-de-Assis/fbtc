import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxPermissionsService } from 'ngx-permissions';

import { AuthService } from '../shared/services/auth.service';

import { UserProfile } from './../shared/model/user-profile';
import { UserProfileService } from '../shared/services/user-profile.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login.form.component.html',
  styleUrls: ['./login.form.component.css']
})

export class LoginComponent {

  message: string;
  title: string;
  _msg: string;
  _msgDng: string;
  _msgPWD: string;
  editeMail: string;
  editPassword: string;
  permission = [];

  constructor(
      public authService: AuthService,
      public userProfileService: UserProfileService,
      public router: Router,
      private permissionsService: NgxPermissionsService) {
    this.setMessage();
    this.title = 'Login';
    this._msg = '';
    this._msgDng = '';
    this.editeMail = '';
    this.editPassword = '';
    this._msgPWD = '';
  }

  setMessage() {
    this.message = 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out');

// console.log ( 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out'));
  }

  login() {

    this._msg = '';
    this._msgDng = '';

    this.message = 'Trying to log in ...';

//  console.log('Trying to log in ...');

    if (this.editeMail.trim() === '') {
      this._msgDng = 'Por favor, informe o seu E-Mail.';
      return;
    }

    if (this.editPassword.trim() === '') {
      this._msgDng = 'Por favor, informe a sua Senha.';
      return;
    }

//  console.log('pre: ' + this.authService.isLoggedIn);

    this.editeMail = this.editeMail.trim();
    this.editeMail = this.editeMail.toLowerCase();

    this.editPassword = this.editPassword.trim();

    this.authService.login(this.editPassword, this.editeMail).subscribe((userProfile: UserProfile) => {
          this.setMessage();

//        console.log('antes: ' + this.authService.isLoggedIn);
//        console.log('userProfile: ' + userProfile);

          if (userProfile) {
            // Get the redirect URL from our auth service
            // If no redirect has been set, use the default
            const redirect = this.authService.redirectUrl ? this.authService.redirectUrl : '/admin';

            // console.log(`User Name: ${this.authService.getUserProfile().nome}`);
            // console.log(`User: ${JSON.stringify(this.authService.getUserProfile())}`);

            // Redirect the user
            this.router.navigate([redirect]);

          } else {
            this._msgDng = 'ATENÇÃO: E-Mail e/ou Senha inválidos.';
          }
        }
      );
  }

  gotoReenviarSenha() {

    this._msg = '';
    this._msgDng = '';

    if (this.editeMail !== '') {

    // console.log('Reenvio ...');

        this.editeMail = this.editeMail.trim();
        this.editeMail = this.editeMail.toLowerCase();

        this.userProfileService.ressetPassWordByEMail(this.editeMail)
           .subscribe(msg => {
             this._msgPWD = msg;
             this.gotoAvaliaRetornoEMail(this._msgPWD);
            });

    } else {

        this._msgDng = 'Por favor, informe o seu E-Mail.';
    }
  }

  gotoNovoCadastro() {

      this.router.navigate(['/AssociadoCaptacao']);
  }

  gotoAvaliaRetornoEMail(msg: string) {

    if (msg.substring(0, 7) === 'ATENÇÃO') {
        // this.alertClassType = 'alert alert-danger';
        this._msgDng = msg;
    } else {
        // this.alertClassType = 'alert alert-success';
        this._msg = msg;
    }
}

  onSubmit() {

    this._msg = '';
    this._msgDng = '';

    this.login();
  }

  logout() {

    this.authService.logout();
    this.setMessage();
  }
}
