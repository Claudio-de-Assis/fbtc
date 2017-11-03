import { UserService } from './shared/services/user.service';
import { AtaEventoListComponent } from './ata-evento/ata-evento-list/ata.evento.list.component';
import { AtaAnuidadeListComponent } from './ata-anuidade/ata-anuidade-list/ata.anuidade.list.component';
import { RecebimentoAnuidadeListComponent } from './recebimento-anuidade/recebimento-anuidade-list/recebimento.anuidade.list.component';
import { EventoListComponent } from './evento/evento-list/evento.list.component';
import { HeaderComponent } from './shared/header/header.component';
import { ColaboradorModule } from './colaborador/colaborador.module';
import { AssociadoModule } from './associado/associado.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app.routing.module';


import { AppComponent } from './app.component';
import { AssociadoListComponent } from './associado/associado-list/associado-list.component';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      BrowserModule,
      AssociadoModule,
      ColaboradorModule,
      AppRoutingModule
  ],
  declarations: [
      AppComponent,
      HeaderComponent,
      EventoListComponent,
      RecebimentoAnuidadeListComponent,
      RecebimentoAnuidadeListComponent,
      AtaAnuidadeListComponent,
      AtaEventoListComponent
  ],
  bootstrap: [AppComponent],
  
  providers: [
      UserService
  ]
})
export class AppModule { }
