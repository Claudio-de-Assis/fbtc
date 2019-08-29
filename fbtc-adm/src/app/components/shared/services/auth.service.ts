import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/delay';
import { UserProfileService } from './user-profile.service';
import { UserProfileRoute } from '../webapi-routes/user-profile.route';
import { UserProfile, UserProfileLogin } from '../model/user-profile';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

const httpOptions2 = {
  headers: new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded' })
};


@Injectable()
export class AuthService {

  isLoggedIn: boolean;
  isErrorLoggedIn: boolean;
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
      this.isErrorLoggedIn = false;
      this.redirectUrl = '';
      // this.userProfile = new UserProfile();
    }

  loginUser(userProfileLogin: UserProfileLogin): Observable<UserProfile> {
    return this.http.post<UserProfile>(this.apiRoute.loginUser(), userProfileLogin, httpOptions)
        .do(result => {
          this.userProfile = result;
          this.isLoggedIn = true;
          // localStorage.setItem('teste', 'amor');
          // localStorage.getItem('teste');
        }, (err:  HttpErrorResponse) => {
            this.isErrorLoggedIn = true;
            console.log('VEja:....' + this.isErrorLoggedIn);
            console.log('outros:....' + err.status + ' - ' + err.message + ' - ' + err.statusText  + ' - ' + err.type);
          });

  }

  loginUserToken(userEmail: string, password: string) {

    const data = `username=${userEmail}&password=${password}&grant_type=password`;
    const reqHeader = new HttpHeaders();
    reqHeader.append('Content-Type', 'application/x-www-urlencoded');
    reqHeader.append('Access-Control-Allow-Origin', '*');

    return this.http.post(this.apiRoute.loginUserToken(), data, {headers: reqHeader});
  }

  getUserProfile(): UserProfile {
    return this.userProfile;
  }

  logout(): void {
    this.isLoggedIn = false;
  }
}
