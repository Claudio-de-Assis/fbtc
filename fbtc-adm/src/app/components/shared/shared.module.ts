import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';

import { BooMessagePipe } from './pipes/boolean-viewer.pipe';
import { TipoPerfilPipe } from './pipes/tipo-perfil.pipe';
import { FindNameInTipoPublico } from './pipes/find-name-in-tipo-publico.pipe';
import { TipoEventoPipe } from './pipes/tipo-evento.pipe';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    BooMessagePipe,
    FindNameInTipoPublico,
    TipoPerfilPipe,
    TipoEventoPipe
    // CommonModule
    // FormsModule
  ],
  exports: [
    BooMessagePipe,
    FindNameInTipoPublico,
    TipoPerfilPipe,
    TipoEventoPipe
  ]
})
export class SharedModule { }
