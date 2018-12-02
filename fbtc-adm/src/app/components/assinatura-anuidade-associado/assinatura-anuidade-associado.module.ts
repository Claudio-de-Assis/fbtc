import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser/src/browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssinaturaAnuidadeService } from '../shared/services/assinatura-anuidade.service';
import { AnuidadeService } from '../shared/services/anuidade.service';

import { AssinaturaAnuidadeRoute } from '../shared/webapi-routes/assinatura-anuidade.route';
import { SharedModule } from '../shared/shared.module';
import { AssinaturaAnuidadeAssociadoRoutingModule } from './assinatura-anuidade-associado.routing.module';
import { AssinaturaAnuidadeAssociadoListComponent } from './assinatura-anuidade-associado-list/assinatura.anuidade.associado.list.component';
import { AssinaturaAnuidadeAssociadoFormComponent } from './assinatura-anuidade-associado-form/assinatura.anuidade.associado.form.component';


import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      AssinaturaAnuidadeAssociadoRoutingModule,
      SharedModule,
      NgxPaginationModule,
      HttpModule
  ],
  declarations: [
      AssinaturaAnuidadeAssociadoFormComponent,
      AssinaturaAnuidadeAssociadoListComponent

  ],
  exports: [
    AssinaturaAnuidadeAssociadoFormComponent,
    AssinaturaAnuidadeAssociadoListComponent
],
  providers: [
    AssinaturaAnuidadeService,
    AssinaturaAnuidadeRoute,
    AnuidadeService
  ]
})
export class AssinaturaAnuidadeAssociadoModule { }
