import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioTotalAssociadosTipoComponent } from './relatorio-total-associados-tipo.component';

describe('RelatorioTotalAssociadosTipoComponent', () => {
  let component: RelatorioTotalAssociadosTipoComponent;
  let fixture: ComponentFixture<RelatorioTotalAssociadosTipoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RelatorioTotalAssociadosTipoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RelatorioTotalAssociadosTipoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
