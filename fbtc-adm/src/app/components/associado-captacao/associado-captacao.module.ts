import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser/src/browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from '../shared/services/associado.service';
import { CepCorreiosService } from '../shared/services/cep-correios.service';
import { EnderecoService } from '../shared/services/endereco.service';

import { AssociadoRoute } from '../shared/webApi-routes/associado.route';
import { CepCorreiosRoute } from '../shared/webApi-routes/cep-correios.route';
import { EnderecoRoute } from '../shared/webapi-routes/endereco.route';

import { AssociadoCaptacaoFormComponent } from './associado-captacao-form/associado-captacao.form.component';
import { MessagesComponent } from '../../messages/messages.component';

import { AssociadoCaptacaoRoutingModule } from './associado-captacao.routing.module';

import { SharedModule } from '../shared/shared.module';
// import { FileUploadModule } from '../shared/upload/file-upload.module';
import { FileUploadRoute } from '../shared/webapi-routes/file-upload.route';
import { AtcRoute } from '../shared/webApi-routes/atc.route';
import { AtcService } from '../shared/services/atc.service';

import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      AssociadoCaptacaoRoutingModule,
      SharedModule,
//       FileUploadModule,
      NgxPaginationModule,
      HttpModule
  ],
  declarations: [
      AssociadoCaptacaoFormComponent
  ],
  exports: [
      AssociadoCaptacaoFormComponent
  ],
  providers: [
      AssociadoService,
      AssociadoRoute,
      CepCorreiosService,
      CepCorreiosRoute,
      EnderecoService,
      EnderecoRoute,
      AtcRoute,
      AtcService,
      FileUploadRoute
  ]
})
export class AssociadoCaptacaoModule { }
