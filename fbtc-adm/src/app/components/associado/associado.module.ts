import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser/src/browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from './../shared/services/associado.service';
import { CepCorreiosService } from '../shared/services/cep-correios.service';
import { AtcService } from './../shared/services/atc.service';
import { EnderecoService } from '../shared/services/endereco.service';

import { AssociadoRoute } from '../shared/webApi-routes/associado.route';
import { CepCorreiosRoute } from '../shared/webApi-routes/cep-correios.route';
import { EnderecoRoute } from './../shared/webapi-routes/endereco.route';
import { AtcRoute } from '../shared/webApi-routes/atc.route';

import { AssociadoListComponent } from './associado-list/associado.list.component';
import { AssociadoFormComponent } from './associado-form/associado.form.component';
import { MessagesComponent } from './../../messages/messages.component';

import { AssociadoRoutingModule } from './associado.routing.module';

import { SharedModule } from '../shared/shared.module';
import { FileUploadModule } from '../shared/upload/file-upload.module';
import { FileUploadRoute } from '../shared/webapi-routes/file-upload.route';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      AssociadoRoutingModule,
      SharedModule,
      FileUploadModule,
      HttpModule
  ],
  declarations: [
      AssociadoFormComponent,
      AssociadoListComponent
  ],
  exports: [
      AssociadoFormComponent,
      AssociadoListComponent
  ],
  providers: [
      AssociadoService,
      AssociadoRoute,
      CepCorreiosService,
      CepCorreiosRoute,
      AtcService,
      AtcRoute,
      EnderecoService,
      EnderecoRoute,
      FileUploadRoute
  ]
})
export class AssociadoModule { }
