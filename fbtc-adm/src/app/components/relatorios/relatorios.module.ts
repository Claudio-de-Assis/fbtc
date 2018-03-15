import { RelatoriosService } from './../shared/services/relatorios.service';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RelatoriosRoutingModule } from './relatorios.routing.module';

import { RelatorioTotalAssociadosTipoComponent } from './relatorio-total-associados-tipo/relatorio-total-associados-tipo.component';
import { RelatorioRecebimentoStatusComponent } from './relatorio-recebimento-status/relatorio-recebimento-status.component';
import { RelatorioAssociadosFaixaComponent } from './relatorio-associados-faixa/relatorio-associados-faixa.component';
import { RelatorioAssociadosEstadosComponent } from './relatorio-associados-estados/relatorio-associados-estados.component';
import { RelatorioAssociadosGeneroComponent } from './relatorio-associados-genero/relatorio-associados-genero.component';
import { RelatorioAssociadosAnoComponent } from './relatorio-associados-ano/relatorio-associados-ano.component';
import { RelatorioReceitaAnualComponent } from './relatorio-receita-anual/relatorio-receita-anual.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RelatoriosRoutingModule,
    HttpModule
  ],
  declarations: [
    RelatorioTotalAssociadosTipoComponent,
    RelatorioRecebimentoStatusComponent,
    RelatorioAssociadosFaixaComponent,
    RelatorioAssociadosEstadosComponent,
    RelatorioAssociadosGeneroComponent,
    RelatorioAssociadosAnoComponent,
    RelatorioReceitaAnualComponent
  ],
  exports: [
    RelatorioTotalAssociadosTipoComponent,
    RelatorioRecebimentoStatusComponent,
    RelatorioAssociadosFaixaComponent,
    RelatorioAssociadosEstadosComponent,
    RelatorioAssociadosGeneroComponent,
    RelatorioAssociadosAnoComponent,
    RelatorioReceitaAnualComponent
  ],
  providers: [
    RelatoriosService
  ]
})
export class RelatoriosModule { }
