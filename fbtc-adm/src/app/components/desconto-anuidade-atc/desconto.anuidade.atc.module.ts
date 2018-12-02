import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser/src/browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DescontoAnuidadeAtcRoute } from './../shared/webapi-routes/desconto-anuidade-atc.route';
import { DescontoAnuidadeAtcService } from './../shared/services/desconto-anuidade-atc.service';
import { DescontoAnuidadeAtcListComponent } from './desconto-anuidade-atc-list/desconto.anuidade.atc.list.component';
import { DescontoAnuidadeAtcFormComponent } from './desconto-anuidade-atc-form/desconto.anuidade.atc.form.component';
import { DescontoAnuidadeAtcRoutingModule } from './desconto.anuidade.atc.routing.module';

import { AnuidadeService } from '../shared/services/anuidade.service';

import { SharedModule } from '../shared/shared.module';

import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      DescontoAnuidadeAtcRoutingModule,
      SharedModule,
      NgxPaginationModule,
      HttpModule
  ],
  declarations: [
      DescontoAnuidadeAtcFormComponent,
      DescontoAnuidadeAtcListComponent
  ],
  exports: [
    DescontoAnuidadeAtcFormComponent,
    DescontoAnuidadeAtcListComponent
],
  providers: [
    DescontoAnuidadeAtcService,
    DescontoAnuidadeAtcRoute,
    AnuidadeService
  ]
})
export class DescontoAnuidadeAtcModule { }
