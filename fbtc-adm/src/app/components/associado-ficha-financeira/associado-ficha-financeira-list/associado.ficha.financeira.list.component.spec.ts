import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociadoFichaFinanceiraListComponent } from './associado.ficha.financeira.list.component';

describe('AssociadoFichaFinanceiraListComponent', () => {
  let component: AssociadoFichaFinanceiraListComponent;
  let fixture: ComponentFixture<AssociadoFichaFinanceiraListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssociadoFichaFinanceiraListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssociadoFichaFinanceiraListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
