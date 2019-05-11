import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import { EventoService } from '../../shared/services/evento.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { ValueShareService } from '../../shared/services/value-share.service';

import { EventoDao } from '../../shared/model/evento';
import { TipoPublicoValorDao } from '../../shared/model/tipo-publico';

import { FileUploadRoute } from '../../shared/webapi-routes/file-upload.route';

import { Util } from '../../shared/util/util';

@Component({
  selector: 'app-assinatura-evento-associado-form',
  templateUrl: './assinatura.evento.associado.form.component.html',
  styleUrls: ['./assinatura.evento.associado.form.component.css'],
  providers: [TipoPublicoService, ValueShareService]
})
export class AssinaturaEventoAssociadoFormComponent implements OnInit {

  @Input() tiposPublicosValoresDao: TipoPublicoValorDao[];

  @Input() eventoDao: EventoDao = { eventoId: 0, titulo: '', descricao: '', codigo: '', dtInicio: null,
            dtTermino: null, dtTerminoInscricao: null, tipoEvento: '', aceitaIsencaoAta: false,
            ativo: false, nomeFoto: '_no-foto-evento.jpg', tiposPublicosValoresDao: this.tiposPublicosValoresDao
  };

  title: string;
  badge: string;

  _util = Util;
  _nomeFotoPadrao: string;
  _nomeFoto: string;
  _eventoId: number;
  _msgRetorno: string;
  _msg: string;

  editEventoId: number;

  _editDtPagamento: string;

  submitted: boolean;

  _targetPagSeguro: string;
  _tokenPagSeguro: string;

  _botaoPagSeguroOk: boolean;
  _valorAnuidadeIdOriginal: number;
  _emProcessoPagamento: string;


  history: string[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private serviceTP: TipoPublicoService,
    private service: EventoService,
    private apiRoute: FileUploadRoute,
    private valueShareService: ValueShareService
  ) {
      valueShareService.valueStringInformada$.subscribe(
        nomeFoto => {
            this.history.push(nomeFoto);
      });
      this.title = 'Aquisição de Evento';
      this.badge = '';
      this._nomeFotoPadrao = '_no-foto-evento.jpg';
      this._nomeFoto = '_no-foto-evento.jpg';
      this.editEventoId = 0;
      this.submitted = false;
      this._eventoId = 0;
      this._msg = '';
      this._msgRetorno = '';

      this._editDtPagamento =  '10/01/2018';

      this._targetPagSeguro = '';
      this._tokenPagSeguro = '';

      this._botaoPagSeguroOk = true; // false
      this._valorAnuidadeIdOriginal = 0;
      this._emProcessoPagamento = 'false';
    }

  getEventoById(id: number): void {

      this.service.getById(id)
          .subscribe(eventoDao => this.eventoDao = eventoDao);
  }

  getTiposPublicosById(id: number): void {

    this.serviceTP.getTiposPublicoByEventoId(id).
      subscribe(tiposPublicosValoresDao => this.eventoDao.tiposPublicosValoresDao = tiposPublicosValoresDao);
  }

  gotoEventos() {

    const eventoId =  this.editEventoId;  // this.eventoDao ? this.eventoDao.eventoId : null;
    this.router.navigate(['admin/MinhaAssinaturaEvento', { id: eventoId, foo: 'foo' }]);
  }

  SaveEvento() {

    this._msg = '';
    this._nomeFoto = this.history[0];

    if (this._nomeFoto === undefined) {
        this._nomeFoto = this._nomeFotoPadrao;
    }

    this.eventoDao.nomeFoto = this._nomeFoto;
    this.service.addEventoDao(this.eventoDao)
    .subscribe(
      msg => {
          this._msgRetorno = msg;
          this.avaliaRetorno(this._msgRetorno);
      });
  }

  avaliaRetorno(msgRet: string) {

    if (msgRet.substring(0, 1) === '0') {

        this._eventoId = parseInt(msgRet.substring(0, 10), 10);

        this.router.navigate([`admin/Evento/${this._eventoId}`]);

        this.getEventoById(this._eventoId);

        this._msg = this._msgRetorno.substring(10);

        this.badge = 'Edição';

    } else {

        this._msg = this._msgRetorno;
    }
  }

  onSubmit() {

      this.SaveEvento();
  }

  gotoShowPopUp(msg: string) {

    // Colocar a chamada para a implementação do PopUp modal de aviso:
    alert(msg);
  }

  gotoPreviewAnuncio() {

    this.router.navigate(['admin/EventoPreview', +this.route.snapshot.paramMap.get('id') ]);
  }

  ngOnInit() {

    this.editEventoId = +this.route.snapshot.paramMap.get('id');

    if (this.editEventoId > 0) {
      this.badge = 'Edição';
      this.getEventoById(this.editEventoId);
      this.getTiposPublicosById(this.editEventoId);

    } else {
      this.badge = 'Novo';
      this.getTiposPublicosById(0);
    }
  }

  refreshImages(status) {
    if (status) {
      console.log( 'Upload realizado com sucesso!');
    }
  }
}
