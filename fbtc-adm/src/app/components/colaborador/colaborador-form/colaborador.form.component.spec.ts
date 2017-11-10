import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ColaboradorFormComponent } from './colaborador.form.component';

describe('ColaboradorFormComponent', () => {
  let component: ColaboradorFormComponent;
  let fixture: ComponentFixture<ColaboradorFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ColaboradorFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ColaboradorFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
