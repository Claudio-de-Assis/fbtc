import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventoListComponent } from './evento-list/evento.list.component';
import { EventoFormComponent } from './evento-form/evento.form.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [EventoListComponent, EventoFormComponent]
})
export class EventoModule { }
