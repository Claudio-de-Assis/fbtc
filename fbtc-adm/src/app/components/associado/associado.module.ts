import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from './../shared/services/associado.service';
import { CepCorreiosService } from '../shared/services/cep-correios.service';
import { AtcService } from './../shared/services/atc.service';
// import { EnderecoService } from '../shared/services/endereco.service';

import { AssociadoRoute } from '../shared/webApi-routes/associado.route';
import { CepCorreiosRoute } from '../shared/webApi-routes/cep-correios.route';

import { AtcRoute } from '../shared/webApi-routes/atc.route';
// import { EnderecoRoute } from '../shared/webapi-routes/endereco.route';

import { AssociadoListComponent } from './associado-list/associado.list.component';
import { AssociadoFormComponent } from './associado-form/associado.form.component';

import { AssociadoRoutingModule } from './associado.routing.module';

import { SharedModule } from '../shared/shared.module';
import { MessagesComponent } from './../../messages/messages.component';


@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      AssociadoRoutingModule,
      SharedModule
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
      // EnderecoService,
      // EnderecoRoute
  ]
})
export class AssociadoModule { }
