import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { RecebimentoEventoListComponent } from './recebimento-evento-list/recebimento.evento.list.component';
import { RecebimentoEventoFormComponent } from './recebimento-evento-form/recebimento.evento.form.component';

const anuidadesRoutes: Routes = [
    { path: 'RecebimentoEvento', component: RecebimentoEventoListComponent },
    { path: 'RecebimentoEvento/:id', component: RecebimentoEventoFormComponent },
  ];

  @NgModule({
    imports: [
        RouterModule.forChild(anuidadesRoutes)
    ],
    exports: [
        RouterModule
    ]
  })
export class RecebimentoEventoRoutingModule {}
