import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssinaturaEventoAssociadoFormComponent } from './assinatura.evento.associado.form.component';

describe('AssinaturaEventoAssociadoFormComponent', () => {
  let component: AssinaturaEventoAssociadoFormComponent;
  let fixture: ComponentFixture<AssinaturaEventoAssociadoFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssinaturaEventoAssociadoFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssinaturaEventoAssociadoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
