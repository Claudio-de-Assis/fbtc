import {Component, OnInit} from '@angular/core';
import {Subject} from 'rxjs/Subject';
import {debounceTime} from 'rxjs/operator/debounceTime';

@Component({
  selector: 'app-alert-selfclosing',
  templateUrl: './alert-selfclosing.component.html',
  styleUrls: ['./alert-selfclosing.component.css']
})
export class AlertSelfclosingComponent implements OnInit {

  private _success = new Subject<string>();

  staticAlertClosed = false;
  successMessage: string;

  ngOnInit(): void {
    setTimeout(() => this.staticAlertClosed = true, 20000);

    this._success.subscribe((message) => this.successMessage = message);
    debounceTime.call(this._success, 5000).subscribe(() => this.successMessage = null);
  }

  public changeSuccessMessage() {
    this._success.next(`${new Date()} - Message successfully changed.`);
  }
}
