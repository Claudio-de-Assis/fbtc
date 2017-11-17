import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EventoListComponent } from './evento-list/evento.list.component';
import { EventoFormComponent } from './evento-form/evento.form.component';
import { EventoPreviewComponent } from './evento-preview/evento-preview.component';

const eventoRoutes: Routes = [
    { path: 'Evento', component: EventoListComponent },
    { path: 'Evento/:id', component: EventoFormComponent },
    { path: 'EventoNovo', component: EventoFormComponent },
    { path: 'EventoPreview/:id', component: EventoPreviewComponent},
];

@NgModule({
    imports: [
        RouterModule.forChild(eventoRoutes)
    ],
    exports: [
        RouterModule
    ]
})

export class EventoRoutingModule {
}
