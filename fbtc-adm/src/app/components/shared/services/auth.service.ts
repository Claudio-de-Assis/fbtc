import { UserProfile } from '../model/user-profile';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/delay';
import { UserProfileService } from './user-profile.service';

@Injectable()
export class AuthService {

  isLoggedIn: boolean;
  // store the URL so we can redirect after logging in
  redirectUrl: string;
  userProfile: UserProfile;
  _booControle?: boolean;
  _i: number;

  constructor(
    private userProfileService: UserProfileService
    ) {
      this.isLoggedIn = false;
      this.redirectUrl = '';
      this.userProfile = new UserProfile();
    }

   getDadosUser(senha: string, eMail: string) {

      this.userProfileService.getByEmailPassword(senha, eMail).subscribe(
      userProfile => {
        this.userProfile = userProfile;
        console.log('então...: ' + this.userProfile.nome);
      },

    );
   }

  login(senha: string, eMail: string ): Observable<boolean> {
//  return Observable.of(true).delay(1000).do(val => this.isLoggedIn = true);
  return Observable.of(true).delay(1000)
  .do(val => {
    this.isLoggedIn = (true);

    // this.userProfileService.getLogin(senha, eMail)
     // .subscribe(isLoggedIn => this.isLoggedIn = isLoggedIn);

  });

  }

  getUserProfile(): UserProfile {
    return this.userProfile;
  }

  logout(): void {
    this.isLoggedIn = false;
  }
}
