import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'tipoDesconto', pure: true})
export class TipoDescontoPipe implements PipeTransform {
    transform(id: string): string {
        return this.getTipoPerfil(id);
    }
    private getTipoPerfil(id: string): string {
        if (id === '0' ) {
            return 'Não houve';
        } else if  (id === '1' ) {
            return 'Membro Gestão';
        } else if (id === '2') {
            return 'Membro ATC';
        } else if (id === '3') {
            return 'Secretaria';
        } else if (id === '4') {
            return 'Confi';
        } else {
            return id;
        }
    }
}
