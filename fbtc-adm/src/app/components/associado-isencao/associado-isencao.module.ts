import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from './../shared/services/associado.service';
import { AssociadoIsencaoListComponent } from './associado-isencao-list/associado-isencao-list.component';

import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgxPaginationModule
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
