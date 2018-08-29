import { Component, ViewChild } from '@angular/core';

import { UserService } from '../services/user.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';


@Component({
    selector: 'app-shared-header',
    templateUrl: './header.component.html',
})

export class HeaderComponent {

    user: string;
    isLoged: boolean;
    isToggleIn: string = '';

    @ViewChild("toogle") toggle;
    constructor(userService: UserService, private authService: AuthService, private router: Router) {

        this.isLoged = true;
        this.user = userService.userName;
    }

    isToggle(menuItem: string) {        
        if (menuItem === this.isToggleIn) {
            this.isToggleIn = '0';
        } else {
            this.isToggleIn = menuItem;
        }
    }

    logout(){
        this.authService.logout();
        this.router.navigate(['login']);
    }

    
}
