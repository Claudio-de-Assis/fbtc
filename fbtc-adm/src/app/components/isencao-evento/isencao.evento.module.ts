
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IsencaoEventoService } from './../shared/services/isencao-evento.service';
import { IsencaoEventoFormComponent } from './isencao-evento-form/isencao.evento.form.component';
import { IsencaoEventoListComponent } from './isencao-evento-list/isencao.evento.list.component';
import { IsencaoEventoRoutingModule } from './isencao.evento.routing.module';
import { AssociadoIsencaoModule } from './../associado-isencao/associado-isencao.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AssociadoIsencaoModule,
    IsencaoEventoRoutingModule
  ],
  declarations: [
    IsencaoEventoFormComponent,
    IsencaoEventoListComponent,
  ],
  exports: [
    IsencaoEventoFormComponent,
    IsencaoEventoListComponent
  ],
  providers: [
    IsencaoEventoService
  ]
})
export class IsencaoEventoModule { }