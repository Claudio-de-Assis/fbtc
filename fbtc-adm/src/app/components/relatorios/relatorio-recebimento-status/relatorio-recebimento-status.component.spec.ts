import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioRecebimentoStatusComponent } from './relatorio-recebimento-status.component';

describe('RelatorioRecebimentoStatusComponent', () => {
  let component: RelatorioRecebimentoStatusComponent;
  let fixture: ComponentFixture<RelatorioRecebimentoStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RelatorioRecebimentoStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RelatorioRecebimentoStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
