/// <reference path="../../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { RecebimentoAnuidadeListComponent } from './recebimento.anuidade.list.component';

let component: RecebimentoAnuidadeListComponent;
let fixture: ComponentFixture<RecebimentoAnuidadeListComponent>;

describe('RecebimentoAnuidadeList component', () =>
{
    beforeEach(async(() =>
    {
        TestBed.configureTestingModule({
            declarations: [RecebimentoAnuidadeListComponent],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(RecebimentoAnuidadeListComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() =>
    {
        expect(true).toEqual(true);
    }));
});