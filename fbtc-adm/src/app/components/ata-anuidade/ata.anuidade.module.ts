import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AtaAnuidadeFormComponent } from './ata-anuidade-form/ata.anuidade.form.component';
import { AtaAnuidadeListComponent } from './ata-anuidade-list/ata.anuidade.list.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [AtaAnuidadeFormComponent, AtaAnuidadeListComponent]
})
export class AtaAnuidadeModule { }
