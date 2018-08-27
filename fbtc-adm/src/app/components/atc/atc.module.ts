import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AtcListComponent } from './atc-list/atc.list.component';
import { AtcFormComponent } from './atc-form/atc.form.component';

import { AtcService } from '../shared/services/atc.service';
import { UnidadeFederacaoService } from '../shared/services/unidade-federacao.service';

import { SharedModule } from '../shared/shared.module';
import { AtcRoutingModule } from './atc.routing.module';
import { UnidadeFederacaoRoute } from '../shared/webapi-routes/unidade-federacao.route';

import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AtcRoutingModule,
    SharedModule,
    NgxPaginationModule,
    HttpModule
  ],
  declarations: [
    AtcFormComponent,
    AtcListComponent
  ],
  exports: [
    AtcFormComponent,
    AtcListComponent
  ],
  providers: [
    AtcService,
    UnidadeFederacaoService,
    UnidadeFederacaoRoute
  ]

})
export class AtcModule { }
