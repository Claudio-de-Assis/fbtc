import { EventoService } from './../shared/services/evento.service';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EventoFormComponent } from '../evento/evento-form/evento.form.component';
import { EventoListComponent } from '../evento/evento-list/evento.list.component';


@NgModule({
    imports: [
        CommonModule,
        FormsModule
    ],
    declarations: [
        EventoFormComponent,
        EventoListComponent
    ],
    exports: [
        EventoFormComponent,
        EventoListComponent
    ],
    providers: [
        EventoService
    ]
})
@NgModule({})
export class EventoModule {
}