import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AtaEventoFormComponent } from './ata-evento-form/ata.evento.form.component';
import { AtaEventoListComponent } from './ata-evento-list/ata.evento.list.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [AtaEventoFormComponent, AtaEventoListComponent]
})
export class AtaEventoModule { }
