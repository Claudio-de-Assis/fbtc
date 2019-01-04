import { Router } from '@angular/router';
import { Component } from '@angular/core';

/*
@Component({
  template: '<h2>Page not found</h2>'
})*/

@Component({
    selector: 'app-not-found-page',
    templateUrl: './not-found.component.html'
  })


export class PageNotFoundComponent {

constructor(
    private router: Router,

) {}

    gotoLogin() {

        this.router.navigate(['']);
    }
}
