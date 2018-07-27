import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { UserProfileFormComponent } from './user-profile-form/user-profile-form.component';

const userProfileRoutes: Routes = [
    { path: 'UserProfile/:id', component: UserProfileFormComponent },
  ];

  @NgModule({
    imports: [
        RouterModule.forChild(userProfileRoutes)
    ],
    exports: [
        RouterModule
    ]
  })
export class UserProfileRoutingModule {}
