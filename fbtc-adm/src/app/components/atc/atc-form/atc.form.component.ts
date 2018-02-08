import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import { Atc } from '../../shared/model/atc';
import { AtcService } from '../../shared/services/atc.service';

@Component({
  selector: 'app-atc-form',
  templateUrl: './atc.form.component.html',
  styleUrls: ['./atc.form.component.css']
})
export class AtcFormComponent implements OnInit {

  @Input() atc: Atc = { atcId: 0, nome: '', uf: '', nomePres: '', nomeVPres: '', nomePSec: '', nomeSSec: '', nomePTes: '',
                        nomeSTes: '', site: '', siteDiretoria: '', ativo: true};

  title = 'Atc';
  badge = '';

  editAtcId: number = 0;

  private selectedId: any;

  submitted = false;

  constructor(
    private service: AtcService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }


  getAtcById(id: number): void {

    this.service.getById(id)
        .subscribe(atc => this.atc = atc);
  }

  gotoAtcs() {

    let atcId = this.atc ? this.atc.atcId : null;
    this.router.navigate(['/Atc', { id: atcId, foo: 'foo' }]);
  }

save() {

  this.service.addAtc(this.atc)
  .subscribe(() =>  this.gotoShowPopUp('Registro salvo com sucesso!'));

  this.submitted = false;
}


gotoShowPopUp(msg: string) {

  // Colocar a chamada para a implementação do PopUp modal de aviso:
  alert(msg);
}

excluir() {

    this.gotoAtcs();
}

onSubmit() {

  this.submitted = true;
  this.save();
}


  ngOnInit() {

    this.editAtcId = +this.route.snapshot.paramMap.get('id');

    if (this.editAtcId > 0) {
        this.badge = 'Edição';
        this.getAtcById(this.editAtcId);

    } else {
        this.badge = 'Novo';
    }
  }
}
