import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser/src/browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EventoService } from './../shared/services/evento.service';
import { AssinaturaEventoAssociadoListComponent } from './assinatura-evento-associado-list/assinatura.evento.associado.list.component';
import { AssinaturaEventoAssociadoFormComponent } from './assinatura-evento-associado-form/assinatura.evento.associado.form.component';

import { SharedModule } from '../shared/shared.module';

import { NgxPaginationModule } from 'ngx-pagination';
import { AssinaturaEventoAssociadoRoutingModule } from './assinatura-evento-associado.routing.module';
import { EventoRoute } from '../shared/webapi-routes/evento.route';
import { CKEditorModule } from 'ngx-ckeditor';
// import { NgxCurrencyModule } from 'ngx-currency';


@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      AssinaturaEventoAssociadoRoutingModule,
      SharedModule,
      NgxPaginationModule,
      HttpModule,
      // NgxCurrencyModule,
      CKEditorModule
  ],
  declarations: [
    AssinaturaEventoAssociadoFormComponent,
    AssinaturaEventoAssociadoListComponent
  ],
  exports: [
    AssinaturaEventoAssociadoFormComponent,
    AssinaturaEventoAssociadoListComponent
],
  providers: [
    EventoService,
    EventoRoute
  ]
})
export class AssinaturaEventoAssociadoModule { }
