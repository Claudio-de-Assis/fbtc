import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { RelatorioReceitaAnualComponent } from './relatorio-receita-anual/relatorio-receita-anual.component';
import { RelatorioAssociadosGeneroComponent } from './relatorio-associados-genero/relatorio-associados-genero.component';
import { RelatorioAssociadosEstadosComponent } from './relatorio-associados-estados/relatorio-associados-estados.component';
import { RelatorioAssociadosFaixaComponent } from './relatorio-associados-faixa/relatorio-associados-faixa.component';
import { RelatorioRecebimentoStatusComponent } from './relatorio-recebimento-status/relatorio-recebimento-status.component';
import { RelatorioAssociadosAnoComponent } from './relatorio-associados-ano/relatorio-associados-ano.component';
import { RelatorioTotalAssociadosTipoComponent } from './relatorio-total-associados-tipo/relatorio-total-associados-tipo.component';

const relatoriosRoutes: Routes = [
    { path: 'RptTotalAssociadosTipo', component: RelatorioTotalAssociadosTipoComponent },
    { path: 'RptRecebimentoStatus', component: RelatorioRecebimentoStatusComponent },
    { path: 'RptAssociadosFaixa', component: RelatorioAssociadosFaixaComponent },
    { path: 'RptAssociadosEstados', component: RelatorioAssociadosEstadosComponent },
    { path: 'RptAssociadosGenero', component: RelatorioAssociadosGeneroComponent },
    { path: 'RptAssociadosAno', component: RelatorioAssociadosAnoComponent },
    { path: 'RptReceitaAnual', component: RelatorioReceitaAnualComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(relatoriosRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class RelatoriosRoutingModule { }
