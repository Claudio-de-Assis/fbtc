import { SharedModule } from './../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from './../shared/services/associado.service';
import { RecebimentoAnuidadeService } from './../shared/services/recebimento-anuidade.service';

import { RecebimentoAnuidadeFormComponent } from './recebimento-anuidade-form/recebimento.anuidade.form.component';
import { RecebimentoAnuidadeListComponent } from './recebimento-anuidade-list/recebimento.anuidade.list.component';
import { RecebimentoAnuidadeRoutingModule } from './recebimento.anuidade.routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RecebimentoAnuidadeRoutingModule,
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
    RecebimentoAnuidadeService
  ]
})
export class RecebimentoAnuidadeModule { }
