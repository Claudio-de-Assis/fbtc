import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxPermissionsService } from 'ngx-permissions';

import { AuthService } from '../shared/services/auth.service';

import { UserProfile, UserProfileLogin } from './../shared/model/user-profile';
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
  _msgPWD: string;
  editeMail: string;
  editPassword: string;
  permission = [];

  alertClassType: string;

  userProfileLogin: UserProfileLogin;
  _msgProgresso: string;

  submitted: boolean;

  constructor(
      public authService: AuthService,
      public userProfileService: UserProfileService,
      public router: Router,
      private permissionsService: NgxPermissionsService
  ) {
    this.setMessage();
    this.title = 'Login';
    this._msg = '';
    this.editeMail = '';
    this.editPassword = '';
    this._msgPWD = '';
    this.userProfileLogin = new UserProfileLogin();

    this.alertClassType = 'alert alert-info';
    this._msgProgresso = '';
    this.submitted = false;
  }

  setMessage(): void {
    this.message = 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out');
  }

  login(): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    this._msg = '';
//    this.message = 'Trying to log in ...';
//    console.log('Trying to log in ...');

    if (this.editeMail.trim() === '') {

      this.alertClassType = 'alert alert-danger';
      this._msg  = 'Por favor, informe o seu E-Mail.';
      return;
    }

    if (this.editPassword.trim() === '') {

      this.alertClassType = 'alert alert-danger';
      this._msg  = 'Por favor, informe a sua Senha.';
      return;
    }

//  console.log('pre: ' + this.authService.isLoggedIn);

    this.editeMail = this.editeMail.trim();
    this.editeMail = this.editeMail.toLowerCase();

    this.editPassword = this.editPassword.trim();

    this.userProfileLogin.eMail = this.editeMail;
    this.userProfileLogin.passwordHash = this.editPassword;

    this._msgProgresso = 'Validando os dados informados. Por favor, aguarde!...';

    this.authService.loginUser( this.userProfileLogin).subscribe((userProfile: UserProfile) => {
          this.setMessage();
          this._msgProgresso = '';
          this.submitted = false;

        // console.log('antes: ' + this.authService.isLoggedIn);
        // console.log('userProfile: ' + userProfile);

          if (userProfile) {
            // Get the redirect URL from our auth service
            // If no redirect has been set, use the default
            const redirect = this.authService.redirectUrl ? this.authService.redirectUrl : '/admin';

            // console.log(`User Name: ${this.authService.getUserProfile().nome}`);
            // console.log(`User: ${JSON.stringify(this.authService.getUserProfile())}`);

            // Redirect the user
            this.router.navigate([redirect]);

          } else {

            this.alertClassType = 'alert alert-danger';
            this._msg  = 'ATENÇÃO: E-Mail e/ou Senha inválidos.';
            this.editPassword = '';
          }
        }
      );
  }

  gotoReenviarSenha(): void {

    if (this.submitted === false) {
      this.submitted = true;
    } else {
      return;
    }

    this._msg = '';
    this.editPassword = '';

    if (this.editeMail !== '') {

    // console.log('Reenvio ...');

        this.editeMail = this.editeMail.trim();
        this.editeMail = this.editeMail.toLowerCase();

        this._msgProgresso = 'Consultando o e-mail informado. Por favor, aguarde!...';

        this.userProfileService.ressetPassWordByEMail(this.editeMail)
           .subscribe(msg => {
              this._msgProgresso = '';
              this._msgPWD = msg;
              this.gotoAvaliaRetornoEMail(this._msgPWD);
              this.submitted = false;
            });

    } else {

      this.alertClassType = 'alert alert-danger';
      this._msg  = 'Por favor, informe o seu E-Mail.';
      this.submitted = false;
    }
  }

  gotoNovoCadastro(): void {

      this.router.navigate(['/AssociadoCaptacao']);
  }

  gotoAvaliaRetornoEMail(msg: string): void {

    if (msg.substring(0, 7) === 'ATENÇÃO') {
        this.alertClassType = 'alert alert-danger';
        this._msg  = msg;
    } else {
        this.alertClassType = 'alert alert-success';
        this._msg = msg;
    }
}

  onSubmit(): void {

    this._msg = '';
    this.login();
  }

  logout(): void {

    this.authService.logout();
    this.setMessage();
  }
}
