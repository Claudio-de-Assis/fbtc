import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AssociadoListComponent } from '../associado/associado-list/associado.list.component';
import { AssociadoFormComponent } from '../associado/associado-form/associado.form.component';

const associadoRoutes: Routes = [
    { path: 'Associado', component: AssociadoListComponent },
    { path: 'Associado/:id', component: AssociadoFormComponent },
];

@NgModule({
    imports: [
        RouterModule.forChild(associadoRoutes)
    ],
    exports: [
        RouterModule
    ]
})

export class AssociadoRoutingModule{}