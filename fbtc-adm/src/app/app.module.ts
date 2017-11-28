import { MessageService } from './message.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app.routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { UserService } from './components/shared/services/user.service';
import { RecebimentoEventoModule } from './components/recebimento-evento/recebimento-evento.module';
import { RecebimentoAnuidadeModule } from './components/recebimento-anuidade/recebimento-anuidade.module';
import { ColaboradorModule } from './components/colaborador/colaborador.module';
import { AssociadoModule } from './components/associado/associado.module';
import { IsencaoAnuidadeModule } from './components/isencao-anuidade/isencao.anuidade.module';
import { IsencaoEventoModule } from './components/isencao-evento/isencao.evento.module';
import { EventoModule } from './components/evento/evento.module';
import { MessagesComponent } from './messages/messages.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    BrowserModule,
    HttpClientModule,
    AssociadoModule,
    ColaboradorModule,
    IsencaoAnuidadeModule,
    IsencaoEventoModule,
    EventoModule,
    RecebimentoAnuidadeModule,
    RecebimentoEventoModule,
    AppRoutingModule,
  ],
  declarations: [
    AppComponent,
    MessagesComponent,
    HeaderComponent
  ],
  providers: [
    UserService,
    MessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
