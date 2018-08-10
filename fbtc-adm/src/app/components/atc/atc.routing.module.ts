import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { AtcListComponent } from './atc-list/atc.list.component';
import { AtcFormComponent } from './atc-form/atc.form.component';



@NgModule({
    imports: [

    ],
    exports: [
        RouterModule
    ]
})
export class AtcRoutingModule { }
