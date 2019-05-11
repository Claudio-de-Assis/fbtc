import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { NgModule, LOCALE_ID } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';

import { EventoListComponent } from './evento-list/evento.list.component';
import { EventoFormComponent } from './evento-form/evento.form.component';
import { EventoPreviewComponent } from './evento-preview/evento-preview.component';

import { EventoService } from '../shared/services/evento.service';

import { EventoRoutingModule } from './evento.routing.module';
import { SharedModule } from '../shared/shared.module';
// import { FileUploadModule } from './../shared/upload/file-upload.module';

import { FileUploadRoute } from '../shared/webapi-routes/file-upload.route';

import { NgxPaginationModule } from 'ngx-pagination';
import { CKEditorModule } from 'ngx-ckeditor';
// import { NgxCurrencyModule } from 'ngx-currency';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    EventoRoutingModule,
    SharedModule,
//    FileUploadModule,
    NgxPaginationModule,
    HttpModule,
    // NgxCurrencyModule,
    CKEditorModule
  ],
  declarations: [
    EventoListComponent,
    EventoFormComponent,
    EventoPreviewComponent
  ],
  exports: [
    EventoListComponent,
    EventoFormComponent,
    EventoPreviewComponent
  ],
  providers: [
    EventoService,
    FileUploadRoute,
    { provide: LOCALE_ID, useValue: 'pt' }
  ]
})
export class EventoModule { }
