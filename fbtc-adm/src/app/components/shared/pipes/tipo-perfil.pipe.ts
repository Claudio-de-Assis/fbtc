import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'tipoPerfil', pure: true})
export class TipoPerfilPipe implements PipeTransform {
    transform(id: string): string {
        return this.getTipoPerfil(id);
    }
    private getTipoPerfil(id: string): string {
        if (id === '1' ) {
            return 'Gestor';
        } else if  (id === '2' ) {
            return 'Secretário';
        } else if (id === '3') {
            return 'Financeiro';
        } else {
            return id;
        }
    }
}
