import { EventoListComponent } from './evento/evento-list/evento.list.component';
import { AtaEventoListComponent } from './ata-evento/ata-evento-list/ata.evento.list.component';
import { AtaAnuidadeListComponent } from './ata-anuidade/ata-anuidade-list/ata.anuidade.list.component';
import { RecebimentoEventoListComponent } from './recebimento-evento/recebimento-evento-list/recebimento.evento.list.component';
import { RecebimentoAnuidadeListComponent } from './recebimento-anuidade/recebimento-anuidade-list/recebimento.anuidade.list.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';


const appRoutes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  //{ path: 'home', component: HomeComponent },
  { path: 'Evento', component: EventoListComponent },
  { path: 'RecebimentoAnuidade', component: RecebimentoAnuidadeListComponent },
  { path: 'RecebimentoEvento', component: RecebimentoEventoListComponent },
  { path: 'AtaAnuidade', component: AtaAnuidadeListComponent },
  { path: 'AtaEvento', component: AtaEventoListComponent },
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
  )
  ],
  declarations: [],
  exports: [
    RouterModule
]
})
export class AppRoutingModule { }
