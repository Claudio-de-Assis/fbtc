import { AppModule } from './../../app.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IsencaoAnuidadeService } from './../shared/services/isencao-anuidade.service';
import { IsencaoAnuidadeFormComponent } from './isencao-anuidade-form/isencao.anuidade.form.component';
import { IsencaoAnuidadeListComponent } from './isencao-anuidade-list/isencao.anuidade.list.component';
import { IsencaoAnuidadeRoutingModule } from './isencao.anuidade.routing.module';
import { AssociadoIsencaoModule } from './../associado-isencao/associado-isencao.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AssociadoIsencaoModule,
    IsencaoAnuidadeRoutingModule,
  ],
  declarations: [
    IsencaoAnuidadeFormComponent,
    IsencaoAnuidadeListComponent,
  ],
    exports: [
    IsencaoAnuidadeFormComponent,
    IsencaoAnuidadeListComponent
  ],
  providers: [
    IsencaoAnuidadeService
  ]
})
export class IsencaoAnuidadeModule { }