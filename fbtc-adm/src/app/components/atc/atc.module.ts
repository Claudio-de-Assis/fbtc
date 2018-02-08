import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AtcListComponent } from './atc-list/atc.list.component';
import { AtcFormComponent } from './atc-form/atc.form.component';

import { AtcService } from './../shared/services/atc.service';

import { SharedModule } from '../shared/shared.module';
import { AtcRoutingModule } from './atc.routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AtcRoutingModule,
    SharedModule,
    HttpModule
  ],
  declarations: [
    AtcFormComponent,
    AtcListComponent
  ],
  exports: [
    AtcFormComponent,
    AtcListComponent
  ],
  providers: [
    AtcService
  ]

})
export class AtcModule { }
