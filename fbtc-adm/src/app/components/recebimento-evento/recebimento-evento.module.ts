import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecebimentoEventoListComponent } from './recebimento-evento-list/recebimento.evento.list.component';
import { RecebimentoEventoFormComponent } from './recebimento-evento-form/recebimento.evento.form.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [RecebimentoEventoListComponent, RecebimentoEventoFormComponent]
})
export class RecebimentoEventoModule { }
