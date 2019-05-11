import { UserProfile, UserProfileLogin } from '../model/user-profile';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/delay';
import { UserProfileService } from './user-profile.service';
import { UserProfileRoute } from '../webapi-routes/user-profile.route';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AuthService {

  isLoggedIn: boolean;
  // store the URL so we can redirect after logging in
  redirectUrl: string;
  userProfile: UserProfile;
  _booControle?: boolean;
  _i: number;

  constructor(
    private http: HttpClient,
    private apiRoute: UserProfileRoute,
    private userProfileService: UserProfileService
    ) {
      this.isLoggedIn = false;
      this.redirectUrl = '';
      // this.userProfile = new UserProfile();
    }

  loginUser(userProfileLogin: UserProfileLogin): Observable<UserProfile> {
    return this.http.post<UserProfile>(this.apiRoute.loginUser(), userProfileLogin, httpOptions)
        .do(result => {
          this.userProfile = result;
          this.isLoggedIn = true;
        });
  }

  getUserProfile(): UserProfile {
    return this.userProfile;
  }

  logout(): void {
    this.isLoggedIn = false;
  }
}
