import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RecebimentoAnuidadeFormComponent } from '../recebimento-anuidade/recebimento-anuidade-form/recebimento.anuidade.form.component';
import { RecebimentoAnuidadeListComponent } from '../recebimento-anuidade/recebimento-anuidade-list/recebimento.anuidade.list.component';
import { RecebimentoAnuidadeService } from '../../components/shared/services/recebimento-anuidade.service';


@NgModule({
    imports: [
        CommonModule,
        FormsModule
    ],
    declarations: [
        RecebimentoAnuidadeFormComponent,
        RecebimentoAnuidadeListComponent
    ],
    exports: [
        RecebimentoAnuidadeFormComponent,
        RecebimentoAnuidadeListComponent
    ],
    providers: [
        RecebimentoAnuidadeService
    ]
})
export class RebimentoAnuidadeModule {
}