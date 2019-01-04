import { PageNotFoundComponent } from './not-found.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.form.component';
import { AssociadoCaptacaoFormComponent } from './components/associado-captacao/associado-captacao-form/associado-captacao.form.component';

const appRoutes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'AssociadoCaptacao', component: AssociadoCaptacaoFormComponent },
    // { path: 'AssociadoCaptacao', redirectTo: 'AssociadoCaptacao'},
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    // { path: '**', redirectTo: 'home' }
    { path: '**', component: PageNotFoundComponent }
];

@NgModule({
    imports: [
        RouterModule.forRoot(
            appRoutes,
            { enableTracing: false } // <-- debugging purposes only
        )
    ],
    exports: [
        RouterModule
    ]
})

export class AppRoutingModule {
}
