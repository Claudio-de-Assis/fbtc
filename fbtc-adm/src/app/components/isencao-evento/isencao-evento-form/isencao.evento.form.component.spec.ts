import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IsencaoEventoFormComponent } from './isencao.evento.form.component';

describe('IsencaoEventoFormComponent', () => {
  let component: IsencaoEventoFormComponent;
  let fixture: ComponentFixture<IsencaoEventoFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IsencaoEventoFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IsencaoEventoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
