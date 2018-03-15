import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioAssociadosGeneroComponent } from './relatorio-associados-genero.component';

describe('RelatorioAssociadosGeneroComponent', () => {
  let component: RelatorioAssociadosGeneroComponent;
  let fixture: ComponentFixture<RelatorioAssociadosGeneroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RelatorioAssociadosGeneroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RelatorioAssociadosGeneroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
