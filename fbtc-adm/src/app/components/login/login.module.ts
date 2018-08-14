import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from './login.form.component';
import { AuthService } from './../shared/services/auth.service';
import { LoginRoutingModule } from './login-routing.module';
import { UserProfile } from '../shared/model/user-profile';

// import { NgxPermissionsModule } from 'ngx-permissions';


@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      LoginRoutingModule,
      HttpModule,
      // NgxPermissionsModule.forChild()
    ],
    declarations: [
      LoginComponent
    ],
    exports: [
        LoginComponent,
        // NgxPermissionsModule.forChild(),
    ],
    providers: [
      AuthService,
      UserProfile
    ]
  })
  export class LoginModule { }
