import { AtaIsencaoService } from './../shared/services/ata-isencao.service';
import { AtaAnuidadeListComponent } from './ata-anuidade-list/ata.anuidade.list.component';
import { AtaAnuidadeFormComponent } from './ata-anuidade-form/ata.anuidade.form.component';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



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