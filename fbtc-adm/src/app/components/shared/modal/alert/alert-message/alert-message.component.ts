import { Data } from '@angular/router/src/config';
import { Component, OnInit, Input, ViewChild } from '@angular/core';

@Component({
  selector: 'app-alert-message',
  templateUrl: './alert-message.component.html',
  styleUrls: ['./alert-message.component.css']
})
export class AlertMessageComponent implements OnInit {

  @Input() message: string;
  @Input() showPopup: string = 'true';

  constructor() {

  }

  ngOnInit() {

    // this.message = '';
    // this.showPopup = false;
  }
}
