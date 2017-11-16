import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { IsencaoAnuidadeListComponent } from './isencao-anuidade-list/isencao.anuidade.list.component';
import { IsencaoAnuidadeFormComponent } from './isencao-anuidade-form/isencao.anuidade.form.component';

const IsencaoAnuidadeRoutes: Routes = [
    { path: 'IsencaoAnuidade', component: IsencaoAnuidadeListComponent },
    { path: 'IsencaoAnuidade/:id', component: IsencaoAnuidadeFormComponent },
    { path: 'IsencaoAnuidadeNova', component: IsencaoAnuidadeFormComponent },
];

@NgModule({
    imports: [
        RouterModule.forChild(IsencaoAnuidadeRoutes)
    ],
    exports: [
        RouterModule
    ]
})

export class IsencaoAnuidadeRoutingModule {
}
