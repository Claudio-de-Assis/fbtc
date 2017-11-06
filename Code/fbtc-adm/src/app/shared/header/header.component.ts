import { Component } from '@angular/core';

import { UserService } from '../services/user.service';


@Component({
    selector: 'shared-header',
    templateUrl: './header.component.html' 
})

export class HeaderComponent {

    user: string = '';


    constructor(userService: UserService) {

        this.user = userService.userName;
    }
}
