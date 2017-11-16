import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app.routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { UserService } from './components/shared/services/user.service';
// import { AtaEventoListComponent } from './components/ata-evento/ata-evento-list/ata.evento.list.component';
// import { AtaAnuidadeListComponent } from './components/ata-anuidade/ata-anuidade-list/ata.anuidade.list.component';
 import { RecebimentoEventoListComponent } from './components/recebimento-evento/recebimento-evento-list/recebimento.evento.list.component';
import { RecebimentoAnuidadeListComponent } from './components/recebimento-anuidade/recebimento-anuidade-list/recebimento.anuidade.list.component';
// import { EventoListComponent } from './components/evento/evento-list/evento.list.component';

import { ColaboradorModule } from './components/colaborador/colaborador.module';
import { AssociadoModule } from './components/associado/associado.module';
import { IsencaoAnuidadeModule } from './components/isencao-anuidade/isencao.anuidade.module';
import { IsencaoEventoModule } from './components/isencao-evento/isencao.evento.module';
import { EventoModule } from './components/evento/evento.module';
// import { AssociadoIsencaoListComponent } from './components/associado/associado-isencao-list/associado-isencao-list.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    BrowserModule,
    AssociadoModule,
    ColaboradorModule,
    IsencaoAnuidadeModule,
    IsencaoEventoModule,
    EventoModule,
    AppRoutingModule,
  ],
  declarations: [
    AppComponent,
    HeaderComponent,
    RecebimentoAnuidadeListComponent,
    RecebimentoEventoListComponent,
  ],
  providers: [
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
