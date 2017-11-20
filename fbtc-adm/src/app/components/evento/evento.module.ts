import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EventoService } from './../shared/services/evento.service';
import { TipoPublicoService } from './../shared/services/tipo-publico.service';
import { EventoListComponent } from './evento-list/evento.list.component';
import { EventoFormComponent } from './evento-form/evento.form.component';
import { EventoPreviewComponent } from './evento-preview/evento-preview.component';
import { EventoRoutingModule } from './evento.routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    EventoRoutingModule
  ],
  declarations: [
    EventoListComponent,
    EventoFormComponent,
    EventoPreviewComponent
  ],
  exports: [
    EventoListComponent,
    EventoFormComponent,
    EventoPreviewComponent
  ],
  providers: [
    EventoService,
    TipoPublicoService
  ]
})
export class EventoModule { }
