import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoFormComponent } from './evento.form.component';

describe('EventoFormComponent', () => {
  let component: EventoFormComponent;
  let fixture: ComponentFixture<EventoFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
