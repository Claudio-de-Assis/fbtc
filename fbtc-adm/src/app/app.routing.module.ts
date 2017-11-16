import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

// import { EventoListComponent } from './components/evento/evento-list/evento.list.component';
import { RecebimentoAnuidadeListComponent } from './components/recebimento-anuidade/recebimento-anuidade-list/recebimento.anuidade.list.component';
import { RecebimentoEventoListComponent } from './components/recebimento-evento/recebimento-evento-list/recebimento.evento.list.component';
import { IsencaoAnuidadeListComponent } from './components/isencao-anuidade/isencao-anuidade-list/isencao.anuidade.list.component';
// import { AtaEventoListComponent } from './components/ata-evento/ata-evento-list/ata.evento.list.component';

const appRoutes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
//    { path: 'Evento', component: EventoListComponent },
    { path: 'RecebimentoAnuidade', component: RecebimentoAnuidadeListComponent },
    { path: 'RecebimentoEvento', component: RecebimentoEventoListComponent },
//    { path: 'AtaEvento', component: AtaEventoListComponent },
    { path: '**', redirectTo: 'home' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(
            appRoutes,
            { enableTracing: true } // <-- debugging purposes only
        )
    ],
    exports: [
        RouterModule
    ]
})

export class AppRoutingModule {
}
