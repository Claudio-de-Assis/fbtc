import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from './../shared/services/associado.service';
import { AssociadoListComponent } from './associado-list/associado.list.component';
import { AssociadoFormComponent } from './associado-form/associado.form.component';
import { AssociadoRoutingModule } from './associado.routing.module';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      AssociadoRoutingModule,
      // BrowserAnimationsModule
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
      AssociadoService
  ]
})
export class AssociadoModule { }
