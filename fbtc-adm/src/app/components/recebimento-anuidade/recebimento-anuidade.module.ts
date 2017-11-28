import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from './../shared/services/associado.service';
import { RecebimentoAnuidadeFormComponent } from './recebimento-anuidade-form/recebimento.anuidade.form.component';
import { RecebimentoAnuidadeListComponent } from './recebimento-anuidade-list/recebimento.anuidade.list.component';
import { RecebimentoAnuidadeRoutingModule } from './recebimento.anuidade.routing.module';
import { BooMessagePipe } from './../shared/pipes/boolean-viewer.pipe';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RecebimentoAnuidadeRoutingModule
  ],
  declarations: [
    RecebimentoAnuidadeFormComponent,
    RecebimentoAnuidadeListComponent,
    BooMessagePipe
  ],
  exports: [
    RecebimentoAnuidadeFormComponent,
    RecebimentoAnuidadeListComponent
  ],
  providers: [
    AssociadoService
  ]
})
export class RecebimentoAnuidadeModule { }
