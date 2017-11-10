import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ColaboradorService } from './../shared/services/colaborador.service';
import { ColaboradorListComponent } from './colaborador-list/colaborador.list.component';
import { ColaboradorFormComponent } from './colaborador-form/colaborador.form.component';
import { ColaboradorRoutingModule } from './colaborador.routing.module';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      ColaboradorRoutingModule
  ],
  declarations: [
      ColaboradorFormComponent,
      ColaboradorListComponent
  ],
  exports: [
      ColaboradorFormComponent,
      ColaboradorListComponent
  ],
  providers: [
      ColaboradorService
  ]
})
export class ColaboradorModule { }
