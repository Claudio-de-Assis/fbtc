import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { FileUploadRoute } from '../shared/webapi-routes/file-upload.route';
import { UserProfileService } from '../shared/services/user-profile.service';
// import { FileUploadModule } from './../shared/upload/FileUploadModule';
import { SharedModule } from '../shared/shared.module';

import { UserProfileFormComponent } from './user-profile-form/user-profile-form.component';
import { UserProfileRoutingModule } from './user-profile-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    UserProfileRoutingModule,
    SharedModule,
//     FileUploadModule,
    HttpModule
  ],
  declarations: [
    UserProfileFormComponent
  ],
  exports: [
    UserProfileFormComponent
  ],
  providers: [
    UserProfileService,
    FileUploadRoute
  ]

})
export class UserProfileModule { }
