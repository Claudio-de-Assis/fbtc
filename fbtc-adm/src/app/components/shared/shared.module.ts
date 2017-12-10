import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BooMessagePipe } from './pipes/boolean-viewer.pipe';
import { TipoPerfilPipe } from './pipes/tipo-perfil.pipe';
import { TipoEventoPipe } from './pipes/tipo-evento.pipe';
import { FindNameInTipoPublicoPipe } from './pipes/find-name-in-tipo-publico.pipe';
import { StatusPagamentoPipe } from './pipes/status-pagamento-pipe';
import { ObjetivoPagamentoPipe } from './pipes/objetivo-pagamento.pipe';
import { FormaPagamentoPipe } from './pipes/forma-pagamento.pipe';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    BooMessagePipe,
    FindNameInTipoPublicoPipe,
    TipoPerfilPipe,
    TipoEventoPipe,
    ObjetivoPagamentoPipe,
    StatusPagamentoPipe,
    FormaPagamentoPipe
  ],
  exports: [
    BooMessagePipe,
    FindNameInTipoPublicoPipe,
    TipoPerfilPipe,
    TipoEventoPipe,
    ObjetivoPagamentoPipe,
    StatusPagamentoPipe,
    FormaPagamentoPipe
  ],
  providers: [
   // AtcRoute
  ]
})
export class SharedModule { }
