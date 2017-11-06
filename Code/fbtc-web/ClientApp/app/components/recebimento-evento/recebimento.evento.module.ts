import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RecebimentoEventoFormComponent } from '../recebimento-evento/recebimento-evento-form/recebimento.evento.form.component';
import { RecebimentoEventoListComponent } from '../recebimento-evento/recebimento-evento-list/recebimento.evento.list.component';
import { RecebimentoEventoService } from '../../components/shared/services/recebimento-evento.service';


@NgModule({
    imports: [
        CommonModule,
        FormsModule
    ],
    declarations: [
        RecebimentoEventoFormComponent,
        RecebimentoEventoListComponent
    ],
    exports: [
        RecebimentoEventoFormComponent,
        RecebimentoEventoListComponent
    ],
    providers: [
        RecebimentoEventoService
    ]
})
export class RecebimentoEventoModule {
}