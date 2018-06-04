import { Component } from '@angular/core';

import { UserService } from '../services/user.service';

@Component({
    selector: 'app-shared-header',
    templateUrl: './header.component.html',
})

export class HeaderComponent {

    user: string;
    isLoged: boolean;

    constructor(userService: UserService) {

        this.isLoged = true;
        this.user = userService.userName;
    }
}
