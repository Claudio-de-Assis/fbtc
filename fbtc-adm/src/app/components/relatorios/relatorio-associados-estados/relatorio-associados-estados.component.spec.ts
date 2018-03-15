import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioAssociadosEstadosComponent } from './relatorio-associados-estados.component';

describe('RelatorioAssociadosEstadosComponent', () => {
  let component: RelatorioAssociadosEstadosComponent;
  let fixture: ComponentFixture<RelatorioAssociadosEstadosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RelatorioAssociadosEstadosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RelatorioAssociadosEstadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
