import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from '../shared/services/associado.service';
import { RecebimentoService } from '../shared/services/recebimento.service';
import { PagSeguroService } from '../shared/services/pagSeguro.service';

import { FichaFinanceiraListComponent } from './ficha-financeira-list/ficha.financeira.list.component';
import { FichaFinanceiraFormComponent } from './ficha-financeira-form/ficha.financeira.form.component';
import { FichaFinanceiraRoutingModule } from './ficha-financeira.routing.module';

import { NgxPaginationModule } from 'ngx-pagination';
import { NgxCurrencyModule } from 'ngx-currency';
import { SharedModule } from '../shared/shared.module';

import {LOCALE_ID} from '@angular/core';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    FichaFinanceiraRoutingModule,
    NgxPaginationModule,
    NgxCurrencyModule,
    SharedModule
  ],
  declarations: [
    FichaFinanceiraFormComponent,
    FichaFinanceiraListComponent
  ],
  exports: [
    FichaFinanceiraFormComponent,
    FichaFinanceiraListComponent
  ],
  providers: [
    AssociadoService,
    RecebimentoService,
    PagSeguroService,
    { provide: LOCALE_ID, useValue: 'pt' }
  ]
})
export class FichaFinanceiraModule { }
