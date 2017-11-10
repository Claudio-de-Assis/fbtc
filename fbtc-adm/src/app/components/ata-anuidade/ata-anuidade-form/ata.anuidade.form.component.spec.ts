import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtaAnuidadeFormComponent } from './ata.anuidade.form.component';

describe('AtaAnuidadeFormComponent', () => {
  let component: AtaAnuidadeFormComponent;
  let fixture: ComponentFixture<AtaAnuidadeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtaAnuidadeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtaAnuidadeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
