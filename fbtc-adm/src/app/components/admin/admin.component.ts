import { Component, OnInit } from '@angular/core';
import { NgxPermissionsService } from 'ngx-permissions';
import { UserProfileService } from '../shared/services/user-profile.service';
import { UserProfile } from '../shared/model/user-profile';
import { Util } from '../shared/util/util'
import { AuthService } from '../shared/services/auth.service';



@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  permission = [];
  userProfile:  UserProfile;
  constructor(
              private permissionsService: NgxPermissionsService,
              private userProfileService: UserProfileService,
              public authService: AuthService,
              private util: Util) { }

  ngOnInit() {
  }
}
