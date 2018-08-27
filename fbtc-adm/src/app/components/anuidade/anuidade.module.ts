import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AnuidadeFormComponent } from './anuidade-form/anuidade.form.component';
import { AnuidadeListComponent } from './anuidade-list/anuidade.list.component';

import { AnuidadeService } from '../shared/services/anuidade.service';

import { AnuidadeRoutingModule } from './anuidade.routing.module';
import { SharedModule } from '../shared/shared.module';

import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AnuidadeRoutingModule,
    SharedModule,
    NgxPaginationModule,
    HttpModule
  ],
  declarations: [
    AnuidadeFormComponent,
    AnuidadeListComponent
  ],
  exports: [
    AnuidadeFormComponent,
    AnuidadeListComponent
   ],
   providers: [
    AnuidadeService
   ]
})
export class AnuidadeModule { }
