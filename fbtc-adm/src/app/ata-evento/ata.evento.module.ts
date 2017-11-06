import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AtaEventoListComponent } from './ata-evento-list/ata.evento.list.component';
import { AtaEventoFormComponent } from './ata-evento-form/ata.evento.form.component';
//import { AtaIsencaoService } from './../shared/services/ata-isencao.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule
    ],
    declarations: [
        AtaEventoFormComponent,
        AtaEventoListComponent
    ],
    exports: [
        AtaEventoFormComponent,
        AtaEventoListComponent
    ],
    providers: [
        //AtaIsencaoService
    ]
})
export class AtaEventoModule {
}