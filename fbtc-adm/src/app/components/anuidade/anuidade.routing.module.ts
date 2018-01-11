import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AnuidadeListComponent } from './anuidade-list/anuidade.list.component';
import { AnuidadeFormComponent } from './anuidade-form/anuidade.form.component';

const AnuidadeRoutes: Routes = [
  { path: 'Anuidade', component: AnuidadeListComponent },
  { path: 'Anuidade/:id', component: AnuidadeFormComponent },
  { path: 'AnuidadeNova', component: AnuidadeFormComponent },
];

@NgModule({
  imports: [
    RouterModule.forChild(AnuidadeRoutes)
  ],
  exports: [
    RouterModule
]
})

export class AnuidadeRoutingModule { }
