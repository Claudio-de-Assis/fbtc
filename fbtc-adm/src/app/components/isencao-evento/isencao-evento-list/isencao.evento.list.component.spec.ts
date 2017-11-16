import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IsencaoEventoListComponent } from './isencao.evento.list.component';

describe('AtaEventoListComponent', () => {
  let component: IsencaoEventoListComponent;
  let fixture: ComponentFixture<IsencaoEventoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IsencaoEventoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IsencaoEventoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
