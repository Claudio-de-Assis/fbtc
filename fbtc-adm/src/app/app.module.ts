import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { MessagesComponent } from './messages/messages.component';

import { MessageService } from './message.service';
import { UserService } from './components/shared/services/user.service';

import { AppRoutingModule } from './app.routing.module';
import { RecebimentoEventoModule } from './components/recebimento-evento/recebimento-evento.module';
import { RecebimentoAnuidadeModule } from './components/recebimento-anuidade/recebimento-anuidade.module';
import { ColaboradorModule } from './components/colaborador/colaborador.module';
import { AssociadoModule } from './components/associado/associado.module';
import { IsencaoAnuidadeModule } from './components/isencao-anuidade/isencao.anuidade.module';
import { IsencaoEventoModule } from './components/isencao-evento/isencao.evento.module';
import { EventoModule } from './components/evento/evento.module';
import { AnuidadeModule } from './components/anuidade/anuidade.module';

import { AnuidadeRoute } from './components/shared/webapi-routes/anuidade.route';
import { IsencaoRoute } from './components/shared/webapi-routes/isencao.route';
import { RecebimentoRoute } from './components/shared/webapi-routes/recebimento.route';
import { EventoRoute } from './components/shared/webapi-routes/evento.route';
import { PagSeguroRoute } from './components/shared/webapi-routes/pagSeguro.route';
// import { AtcRoute } from './components/shared/webApi-routes/atc.route';
import { AtcModule } from './components/atc/atc.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    AssociadoModule,
    ColaboradorModule,
    IsencaoAnuidadeModule,
    IsencaoEventoModule,
    EventoModule,
    AnuidadeModule,
    RecebimentoAnuidadeModule,
    RecebimentoEventoModule,
    AtcModule,
    AppRoutingModule,
  ],
  declarations: [
    AppComponent,
    MessagesComponent,
    HeaderComponent
  ],
  providers: [
    UserService,
    MessageService,
    IsencaoRoute,
    RecebimentoRoute,
    EventoRoute,
    AnuidadeRoute,
    PagSeguroRoute,
    // AtcRoute,
    { provide: LOCALE_ID, useValue: 'PT-BR' }
  ],
  exports: [

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
