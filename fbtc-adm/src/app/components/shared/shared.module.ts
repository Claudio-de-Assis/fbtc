import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BooMessagePipe } from './pipes/boolean-viewer.pipe';
import { TipoPerfilPipe } from './pipes/tipo-perfil.pipe';
import { TipoEventoPipe } from './pipes/tipo-evento.pipe';
import { FindNameInTipoPublicoPipe } from './pipes/find-name-in-tipo-publico.pipe';
import { StatusPagamentoPipe } from './pipes/status-pagamento-pipe';
import { ObjetivoPagamentoPipe } from './pipes/objetivo-pagamento.pipe';
import { FormaPagamentoPipe } from './pipes/forma-pagamento.pipe';

import { FileUploadModule } from './upload/file-upload.module';
import { FileUploadRoute } from './webapi-routes/file-upload.route';
import { MycurrencyPipe } from './pipes/mycurrency';
import { TipoAnuidadePipe } from './pipes/tipo-anuidade';
import { StatusPagamentoFBTCPipe } from './pipes/status-pagamento-fbtc.pipe';
import { TipoDescontoPipe } from './pipes/tipo-desconto.pipe';

@NgModule({
  imports: [
    CommonModule,
    FileUploadModule
  ],
  declarations: [
    BooMessagePipe,
    FindNameInTipoPublicoPipe,
    TipoPerfilPipe,
    TipoAnuidadePipe,
    TipoEventoPipe,
    ObjetivoPagamentoPipe,
    StatusPagamentoPipe,
    StatusPagamentoFBTCPipe,
    FormaPagamentoPipe,
    MycurrencyPipe,
    TipoDescontoPipe,
  ],
  exports: [
    BooMessagePipe,
    FindNameInTipoPublicoPipe,
    TipoPerfilPipe,
    TipoAnuidadePipe,
    TipoEventoPipe,
    ObjetivoPagamentoPipe,
    StatusPagamentoPipe,
    StatusPagamentoFBTCPipe,
    FormaPagamentoPipe,
    FileUploadModule,
    MycurrencyPipe,
    TipoDescontoPipe,
  ],
  providers: [
    FileUploadRoute
  ]
})
export class SharedModule { }
