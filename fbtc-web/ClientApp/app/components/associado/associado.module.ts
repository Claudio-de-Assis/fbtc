import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssociadoFormComponent } from '../associado/associado-form/associado.form.component';
import { AssociadoListComponent } from '../associado/associado-list/associado.list.component';
import { AssociadoService } from '../../components/shared/services/associado.service';
import { AssociadoRoutingModule } from './associado.routing.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        AssociadoRoutingModule
    ],
    declarations: [
        AssociadoFormComponent,
        AssociadoListComponent
    ],
    exports: [
        AssociadoFormComponent,
        AssociadoListComponent
    ],
    providers: [
        AssociadoService
    ]
})
export class AssociadoModule { }