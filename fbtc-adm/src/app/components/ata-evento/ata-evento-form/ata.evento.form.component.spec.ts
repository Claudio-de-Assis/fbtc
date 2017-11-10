import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtaEventoFormComponent } from './ata.evento.form.component';

describe('AtaEventoFormComponent', () => {
  let component: AtaEventoFormComponent;
  let fixture: ComponentFixture<AtaEventoFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtaEventoFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtaEventoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
