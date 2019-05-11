import { Anuidade } from '../../shared/model/anuidade';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { Util } from '../../shared/util/util';
import { AnuidadeService } from '../../shared/services/anuidade.service';

@Component({
  selector: 'app-anuidade.form',
  templateUrl: './anuidade.form.component.html',
  styleUrls: ['./anuidade.form.component.css']
})
export class AnuidadeFormComponent implements OnInit {

  title: string;
  badge: string;
  submitted: boolean;

  private selectedId: any;
  _util = Util;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: AnuidadeService
  ) {
    this.title = 'Anuidade';
    this.badge = '';
    this.submitted = false;
   }

  gotoAnuidades() {

    // let anuidadeId = this.anuidade ? this.anuidade.anuidadeId : null;
    // this.router.navigate(['/Anuidade', { id: anuidadeId, foo: 'foo' }]);
  }

  getAnuidadeById(id: number): void {

    // this.service.getById(id).subscribe(anuidade => this.anuidade = anuidade);
  }

  setAnuidade(): void {

    // this.service.setAnuidade().subscribe(anuidade => this.anuidade = anuidade);
  }

  onSubmit() {

    this.saveAnuidade();
  }

  saveAnuidade() {

    // this.service.addAnuidade(this.anuidade).subscribe(() =>  this.gotoShowPopUp('Registro salvo com sucesso!'));
  }

  gotoShowPopUp(msg: string) {

    // Colocar a chamada para a implementação do PopUp modal de aviso:
    alert(msg);
  }

  ngOnInit() {

    const id = +this.route.snapshot.paramMap.get('id');
    if (id > 0) {

      this.badge = '"Edição';
        this.getAnuidadeById(id);
    } else {

      this.badge = 'Novo';
    }
  }
}
