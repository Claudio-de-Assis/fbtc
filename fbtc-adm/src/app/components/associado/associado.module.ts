import { AtcService } from './../shared/services/atc.service';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from './../shared/services/associado.service';
import { AssociadoListComponent } from './associado-list/associado.list.component';
import { AssociadoFormComponent } from './associado-form/associado.form.component';
import { AssociadoRoutingModule } from './associado.routing.module';
import { AssociadoRoute } from '../shared/webApi-routes/associado.route';
import { SharedModule } from '../shared/shared.module';
import { CepCorreiosService } from '../shared/services/cep-correios.service';
import { MessagesComponent } from './../../messages/messages.component';
import { CepCorreiosRoute } from '../shared/webApi-routes/cep-correios.route';
import { AtcRoute } from '../shared/webApi-routes/atc.route';

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
      AtcRoute
  ]
})
export class AssociadoModule { }
