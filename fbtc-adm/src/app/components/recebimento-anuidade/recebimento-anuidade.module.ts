import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecebimentoAnuidadeFormComponent } from './recebimento-anuidade-form/recebimento.anuidade.form.component';
import { RecebimentoAnuidadeListComponent } from './recebimento-anuidade-list/recebimento.anuidade.list.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [RecebimentoAnuidadeFormComponent, RecebimentoAnuidadeListComponent]
})
export class RecebimentoAnuidadeModule { }
