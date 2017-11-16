import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IsencaoAnuidadeListComponent } from './isencao.anuidade.list.component';

describe('IsencaoAnuidadeListComponent', () => {
  let component: IsencaoAnuidadeListComponent;
  let fixture: ComponentFixture<IsencaoAnuidadeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IsencaoAnuidadeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IsencaoAnuidadeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
