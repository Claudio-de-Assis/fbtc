import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ColaboradorFormComponent } from '../colaborador/colaborador-form/colaborador.form.component';
import { ColaboradorListComponent } from '../colaborador/colaborador-list/colaborador.list.component';
import { ColaboradorService } from '../../components/shared/services/colaborador.service';
import { ColaboradorRoutingModule } from './colaborador.routing.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ColaboradorRoutingModule
    ],
    declarations: [
        ColaboradorFormComponent,
        ColaboradorListComponent
    ],
    exports: [
        ColaboradorFormComponent,
        ColaboradorListComponent
    ],
    providers: [
        ColaboradorService
    ]
})
export class ColaboradorModule {
}