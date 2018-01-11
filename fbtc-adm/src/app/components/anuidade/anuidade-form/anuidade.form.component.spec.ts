import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnuidadeFormComponent } from './anuidade.form.component';

describe('Anuidade.FormComponent', () => {
  let component: AnuidadeFormComponent;
  let fixture: ComponentFixture<AnuidadeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnuidadeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnuidadeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
