
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser/src/browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoService } from '../shared/services/associado.service';
import { RecebimentoService } from '../shared/services/recebimento.service';

import { SharedModule } from '../shared/shared.module';
import { AssociadoFichaFinanceiraRoutingModule } from './associado-ficha-financeira.routing.module';
import { NgxPaginationModule } from 'ngx-pagination';

import { AssociadoFichaFinanceiraListComponent } from './associado-ficha-financeira-list/associado.ficha.financeira.list.component';
// tslint:disable-next-line:max-line-length
import { AssociadoFichaFinanceiraEventoFormComponent } from './associado-ficha-financeira-evento-form/associado.ficha.financeira.evento.form.component';
import { AssociadoFichaFinanceiraAnuidadeFormComponent } from './associado-ficha-financeira-anuidade-form/associado.ficha.financeira.anuidade.form.component';
import { PdfService } from '../shared/services/pdf.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgxPaginationModule,
    AssociadoFichaFinanceiraRoutingModule,
    HttpModule,
    SharedModule
  ],
  declarations: [
    AssociadoFichaFinanceiraAnuidadeFormComponent,
    AssociadoFichaFinanceiraEventoFormComponent,
    AssociadoFichaFinanceiraListComponent
  ],
  exports: [
    AssociadoFichaFinanceiraAnuidadeFormComponent,
    AssociadoFichaFinanceiraEventoFormComponent,
    AssociadoFichaFinanceiraListComponent
  ],
  providers: [
    AssociadoService,
    RecebimentoService,
    PdfService
  ]
})
export class AssociadoFichaFinanceiraModule { }
