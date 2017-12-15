import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { ColaboradorService } from './../shared/services/colaborador.service';
import { ColaboradorListComponent } from './colaborador-list/colaborador.list.component';
import { ColaboradorFormComponent } from './colaborador-form/colaborador.form.component';
import { ColaboradorRoutingModule } from './colaborador.routing.module';
import { ColaboradorRoute } from '../shared/webApi-routes/colaborador.route';
import { TipoPublicoService } from '../shared/services/tipo-publico.service';
import { TipoPublicoRoute } from '../shared/webApi-routes/tipo-publico.route';
import { SharedModule } from './../shared/shared.module';

@NgModule({
  imports: [
      BrowserModule,
      CommonModule,
      FormsModule,
      ColaboradorRoutingModule,
      SharedModule,
      ReactiveFormsModule
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
      TipoPublicoRoute
  ]
})

export class ColaboradorModule { }
