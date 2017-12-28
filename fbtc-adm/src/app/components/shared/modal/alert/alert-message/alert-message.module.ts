import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AlertMessageComponent } from './alert-message.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule
  ],
  declarations: [
   AlertMessageComponent
  ],
  exports: [
    AlertMessageComponent
  ],
  providers: [],
})
export class AlertMessageModule { }
