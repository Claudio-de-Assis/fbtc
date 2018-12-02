import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser/src/browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssinaturaAnuidadeService } from './../shared/services/assinatura-anuidade.service';
import { AnuidadeService } from './../shared/services/anuidade.service';

import { AssinaturaAnuidadeRoute } from './../shared/webapi-routes/assinatura-anuidade.route';
import { AssinaturaAnuidadeListComponent } from './assinatura-anuidade-list/assinatura.anuidade.list.component';
import { SharedModule } from './../shared/shared.module';

import { NgxPaginationModule } from 'ngx-pagination';
import { AssinaturaAnuidadeRoutingModule } from './assinatura-anuidade.routing.module';
import { AssinaturaAnuidadeFormComponent } from './assinatura-anuidade-form/assinatura.anuidade.form.component';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      AssinaturaAnuidadeRoutingModule,
      SharedModule,
      NgxPaginationModule,
      HttpModule
  ],
  declarations: [
      AssinaturaAnuidadeFormComponent,
      AssinaturaAnuidadeListComponent

  ],
  exports: [
    AssinaturaAnuidadeFormComponent,
    AssinaturaAnuidadeListComponent
],
  providers: [
    AssinaturaAnuidadeService,
    AssinaturaAnuidadeRoute,
    AnuidadeService
  ]
})
export class AssinaturaAnuidadeModule { }
