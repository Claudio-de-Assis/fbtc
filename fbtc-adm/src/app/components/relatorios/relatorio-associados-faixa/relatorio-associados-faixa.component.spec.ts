import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioAssociadosFaixaComponent } from './relatorio-associados-faixa.component';

describe('RelatorioAssociadosFaixaComponent', () => {
  let component: RelatorioAssociadosFaixaComponent;
  let fixture: ComponentFixture<RelatorioAssociadosFaixaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RelatorioAssociadosFaixaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RelatorioAssociadosFaixaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
