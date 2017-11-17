import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from './../shared/services/associado.service';
import { AssociadoIsencaoListComponent } from './associado-isencao-list/associado-isencao-list.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule
  ],
  declarations: [
    AssociadoIsencaoListComponent
  ],
  exports: [
    AssociadoIsencaoListComponent
  ],
  providers: [
    AssociadoService
  ]
})
export class AssociadoIsencaoModule { }
