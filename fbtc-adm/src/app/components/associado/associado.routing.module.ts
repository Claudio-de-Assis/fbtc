import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { AssociadoFormComponent } from './associado-form/associado.form.component';
import { AssociadoListComponent } from './associado-list/associado.list.component';

// const associadoRoutes: Routes = [
//   { path:'' }
//   { path: 'Associado', component: AssociadoListComponent },
//   { path: 'Associado/:id', component: AssociadoFormComponent },
//   { path: 'AssociadoSelf/:id', component: AssociadoSelfFormComponent }, --> Nova Rota
// ];

@NgModule({
  imports: [
      // RouterModule.forChild(associadoRoutes)
  ],
  exports: [
      RouterModule
  ]
})
export class AssociadoRoutingModule { }
