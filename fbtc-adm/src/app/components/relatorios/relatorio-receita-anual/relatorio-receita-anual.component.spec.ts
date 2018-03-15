import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioReceitaAnualComponent } from './relatorio-receita-anual.component';

describe('RelatorioReceitaAnualoComponent', () => {
  let component: RelatorioReceitaAnualComponent;
  let fixture: ComponentFixture<RelatorioReceitaAnualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RelatorioReceitaAnualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RelatorioReceitaAnualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
