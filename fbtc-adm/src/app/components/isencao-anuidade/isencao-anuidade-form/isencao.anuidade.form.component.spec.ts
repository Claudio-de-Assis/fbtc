import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IsencaoAnuidadeFormComponent } from './isencao.anuidade.form.component';

describe('IsencaoAnuidadeFormComponent', () => {
  let component: IsencaoAnuidadeFormComponent;
  let fixture: ComponentFixture<IsencaoAnuidadeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IsencaoAnuidadeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IsencaoAnuidadeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
