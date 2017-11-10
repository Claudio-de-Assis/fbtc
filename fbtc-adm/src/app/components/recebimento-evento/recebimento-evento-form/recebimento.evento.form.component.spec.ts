import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecebimentoEventoFormComponent } from './recebimento.evento.form.component';

describe('RecebimentoEventoFormComponent', () => {
  let component: RecebimentoEventoFormComponent;
  let fixture: ComponentFixture<RecebimentoEventoFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecebimentoEventoFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecebimentoEventoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
