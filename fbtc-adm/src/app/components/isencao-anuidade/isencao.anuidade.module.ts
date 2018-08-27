import { AppModule } from '../../app.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IsencaoService } from '../shared/services/isencao.service';
import { AnuidadeService } from '../shared/services/anuidade.service';
import { IsencaoAnuidadeFormComponent } from './isencao-anuidade-form/isencao.anuidade.form.component';
import { IsencaoAnuidadeListComponent } from './isencao-anuidade-list/isencao.anuidade.list.component';
import { IsencaoAnuidadeRoutingModule } from './isencao.anuidade.routing.module';
import { AssociadoIsencaoModule } from '../associado-isencao/associado-isencao.module';

import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AssociadoIsencaoModule,
    NgxPaginationModule,
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
    IsencaoService
  ]
})
export class IsencaoAnuidadeModule { }
