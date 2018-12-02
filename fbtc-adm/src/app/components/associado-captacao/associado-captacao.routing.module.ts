import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AssociadoCaptacaoFormComponent } from './associado-captacao-form/associado-captacao.form.component';


 const associadoCaptacaoRoutes: Routes = [
    {path: '', redirectTo: 'admin', pathMatch: 'full'},
    { path: 'AssociadoCaptacao', component: AssociadoCaptacaoFormComponent }
];

@NgModule({
  imports: [
       RouterModule.forChild(associadoCaptacaoRoutes)
  ],
  exports: [
      RouterModule
  ]
})
export class AssociadoCaptacaoRoutingModule { }
