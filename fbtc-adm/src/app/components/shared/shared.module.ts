import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';

import { BooMessagePipe } from './pipes/boolean-viewer.pipe';
import { TipoPerfilPipe } from './pipes/tipo-perfil.pipe';
import { TipoEventoPipe } from './pipes/tipo-evento.pipe';
import { FindNameInTipoPublicoPipe } from './pipes/find-name-in-tipo-publico.pipe';
// import { AtcRoute } from './webapi-routes/atc.route';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    BooMessagePipe,
    FindNameInTipoPublicoPipe,
    TipoPerfilPipe,
    TipoEventoPipe
    // CommonModule
    // FormsModule
  ],
  exports: [
    BooMessagePipe,
    FindNameInTipoPublicoPipe,
    TipoPerfilPipe,
    TipoEventoPipe
  ],
  providers: [
   // AtcRoute

  ]
})
export class SharedModule { }
