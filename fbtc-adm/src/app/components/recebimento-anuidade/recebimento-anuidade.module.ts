import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from '../shared/services/associado.service';
import { RecebimentoService } from '../shared/services/recebimento.service';
import { PagSeguroService } from '../shared/services/pagSeguro.service';

import { RecebimentoAnuidadeFormComponent } from './recebimento-anuidade-form/recebimento.anuidade.form.component';
import { RecebimentoAnuidadeListComponent } from './recebimento-anuidade-list/recebimento.anuidade.list.component';
import { RecebimentoAnuidadeRoutingModule } from './recebimento.anuidade.routing.module';

import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RecebimentoAnuidadeRoutingModule,
    NgxPaginationModule,
    SharedModule
  ],
  declarations: [
    RecebimentoAnuidadeFormComponent,
    RecebimentoAnuidadeListComponent
  ],
  exports: [
    RecebimentoAnuidadeFormComponent,
    RecebimentoAnuidadeListComponent
  ],
  providers: [
    AssociadoService,
    RecebimentoService,
    PagSeguroService
  ]
})
export class RecebimentoAnuidadeModule { }
