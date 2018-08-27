import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './login.form.component';
import { AuthService } from '../shared/services/auth.service';
import { AuthGuard } from '../shared/services/auth-guard.service';

const loginRoutes: Routes = [
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(loginRoutes)
  ],
  exports: [
    RouterModule
  ],
  providers: [
    AuthGuard,
    AuthService
  ]
})
export class LoginRoutingModule {}
