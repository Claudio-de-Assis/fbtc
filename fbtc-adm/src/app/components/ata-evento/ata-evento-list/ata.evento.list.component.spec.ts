import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtaEventoListComponent } from './ata.evento.list.component';

describe('AtaEventoListComponent', () => {
  let component: AtaEventoListComponent;
  let fixture: ComponentFixture<AtaEventoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtaEventoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtaEventoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
