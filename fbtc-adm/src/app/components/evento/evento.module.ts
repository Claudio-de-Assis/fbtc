import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EventoService } from './../shared/services/evento.service';
import { EventoListComponent } from './evento-list/evento.list.component';
import { EventoFormComponent } from './evento-form/evento.form.component';
import { EventoPreviewComponent } from './evento-preview/evento-preview.component';
import { EventoRoutingModule } from './evento.routing.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    EventoRoutingModule,
    SharedModule
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
    EventoService
  ]
})
export class EventoModule { }
