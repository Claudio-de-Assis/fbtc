import { UserProfileService } from '../shared/services/user-profile.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxPermissionsService } from 'ngx-permissions';

import { AuthService } from '../shared/services/auth.service';
/*
@Component({
  template: `
    <h2>LOGIN</h2>
    <p>{{message}}</p>
    <p>
      <button (click)="login()"  *ngIf="!authService.isLoggedIn">Login</button>
      <button (click)="logout()" *ngIf="authService.isLoggedIn">Logout</button>
    </p>`
})
*/
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
  }

  setMessage() {
    this.message = 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out');

console.log ( 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out'));
  }

  login() {
    this._msgDng = '';

    this.message = 'Trying to log in ...';

    console.log('Trying to log in ...');

    if (this.editeMail.trim() === '') {
      this._msgDng = 'Por favor, informe o seu E-Mail';
      return;
    }

    if (this.editPassword.trim() === '') {
      this._msgDng = 'Por favor, informe a sua senha E-Mail';
      return;
    }

    console.log('pre: ' + this.authService.isLoggedIn);

    this.authService.login(this.editPassword, this.editeMail)
      .subscribe(
        () => {
          this.setMessage();

          console.log('antes: ' + this.authService.isLoggedIn);

          if (this.authService.isLoggedIn) {
            // Get the redirect URL from our auth service
            // If no redirect has been set, use the default
            const redirect = this.authService.redirectUrl ? this.authService.redirectUrl : '/admin';

            console.log(`User Name: ${this.authService.getUserProfile().nome}`);
            console.log(`User: ${JSON.stringify(this.authService.getUserProfile())}`);
            this.permission = ['Financeiro'];
            this.permissionsService.loadPermissions(this.permission);
            // Redirect the user
            this.router.navigate([redirect]);
          }
        }
      );
  }


  gotoReenviarSenha() {

    this._msg = '';
    if (this.editeMail !== '') {

      console.log('Reenvio ...');

        this.editeMail = this.editeMail.trim();
        this.editeMail = this.editeMail.toLowerCase();

        this.userProfileService.ressetPassWordByEMail(this.editeMail)
           .subscribe(msg => this._msg = msg);

    } else {

        this._msgDng = 'Por favor, informe o seu E-Mail';
    }
}


  onSubmit() {
    this.login();
  }

  logout() {
    this.authService.logout();
    this.setMessage();
  }
}
