import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { IsencaoEventoListComponent } from './isencao-evento-list/isencao.evento.list.component';
import { IsencaoEventoFormComponent } from './isencao-evento-form/isencao.evento.form.component';

const IsencaoEventoRoutes: Routes = [
    { path: 'IsencaoEvento', component: IsencaoEventoListComponent },
    { path: 'IsencaoEvento/:id', component: IsencaoEventoFormComponent },
    { path: 'IsencaoEventoNova', component: IsencaoEventoFormComponent },
];

@NgModule({
    imports: [
        RouterModule.forChild(IsencaoEventoRoutes)
    ],
    exports: [
        RouterModule
    ]
})

export class IsencaoEventoRoutingModule {
}
