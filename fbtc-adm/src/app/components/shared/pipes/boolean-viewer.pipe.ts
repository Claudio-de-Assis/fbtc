import {Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'booMessage', pure: true})
export class BooMessagePipe implements PipeTransform {
    transform(boo: boolean): string {
        return this.getMessage(boo);
    }
    private getMessage(boo: boolean): string {
        if (boo === true ) {
            return 'Sim';
        } else {
            return 'Não';
        }
    }
}
