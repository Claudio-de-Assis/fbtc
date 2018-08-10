import { Component, OnInit } from '@angular/core';
import { NgxPermissionsService } from 'ngx-permissions';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  constructor(private permissionsService: NgxPermissionsService,) { }

  ngOnInit() {
    const permission = ["Financeiro", "Gestor", "Secretário"];
    this.permissionsService.loadPermissions(permission);
  }


  
}
