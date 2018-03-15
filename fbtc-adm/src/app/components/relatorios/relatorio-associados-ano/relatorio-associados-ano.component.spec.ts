import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioAssociadosAnoComponent } from './relatorio-associados-ano.component';

describe('RelatorioAssociadosAnoComponent', () => {
  let component: RelatorioAssociadosAnoComponent;
  let fixture: ComponentFixture<RelatorioAssociadosAnoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RelatorioAssociadosAnoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RelatorioAssociadosAnoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
