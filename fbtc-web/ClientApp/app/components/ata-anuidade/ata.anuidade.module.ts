import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AtaAnuidadeFormComponent } from '../ata-anuidade/ata-anuidade-form/ata.anuidade.form.component';
import { AtaAnuidadeListComponent } from '../ata-anuidade/ata-anuidade-list/ata.anuidade.list.component';
import { AtaIsencaoService } from '../../components/shared/services/ata-isencao.service';


@NgModule({
    imports: [
        CommonModule,
        FormsModule
    ],
    declarations: [
        AtaAnuidadeFormComponent,
        AtaAnuidadeListComponent
    ],
    exports: [
        AtaAnuidadeFormComponent,
        AtaAnuidadeListComponent
    ],
    providers: [
        AtaIsencaoService
    ]
})
export class AtaAnuidadeModule { }