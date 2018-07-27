import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from './login.form.component';
import { AuthService } from './../shared/services/auth.service';
import { LoginRoutingModule } from './login-routing.module';
import { UserProfile } from '../shared/model/user-profile';


@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      LoginRoutingModule,
      HttpModule
    ],
    declarations: [
      LoginComponent
    ],
    exports: [
        LoginComponent
    ],
    providers: [
      AuthService,
      UserProfile
    ]
  })
  export class LoginModule { }
