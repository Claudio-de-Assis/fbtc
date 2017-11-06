import { AssociadoFormComponent } from './associado-form/associado-form.component';
import { AssociadoListComponent } from './associado-list/associado-list.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

const associadoRoutes: Routes = [
  { path: 'Associado', component: AssociadoListComponent },
  { path: 'Associado/:AssociadoId', component: AssociadoFormComponent },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(associadoRoutes)
  ],
  declarations: [],
  exports: [
    RouterModule
]
})
export class AssociadoRoutingModule { }
