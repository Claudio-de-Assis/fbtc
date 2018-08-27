import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminComponent } from './admin.component';
import { AdminDashboardComponent } from './admin-dashboard.component';
import { ManageCrisesComponent } from './manage-crises.component';
import { ManageHeroesComponent } from './manage-heroes.component';
import { HeaderComponent } from '../shared/header/header.component';

import { AdminRoutingModule } from './admin-routing.module';

import { NgxPermissionsModule } from 'ngx-permissions';
import { UserProfileService } from '../shared/services/user-profile.service';
import { Util } from '../shared/util/util'


@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule,
    NgxPermissionsModule.forRoot(),
  ],
  declarations: [
    AdminComponent,
    AdminDashboardComponent,
    ManageCrisesComponent,
    ManageHeroesComponent,
    HeaderComponent

  ],
  providers:[
    UserProfileService,
    Util
  ]
  
})
export class AdminModule {}
