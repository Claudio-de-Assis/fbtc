import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociadoListComponent } from './associado.list.component';

describe('AssociadoListComponent', () => {
  let component: AssociadoListComponent;
  let fixture: ComponentFixture<AssociadoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssociadoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssociadoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
