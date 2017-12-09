import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { RecebimentoAnuidadeService } from './../../shared/services/recebimento-anuidade.service';
import { Recebimento } from './../../shared/model/recebimento';

@Component({
  selector: 'app-recebimento-anuidade-form',
  templateUrl: './recebimento.anuidade.form.component.html',
  styleUrls: ['./recebimento.anuidade.form.component.css']
})
export class RecebimentoAnuidadeFormComponent implements OnInit {

  @Input() recebimento: Recebimento;

  title = 'Dados de pagamento de anuidade do associado';
  badget = '';

  private selectedId: any;

  constructor(
    private service: RecebimentoAnuidadeService,
    private router: Router,
    private route: ActivatedRoute
) { }

  getRecebimentoById(id: number): void {

    this.service.getById(id)
          .subscribe(recebimento => this.recebimento = recebimento);
  }

  save() {
    this.service.addRecebimentoAnuidade(this.recebimento)
    .subscribe(() => this.gotoRecebimentoAnuidade());    }

/*  gotoSave() {
    alert('Registro salvo com sucesso');
    this.gotoRecebimentoAnuidade();
  }*/

  gotoNotificarAssociado() {

    if (confirm('Deseja notificar o Associado?')) {
      alert('Notificação enviada com sucesso');
    }
  }

  gotoRecebimentoAnuidade() {

    let recebimentoId = this.recebimento ? this.recebimento.recebimentoId : null;
    this.router.navigate(['/RecebimentoAnuidade', { id: recebimentoId, foo: 'foo' }]);
  }

  ngOnInit() {

    const id = +this.route.snapshot.paramMap.get('id');
      if (id > 0) {
          this.badget = 'Edição';
          this.getRecebimentoById(id);
    } else {
      alert('Não foi encontrato recebimento para o Id Informado');
      this.gotoRecebimentoAnuidade();
    }
  }
}
