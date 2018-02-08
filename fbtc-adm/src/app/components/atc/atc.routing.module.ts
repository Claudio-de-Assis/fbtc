import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { AtcListComponent } from './atc-list/atc.list.component';
import { AtcFormComponent } from './atc-form/atc.form.component';

const atcRoutes: Routes = [
    { path: 'Atc', component: AtcListComponent },
    { path: 'Atc/:id', component: AtcFormComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(atcRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AtcRoutingModule { }
