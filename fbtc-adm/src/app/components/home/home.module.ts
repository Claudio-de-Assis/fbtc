import { NgModule, LOCALE_ID } from '@angular/core';

import { HomeRoutingModule } from './home.routing.module';
import { HomeComponent } from './home.component'



@NgModule({
  imports: [
    HomeRoutingModule,
    HomeComponent
],
  declarations: [HomeRoutingModule, HomeComponent],
  exports: [HomeRoutingModule, HomeComponent],
  providers: []
})
export class HomeModule { }
