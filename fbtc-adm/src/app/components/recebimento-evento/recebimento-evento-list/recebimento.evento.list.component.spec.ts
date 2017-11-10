import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecebimentoEventoListComponent } from './recebimento.evento.list.component';

describe('RecebimentoEventoListComponent', () => {
  let component: RecebimentoEventoListComponent;
  let fixture: ComponentFixture<RecebimentoEventoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecebimentoEventoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecebimentoEventoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
