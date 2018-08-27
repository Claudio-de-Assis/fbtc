import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from '../shared/services/associado.service';
import { PagSeguroService } from '../shared/services/pagSeguro.service';

import { RecebimentoEventoListComponent } from './recebimento-evento-list/recebimento.evento.list.component';
import { RecebimentoEventoFormComponent } from './recebimento-evento-form/recebimento.evento.form.component';
import { RecebimentoEventoRoutingModule } from './recebimento.evento.routing.module';
import { SharedModule } from '../shared/shared.module';

import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RecebimentoEventoRoutingModule,
    NgxPaginationModule,
    SharedModule
  ],
  declarations: [
    RecebimentoEventoListComponent,
    RecebimentoEventoFormComponent
  ],
  exports: [
    RecebimentoEventoListComponent,
    RecebimentoEventoFormComponent
  ],
  providers: [
    AssociadoService,
    PagSeguroService
  ]
})
export class RecebimentoEventoModule { }
