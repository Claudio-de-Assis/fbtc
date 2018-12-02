import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssinaturaEventoAssociadoListComponent } from './assinatura.evento.associado.list.component';

describe('AssinaturaEventoAssociadoListComponent', () => {
  let component: AssinaturaEventoAssociadoListComponent;
  let fixture: ComponentFixture<AssinaturaEventoAssociadoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssinaturaEventoAssociadoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssinaturaEventoAssociadoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
