import { RecebimentoService } from './../../shared/services/recebimento.service';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { Data } from '@angular/router/src/config';
import { Recebimento } from '../../shared/model/recebimento';
import { TipoPublico } from '../../shared/model/tipo-publico';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';

@Component({
  selector: 'app-recebimento-evento-form',
  templateUrl: './recebimento.evento.form.component.html',
  styleUrls: ['./recebimento.evento.form.component.css']
})
export class RecebimentoEventoFormComponent implements OnInit {

  @Input() recebimento: Recebimento;

  title = 'Dados de pagamento de evento do associado';

  private selectedId: any;

  tiposPublicos: TipoPublico[];

  constructor(
    private service: RecebimentoService,
    private serviceTP: TipoPublicoService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  getRecebimentoById(id: number): void {

    this.service.getById(id)
          .subscribe(recebimento => this.recebimento = recebimento);
  }

  gotoSave() {

    this.service.addRecebimento(this.recebimento)
    .subscribe(() =>  this.gotoShowPopUp());
  }

  gotoShowPopUp() {

    // Colocar a chamada para a implementação do PopUp modal de aviso:
    alert('Registro salvo com sucesso!');
  }

  gotoNotificarAssociado() {

    if (confirm('Deseja notificar o Associado?')) {
      alert('Notificação enviada com sucesso');
    }
  }

  gotoRecebimentoAnuidade() {

    let recebimentoId = this.recebimento ? this.recebimento.recebimentoId : null;
    this.router.navigate(['/RecebimentoEvento', { id: recebimentoId, foo: 'foo' }]);
  }

  getTiposPublicos(): void {

    this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
  }

  ngOnInit() {

    this.getTiposPublicos();

    const id = +this.route.snapshot.paramMap.get('id');
    if (id > 0) {
        this.getRecebimentoById(id);
    } else {
      alert('Não foi encontrato recebimento para o Id Informado');
      this.gotoRecebimentoAnuidade();
    }
  }
}
