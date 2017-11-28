import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { RecebimentoAnuidadeListComponent } from './recebimento-anuidade-list/recebimento.anuidade.list.component';
import { RecebimentoAnuidadeFormComponent } from './recebimento-anuidade-form/recebimento.anuidade.form.component';

const anuidadesRoutes: Routes = [
    { path: 'RecebimentoAnuidade', component: RecebimentoAnuidadeListComponent },
    { path: 'RecebimentoAnuidade/:id', component: RecebimentoAnuidadeFormComponent },
  ];

  @NgModule({
    imports: [
        RouterModule.forChild(anuidadesRoutes)
    ],
    exports: [
        RouterModule
    ]
  })

export class RecebimentoAnuidadeRoutingModule {}
