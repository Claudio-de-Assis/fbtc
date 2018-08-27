import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertConfigComponent } from './alert-config/alert-config.component';
import { AlertCustomComponent } from './alert-custom/alert-custom.component';
import { AlertCloseableComponent } from './alert-closeable/alert-closeable.component';
import { AlertBasicComponent } from './alert-basic/alert-basic.component';
import { AlertSelfclosingComponent } from './alert-selfclosing/alert-selfclosing.component';
import { AlertConfigService } from '../services/alert-config.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    AlertBasicComponent,
    AlertCloseableComponent,
    AlertConfigComponent,
    AlertCustomComponent,
    AlertSelfclosingComponent
  ],
  providers: [
    AlertConfigService
  ]
})
export class CustomAlertsModule { }
