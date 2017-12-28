import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

// Classe que permite enviar dados de um componente para outro.
@Injectable()
export class ValueShareService {

    // Observable string sources
    private valueStringSource = new Subject<string>();
    // private nomeImagemSource = new Subject<string>();

    // Observable string streams
    valueStringInformada$ = this.valueStringSource.asObservable();
    // nomeImagemGravada$ = this.nomeImagemSource.asObservable();

    // Service message commands
    addValueString(valueString: string) {
    this.valueStringSource.next(valueString);
    }

    /*
    addNomeImagem(nomeImagem: string) {
        this.nomeImagemSource.next(nomeImagem);
    }*/
}
