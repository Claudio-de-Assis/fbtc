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
import { UserProfileModule } from '../user-profile/user-profile.module';
import { UserProfileRoute } from '../shared/webapi-routes/user-profile.route';



@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule,
    NgxPermissionsModule.forRoot(),
    UserProfileModule,
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
    Util,
    UserProfileRoute

  ]
  
})
export class AdminModule {}
