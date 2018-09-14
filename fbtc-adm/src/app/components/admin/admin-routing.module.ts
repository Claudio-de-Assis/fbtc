import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { AdminDashboardComponent } from './admin-dashboard.component';
import { ManageHeroesComponent } from './manage-heroes.component';
import { ManageCrisesComponent } from './manage-crises.component';
import { AdminComponent } from './admin.component';


import { AuthGuard } from '../shared/services/auth-guard.service';
import { AssociadoListComponent } from '../associado/associado-list/associado.list.component';
import { AssociadoFormComponent } from '../associado/associado-form/associado.form.component';
import { ColaboradorListComponent } from '../colaborador/colaborador-list/colaborador.list.component';
import { ColaboradorFormComponent } from '../colaborador/colaborador-form/colaborador.form.component';
import { RecebimentoAnuidadeListComponent } from '../recebimento-anuidade/recebimento-anuidade-list/recebimento.anuidade.list.component';
import { RecebimentoAnuidadeFormComponent } from '../recebimento-anuidade/recebimento-anuidade-form/recebimento.anuidade.form.component';
import { RecebimentoEventoListComponent } from '../recebimento-evento/recebimento-evento-list/recebimento.evento.list.component';
import { RecebimentoEventoFormComponent } from '../recebimento-evento/recebimento-evento-form/recebimento.evento.form.component';
import { IsencaoAnuidadeListComponent } from '../isencao-anuidade/isencao-anuidade-list/isencao.anuidade.list.component';
import { IsencaoAnuidadeFormComponent } from '../isencao-anuidade/isencao-anuidade-form/isencao.anuidade.form.component';
import { IsencaoEventoListComponent } from '../isencao-evento/isencao-evento-list/isencao.evento.list.component';
import { IsencaoEventoFormComponent } from '../isencao-evento/isencao-evento-form/isencao.evento.form.component';
import { AtcListComponent } from '../atc/atc-list/atc.list.component';
import { AtcFormComponent } from '../atc/atc-form/atc.form.component';
import { RelatorioTotalAssociadosTipoComponent } from '../relatorios/relatorio-total-associados-tipo/relatorio-total-associados-tipo.component';
import { RelatorioRecebimentoStatusComponent } from '../relatorios/relatorio-recebimento-status/relatorio-recebimento-status.component';
import { RelatorioAssociadosFaixaComponent } from '../relatorios/relatorio-associados-faixa/relatorio-associados-faixa.component';
import { RelatorioAssociadosEstadosComponent } from '../relatorios/relatorio-associados-estados/relatorio-associados-estados.component';
import { RelatorioAssociadosGeneroComponent } from '../relatorios/relatorio-associados-genero/relatorio-associados-genero.component';
import { RelatorioAssociadosAnoComponent } from '../relatorios/relatorio-associados-ano/relatorio-associados-ano.component';
import { RelatorioReceitaAnualComponent } from '../relatorios/relatorio-receita-anual/relatorio-receita-anual.component';
import { EventoListComponent } from '../evento/evento-list/evento.list.component';
import { EventoFormComponent } from '../evento/evento-form/evento.form.component';
import { EventoPreviewComponent } from '../evento/evento-preview/evento-preview.component';
import { UserProfileFormComponent } from '../user-profile/user-profile-form/user-profile-form.component';
import { AssociadoSelfFormComponent } from '../associado/associado-self-form/associado-self.form.component';
import { FichaFinanceiraFormComponent } from './../ficha-financeira/ficha-financeira-form/ficha.financeira.form.component';
import { FichaFinanceiraListComponent } from './../ficha-financeira/ficha-financeira-list/ficha.financeira.list.component';



const adminRoutes: Routes = [
    {path: '', redirectTo: 'admin', pathMatch: 'full'},
    { path: 'admin',
      component: AdminComponent,
      canActivate: [AuthGuard],
      children: [
            { path: 'Associado', component: AssociadoListComponent },
            { path: 'AssociadoNovo', component: AssociadoFormComponent },
            { path: 'Associado/:id', component: AssociadoFormComponent },
            { path: 'Colaborador', component: ColaboradorListComponent },
            { path: 'Colaborador/:id', component: ColaboradorFormComponent },
            { path: 'ColaboradorNovo', component: ColaboradorFormComponent },
            { path: 'RecebimentoAnuidade', component: RecebimentoAnuidadeListComponent },
            { path: 'RecebimentoAnuidade/:id', component: RecebimentoAnuidadeFormComponent },
            { path: 'RecebimentoEvento', component: RecebimentoEventoListComponent },
            { path: 'RecebimentoEvento/:id', component: RecebimentoEventoFormComponent },
            { path: 'IsencaoAnuidade', component: IsencaoAnuidadeListComponent },
            { path: 'IsencaoAnuidade/:id', component: IsencaoAnuidadeFormComponent },
            { path: 'IsencaoAnuidadeNova', component: IsencaoAnuidadeFormComponent },
            { path: 'IsencaoEvento', component: IsencaoEventoListComponent },
            { path: 'IsencaoEvento/:id', component: IsencaoEventoFormComponent },
            { path: 'IsencaoEventoNova', component: IsencaoEventoFormComponent },
            { path: 'Atc', component: AtcListComponent },
            { path: 'Atc/:id', component: AtcFormComponent },
            { path: 'RptTotalAssociadosTipo', component: RelatorioTotalAssociadosTipoComponent },
            { path: 'RptRecebimentoStatus', component: RelatorioRecebimentoStatusComponent },
            { path: 'RptAssociadosFaixa', component: RelatorioAssociadosFaixaComponent },
            { path: 'RptAssociadosEstados', component: RelatorioAssociadosEstadosComponent },
            { path: 'RptAssociadosGenero', component: RelatorioAssociadosGeneroComponent },
            { path: 'RptAssociadosAno', component: RelatorioAssociadosAnoComponent },
            { path: 'RptReceitaAnual', component: RelatorioReceitaAnualComponent },
            { path: 'Evento', component: EventoListComponent },
            { path: 'Evento/:id', component: EventoFormComponent },
            { path: 'EventoNovo', component: EventoFormComponent },
            { path: 'EventoPreview/:id', component: EventoPreviewComponent},
            { path: 'UserProfile/:id', component: UserProfileFormComponent },
            { path: 'AssociadoPublico', component: AssociadoSelfFormComponent},
            { path: 'FichaFinanceira', component: FichaFinanceiraListComponent},
            { path: 'FichaFinanceira/:id', component: FichaFinanceiraFormComponent},
            { path: 'FichaFinanceiraNova', component: FichaFinanceiraFormComponent}
          ]
    }
  ];

  @NgModule({
    imports: [
      RouterModule.forChild(adminRoutes)
    ],
    exports: [
      RouterModule
    ]
  })
  export class AdminRoutingModule {}
