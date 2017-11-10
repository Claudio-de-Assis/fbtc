import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ColaboradorListComponent } from '../colaborador/colaborador-list/colaborador.list.component';
import { ColaboradorFormComponent } from '../colaborador/colaborador-form/colaborador.form.component';

const colaboradorRoutes: Routes = [
    { path: 'Colaborador', component: ColaboradorListComponent },
    { path: 'Colaborador/:id', component: ColaboradorFormComponent },
];

@NgModule({
    imports: [
        RouterModule.forChild(colaboradorRoutes)
    ],
    exports: [
        RouterModule
    ]
})

export class ColaboradorRoutingModule {

}
