import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociadoFichaFinanceiraEventoFormComponent } from './associado.ficha.financeira.evento.form.component';


describe('AssociadoFichaFinanceiraEventoFormComponent', () => {
  let component: AssociadoFichaFinanceiraEventoFormComponent;
  let fixture: ComponentFixture<AssociadoFichaFinanceiraEventoFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssociadoFichaFinanceiraEventoFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssociadoFichaFinanceiraEventoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
