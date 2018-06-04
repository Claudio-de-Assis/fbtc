import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

// import { Atc } from '../../shared/model/atc';

// import { AtcService } from '../../shared/services/atc.service';
// import { UnidadeFederacaoService } from '../../shared/services/unidade-federacao.service';

// import { Util } from '../../shared/util/util';
// import { UnidadeFederacao } from '../../shared/model/unidade-federacao';

@Component({
  selector: 'app-login-form',
  templateUrl: './login.form.component.html',
  styleUrls: ['./login.form.component.css']
})
export class LoginFormComponent implements OnInit {

  // @Input() atc: Atc = { atcId: 0, nome: '', uf: '', nomePres: '', nomeVPres: '', nomePSec: '', nomeSSec: '', nomePTes: '',
  //                       nomeSTes: '', site: '', siteDiretoria: '', ativo: true};

  title = 'Login';
  badge = '';

  userName: string;
  passWord: string;


  // editAtcId: number = 0;

  // private selectedId: any;

  // unidadesFederacao: UnidadeFederacao[];

  submitted = false;

  // _util = Util;

  constructor(
    // private service: AtcService,
    // private serviceUF: UnidadeFederacaoService,
    private router: Router,
    private route: ActivatedRoute,
  ) {

    this.userName = '';
    this.passWord = '';

  }





loginUser() {
/*
  this.service.addAtc(this.atc)
  .subscribe(() =>  this.gotoShowPopUp('Registro salvo com sucesso!'));
*/
  this.submitted = false;
}


gotoShowPopUp(msg: string) {

  // Colocar a chamada para a implementação do PopUp modal de aviso:
  alert(msg);
}



onSubmit() {

  this.submitted = true;
  this.loginUser();
}


  ngOnInit() {

  }
}
