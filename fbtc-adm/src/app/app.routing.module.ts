import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

// import { RecebimentoEventoListComponent } from './components/recebimento-evento/recebimento-evento-list/recebimento.evento.list.component';
// import { IsencaoAnuidadeListComponent } from './components/isencao-anuidade/isencao-anuidade-list/isencao.anuidade.list.component';

const appRoutes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
//    { path: 'RecebimentoEvento', component: RecebimentoEventoListComponent },
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
