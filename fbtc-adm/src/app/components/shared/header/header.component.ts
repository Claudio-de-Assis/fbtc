import { Component, ViewChild } from '@angular/core';

import { UserService } from '../services/user.service';


@Component({
    selector: 'app-shared-header',
    templateUrl: './header.component.html',
})

export class HeaderComponent {

    user: string;
    isLoged: boolean;
    isToggleIn: string = '';

    @ViewChild("toogle") toggle;
    constructor(userService: UserService) {

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

    
}
