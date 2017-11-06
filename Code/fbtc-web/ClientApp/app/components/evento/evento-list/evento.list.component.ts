import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'evento-list',
    templateUrl: './evento.list.component.html',
    styleUrls: ['./evento.list.component.css']
})
/** EventoList component*/
export class EventoListComponent implements OnInit
{
    /** EventoList ctor */
    constructor() { }

    /** Called by Angular after EventoList component initialized */
    ngOnInit(): void { }
}