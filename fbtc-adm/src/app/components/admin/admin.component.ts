import { Component, OnInit } from '@angular/core';
import { NgxPermissionsService } from 'ngx-permissions';
import { UserProfileService } from '../shared/services/user-profile.service';
import { UserProfile } from '../shared/model/user-profile';
import { Util } from '../shared/util/util'



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
              private util: Util) { }

  ngOnInit() {
    // this.getPerfil()
    this.permission = ['Financeiro'];//Exemplo de perfil permitido
    this.permissionsService.loadPermissions(this.permission);    
  }

  getPerfil(){
    this.userProfile = this.userProfileService.getUserProfile();

    console.log("user=>", this.userProfile)
  }

  
}
