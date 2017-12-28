import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { ColaboradorListComponent } from './colaborador-list/colaborador.list.component';
import { ColaboradorFormComponent } from './colaborador-form/colaborador.form.component';

import { ColaboradorService } from './../shared/services/colaborador.service';
import { TipoPublicoService } from '../shared/services/tipo-publico.service';

import { ColaboradorRoutingModule } from './colaborador.routing.module';
import { SharedModule } from './../shared/shared.module';

import { ColaboradorRoute } from '../shared/webApi-routes/colaborador.route';
import { TipoPublicoRoute } from '../shared/webApi-routes/tipo-publico.route';
import { FileUploadRoute } from '../shared/webapi-routes/file-upload.route';
import { AlertMessageModule } from '../shared/modal/alert/alert-message/alert-message.module';

@NgModule({
  imports: [
      BrowserModule,
      CommonModule,
      FormsModule,
      ColaboradorRoutingModule,
      SharedModule,
      ReactiveFormsModule,
      AlertMessageModule
  ],
  declarations: [
      ColaboradorFormComponent,
      ColaboradorListComponent
  ],
  exports: [
      ColaboradorFormComponent,
      ColaboradorListComponent
  ],
  providers: [
      ColaboradorService,
      ColaboradorRoute,
      TipoPublicoService,
      TipoPublicoRoute,
      FileUploadRoute
  ]
})

export class ColaboradorModule { }
