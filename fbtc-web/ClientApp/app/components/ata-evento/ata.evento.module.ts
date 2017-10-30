import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AtaEventoFormComponent } from '../ata-evento/ata-evento-form/ata.evento.form.component';
import { AtaEventoListComponent } from '../ata-evento/ata-evento-list/ata.evento.list.component';
import { AtaIsencaoService } from '../../components/shared/services/ata-isencao.service';


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
        AtaIsencaoService
    ]
})
export class AtaEventoModule {
}