import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IsencaoEventoService } from './../shared/services/isencao-evento.service';
import { IsencaoEventoFormComponent } from './isencao-evento-form/isencao.evento.form.component';
import { IsencaoEventoListComponent } from './isencao-evento-list/isencao.evento.list.component';
import { IsencaoEventoRoutingModule } from './isencao.evento.routing.module';
import { AssociadoIsencaoListComponent } from './../associado/associado-isencao-list/associado-isencao-list.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IsencaoEventoRoutingModule
  ],
  declarations: [
    IsencaoEventoFormComponent,
    IsencaoEventoListComponent,
    AssociadoIsencaoListComponent
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
