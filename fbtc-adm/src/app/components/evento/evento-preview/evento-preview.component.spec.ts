import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoPreviewComponent } from './evento-preview.component';

describe('EventoPreviewComponent', () => {
  let component: EventoPreviewComponent;
  let fixture: ComponentFixture<EventoPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
