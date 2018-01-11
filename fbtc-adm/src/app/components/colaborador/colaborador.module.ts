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

// import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
// import { CustomAlertsModule } from '../shared/custom-alerts/custom-alerts.module';
// import { AlertConfigService } from '../shared/services/alert-config.service';

@NgModule({
  imports: [
      BrowserModule,
      CommonModule,
      FormsModule,
      ColaboradorRoutingModule,
      SharedModule,
      ReactiveFormsModule,
//      NgbModule.forRoot(),
//      CustomAlertsModule
  ],
  declarations: [
      ColaboradorFormComponent,
      ColaboradorListComponent,
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
      FileUploadRoute,
//      AlertConfigService,
  ]
})

export class ColaboradorModule { }
